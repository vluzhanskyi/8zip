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
            Dictionary<int, string> ComboSource = new Dictionary<int, string>
            {
                {0, "No Compression"},
                {1, "Deflate"},
                {2, "BZip2"}
            };
            ComprLevelcomboBox.DataSource = new BindingSource(ComboSource, null);
            ComprLevelcomboBox.ValueMember = "Key";
            ComprLevelcomboBox.DisplayMember = "Value";          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int size = 1;
            InputData _inputs = new InputData();
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Open File To Archive";
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
            _inputs.GetFilesToArchive(this);
        }

        private void Browsebutton2_Click(object sender, EventArgs e)
        {           
            SaveFileDialog SaveArchive = new SaveFileDialog();
            InputData _inputs = new InputData();
            SaveArchive.Title = "Save Archive As: ";
            SaveArchive.Filter = "Zip files (*.zip)|*.zip";
            DialogResult result = SaveArchive.ShowDialog(); // Show the dialog.
            SaveArchive.RestoreDirectory = true;

            if (result == DialogResult.OK) // Test result.
            {
                if (SaveArchive.CheckFileExists)
                {
                    MessageBoxButtons buttons = new MessageBoxButtons();
                    MessageBox.Show("File alredy exists", "Update?", buttons);
                }
                DestinationTextBox.Text = SaveArchive.FileName;
                _inputs.GetZipPath(this);
            }
            
        }

        private void SourceTextBox_TextChanged(object sender, EventArgs e)
        {
            InputData _inputs = new InputData();
            if (_inputs.SourcePath != null)
                _inputs.GetFilesToArchive(this);
        }

        private void DestinationTextBox_TextChanged(object sender, EventArgs e)
        {
            InputData _inputs = new InputData();
            if (_inputs.ZipPath != null)
                _inputs.GetZipPath(this);
        }

        private void ArchiveButton_Click(object sender, EventArgs e)
        {
            InputData _inputs = new InputData();
            ZipWrapper ArchiveMethods = new ZipWrapper();
            _inputs.SourcePath = _inputs.GetFilesToArchive(this);
            _inputs.ZipPath = _inputs.GetZipPath(this);
            _inputs.Compresion = _inputs.GetCompressLevel(this);
            ArchiveMethods.AddFilesToZip(_inputs.SourcePath, _inputs.ZipPath, _inputs.Compresion);
            MessageBox.Show("Done!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputData _inputs = new InputData();
            var key = ((System.Collections.Generic.KeyValuePair<int, string>)ComprLevelcomboBox.SelectedItem).Key;
            var value = ((System.Collections.Generic.KeyValuePair<int, string>)ComprLevelcomboBox.SelectedItem).Value;
            _inputs.GetCompressLevel(this);
            
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            List<string> files = (List<string>) e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
            {
                List<string> list = new List<string>();
                list.Add(file);
             
            }
        }
    }

}
