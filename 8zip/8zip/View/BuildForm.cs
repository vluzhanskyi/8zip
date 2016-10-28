using System;
using System.Windows.Forms;

namespace _8zip.View
{

    public partial class BuildForm : Form
    {
        public bool IsRecOnly;
        public bool IsCleanInstallation;
        public string BuildVersion { set; get; }
        public string MiniBusBuildVersion { set; get; }
        public string SplashBuildVersion { set; get; }
        public string AAgentBuildVersion { set; get; }

        public BuildForm(bool textBoxAvailability, bool isRecOnlyCheckBoxAvailability, bool isCleanCheckBoxAvailability)
        {
            InitializeComponent();
            AcceptButton = OkButton;
            IsRecOnlyCheckBox.Enabled = isRecOnlyCheckBoxAvailability;
            IsCleanCheckBox.Enabled = isCleanCheckBoxAvailability;
            buildVersionTextBox.Enabled = textBoxAvailability;
            MiniBusTextBox.Enabled = textBoxAvailability;
            SplashTextBox.Enabled = textBoxAvailability;
            AAgentTextBox.Enabled = textBoxAvailability;
            label1.Enabled = buildVersionTextBox.Enabled;
            label2.Enabled = MiniBusTextBox.Enabled;
            label3.Enabled = SplashTextBox.Enabled;
            label4.Enabled = AAgentTextBox.Enabled;
            OkButton.DialogResult = DialogResult.OK;

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            BuildVersion = buildVersionTextBox.Text;
            MiniBusBuildVersion = MiniBusTextBox.Text;
            SplashBuildVersion = SplashTextBox.Text;
            AAgentBuildVersion = AAgentTextBox.Text;
            IsRecOnly = IsRecOnlyCheckBox.Checked;
            IsCleanInstallation = IsCleanCheckBox.Checked;
            ProgressBar.BuildVersion = BuildVersion;
            Close();           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    
    }
}
