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
            this.SourceLabel = new System.Windows.Forms.Label();
            this.Destination = new System.Windows.Forms.Label();
            this.ArchiveButton = new System.Windows.Forms.Button();
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
            this.SourceTextBox.Location = new System.Drawing.Point(12, 54);
            this.SourceTextBox.Name = "SourceTextBox";
            this.SourceTextBox.Size = new System.Drawing.Size(179, 20);
            this.SourceTextBox.TabIndex = 2;
            this.SourceTextBox.TextChanged += new System.EventHandler(this.SourceTextBox_TextChanged);
            // 
            // DestinationTextBox
            // 
            this.DestinationTextBox.Location = new System.Drawing.Point(12, 113);
            this.DestinationTextBox.Name = "DestinationTextBox";
            this.DestinationTextBox.Size = new System.Drawing.Size(178, 20);
            this.DestinationTextBox.TabIndex = 3;
            this.DestinationTextBox.TextChanged += new System.EventHandler(this.DestinationTextBox_TextChanged);
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
            // EightZip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.ArchiveButton);
            this.Controls.Add(this.Destination);
            this.Controls.Add(this.SourceLabel);
            this.Controls.Add(this.DestinationTextBox);
            this.Controls.Add(this.SourceTextBox);
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
        private System.Windows.Forms.TextBox SourceTextBox;
        private System.Windows.Forms.TextBox DestinationTextBox;
        private System.Windows.Forms.Label SourceLabel;
        private System.Windows.Forms.Label Destination;
        private System.Windows.Forms.Button ArchiveButton;
    }
}

