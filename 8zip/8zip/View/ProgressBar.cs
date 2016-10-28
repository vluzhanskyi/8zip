﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using _8zip.Controller;
using _8zip.CustomEvents;
using _8zip.Properties;
using _8zip.Sources;

namespace _8zip.View
{
    public delegate void SetBuildVersion(string build);

    public partial class ProgressBar : Form
    {
        
        private string _unZipPath;
        public static string BuildVersion;
        public ProgressBar()
        {
            InitializeComponent();
            GetUnzipDirectory();
            withHotFixes.Enabled = false;
            label1.BackColor = DefaultBackColor;
            label1.ForeColor = Color.Black;
            linkLabel1.MaximumSize = new Size(310, 0);
            linkLabel1.AutoEllipsis = true;
            linkLabel1.Links.Add(new LinkLabel.Link(0, 0,_unZipPath));
            linkLabel1.Text = GetStringToShow(_unZipPath);

        }

        private void ExtractButton_Click(object sender, EventArgs e)
        {
            var archiveMethods = new ZipWrapper();
            archiveMethods.UpdateFormEvent += ChangeFormControlsState;
            archiveMethods.MaxValueChangedEvent += MaxPorgressValueChangedEventHandler;
            archiveMethods.ProgesEvent += PorgressEventHandler;
            archiveMethods.ChangePackageNameEvent += ChangePackageName;
            archiveMethods.ExceptionEvent += ShowExceptionMessageEventHandler;
            archiveMethods.ExtractAllPackages(_unZipPath);
        }

        private void GetRecPackButton_Click(object sender, EventArgs e)
        {
            var archiveMethods = new Processor();
            archiveMethods.UpdateFormEvent += ChangeFormControlsState;
            archiveMethods.MaxValueChangedEvent += MaxPorgressValueChangedEventHandler;
            archiveMethods.ProgesEvent += PorgressEventHandler;
            archiveMethods.ExtratingProgressEvent += ExtratingPorgressEventHandler;
            archiveMethods.ChangePackageNameEvent += ChangePackageName;
            archiveMethods.ExceptionEvent += ShowExceptionMessageEventHandler;

            if (get65.Checked || withHotFixes.Checked)
            {
                var engage = new Engage(6.5, null, null, null, null, withHotFixes.Checked);
                var build = new BuildForm(false, true, true);
                var result = build.ShowDialog();
                if (result == DialogResult.OK)
                {
                    archiveMethods.GetEngagePackages(engage, _unZipPath, build.IsRecOnly, build.IsCleanInstallation);    
                }                
            }
           
            if (radioButton66.Checked)
            {
                var build = new BuildForm(true, true, true);
                var result = build.ShowDialog();
               if (result == DialogResult.OK && build.BuildVersion != null)
                {
                    var engage = new Engage(6.6, build.BuildVersion, build.SplashBuildVersion, build.AAgentBuildVersion, 
                        build.MiniBusBuildVersion, withHotFixes.Checked);
                    archiveMethods.GetEngagePackages(engage, _unZipPath, build.IsRecOnly, build.IsCleanInstallation);
                }               
            }
        }

        private void GetDeploymentPack_Click(object sender, EventArgs e)
        {
            label1.Text = Resources.ProgressBar_GetRecPackButton_Click_Initialization;
            var archiveMethods = new Processor();
            archiveMethods.ProgesEvent += PorgressEventHandler;
            archiveMethods.MaxValueChangedEvent += MaxPorgressValueChangedEventHandler;
            archiveMethods.ExtratingProgressEvent += ExtratingPorgressEventHandler;
            archiveMethods.UpdateFormEvent += ChangeFormControlsState;
            archiveMethods.ChangePackageNameEvent += ChangePackageName;
            archiveMethods.ExceptionEvent += ShowExceptionMessageEventHandler;      
            if (get65.Checked)
            {
                var build = new BuildForm(false, false, true)
                {
                    AAgentTextBox = {Enabled = false},
                    MiniBusTextBox = {Enabled = false},
                    SplashTextBox = {Enabled = false}                   
                };
                if (build.ShowDialog() == DialogResult.OK)
                {
                    var engage = new Engage(6.5, null, null, null, null, false);
                    archiveMethods.GetDeployment(engage, _unZipPath, build.IsCleanInstallation);
                }
            }
            else if (radioButton66.Checked)
            {
                var build = new BuildForm(true, false, true)
                {
                    AAgentTextBox = { Enabled = false },
                    MiniBusTextBox = { Enabled = false },
                    SplashTextBox = { Enabled = false },
                    
                };
                if (build.ShowDialog() != DialogResult.OK)
                    return;
                BuildVersion = build.BuildVersion;
                var engage = new Engage(6.6, BuildVersion, build.SplashBuildVersion, build.AAgentBuildVersion,
                    build.MiniBusBuildVersion, false);
                archiveMethods.GetDeployment(engage, null, build.IsCleanInstallation);
            }       
        }

