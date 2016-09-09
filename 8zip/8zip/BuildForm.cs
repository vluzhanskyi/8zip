using System;
using System.Windows.Forms;
using _8zip.Properties;

namespace _8zip
{

    public partial class BuildForm : Form
    {
        
        public string BuildVersion { set; get; }

        public BuildForm()
        {
            InitializeComponent();
            AcceptButton = OkButton;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (buildVersionTextBox.Text != string.Empty)
            {
                BuildVersion = buildVersionTextBox.Text;
                ProgressBar.BuildVersion = BuildVersion;
                Close();
            }
            else
            {
                MessageBox.Show(Resources.BuildForm_OkButton_Click_Input_data);
            }
        }

       
    }
}
