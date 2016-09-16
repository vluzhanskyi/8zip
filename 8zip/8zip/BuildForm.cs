using System;
using System.Windows.Forms;

namespace _8zip
{

    public partial class BuildForm : Form
    {
        public bool IsRecOnly;
        
        public string BuildVersion { set; get; }

        public BuildForm(bool textBoxAvailability, bool chackboxAvailability)
        {
            InitializeComponent();
            AcceptButton = OkButton;
            IsRecOnlyCheckBox.Enabled = chackboxAvailability;
            buildVersionTextBox.Enabled = textBoxAvailability;
            OkButton.DialogResult = DialogResult.OK;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            BuildVersion = buildVersionTextBox.Text;
            IsRecOnly = IsRecOnlyCheckBox.Checked;
            ProgressBar.BuildVersion = BuildVersion;
            Close();
            
        }
    
    }
}
