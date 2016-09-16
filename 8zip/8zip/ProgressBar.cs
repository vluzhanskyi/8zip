using System;
using System.Windows.Forms;
using System.IO;

namespace _8zip
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
            
        }

        private void ProgressBar_Load(object sender, EventArgs e)
        {

            
        }

        private void ExtractButton_Click(object sender, EventArgs e)
        {
            GetUnzipDirectory();
            var archiveMethods = new ZipWrapper();
            archiveMethods.UpdateFormEvent += ChangeFormControlsState;
            archiveMethods.MaxValueChangedEvent += MaxPorgressValueChangedEventHandler;
            archiveMethods.ProgesEvent += PorgressEventHandler;
            archiveMethods.ExtractAllPackages(_unZipPath);
        }

        private void GetRecPackButton_Click(object sender, EventArgs e)
        {
            GetUnzipDirectory();
            var archiveMethods = new ZipWrapper();
            archiveMethods.UpdateFormEvent += ChangeFormControlsState;
            archiveMethods.MaxValueChangedEvent += MaxPorgressValueChangedEventHandler;
            archiveMethods.ProgesEvent += PorgressEventHandler;
            if (get65.Checked || withHotFixes.Checked)
            {
                Engage engage = new Engage(6.5, null, withHotFixes.Checked);
                var build = new BuildForm(false, true);
                var result = build.ShowDialog();
                Sources sources = new Sources(engage);
                if (result == DialogResult.OK)
                {
                    archiveMethods.GetEngagePackages(engage, sources, build.IsRecOnly, _unZipPath);    
                }
                
            }
           
            if (radioButton66.Checked)
            {
                var build = new BuildForm(true, true);
                var result = build.ShowDialog();
               if (result == DialogResult.OK && build.BuildVersion != null)
                {
                    Engage engage = new Engage(6.6, build.BuildVersion, withHotFixes.Checked);
                    Sources sources = new Sources(engage);
                    archiveMethods.GetEngagePackages(engage, sources, build.IsRecOnly, _unZipPath);
                }
                
            }
        }

        private void GetDeploymentPack_Click(object sender, EventArgs e)
        {
            GetUnzipDirectory();
            var archiveMethods = new ZipWrapper();
            archiveMethods.ProgesEvent += PorgressEventHandler;
            archiveMethods.UpdateFormEvent += ChangeFormControlsState;
            progressBar2.Maximum = 4;
            var build = new BuildForm(true, false);
            string unZipPath = null;
                
            if (get65.Checked)
            {
                Engage engage = new Engage(6.5, null, false);
                unZipPath = archiveMethods.GetDeployment(engage);
            } 
            else if (radioButton66.Checked)
            {
                var result = build.ShowDialog();

                if (result == DialogResult.OK)
                {
                    BuildVersion = build.BuildVersion;
                }
                Engage engage = new Engage(6.6, BuildVersion, false);
                unZipPath = archiveMethods.GetDeployment(engage); 
            }
            if (unZipPath != null)
            {
                _unZipPath = unZipPath;
            }         
        }

        private void PorgressEventHandler(object sender, ProgressEventArgs e)
        {
            if (progressBar2.Value >= progressBar2.Maximum)
                return;
            Action action = () => progressBar2.Value += e.Progress;
            progressBar2.Invoke(action);
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
        }

        private void GetUnzipDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            if (Directory.GetDirectories(currentDirectory, "NDM*").Length > 0)
                _unZipPath = string.Format(Directory.GetDirectories(currentDirectory, "NDM*")[0]);
            else
            {
                _unZipPath = currentDirectory;
            }
        }
    }
}
