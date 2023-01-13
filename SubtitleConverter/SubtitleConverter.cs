using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SubtitleConverter
{
    public enum SubtitleType { SAMI, SubRip }

    public class SubtitleConverter
    {
        private static readonly Regex extensionRegex = new Regex("([^\\.]+)$", RegexOptions.Compiled);
        private static readonly Regex subRipRegex = new Regex
        (
            "\\s*\\d+\\s+(\\d+):(\\d+):(\\d+),(\\d+)\\s+-->\\s+(\\d+):(\\d+):(\\d+),(\\d+)\\s+", RegexOptions.Compiled
        );
        private static readonly Regex samiRegex = new Regex
        (
            "\\s*<sync\\s+", RegexOptions.IgnoreCase | RegexOptions.Compiled
        );
        private static readonly Regex samiDataRegex = new Regex
        (
            "start=['\"]?(\\d+)['\"]?\\s*[^>]*>\\s*(.*)\\s*",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled
        );
        private static readonly Regex samiNewLineTagRegex = new Regex("<br\\s*/?>", RegexOptions.Compiled);
        private static readonly Regex samiTagRegex = new Regex("</?\\w+\\s*[^>]*\\s*/?>", RegexOptions.Compiled);
        private static readonly Regex newLineRegex = new Regex("\\n", RegexOptions.Compiled);
        private static readonly Regex leftTrimRegex = new Regex("\\n\\s+", RegexOptions.Compiled);
        private static readonly Regex rightTrimRegex = new Regex("\\s+\\n", RegexOptions.Compiled);
        private static readonly Regex spaceRegex = new Regex("[\t 　]+", RegexOptions.Compiled);
        private static readonly Regex commentsRegex = new Regex("<!--.*?-->", RegexOptions.Compiled);

        public const string SAMI = "smi";
        public const string SubRip = "srt";

        private Queue<Subtitle> subtitleQueue;
        private string inputPath;
        private string inputType;
        private Encoding inputEncoding;
        private Encoding outputEncoding = Encoding.UTF8;
        private string lineDelimiter;
        private long sync = 0L;

        private class Subtitle
        {
            public SubtitleType type;
            public long start;
            public long end;
            public string text;
            public long sync;

            public long Start
            {
                get
                {
                    var l = start + sync;
                    if (l < 0L)
                    {
                        l = 0L;
                    }
                    return l;
                }
            }

            public long End
            {
                get
                {
                    var l = end + sync;
                    if (l < 0L)
                    {
                        l = 0L;
                    }
                    return l;
                }
            }

            public string SAMI
            {
                get
                {
                    switch (type)
                    {
                        case SubtitleType.SAMI:
                            return text;

                        default:
                            return newLineRegex.Replace(text, "<br>\n");
                    }
                }
            }

            public string Plain
            {
                get
                {
                    switch (type)
                    {
                        case SubtitleType.SAMI:
                            var str = HttpUtility.HtmlDecode(text);
                            str = samiNewLineTagRegex.Replace(str, "\n");
                            str = samiTagRegex.Replace(str, "");
                            str = spaceRegex.Replace(str, " ");
                            str = leftTrimRegex.Replace(str, "\n");
                            str = rightTrimRegex.Replace(str, "\n");
                            return str.Trim();

                        default:
                            return text;
                    }
                }
            }

            public string Text
            {
                get
                {
                    return text;
                }
            }

            public Subtitle(SubtitleType type)
            {
                this.type = type;
            }

            public Subtitle(SubtitleType type, int sync)
            {
                this.type = type;
                this.sync = sync;
            }

            public Subtitle(SubtitleType type, long start, long end, string text, long sync)
            {
                this.type = type;
                this.start = start;
                this.end = end;
                this.text = text;
                this.sync = sync;
            }
        }

        public SubtitleConverter() { }

        public SubtitleConverter(string input)
        {
            FileOpen(input);
        }

        public Encoding InputEncoding
        {
            get
            {
                return inputEncoding;
            }

            set
            {
                inputEncoding = value;
            }
        }

        public Encoding OutputEncoding
        {
            get
            {
                return outputEncoding;
            }

            set
            {
                outputEncoding = value;
            }
        }

        public string LineDelimiter
        {
            get
            {
                return lineDelimiter;
            }

            set
            {
                lineDelimiter = value;
            }
        }

        public long Sync
        {
            get
            {
                return sync;
            }

            set
            {
                sync = value;
            }
        }

        public void FileOpen(string path)
        {
            string ansi, unicode;
            StreamReader reader;
            Encoding encoding;

            using (reader = new StreamReader(path))
            {
                unicode = reader.ReadToEnd();
                encoding = reader.CurrentEncoding;
            }

            using (reader = new StreamReader(path, Encoding.Default))
            {
                ansi = reader.ReadToEnd();
            }

            string inputSubtitle;

            if (unicode.Length < ansi.Length)
            {
                inputEncoding = encoding;
                inputSubtitle = unicode;
            }
            else
            {
                inputEncoding = Encoding.Default;
                inputSubtitle = ansi;
            }

            Loading(path, inputSubtitle.Replace("\r\n", "\n"));
        }

        public void FileOpen(string path, Encoding encoding)
        {
            inputEncoding = encoding;
            string inputSubtitle = null;
            using (var reader = new StreamReader(path, encoding))
            {
                inputSubtitle = reader.ReadToEnd().Replace("\r\n", "\n");
            }

            Loading(path, inputSubtitle);
        }

        private void Loading(string path, string subtitle)
        {
            inputPath = path;
            inputType = extensionRegex.Match(path).Groups[1].Value.ToLower();

            switch (inputType)
            {
                case SAMI:
                    if (!LoadingSAMI(subtitle))
                    {
                        LoadingAuto(subtitle);
                    }
                    break;

                case SubRip:
                    if (!LoadingSubRip(subtitle))
                    {
                        LoadingAuto(subtitle);
                    }
                    break;

                default:
                    throw new Exception();
            }
        }

        private void LoadingAuto(string text)
        {
            if (LoadingSAMI(text))
            {
                return;
            }
            else if (LoadingSubRip(text))
            {
                return;
            }
        }

        private bool LoadingSAMI(string sami)
        {
            sami = commentsRegex.Replace(sami, "");
            var split = samiRegex.Split(sami);

            if (split.Length < 3)
            {
                return false;
            }

            inputType = SAMI;

            long start, end = 0;
            string text = "";
            subtitleQueue = new Queue<Subtitle>();
            var subtitle = new Subtitle(SubtitleType.SAMI);
            foreach (string sync in split)
            {
                Match match;

                if (!(match = samiDataRegex.Match(sync)).Success)
                {
                    continue;
                }

                start = end;
                end = long.Parse(match.Groups[1].Value);

                if (text.Length > 0)
                {
                    if (subtitle.Text == text)
                    {
                        subtitle.end = end;
                    }
                    else
                    {
                        if (subtitle.end > start)
                        {
                            subtitle.end = end;
                        }
                        subtitle = new Subtitle(SubtitleType.SAMI, start, end, text, this.sync);
                        subtitleQueue.Enqueue(subtitle);
                    }
                }

                text = match.Groups[2].Value.Trim();
            }

            return true;
        }

        public bool LoadingSubRip(string srt)
        {
            var split = subRipRegex.Split(srt);

            if (split.Length < 8)
            {
                return false;
            }

            inputType = SubRip;

            subtitleQueue = new Queue<Subtitle>();
            var subtitle = new Subtitle(SubtitleType.SubRip);

            for (var i = 1; i < split.Length; i += 9)
            {
                var start = new DateTime
                (
                    1, 1, 1, int.Parse(split[i + 0]), int.Parse(split[i + 1]), int.Parse(split[i + 2]), int.Parse(split[i + 3])
                ).Ticks / 10000;
                var end = new DateTime(
                    1, 1, 1, int.Parse(split[i + 4]), int.Parse(split[i + 5]), int.Parse(split[i + 6]), int.Parse(split[i + 7])
                ).Ticks / 10000;

                var text = split[i + 8];
                text = leftTrimRegex.Replace(text, "\n");
                text = rightTrimRegex.Replace(text, "\n");
                text = spaceRegex.Replace(text, " ");
                text = text.Trim();

                if (subtitle.Text == text)
                {
                    subtitle.end = end;
                }
                else
                {
                    subtitle = new Subtitle(SubtitleType.SubRip, start, end, text, sync);
                    subtitleQueue.Enqueue(subtitle);
                }
            }

            return true;
        }

        public void Save(SubtitleType type)
        {
            switch (type)
            {
                case SubtitleType.SAMI:
                    SaveSAMI();
                    break;

                case SubtitleType.SubRip:
                    SaveSubRip();
                    break;

                default:
                    throw new Exception();
            }
        }

        private void SaveFile(string path, string subtitle, Encoding encoding)
        {
            StreamWriter writer = Encoding.UTF8.Equals(encoding) ?
                writer = new StreamWriter(path) :
                writer = new StreamWriter(path, false, encoding);
            writer.Write(subtitle);
            writer.Close();
            writer.Dispose();
        }

        public void SaveSAMI()
        {
            if (inputType.Equals(SAMI) && sync == 0)
            {
                return;
            }

            if (lineDelimiter == null)
            {
                lineDelimiter = "\r\n";
            }

            var lang = "<p>";

            var subtitle = new StringBuilder();
            subtitle.Append
            (
                "<sami>\n<head>\n<title></title>\n<style><!--\np { font-family: sans-serif; text-align: center; }\n"
            );

            if (inputEncoding.CodePage == 949)
            {
                lang = "<p class=KRCC>";
                subtitle.Append(".KRCC { Name: Korean; lang: ko-KR; }\n");
            }

            subtitle.Append("--></style>\n</head>\n<body>\n");

            string text;
            var end = 0L;
            foreach (Subtitle item in subtitleQueue)
            {
                long e = item.End;
                if (e < 0L)
                {
                    continue;
                }

                long start = item.Start;
                if (start != end && end != 0)
                {
                    subtitle.AppendFormat("<sync start={0}>&nbsp;\n", end);
                }
                
                subtitle.AppendFormat("<sync start={0}>\n{1}{2}\n", start, lang, item.SAMI);

                end = e;
            }

            subtitle.AppendFormat("<sync start={0}>&nbsp;\n</body>\n</sami>", end);
            text = subtitle.ToString().Trim();

            if (lineDelimiter == "\r\n")
            {
                text = text.Replace("\n", "\r\n");
            }

            SaveFile(extensionRegex.Replace(inputPath, SAMI), text, outputEncoding);
        }

        public void SaveSubRip()
        {
            if (inputType.Equals(SubRip) && sync == 0L)
            {
                return;
            }

            if (lineDelimiter == null)
            {
                lineDelimiter = "\n";
            }

            var i = 0;
            var subtitle = new StringBuilder();
            foreach (var item in subtitleQueue)
            {
                long end = item.End;
                if (end < 0L)
                {
                    continue;
                }

                string text = item.Plain;
                if (text.Length < 1)
                {
                    continue;
                }

                subtitle.AppendFormat
                (
                    "{0}\n{1:HH:mm:ss,fff} --> {2:HH:mm:ss,fff}\n{3}\n\n",
                    ++i, new DateTime(item.Start * 10000), new DateTime(end * 10000), text
                );
            }

            string t = subtitle.ToString().Trim();
            if (lineDelimiter == "\r\n")
            {
                t = t.Replace("\n", "\r\n");
            }

            SaveFile(extensionRegex.Replace(inputPath, SubRip), t, outputEncoding);
        }
    }
}
