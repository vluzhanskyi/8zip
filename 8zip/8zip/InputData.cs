using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows.Forms;

namespace _8zip
{
    public class InputData
    {
        public string[] SourcePath { get; set; }
        public string ZipPath { get; set; }
        EightZip Form = new EightZip();
        // public string EntryName { get; set; }
        // public string ExtractPath { get; set; }
        public CompressionLevel Compresion { get; set; }

        public void GetCompressLevel()
        {
            if (Form.ComprLevelcomboBox.SelectedIndex == 0)
                Compresion = CompressionLevel.Optimal;
            if (Form.ComprLevelcomboBox.SelectedIndex == 1)
                Compresion = CompressionLevel.Fastest;
            if (Form.ComprLevelcomboBox.SelectedIndex == 2)
                Compresion = CompressionLevel.NoCompression;
        }

        public void GetZipPath()
        {
            ZipPath = Form.DestinationTextBox.Text;
        }

        public void GetFilesToArchive()
        {
            SourcePath = Form.SourceTextBox.Lines;
        }
    }

}
