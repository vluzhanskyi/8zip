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
        public string[] SourceFoldersPath { get; set; }
        public string ZipPath { get; set; }
        public string UnzipPath { get; set; }
        public ZipArchive Destination { get; set; }
           
        public CompressionLevel Compresion { get; set; }

        public CompressionLevel GetCompressLevel(EightZip form)
        {
            if (form.ComprLevelcomboBox.SelectedIndex == 0)
                Compresion = CompressionLevel.Optimal;
            if (form.ComprLevelcomboBox.SelectedIndex == 1)
                Compresion = CompressionLevel.Fastest;
            if (form.ComprLevelcomboBox.SelectedIndex == 2)
                Compresion = CompressionLevel.NoCompression;
            return Compresion;
        }

        public string GetZipPath(EightZip form)
        {
            ZipPath = form.DestinationTextBox.Text;
            return ZipPath;
        }

        public string[] GetFilesToArchive(EightZip form)
        {
            SourcePath = new string[form.SourceTextBox.Lines.Length];
            SourcePath = form.SourceTextBox.Lines;
            return SourcePath;
        }
    }

}
