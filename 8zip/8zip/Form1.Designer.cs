namespace _8zip
{
    partial class EightZip
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
            this.BrowseButton1 = new System.Windows.Forms.Button();
            this.Browsebutton2 = new System.Windows.Forms.Button();
            this.SourceTextBox = new System.Windows.Forms.TextBox();
            this.DestinationTextBox = new System.Windows.Forms.TextBox();
            this.Destination = new System.Windows.Forms.Label();
            this.ArchiveButton = new System.Windows.Forms.Button();
            this.ComprLevelcomboBox = new System.Windows.Forms.ComboBox();
            this.CompressionLevel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.ArchivefilesCheckBox = new System.Windows.Forms.CheckBox();
            this.ArchiveFoldersCheckBox = new System.Windows.Forms.CheckBox();
            this.ExtractZipCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BrowseButton1
            // 
            this.BrowseButton1.Enabled = false;
            this.BrowseButton1.Location = new System.Drawing.Point(0, 144);
            this.BrowseButton1.Name = "BrowseButton1";
            this.BrowseButton1.Size = new System.Drawing.Size(87, 23);
            this.BrowseButton1.TabIndex = 0;
            this.BrowseButton1.Text = "Open Files";
            this.BrowseButton1.UseVisualStyleBackColor = true;
            this.BrowseButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Browsebutton2
            // 
            this.Browsebutton2.Enabled = false;
            this.Browsebutton2.Location = new System.Drawing.Point(182, 56);
            this.Browsebutton2.Name = "Browsebutton2";
            this.Browsebutton2.Size = new System.Drawing.Size(75, 23);
            this.Browsebutton2.TabIndex = 1;
            this.Browsebutton2.Text = "Browse";
            this.Browsebutton2.UseVisualStyleBackColor = true;
            this.Browsebutton2.Click += new System.EventHandler(this.Browsebutton2_Click);
            // 
            // SourceTextBox
            // 
            this.SourceTextBox.Enabled = false;
            this.SourceTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.SourceTextBox.Location = new System.Drawing.Point(119, 147);
            this.SourceTextBox.Name = "SourceTextBox";
            this.SourceTextBox.Size = new System.Drawing.Size(139, 20);
            this.SourceTextBox.TabIndex = 2;
            this.SourceTextBox.Text = "Define path to files";
            this.SourceTextBox.TextChanged += new System.EventHandler(this.SourceTextBox_TextChanged);
            // 
            // DestinationTextBox
            // 
            this.DestinationTextBox.Enabled = false;
            this.DestinationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.DestinationTextBox.Location = new System.Drawing.Point(0, 56);
            this.DestinationTextBox.Name = "DestinationTextBox";
            this.DestinationTextBox.Size = new System.Drawing.Size(178, 20);
            this.DestinationTextBox.TabIndex = 3;
            this.DestinationTextBox.Text = "Define path to save zip file";
            this.DestinationTextBox.TextChanged += new System.EventHandler(this.DestinationTextBox_TextChanged);
            // 
            // Destination
            // 
            this.Destination.AutoSize = true;
            this.Destination.Location = new System.Drawing.Point(-3, 40);
            this.Destination.Name = "Destination";
            this.Destination.Size = new System.Drawing.Size(70, 13);
            this.Destination.TabIndex = 5;
            this.Destination.Text = "Archive path:";
            // 
            // ArchiveButton
            // 
            this.ArchiveButton.Enabled = false;
            this.ArchiveButton.Location = new System.Drawing.Point(182, 104);
            this.ArchiveButton.Name = "ArchiveButton";
            this.ArchiveButton.Size = new System.Drawing.Size(75, 23);
            this.ArchiveButton.TabIndex = 6;
            this.ArchiveButton.Text = "Run!";
            this.ArchiveButton.UseVisualStyleBackColor = true;
            this.ArchiveButton.Click += new System.EventHandler(this.ArchiveButton_Click);
            // 
            // ComprLevelcomboBox
            // 
            this.ComprLevelcomboBox.AllowDrop = true;
            this.ComprLevelcomboBox.Enabled = false;
            this.ComprLevelcomboBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.ComprLevelcomboBox.FormattingEnabled = true;
            this.ComprLevelcomboBox.Location = new System.Drawing.Point(0, 104);
            this.ComprLevelcomboBox.MaxDropDownItems = 3;
            this.ComprLevelcomboBox.Name = "ComprLevelcomboBox";
            this.ComprLevelcomboBox.Size = new System.Drawing.Size(121, 21);
            this.ComprLevelcomboBox.TabIndex = 7;
            this.ComprLevelcomboBox.Text = "Compression level";
            this.ComprLevelcomboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // CompressionLevel
            // 
            this.CompressionLevel.AutoSize = true;
            this.CompressionLevel.Location = new System.Drawing.Point(-3, 88);
            this.CompressionLevel.Name = "CompressionLevel";
            this.CompressionLevel.Size = new System.Drawing.Size(99, 13);
            this.CompressionLevel.TabIndex = 8;
            this.CompressionLevel.Text = "Compression Level:";
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Controls.Add(this.ExtractZipCheckBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ArchiveFoldersCheckBox);
            this.panel1.Controls.Add(this.btnOpen);
            this.panel1.Controls.Add(this.ArchivefilesCheckBox);
            this.panel1.Controls.Add(this.SourceTextBox);
            this.panel1.Controls.Add(this.CompressionLevel);
            this.panel1.Controls.Add(this.ComprLevelcomboBox);
            this.panel1.Controls.Add(this.DestinationTextBox);
            this.panel1.Controls.Add(this.BrowseButton1);
            this.panel1.Controls.Add(this.Browsebutton2);
            this.panel1.Controls.Add(this.Destination);
            this.panel1.Controls.Add(this.ArchiveButton);
            this.panel1.Location = new System.Drawing.Point(12, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 389);
            this.panel1.TabIndex = 9;
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);

            // 
            // btnOpen
            // 
            this.btnOpen.Enabled = false;
            this.btnOpen.Location = new System.Drawing.Point(0, 173);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(87, 23);
            this.btnOpen.TabIndex = 10;
            this.btnOpen.Text = "Open Folders";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Path:";
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(119, 176);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(139, 20);
            this.txtPath.TabIndex = 12;
            this.txtPath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, 202);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(259, 202);
            this.webBrowser1.TabIndex = 13;
            
            // 
            // ArchivefilesCheckBox
            // 
            this.ArchivefilesCheckBox.AutoSize = true;
            this.ArchivefilesCheckBox.Location = new System.Drawing.Point(4, 7);
            this.ArchivefilesCheckBox.Name = "ArchivefilesCheckBox";
            this.ArchivefilesCheckBox.Size = new System.Drawing.Size(83, 17);
            this.ArchivefilesCheckBox.TabIndex = 0;
            this.ArchivefilesCheckBox.Text = "Archive files";
            this.ArchivefilesCheckBox.UseVisualStyleBackColor = true;
            this.ArchivefilesCheckBox.CheckedChanged += new System.EventHandler(this.ArchivefilesCheckBox_CheckedChanged);
            // 
            // ArchiveFoldersCheckBox
            // 
            this.ArchiveFoldersCheckBox.AutoSize = true;
            this.ArchiveFoldersCheckBox.Location = new System.Drawing.Point(92, 7);
            this.ArchiveFoldersCheckBox.Name = "ArchiveFoldersCheckBox";
            this.ArchiveFoldersCheckBox.Size = new System.Drawing.Size(96, 17);
            this.ArchiveFoldersCheckBox.TabIndex = 1;
            this.ArchiveFoldersCheckBox.Text = "ArchiveFolders";
            this.ArchiveFoldersCheckBox.UseVisualStyleBackColor = true;
            this.ArchiveFoldersCheckBox.CheckedChanged += new System.EventHandler(this.ArchiveFoldersCheckBox_CheckedChanged);
            // 
            // ExtractZipCheckBox
            // 
            this.ExtractZipCheckBox.AutoSize = true;
            this.ExtractZipCheckBox.Location = new System.Drawing.Point(190, 7);
            this.ExtractZipCheckBox.Name = "ExtractZipCheckBox";
            this.ExtractZipCheckBox.Size = new System.Drawing.Size(74, 17);
            this.ExtractZipCheckBox.TabIndex = 2;
            this.ExtractZipCheckBox.Text = "ExtractZip";
            this.ExtractZipCheckBox.UseVisualStyleBackColor = true;
            this.ExtractZipCheckBox.CheckedChanged += new System.EventHandler(this.ExtractZipCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Path:";
            // 
            // EightZip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 409);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EightZip";
            this.Text = "8Zip";
            this.Load += new System.EventHandler(this.EightZip_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BrowseButton1;
        private System.Windows.Forms.Button Browsebutton2;
        public  System.Windows.Forms.TextBox SourceTextBox;
        protected internal System.Windows.Forms.TextBox DestinationTextBox;
        private System.Windows.Forms.Label Destination;
        private System.Windows.Forms.Button ArchiveButton;
        protected internal System.Windows.Forms.ComboBox ComprLevelcomboBox;
        private System.Windows.Forms.Label CompressionLevel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.CheckBox ExtractZipCheckBox;
        private System.Windows.Forms.CheckBox ArchiveFoldersCheckBox;
        private System.Windows.Forms.CheckBox ArchivefilesCheckBox;
        private System.Windows.Forms.Label label2;
    }
}

