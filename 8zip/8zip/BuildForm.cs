using System;
using System.Windows.Forms;
using _8zip.Properties;

namespace _8zip
{

    public partial class BuildForm : Form
    {
        
        public string BuildVersion { set; get; }
        public bool isRecOnly { set; get; }

        public BuildForm(bool textBoxAvailability, bool chackboxAvailability)
        {
            InitializeComponent();
            AcceptButton = OkButton;
            IsRecOnlyCheckBox.Enabled = chackboxAvailability;
            buildVersionTextBox.Enabled = textBoxAvailability;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (buildVersionTextBox.Text != string.Empty)
            {
                BuildVersion = buildVersionTextBox.Text;
                isRecOnly = IsRecOnlyCheckBox.Checked;
                ProgressBar.BuildVersion = BuildVersion;
                Close();
            }
            else
            {
                MessageBox.Show(Resources.BuildForm_OkButton_Click_Input_data);
            }
        }

        private void buildVersionTextBox_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
