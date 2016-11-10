namespace _8zip.View
{
    partial class ProgressBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressBar));
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.get65 = new System.Windows.Forms.RadioButton();
            this.GetRecPackButton = new System.Windows.Forms.Button();
            this.ExtractButton = new System.Windows.Forms.Button();
            this.GetDeploymentPack = new System.Windows.Forms.Button();
            this.radioButton66 = new System.Windows.Forms.RadioButton();
            this.withHotFixes = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CurrentProgressBar = new System.Windows.Forms.ProgressBar();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.GeSPOnlyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar2
            // 
            resources.ApplyResources(this.progressBar2, "progressBar2");
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Step = 1;
            // 
            // get65
            // 
            resources.ApplyResources(this.get65, "get65");
            this.get65.Name = "get65";
            this.get65.UseVisualStyleBackColor = true;
            this.get65.CheckedChanged += new System.EventHandler(this.get65_CheckedChanged);
            // 
            // GetRecPackButton
            // 
            resources.ApplyResources(this.GetRecPackButton, "GetRecPackButton");
            this.GetRecPackButton.Name = "GetRecPackButton";
            this.GetRecPackButton.UseVisualStyleBackColor = true;
            this.GetRecPackButton.Click += new System.EventHandler(this.GetRecPackButton_Click);
            // 
            // ExtractButton
            // 
            resources.ApplyResources(this.ExtractButton, "ExtractButton");
            this.ExtractButton.Name = "ExtractButton";
            this.ExtractButton.UseVisualStyleBackColor = true;
            this.ExtractButton.Click += new System.EventHandler(this.ExtractButton_Click);
            // 
            // GetDeploymentPack
            // 
            resources.ApplyResources(this.GetDeploymentPack, "GetDeploymentPack");
            this.GetDeploymentPack.Name = "GetDeploymentPack";
            this.GetDeploymentPack.UseVisualStyleBackColor = true;
            this.GetDeploymentPack.Click += new System.EventHandler(this.GetDeploymentPack_Click);
            // 
            // radioButton66
            // 
            resources.ApplyResources(this.radioButton66, "radioButton66");
            this.radioButton66.Name = "radioButton66";
            this.radioButton66.UseVisualStyleBackColor = true;
            this.radioButton66.CheckedChanged += new System.EventHandler(this.radioButton66_CheckedChanged);
            // 
            // withHotFixes
            // 
            resources.ApplyResources(this.withHotFixes, "withHotFixes");
            this.withHotFixes.Name = "withHotFixes";
            this.withHotFixes.UseVisualStyleBackColor = true;
            this.withHotFixes.CheckedChanged += new System.EventHandler(this.withHotFixes_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            resources.ApplyResources(this.label1, "label1");
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Name = "label1";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // CurrentProgressBar
            // 
            resources.ApplyResources(this.CurrentProgressBar, "CurrentProgressBar");
            this.CurrentProgressBar.Name = "CurrentProgressBar";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // GeSPOnlyButton
            // 
            resources.ApplyResources(this.GeSPOnlyButton, "GeSPOnlyButton");
            this.GeSPOnlyButton.Name = "GeSPOnlyButton";
            this.GeSPOnlyButton.UseVisualStyleBackColor = true;
            this.GeSPOnlyButton.Click += new System.EventHandler(this.GeSPOnlyButton_Click);
            // 
            // ProgressBar
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GeSPOnlyButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.CurrentProgressBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.withHotFixes);
            this.Controls.Add(this.radioButton66);
            this.Controls.Add(this.GetDeploymentPack);
            this.Controls.Add(this.ExtractButton);
            this.Controls.Add(this.GetRecPackButton);
            this.Controls.Add(this.get65);
            this.Controls.Add(this.progressBar2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressBar";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.RadioButton get65;
        private System.Windows.Forms.Button GetRecPackButton;
        private System.Windows.Forms.Button ExtractButton;
        private System.Windows.Forms.Button GetDeploymentPack;
        private System.Windows.Forms.RadioButton radioButton66;
        private System.Windows.Forms.CheckBox withHotFixes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar CurrentProgressBar;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button GeSPOnlyButton;
    }
}