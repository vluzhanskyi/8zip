using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms.VisualStyles;


namespace _8zip
{
    public partial class EightZip : Form
    {
        

        public EightZip()
        {
            InitializeComponent();
            
        }

        public void EightZip_Load(object sender, EventArgs e)
        {
            Dictionary<int, string> comboSource = new Dictionary<int, string>
            {
                {0, "No Compression"},
                {1, "Deflate"},
                {2, "BZip2"}
            };
            ComprLevelcomboBox.DataSource = new BindingSource(comboSource, null);
            ComprLevelcomboBox.ValueMember = "Key";
            ComprLevelcomboBox.DisplayMember = "Value";          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int size = 1;
            InputData inputs = new InputData();
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = @"Open File To Archive";
            fDialog.InitialDirectory = @"C:\";
            fDialog.Multiselect = true;
            DialogResult result = fDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                for (int i = 0; i < fDialog.FileNames.Length; i++)                    
                    size++;                
            }          
            SourceTextBox.Text = string.Join(", ", fDialog.SafeFileNames);
            SourceTextBox.Lines = fDialog.FileNames;
            inputs.GetFilesToArchive(this);
        }

        private void Browsebutton2_Click(object sender, EventArgs e)
        {           
            SaveFileDialog saveArchive = new SaveFileDialog();
            InputData inputs = new InputData();
            saveArchive.Title = @"Save Archive As: ";
            saveArchive.Filter = @"Zip files (*.zip)|*.zip";
            DialogResult result = saveArchive.ShowDialog(); // Show the dialog.
            saveArchive.RestoreDirectory = true;

            if (result == DialogResult.OK) // Test result.
            {
                if (saveArchive.CheckFileExists)
                {
                    MessageBoxButtons buttons = new MessageBoxButtons();
                    MessageBox.Show(@"File alredy exists", @"Update?", buttons);
                }
                DestinationTextBox.Text = saveArchive.FileName;
                inputs.GetZipPath(this);
            }
            
        }

        private void SourceTextBox_TextChanged(object sender, EventArgs e)
        {
            InputData inputs = new InputData();
            if (inputs.SourcePath != null)
                inputs.GetFilesToArchive(this);
        }

        private void DestinationTextBox_TextChanged(object sender, EventArgs e)
        {
            InputData inputs = new InputData();
            if (inputs.ZipPath != null)
                inputs.GetZipPath(this);
        }

        private void ArchiveButton_Click(object sender, EventArgs e)
        {
            InputData inputs = new InputData();
            ZipWrapper archiveMethods = new ZipWrapper();
            inputs.SourcePath = inputs.GetFilesToArchive(this);
            inputs.ZipPath = inputs.GetZipPath(this);
            inputs.Compresion = inputs.GetCompressLevel(this);

            MessageBox.Show(archiveMethods.AddFilesToZip(inputs.SourcePath, inputs.ZipPath, inputs.Compresion)
                ? @"Done :-)"
                : @"Fail!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputData inputs = new InputData();
            var key = ((System.Collections.Generic.KeyValuePair<int, string>)ComprLevelcomboBox.SelectedItem).Key;
            var value = ((System.Collections.Generic.KeyValuePair<int, string>)ComprLevelcomboBox.SelectedItem).Value;
            inputs.GetCompressLevel(this);
            
        }

        private void SourceLabel_Click(object sender, EventArgs e)
        {

        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
        }
    }

}
