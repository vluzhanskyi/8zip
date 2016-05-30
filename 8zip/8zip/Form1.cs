using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;

namespace _8zip
{
    public partial class EightZip : Form
    {
        public EightZip()
        {
            InitializeComponent();
        }

        private void EightZip_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int size = 1;
            string[] files = new string[size]; 
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Open File To Archive";
            fDialog.InitialDirectory = @"C:\";
            fDialog.Multiselect = true;
            DialogResult result = fDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                for (int i = 0; i < fDialog.FileNames.Length; i++)
                    size++;

                if (files != null)
                    SourceTextBox.Text = String.Join(", ", fDialog.SafeFileNames);
                else
                    MessageBox.Show("Chose file to Archive");
                files = fDialog.FileNames;
            }
           
        
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Browsebutton2_Click(object sender, EventArgs e)
        {           
            SaveFileDialog SaveArchive = new SaveFileDialog();
            SaveArchive.Title = "Save Archive As: ";
            DialogResult result = SaveArchive.ShowDialog(); // Show the dialog.
            SaveArchive.RestoreDirectory = true;
            if (result == DialogResult.OK) // Test result.
            {
                if (SaveArchive.CheckFileExists)
                {
                    MessageBoxButtons buttons = new MessageBoxButtons();
                    MessageBox.Show("File alredy exists", "Rewrite?", buttons);
                }  
    
                string file = SaveArchive.FileName;
                DestinationTextBox.Text = file;
            }
            
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void SourceTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void DestinationTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void ArchiveButton_Click(object sender, EventArgs e)
        {

        }
    }
}
