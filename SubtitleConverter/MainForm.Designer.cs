namespace SubtitleConverter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputTypeComboBox = new System.Windows.Forms.ComboBox();
            this.inputCharsetComboBox = new System.Windows.Forms.ComboBox();
            this.outputCharsetComboBox = new System.Windows.Forms.ComboBox();
            this.lineDelimiterComboBox = new System.Windows.Forms.ComboBox();
            this.outputTypeLabel = new System.Windows.Forms.Label();
            this.inputCharsetLabel = new System.Windows.Forms.Label();
            this.outputCharesetLabel = new System.Windows.Forms.Label();
            this.lineDelimiterLabel = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.syncLabel = new System.Windows.Forms.Label();
            this.syncTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.menuStrip.Size = new System.Drawing.Size(611, 44);
            this.menuStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 36);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(174, 38);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(171, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(174, 38);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(77, 36);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(428, 38);
            this.aboutToolStripMenuItem.Text = "About SubtitleConverter";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // outputTypeComboBox
            // 
            this.outputTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputTypeComboBox.FormattingEnabled = true;
            this.outputTypeComboBox.Location = new System.Drawing.Point(205, 62);
            this.outputTypeComboBox.Margin = new System.Windows.Forms.Padding(6);
            this.outputTypeComboBox.Name = "outputTypeComboBox";
            this.outputTypeComboBox.Size = new System.Drawing.Size(378, 33);
            this.outputTypeComboBox.TabIndex = 1;
            // 
            // inputCharsetComboBox
            // 
            this.inputCharsetComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputCharsetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputCharsetComboBox.FormattingEnabled = true;
            this.inputCharsetComboBox.Location = new System.Drawing.Point(205, 114);
            this.inputCharsetComboBox.Margin = new System.Windows.Forms.Padding(6);
            this.inputCharsetComboBox.Name = "inputCharsetComboBox";
            this.inputCharsetComboBox.Size = new System.Drawing.Size(378, 33);
            this.inputCharsetComboBox.Sorted = true;
            this.inputCharsetComboBox.TabIndex = 2;
            // 
            // outputCharsetComboBox
            // 
            this.outputCharsetComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputCharsetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputCharsetComboBox.FormattingEnabled = true;
            this.outputCharsetComboBox.Location = new System.Drawing.Point(205, 166);
            this.outputCharsetComboBox.Margin = new System.Windows.Forms.Padding(6);
            this.outputCharsetComboBox.Name = "outputCharsetComboBox";
            this.outputCharsetComboBox.Size = new System.Drawing.Size(378, 33);
            this.outputCharsetComboBox.Sorted = true;
            this.outputCharsetComboBox.TabIndex = 3;
            // 
            // lineDelimiterComboBox
            // 
            this.lineDelimiterComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineDelimiterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lineDelimiterComboBox.FormattingEnabled = true;
            this.lineDelimiterComboBox.Location = new System.Drawing.Point(205, 218);
            this.lineDelimiterComboBox.Margin = new System.Windows.Forms.Padding(6);
            this.lineDelimiterComboBox.Name = "lineDelimiterComboBox";
            this.lineDelimiterComboBox.Size = new System.Drawing.Size(378, 33);
            this.lineDelimiterComboBox.TabIndex = 4;
            // 
            // outputTypeLabel
            // 
            this.outputTypeLabel.AutoSize = true;
            this.outputTypeLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.outputTypeLabel.Location = new System.Drawing.Point(24, 69);
            this.outputTypeLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.outputTypeLabel.Name = "outputTypeLabel";
            this.outputTypeLabel.Size = new System.Drawing.Size(130, 25);
            this.outputTypeLabel.TabIndex = 5;
            this.outputTypeLabel.Text = "Output Type";
            // 
            // inputCharsetLabel
            // 
            this.inputCharsetLabel.AutoSize = true;
            this.inputCharsetLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.inputCharsetLabel.Location = new System.Drawing.Point(24, 117);
            this.inputCharsetLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.inputCharsetLabel.Name = "inputCharsetLabel";
            this.inputCharsetLabel.Size = new System.Drawing.Size(140, 25);
            this.inputCharsetLabel.TabIndex = 6;
            this.inputCharsetLabel.Text = "Input Charset";
            // 
            // outputCharesetLabel
            // 
            this.outputCharesetLabel.AutoSize = true;
            this.outputCharesetLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.outputCharesetLabel.Location = new System.Drawing.Point(24, 169);
            this.outputCharesetLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.outputCharesetLabel.Name = "outputCharesetLabel";
            this.outputCharesetLabel.Size = new System.Drawing.Size(169, 25);
            this.outputCharesetLabel.TabIndex = 7;
            this.outputCharesetLabel.Text = "Output Chareset";
            // 
            // lineDelimiterLabel
            // 
            this.lineDelimiterLabel.AutoSize = true;
            this.lineDelimiterLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lineDelimiterLabel.Location = new System.Drawing.Point(24, 221);
            this.lineDelimiterLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lineDelimiterLabel.Name = "lineDelimiterLabel";
            this.lineDelimiterLabel.Size = new System.Drawing.Size(143, 25);
            this.lineDelimiterLabel.TabIndex = 8;
            this.lineDelimiterLabel.Text = "Line Delimiter";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 324);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusStrip.Size = new System.Drawing.Size(611, 40);
            this.statusStrip.TabIndex = 9;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(201, 34);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(225, 35);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // syncLabel
            // 
            this.syncLabel.AutoSize = true;
            this.syncLabel.Location = new System.Drawing.Point(24, 273);
            this.syncLabel.Name = "syncLabel";
            this.syncLabel.Size = new System.Drawing.Size(60, 25);
            this.syncLabel.TabIndex = 10;
            this.syncLabel.Text = "Sync";
            // 
            // syncTextBox
            // 
            this.syncTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.syncTextBox.Location = new System.Drawing.Point(205, 270);
            this.syncTextBox.Name = "syncTextBox";
            this.syncTextBox.Size = new System.Drawing.Size(378, 31);
            this.syncTextBox.TabIndex = 11;
            this.syncTextBox.Text = "0.0 sec";
            this.syncTextBox.Enter += new System.EventHandler(this.syncTextBox_Enter);
            this.syncTextBox.Leave += new System.EventHandler(this.syncTextBox_Leave);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(611, 364);
            this.Controls.Add(this.syncTextBox);
            this.Controls.Add(this.syncLabel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lineDelimiterLabel);
            this.Controls.Add(this.outputCharesetLabel);
            this.Controls.Add(this.inputCharsetLabel);
            this.Controls.Add(this.outputTypeLabel);
            this.Controls.Add(this.lineDelimiterComboBox);
            this.Controls.Add(this.outputCharsetComboBox);
            this.Controls.Add(this.inputCharsetComboBox);
            this.Controls.Add(this.outputTypeComboBox);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "SubtitleConverter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.MainForm_DragOver);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ComboBox outputTypeComboBox;
        private System.Windows.Forms.ComboBox inputCharsetComboBox;
        private System.Windows.Forms.ComboBox outputCharsetComboBox;
        private System.Windows.Forms.ComboBox lineDelimiterComboBox;
        private System.Windows.Forms.Label outputTypeLabel;
        private System.Windows.Forms.Label inputCharsetLabel;
        private System.Windows.Forms.Label outputCharesetLabel;
        private System.Windows.Forms.Label lineDelimiterLabel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label syncLabel;
        private System.Windows.Forms.TextBox syncTextBox;
    }
}

