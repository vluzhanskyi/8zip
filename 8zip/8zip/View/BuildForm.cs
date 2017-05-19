using System;
using System.Windows.Forms;

namespace _8zip.View
{

    public partial class BuildForm : Form
    {
        public bool IsRecOnly;
        public bool IsCleanInstallation;
        public bool IsCustomPackages;
        public string BuildVersion { set; get; }
        public string MiniBusBuildVersion { set; get; }
        public string SplashBuildVersion { set; get; }
        public string AAgentBuildVersion { set; get; }
        public string NcaBuildVersion { set; get; }

        public BuildForm(bool textBoxAvailability, bool isRecOnlyCheckBoxAvailability, bool isCleanCheckBoxAvailability , string buildlabeltext = null)
        {
            InitializeComponent();
            AcceptButton = OkButton;
            IsRecOnlyCheckBox.Enabled = isRecOnlyCheckBoxAvailability;
            IsCleanCheckBox.Enabled = isCleanCheckBoxAvailability;
            buildVersionTextBox.Enabled = textBoxAvailability;
            MiniBusTextBox.Enabled = textBoxAvailability;
            SplashTextBox.Enabled = textBoxAvailability;
            AAgentTextBox.Enabled = textBoxAvailability;
            NCABuildVersionTextBox.Enabled = textBoxAvailability;
            OkButton.DialogResult = DialogResult.OK;
            if (buildlabeltext != null)
            {
                label1.Text = buildlabeltext;
            }
        }

        private void BuildForm_Loaded(object sender, EventArgs e)
        {
            label1.Enabled = buildVersionTextBox.Enabled;
            label2.Enabled = MiniBusTextBox.Enabled;
            label3.Enabled = SplashTextBox.Enabled;
            label4.Enabled = AAgentTextBox.Enabled;
            label5.Enabled = NCABuildVersionTextBox.Enabled;
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            BuildVersion = buildVersionTextBox.Text;
            MiniBusBuildVersion = MiniBusTextBox.Text;
            SplashBuildVersion = SplashTextBox.Text;
            AAgentBuildVersion = AAgentTextBox.Text;
            NcaBuildVersion = NCABuildVersionTextBox.Text;
            IsRecOnly = IsRecOnlyCheckBox.Checked;
            IsCleanInstallation = IsCleanCheckBox.Checked;
            IsCustomPackages = IsCustomCheckBox.Checked;
            ProgressBar.BuildVersion = BuildVersion;
            Close();
        }

        private void CbIsCustom_CheckedChanged(object sender, EventArgs e)
        {
            IsRecOnlyCheckBox.Enabled = !IsCustomCheckBox.Checked;
            IsCleanCheckBox.Enabled = !IsCustomCheckBox.Checked;
        }
    }
}
