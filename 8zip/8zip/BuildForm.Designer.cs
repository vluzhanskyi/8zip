namespace _8zip
{
    partial class BuildForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.buildVersionTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.IsRecOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the build numbet (ex: 6.6.0002.120):";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buildVersionTextBox
            // 
            this.buildVersionTextBox.Location = new System.Drawing.Point(12, 26);
            this.buildVersionTextBox.Name = "buildVersionTextBox";
            this.buildVersionTextBox.Size = new System.Drawing.Size(264, 20);
            this.buildVersionTextBox.TabIndex = 1;
            this.buildVersionTextBox.TextChanged += new System.EventHandler(this.buildVersionTextBox_TextChanged);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(110, 52);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // IsRecOnlyCheckBox
            // 
            this.IsRecOnlyCheckBox.AutoSize = true;
            this.IsRecOnlyCheckBox.Checked = true;
            this.IsRecOnlyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsRecOnlyCheckBox.Location = new System.Drawing.Point(13, 60);
            this.IsRecOnlyCheckBox.Name = "IsRecOnlyCheckBox";
            this.IsRecOnlyCheckBox.Size = new System.Drawing.Size(75, 17);
            this.IsRecOnlyCheckBox.TabIndex = 3;
            this.IsRecOnlyCheckBox.Text = "IsRecOnly";
            this.IsRecOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // BuildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 89);
            this.Controls.Add(this.IsRecOnlyCheckBox);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.buildVersionTextBox);
            this.Controls.Add(this.label1);
            this.Name = "BuildForm";
            this.Text = "BuildForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox buildVersionTextBox;
        public System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.CheckBox IsRecOnlyCheckBox;
    }
}