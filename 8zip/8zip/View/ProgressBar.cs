using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using _8zip.Controller;
using _8zip.CustomEvents;
using _8zip.Properties;
using _8zip.Sources;
using EventHandler = _8zip.Controller.EventHandler;

namespace _8zip.View
{
    public delegate void SetBuildVersion(string build);

    public partial class ProgressBar : Form
    {
        
        private string _unZipPath;
        public static string BuildVersion;
        private readonly Processor _processor;

        public ProgressBar()
        {
            InitializeComponent();
            GetUnzipDirectory();
            _processor = new Processor();
            EventHandler.UpdateFormEvent += ChangeFormControlsState;
            EventHandler.MaxValueChangedEvent += MaxPorgressValueChangedEventHandler;
            EventHandler.ProgressEvent += PorgressEventHandler;
            EventHandler.ExtratingProgressEvent += ExtratingPorgressEventHandler;
            EventHandler.ChangePackageNameEvent += ChangePackageName;
            EventHandler.ExceptionEvent += ShowExceptionMessageEventHandler;

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
            
            _processor.UnzipPackages(_unZipPath);
        }

        private void GetRecPackButton_Click(object sender, EventArgs e)
        {
            if (get65.Checked || withHotFixes.Checked)
            {
                var engage = new Engage(6.5, null, null, null, null, null, withHotFixes.Checked);
                var build = new BuildForm(false, true, true);
                var result = build.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _processor.GetEngagePackages(engage, _unZipPath, build.IsRecOnly, build.IsCleanInstallation);    
                }                
            }

            if (!radioButton66.Checked) return;
            {
                var build = new BuildForm(true, true, true);
                var result = build.ShowDialog();
                if (result != DialogResult.OK || build.BuildVersion == null) return;
                var engage = new Engage(6.6, build.BuildVersion, build.SplashBuildVersion, build.AAgentBuildVersion, 
                    build.MiniBusBuildVersion, build.NcaBuildVersion, withHotFixes.Checked);
                _processor.GetEngagePackages(engage, _unZipPath, build.IsRecOnly, build.IsCleanInstallation);
            }
        }

        protected virtual void GetDeploymentPack_Click(object sender, EventArgs e)
        {
            label1.Text = Resources.ProgressBar_GetRecPackButton_Click_Initialization;     
            if (get65.Checked)
            {
                var build = new BuildForm(false, false, true)
                {
                    AAgentTextBox = {Enabled = false},
                    MiniBusTextBox = {Enabled = false},
                    SplashTextBox = {Enabled = false},
                    NCABuildVersionTextBox = {Enabled = false}   
                };
                if (build.ShowDialog() == DialogResult.OK)
                {
                    var engage = new Engage(6.5, null, null, null, null, null, false);
                    _processor.GetDeployment(engage, _unZipPath, build.IsCleanInstallation);
                }
            }
            else if (radioButton66.Checked)
            {
                var build = new BuildForm(true, false, true, "Enter the build number (ex: 6.6.0001.160):")
                {
                    AAgentTextBox = { Enabled = false },
                    MiniBusTextBox = { Enabled = false },
                    SplashTextBox = { Enabled = false },
                    NCABuildVersionTextBox = { Enabled = false }   
                };
                if (build.ShowDialog() != DialogResult.OK)
                    return;
                BuildVersion = build.BuildVersion;
                var engage = new Engage(6.6, BuildVersion, build.SplashBuildVersion, build.AAgentBuildVersion,
                    build.MiniBusBuildVersion, build.NcaBuildVersion, false);
                _processor.GetDeployment(engage, null, build.IsCleanInstallation);
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

        private static void ShowExceptionMessageEventHandler(object sender, ShowExceptionMessageArks e)
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
            GetSPOnlyButton.Enabled = true;
            GetRecPackButton.Enabled = true;
        }

        private void radioButton66_CheckedChanged(object sender, EventArgs e)
        {
            GetRecPackButton.Enabled = true;
            GetDeploymentPack.Enabled = true;
            if (radioButton66.Checked)
            {
                GetSPOnlyButton.Enabled = false;
                withHotFixes.Enabled = false;
            }
                
        }

        private void ChangeFormControlsState(object sender, UpdateFormArgs e)
        {
            GetDeploymentPack.Enabled = e.IsOpen;
            GetRecPackButton.Enabled = e.IsOpen;
            GetSPOnlyButton.Enabled = e.IsOpen;
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
            var temp = GetStringToShow(string.Format("{0} {1} {2}", e.ItemNumber, e.ActionName, e.Name));
            if (label1.Text == temp) 
                return;
            Action action = () =>
            {
                label1.Text = temp;

                if (e.ActionName == Actions.Extracting)
                {
                    CurrentProgressBar.Value = CurrentProgressBar.Maximum;       
                }

                if (!string.IsNullOrEmpty(e.Name) && !e.Name.Contains("NDM")) return;
                _unZipPath = e.UnzipPath;
                linkLabel1.Text = GetStringToShow(e.UnzipPath);
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

        private static string GetStringToShow(string s)
        {
            if (s != null && s.Length > 65)
            {

                int index = s.Length - 65;
                string res = s.Insert(s.Length - index, "\n");
                string substring = res.Substring(res.IndexOf("\n", StringComparison.Ordinal) + 2);
                if (substring.Length > 65)
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

        private void GeSPOnlyButton_Click(object sender, EventArgs e)
        {
            EventHandler.UpdateFormEvent += ChangeFormControlsState;
            EventHandler.MaxValueChangedEvent += MaxPorgressValueChangedEventHandler;
            EventHandler.ProgressEvent += PorgressEventHandler;
            EventHandler.ExtratingProgressEvent += ExtratingPorgressEventHandler;
            EventHandler.ChangePackageNameEvent += ChangePackageName;
            EventHandler.ExceptionEvent += ShowExceptionMessageEventHandler;

            if (get65.Checked || withHotFixes.Checked)
            {
                var engage = new Engage(6.5, null, null, null, null, null, withHotFixes.Checked);
                _processor.GetServicePack(engage, _unZipPath);

            }
        }

    }
}
