namespace _8zip.View
{
    partial class CustomSelectionForm
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
            this.PackagesListBox = new System.Windows.Forms.CheckedListBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PackagesListBox
            // 
            this.PackagesListBox.FormattingEnabled = true;
            this.PackagesListBox.Location = new System.Drawing.Point(13, 13);
            this.PackagesListBox.Name = "PackagesListBox";
            this.PackagesListBox.Size = new System.Drawing.Size(259, 244);
            this.PackagesListBox.TabIndex = 0;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(96, 270);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK!";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CustomSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 305);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.PackagesListBox);
            this.Name = "CustomSelectionForm";
            this.Text = "CustomSelectionForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox PackagesListBox;
        private System.Windows.Forms.Button OkButton;
    }
}