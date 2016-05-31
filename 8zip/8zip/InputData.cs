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
        // public string EntryName { get; set; }
        // public string ExtractPath { get; set; }

        public CompressionLevel Compresion { get; set; }

        public void GetCompressLevel()
        {
            if (EightZip.ComprLevelcomboBox.SelectedIndex == 0)
                Compresion = CompressionLevel.Optimal;
            if (EightZip.ComprLevelcomboBox.SelectedIndex == 1)
                Compresion = CompressionLevel.Fastest;
            if (EightZip.ComprLevelcomboBox.SelectedIndex == 2)
                Compresion = CompressionLevel.NoCompression;
        }

        public void GetZipPath()
        {
            ZipPath = EightZip.DestinationTextBox.Text;
        }

        public void GetFilesToArchive()
        {
            SourcePath = EightZip.SourceTextBox.Lines;
        }
    }

}
