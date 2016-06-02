using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            OpenFileDialog fDialog = new OpenFileDialog
            {
                Title = @"Open File To Archive",
                InitialDirectory = @"C:\",
                Multiselect = true
            };
            DialogResult result = fDialog.ShowDialog(); // Show the dialog.
            
            if (result == DialogResult.OK) // Test result.
            {
                for (int i = 0; i < fDialog.FileNames.Length; i++)                    
                    size++;                
            }          
            SourceTextBox.Text = string.Join(", ", fDialog.SafeFileNames);
            SourceTextBox.Lines = fDialog.FileNames;
        }

        private void Browsebutton2_Click(object sender, EventArgs e)
        {           
            SaveFileDialog saveArchive = new SaveFileDialog();
            InputData inputs = new InputData();
            saveArchive.Title = @"Save Archive As: ";
            saveArchive.Filter = @"Zip files (*.zip)|*.zip";
            DialogResult result = saveArchive.ShowDialog(); // Show the dialog.
            saveArchive.RestoreDirectory = true;

            if (result != DialogResult.OK) return;

            DestinationTextBox.Text = saveArchive.FileName;;
        }

        private void SourceTextBox_TextChanged(object sender, EventArgs e)
        {
                ArchiveButton.Enabled = true;
        }

        private void DestinationTextBox_TextChanged(object sender, EventArgs e)
        {
            ArchiveButton.Enabled = true;
        }

        private void ArchiveButton_Click(object sender, EventArgs e)
        {
            InputData inputs = new InputData();
            ZipWrapper archiveMethods = new ZipWrapper();
            Exception exception = new Exception();

            inputs = inputs.ColectData(this, inputs);

            if (!ExtractZipCheckBox.Checked)
                exception = archiveMethods.AddFilesToZip(inputs.SourcePath, inputs.ZipPath, inputs.Compresion);
            else 
                exception = archiveMethods.ExtractFilesFromZip(inputs.ZipPath, inputs.UnzipPath);
            MessageBox.Show(exception == null
                ? @"Success :-)"
                : @"ERROR!" + " " + exception.Message);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputData inputs = new InputData();
            var key = ((System.Collections.Generic.KeyValuePair<int, string>)ComprLevelcomboBox.SelectedItem).Key;
            var value = ((System.Collections.Generic.KeyValuePair<int, string>)ComprLevelcomboBox.SelectedItem).Value;           
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            InputData inputs2 = new InputData();
            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
            {
                SourceTextBox.Text = string.Join(", ", file);
            }
            SourceTextBox.Lines = files;

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
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ArchiveButton.Enabled = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog() {Description = @"Select path."})
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    webBrowser1.Url = new Uri(dialog.SelectedPath);
                    txtPath.Text = dialog.SelectedPath;
                }
            }
        }

        //===== Additional work required. Lot of bugs... =============

        private void ArchivefilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ArchivefilesCheckBox.Checked)
                UpdateFormEnabledState(true, true, true, true, true, true, false, true, true, true, true);           
            else
                UpdateFormEnabledState(true, true, true, false, false, false, false, false, false, false, false);       
        }

        private void ArchiveFoldersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ArchiveFoldersCheckBox.Checked)
                UpdateFormEnabledState(true, true, true, true, true, true, false, false, false, true, true);           
            else
                UpdateFormEnabledState(true, true, true, false, false, false, false, false, false, false, false);         
        }

        private void ExtractZipCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ExtractZipCheckBox.Checked) 
                UpdateFormEnabledState(false, false, true, true, true, false, false, false, false, true, true);                          
            else
                UpdateFormEnabledState(true, true, true, false, false, false, false, false, false, false, false);           
        }

        //===== Should be moved to different place ========
        private void UpdateFormEnabledState(bool archiveFiles, bool archiveFolders, bool extract, bool zipPath, bool browseButton, bool compress,
            bool run, bool openFilesButton, bool filesPath, bool openFoldersButton, bool folderPath)
        {
            ArchivefilesCheckBox.Enabled = archiveFiles;
            ArchiveFoldersCheckBox.Enabled = archiveFolders;
            ExtractZipCheckBox.Enabled = extract;
            DestinationTextBox.Enabled = zipPath;
            Browsebutton2.Enabled = browseButton;
            ComprLevelcomboBox.Enabled = compress;
            ArchiveButton.Enabled = run;
            BrowseButton1.Enabled = openFilesButton;
            SourceTextBox.Enabled = filesPath;
            btnOpen.Enabled = openFoldersButton;
            txtPath.Enabled = folderPath;
        }

       
    }

}
