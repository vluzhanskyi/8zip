using Ionic.Zip;
using System.IO.Compression;

namespace _8zip
{
    public class InputData
    {

        public string[] SourcePath { get; set; }
        public string[] SourceFoldersPath { get; set; }
        public string ZipPath { get; set; }
        public string UnzipPath { get; set; }
        public ZipArchive Destination { get; set; }
           
        public CompressionMethod Compresion { get; set; }

        public CompressionMethod GetCompressLevel(EightZip form)
        {
            if (form.ComprLevelcomboBox.SelectedIndex == 0)
                Compresion = CompressionMethod.None;
            if (form.ComprLevelcomboBox.SelectedIndex == 1)
                Compresion = CompressionMethod.Deflate;
            if (form.ComprLevelcomboBox.SelectedIndex == 2)
                Compresion = CompressionMethod.BZip2;
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

        public InputData ColectData(EightZip formEightZip, InputData inputs)
        {
            ZipWrapper archiveMethods = new ZipWrapper();
            inputs.SourcePath = inputs.GetFilesToArchive(formEightZip);
            inputs.ZipPath = inputs.GetZipPath(formEightZip);
            inputs.Compresion = inputs.GetCompressLevel(formEightZip);
            return inputs;
        }
    }

}
