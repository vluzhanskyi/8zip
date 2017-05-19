namespace _8zip.View
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildForm));
            this.label1 = new System.Windows.Forms.Label();
            this.buildVersionTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.IsRecOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.IsCleanCheckBox = new System.Windows.Forms.CheckBox();
            this.AAgentTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MiniBusTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SplashTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.NCABuildVersionTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.IsCustomCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the Engage build number (ex: 6.6.0002.120):";
            // 
            // buildVersionTextBox
            // 
            this.buildVersionTextBox.Location = new System.Drawing.Point(12, 26);
            this.buildVersionTextBox.Name = "buildVersionTextBox";
            this.buildVersionTextBox.Size = new System.Drawing.Size(305, 20);
            this.buildVersionTextBox.TabIndex = 1;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(239, 261);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 7;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // IsRecOnlyCheckBox
            // 
            this.IsRecOnlyCheckBox.AutoSize = true;
            this.IsRecOnlyCheckBox.Location = new System.Drawing.Point(15, 266);
            this.IsRecOnlyCheckBox.Name = "IsRecOnlyCheckBox";
            this.IsRecOnlyCheckBox.Size = new System.Drawing.Size(75, 17);
            this.IsRecOnlyCheckBox.TabIndex = 5;
            this.IsRecOnlyCheckBox.Text = "IsRecOnly";
            this.IsRecOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // IsCleanCheckBox
            // 
            this.IsCleanCheckBox.AutoSize = true;
            this.IsCleanCheckBox.Location = new System.Drawing.Point(97, 267);
            this.IsCleanCheckBox.Name = "IsCleanCheckBox";
            this.IsCleanCheckBox.Size = new System.Drawing.Size(61, 17);
            this.IsCleanCheckBox.TabIndex = 6;
            this.IsCleanCheckBox.Text = "IsClean";
            this.IsCleanCheckBox.UseVisualStyleBackColor = true;
            // 
            // AAgentTextBox
            // 
            this.AAgentTextBox.Location = new System.Drawing.Point(10, 122);
            this.AAgentTextBox.Name = "AAgentTextBox";
            this.AAgentTextBox.Size = new System.Drawing.Size(304, 20);
            this.AAgentTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(307, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Enter the Authentication Agent build number (ex: 6.6.0001.100):";
            // 
            // MiniBusTextBox
            // 
            this.MiniBusTextBox.Location = new System.Drawing.Point(10, 73);
            this.MiniBusTextBox.Name = "MiniBusTextBox";
            this.MiniBusTextBox.Size = new System.Drawing.Size(307, 20);
            this.MiniBusTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(245, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Enter the MiniBus build number (ex: 1.0.0501.340):";
            // 
            // SplashTextBox
            // 
            this.SplashTextBox.Location = new System.Drawing.Point(10, 172);
            this.SplashTextBox.Name = "SplashTextBox";
            this.SplashTextBox.Size = new System.Drawing.Size(304, 20);
            this.SplashTextBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(240, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Enter the Splash build number (ex: 6.6.0001.120):";
            // 
            // NCABuildVersionTextBox
            // 
            this.NCABuildVersionTextBox.Location = new System.Drawing.Point(11, 220);
            this.NCABuildVersionTextBox.Name = "NCABuildVersionTextBox";
            this.NCABuildVersionTextBox.Size = new System.Drawing.Size(304, 20);
            this.NCABuildVersionTextBox.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(230, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Enter the NCA build number (ex: 6.6.0001.260):";
            // 
            // IsCustomCheckBox
            // 
            this.IsCustomCheckBox.AutoSize = true;
            this.IsCustomCheckBox.Location = new System.Drawing.Point(165, 267);
            this.IsCustomCheckBox.Name = "IsCustomCheckBox";
            this.IsCustomCheckBox.Size = new System.Drawing.Size(69, 17);
            this.IsCustomCheckBox.TabIndex = 12;
            this.IsCustomCheckBox.Text = "IsCustom";
            this.IsCustomCheckBox.UseVisualStyleBackColor = true;
            this.IsCustomCheckBox.CheckedChanged += new System.EventHandler(this.CbIsCustom_CheckedChanged);
            // 
            // BuildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(323, 296);
            this.Controls.Add(this.IsCustomCheckBox);
            this.Controls.Add(this.NCABuildVersionTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SplashTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MiniBusTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AAgentTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IsCleanCheckBox);
            this.Controls.Add(this.IsRecOnlyCheckBox);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.buildVersionTextBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BuildForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "BuildForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BuildForm_Loaded);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox buildVersionTextBox;
        public System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.CheckBox IsRecOnlyCheckBox;
        private System.Windows.Forms.CheckBox IsCleanCheckBox;
        public System.Windows.Forms.TextBox AAgentTextBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox MiniBusTextBox;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox SplashTextBox;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox NCABuildVersionTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox IsCustomCheckBox;
    }
}