        private void ExtratingPorgressEventHandler(object sender, Ionic.Zip.ExtractProgressEventArgs e)
        {
            if (e.TotalBytesToTransfer > 0)
            {
                Action action = () => CurrentProgressBar.Value = Convert.ToInt32(100 * e.BytesTransferred / e.TotalBytesToTransfer);
                CurrentProgressBar.Invoke(action);
            }
        }

        private void PorgressEventHandler(object sender, ProgressEventArgs e)
        {
            if (!e.IsCurrent)
            {
                if (progressBar2.Value >= progressBar2.Maximum)
                    return;
                if (e.Progress >= progressBar2.Maximum)
                {
                    Action action = () => progressBar2.Value = progressBar2.Maximum;
                    progressBar2.Invoke(action);
                }
                else
                {
                    Action action = () => progressBar2.Value += e.Progress;
                    progressBar2.Invoke(action);
                }
               
                if (progressBar2.Value == progressBar2.Maximum)
                {
                    Action action2 = () => CurrentProgressBar.Value = CurrentProgressBar.Maximum;
                    CurrentProgressBar.Invoke(action2);
                }
            }
            else
            {
                if (e.Progress >= CurrentProgressBar.Maximum)
                    return;
                Action action = () => CurrentProgressBar.Value = e.Progress;
                progressBar2.Invoke(action);
            }
        }

        private void ShowExceptionMessageEventHandler(object sender, ShowExceptionMessageArks e)
        {
            MessageBox.Show(e.Message);
        }

        private void MaxPorgressValueChangedEventHandler(object sender, ChangeMaxProgressValueEventArgs e)
        {
                Action action = () => progressBar2.Maximum = e.Value;
                progressBar2.Invoke(action);

        }

        private void get65_CheckedChanged(object sender, EventArgs e)
        {
            withHotFixes.Enabled = true;
            GetRecPackButton.Enabled = true;
            GetDeploymentPack.Enabled = true;
        }

        private void withHotFixes_CheckedChanged(object sender, EventArgs e)
        {
            GetRecPackButton.Enabled = true;
        }

        private void radioButton66_CheckedChanged(object sender, EventArgs e)
        {
            GetRecPackButton.Enabled = true;
            GetDeploymentPack.Enabled = true;
            if (radioButton66.Checked)
                withHotFixes.Enabled = false;
        }

        private void ChangeFormControlsState(object sender, UpdateFormArgs e)
        {
            GetDeploymentPack.Enabled = e.IsOpen;
            GetRecPackButton.Enabled = e.IsOpen;
            ExtractButton.Enabled = e.IsOpen;
            withHotFixes.Enabled = e.IsOpen;
            radioButton66.Enabled = e.IsOpen;
            get65.Enabled = e.IsOpen;
            UseWaitCursor = !e.IsOpen;
            progressBar2.Value = 0;
            CurrentProgressBar.Value = 0;
        }

        private void ChangePackageName(object sender, UpdatePackageNameArgs e)
        {
            Action action = () =>
            {
                label1.Text = string.Format("{0} {1} {2}", e.ItemNumber, e.ActionName, e.Name);
                linkLabel1.Text = GetStringToShow(e.UnzipPath);
                if (e.ActionName == Actions.Extracting)
                {
                    CurrentProgressBar.Value = CurrentProgressBar.Maximum;
                    if (e.Name.Contains("NDM"))
                    {
                        _unZipPath = e.UnzipPath;
                    }
                }                
            }; 
            label1.Invoke(action);
        }

        private void GetUnzipDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            _unZipPath = Directory.GetDirectories(currentDirectory, "NDM-6.*").Length > 0
                ? string.Format(Directory.GetDirectories(currentDirectory, "NDM-6.*")[0]) 
                : currentDirectory;
        }

        private string GetStringToShow(string s)
        {
            if (s != null && s.Length > 53)
            {

                int index = s.Length - 53;
                string res = s.Insert(s.Length - index, "\n");
                string substring = res.Substring(res.IndexOf("\n", StringComparison.Ordinal) + 2);
                if (substring.Length > 53)
                {
                    res = string.Format("...{0}", GetStringToShow(substring));
                }
                return res;
            }
            return s;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(_unZipPath);
        }

    }
}