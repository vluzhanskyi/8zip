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
            EightZip.SourceTextBox = new System.Windows.Forms.TextBox();
            EightZip.DestinationTextBox = new System.Windows.Forms.TextBox();
            this.SourceLabel = new System.Windows.Forms.Label();
            this.Destination = new System.Windows.Forms.Label();
            this.ArchiveButton = new System.Windows.Forms.Button();
            EightZip.ComprLevelcomboBox = new System.Windows.Forms.ComboBox();
            this.CompressionLevel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BrowseButton1
            // 
            this.BrowseButton1.Location = new System.Drawing.Point(197, 54);
            this.BrowseButton1.Name = "BrowseButton1";
            this.BrowseButton1.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton1.TabIndex = 0;
            this.BrowseButton1.Text = "Browse";
            this.BrowseButton1.UseVisualStyleBackColor = true;
            this.BrowseButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Browsebutton2
            // 
            this.Browsebutton2.Location = new System.Drawing.Point(196, 113);
            this.Browsebutton2.Name = "Browsebutton2";
            this.Browsebutton2.Size = new System.Drawing.Size(75, 23);
            this.Browsebutton2.TabIndex = 1;
            this.Browsebutton2.Text = "Browse";
            this.Browsebutton2.UseVisualStyleBackColor = true;
            this.Browsebutton2.Click += new System.EventHandler(this.Browsebutton2_Click);
            // 
            // SourceTextBox
            // 
            EightZip.SourceTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            EightZip.SourceTextBox.Location = new System.Drawing.Point(12, 54);
            EightZip.SourceTextBox.Name = "SourceTextBox";
            EightZip.SourceTextBox.Size = new System.Drawing.Size(179, 20);
            EightZip.SourceTextBox.TabIndex = 2;
            EightZip.SourceTextBox.Text = "Define path to files";
            EightZip.SourceTextBox.TextChanged += new System.EventHandler(this.SourceTextBox_TextChanged);
            // 
            // DestinationTextBox
            // 
            EightZip.DestinationTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            EightZip.DestinationTextBox.Location = new System.Drawing.Point(12, 113);
            EightZip.DestinationTextBox.Name = "DestinationTextBox";
            EightZip.DestinationTextBox.Size = new System.Drawing.Size(178, 20);
            EightZip.DestinationTextBox.TabIndex = 3;
            EightZip.DestinationTextBox.Text = "Define path to save zip file";
            EightZip.DestinationTextBox.TextChanged += new System.EventHandler(this.DestinationTextBox_TextChanged);
            // 
            // SourceLabel
            // 
            this.SourceLabel.AutoSize = true;
            this.SourceLabel.Location = new System.Drawing.Point(12, 35);
            this.SourceLabel.Name = "SourceLabel";
            this.SourceLabel.Size = new System.Drawing.Size(94, 13);
            this.SourceLabel.TabIndex = 4;
            this.SourceLabel.Text = "Source file/Folder:";
            this.SourceLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // Destination
            // 
            this.Destination.AutoSize = true;
            this.Destination.Location = new System.Drawing.Point(12, 94);
            this.Destination.Name = "Destination";
            this.Destination.Size = new System.Drawing.Size(87, 13);
            this.Destination.TabIndex = 5;
            this.Destination.Text = "Destination path:";
            // 
            // ArchiveButton
            // 
            this.ArchiveButton.Location = new System.Drawing.Point(196, 209);
            this.ArchiveButton.Name = "ArchiveButton";
            this.ArchiveButton.Size = new System.Drawing.Size(75, 23);
            this.ArchiveButton.TabIndex = 6;
            this.ArchiveButton.Text = "Archive!";
            this.ArchiveButton.UseVisualStyleBackColor = true;
            this.ArchiveButton.Click += new System.EventHandler(this.ArchiveButton_Click);
            // 
            // ComprLevelcomboBox
            // 
            EightZip.ComprLevelcomboBox.AllowDrop = true;
            EightZip.ComprLevelcomboBox.ForeColor = System.Drawing.SystemColors.InfoText;
            EightZip.ComprLevelcomboBox.FormattingEnabled = true;
            EightZip.ComprLevelcomboBox.Location = new System.Drawing.Point(12, 209);
            EightZip.ComprLevelcomboBox.MaxDropDownItems = 3;
            EightZip.ComprLevelcomboBox.Name = "ComprLevelcomboBox";
            EightZip.ComprLevelcomboBox.Size = new System.Drawing.Size(121, 21);
            EightZip.ComprLevelcomboBox.TabIndex = 7;
            EightZip.ComprLevelcomboBox.Text = "Compression level";
            EightZip.ComprLevelcomboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // CompressionLevel
            // 
            this.CompressionLevel.AutoSize = true;
            this.CompressionLevel.Location = new System.Drawing.Point(12, 184);
            this.CompressionLevel.Name = "CompressionLevel";
            this.CompressionLevel.Size = new System.Drawing.Size(99, 13);
            this.CompressionLevel.TabIndex = 8;
            this.CompressionLevel.Text = "Compression Level:";
            // 
            // EightZip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.CompressionLevel);
            this.Controls.Add(EightZip.ComprLevelcomboBox);
            this.Controls.Add(this.ArchiveButton);
            this.Controls.Add(this.Destination);
            this.Controls.Add(this.SourceLabel);
            this.Controls.Add(EightZip.DestinationTextBox);
            this.Controls.Add(EightZip.SourceTextBox);
            this.Controls.Add(this.Browsebutton2);
            this.Controls.Add(this.BrowseButton1);
            this.Name = "EightZip";
            this.Text = "8Zip";
            this.Load += new System.EventHandler(this.EightZip_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BrowseButton1;
        private System.Windows.Forms.Button Browsebutton2;
        public static System.Windows.Forms.TextBox SourceTextBox;
        protected internal static System.Windows.Forms.TextBox DestinationTextBox;
        private System.Windows.Forms.Label SourceLabel;
        private System.Windows.Forms.Label Destination;
        private System.Windows.Forms.Button ArchiveButton;
        protected internal static System.Windows.Forms.ComboBox ComprLevelcomboBox;
        private System.Windows.Forms.Label CompressionLevel;
    }
}

