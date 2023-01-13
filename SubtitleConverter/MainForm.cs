using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SubtitleConverter
{
    public partial class MainForm : Form
    {
        private static readonly string INPUT_CHARSET_DEFAULT = string.Format
        (
            "Auto (UTF-8 Or {0})", Encoding.Default.EncodingName
        );
        private static readonly Regex syncRegex = new Regex("-?\\d+\\.?\\d*", RegexOptions.Compiled);

        private bool processing = false;
        private Encoding outputCharset;
        private Encoding inputCharset;
        private string lineDelimiter;
        private SubtitleType outputType;
        private long sync = 0L;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            inputCharsetComboBox.Items.Add(INPUT_CHARSET_DEFAULT);

            foreach (EncodingInfo info in Encoding.GetEncodings())
            {
                inputCharsetComboBox.Items.Add(info.DisplayName);
                outputCharsetComboBox.Items.Add(info.DisplayName);
            }

            outputTypeComboBox.Items.Add("SAMI (smi)");
            outputTypeComboBox.Items.Add("SubRip (srt)");

            lineDelimiterComboBox.Items.Add("Default");
            lineDelimiterComboBox.Items.Add("Unix");
            lineDelimiterComboBox.Items.Add("Windows");

            toolStripStatusLabel.Text = "";

            aboutToolStripMenuItem.Text = string.Format("&About {0}", Text);

            var update = false;
            if (Properties.Settings.Default.InputCharset == null || Properties.Settings.Default.InputCharset.Equals(""))
            {
                Properties.Settings.Default.InputCharset = INPUT_CHARSET_DEFAULT;
                update = true;
            }

            if (Properties.Settings.Default.OutputCharset == null || Properties.Settings.Default.OutputCharset.Equals(""))
            {
                Properties.Settings.Default.OutputCharset = Encoding.UTF8.EncodingName;
                update = true;
            }

            if (Properties.Settings.Default.OutputType == null || Properties.Settings.Default.OutputType.Equals(""))
            {
                Properties.Settings.Default.OutputType = "SubRip (srt)";
                update = true;
            }

            if (Properties.Settings.Default.LineDelimiter == null || Properties.Settings.Default.LineDelimiter.Equals(""))
            {
                Properties.Settings.Default.LineDelimiter = "Default";
                update = true;
            }

            if (update)
            {
                Properties.Settings.Default.Save();
            }

            inputCharsetComboBox.SelectedItem = Properties.Settings.Default.InputCharset;
            outputCharsetComboBox.SelectedItem = Properties.Settings.Default.OutputCharset;
            outputTypeComboBox.SelectedItem = Properties.Settings.Default.OutputType;
            lineDelimiterComboBox.SelectedItem = Properties.Settings.Default.LineDelimiter;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.InputCharset = inputCharsetComboBox.Text;
            Properties.Settings.Default.OutputCharset = outputCharsetComboBox.Text;
            Properties.Settings.Default.OutputType = outputTypeComboBox.Text;
            Properties.Settings.Default.LineDelimiter = lineDelimiterComboBox.Text;
            Properties.Settings.Default.Save();
        }

        private void MainForm_DragOver(object sender, DragEventArgs e)
        {
            if (processing)
            {
                return;
            }

            var b = true;

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files)
            {
                if (!Regex.IsMatch(file, "\\.(sa?mi|srt)$", RegexOptions.IgnoreCase))
                {
                    b = false;
                    break;
                }
            }

            e.Effect = b && e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                convert((string[])e.Data.GetData(DataFormats.FileDrop));
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileOpenDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                this,
                string.Format(
                    @"{0}
Version: {1}

E-Mail: myhyuny@live.com
Homepage: https://github.com/myhyuny/subtitle-converter-win",
                    Text, ProductVersion
                  ),
                aboutToolStripMenuItem.Text.Replace("&", "")
            );
        }

        /// <summary>
        /// 파일을 다이얼로그로 열기
        /// </summary>
        private void fileOpenDialog()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "All Subtitle Files|*.smi;*.srt|SAMI Files|*.smi|SubRip Files|*.srt";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                convert(ofd.FileNames);
            }

            ofd.Dispose();
        }

        /// <summary>
        /// 파일 변환(스레드 풀)
        /// </summary>
        [MTAThread]
        public void convert(object data)
        {
            if (processing)
            {
                return;
            }

            var files = (string[])data;

            outputType = type(outputTypeComboBox.Text);
            inputCharset = encoding(inputCharsetComboBox.Text);
            outputCharset = encoding(outputCharsetComboBox.Text);
            lineDelimiter = lineDelimiterType(lineDelimiterComboBox.Text);
            sync = (long)(parseFloat(syncTextBox.Text) * 1000);

            ThreadPool.QueueUserWorkItem((object state) =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    processing = true;
                    toolStripStatusLabel.Text = "Converting";
                    menuStrip.Enabled = false;
                    outputTypeComboBox.Enabled = false;
                    inputCharsetComboBox.Enabled = false;
                    outputCharsetComboBox.Enabled = false;
                    lineDelimiterComboBox.Enabled = false;
                    toolStripProgressBar.Maximum = files.Length;
                    toolStripProgressBar.Value = 0;
                }));

                var waitHandles = new ManualResetEvent[files.Length];
                for (var i = 0; i < files.Length; i++)
                {
                    var file = files[i];
                    var waitHandle = new ManualResetEvent(false);
                    waitHandles[i] = waitHandle;
                    ThreadPool.QueueUserWorkItem((object s) =>
                    {
                        var sc = new SubtitleConverter();
                        sc.OutputEncoding = outputCharset;
                        sc.LineDelimiter = lineDelimiter;
                        sc.Sync = sync;

                        if (inputCharset != null)
                        {
                            sc.FileOpen(file, inputCharset);
                        }
                        else
                        {
                            sc.FileOpen(file);
                        }

                        sc.Save(outputType);

                        Invoke(new MethodInvoker(() =>
                        {
                            toolStripProgressBar.Value += 1;
                        }));

                        waitHandle.Set();
                    });
                }

                foreach (var waitHandle in waitHandles)
                {
                    waitHandle.WaitOne();
                }

                Invoke(new MethodInvoker(() =>
                {
                    toolStripStatusLabel.Text = "Complete";
                    menuStrip.Enabled = true;
                    outputTypeComboBox.Enabled = true;
                    inputCharsetComboBox.Enabled = true;
                    outputCharsetComboBox.Enabled = true;
                    lineDelimiterComboBox.Enabled = true;
                    toolStripProgressBar.Value = 0;
                    processing = false;
                }));
            });
        }

        private SubtitleType type(string type)
        {
            if (type == "SAMI (smi)")
            {
                return SubtitleType.SAMI;
            }

            if (type == "SubRip (srt)")
            {
                return SubtitleType.SubRip;
            }

            throw new Exception();
        }

        private string lineDelimiterType(string name)
        {
            if (name == "Default")
            {
                return null;
            }

            if (name == "Unix")
            {
                return "\n";
            }

            if (name == "Windows")
            {
                return "\r\n";
            }

            throw new Exception();
        }

        private Encoding encoding(string displayName)
        {
            foreach (var info in Encoding.GetEncodings())
            {
                if (info.DisplayName == displayName)
                {
                    return Encoding.GetEncoding(info.CodePage);
                }
            }

            return null;
        }

        private float parseFloat(string str)
        {
            var match = syncRegex.Match(str);
            if (match.Success)
            {
                return float.Parse(match.Value);
            }

            return 0f;
        }

        private string parseFloatString(String str)
        {
            return string.Format("{0:0.0#######}", parseFloat(str));
        }

        private void syncTextBox_Enter(object sender, EventArgs e)
        {
            syncTextBox.Text = parseFloatString(syncTextBox.Text);
        }

        private void syncTextBox_Leave(object sender, EventArgs e)
        {
            syncTextBox.Text = parseFloatString(syncTextBox.Text) + " sec";
        }
    }

}
