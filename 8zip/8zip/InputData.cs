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
 //       public ZipArchive Destination { get; set; }
        public CompressionMethod Compresion { get; set; }

        private CompressionMethod GetCompressLevel(EightZip form)
        {
            if (form.ComprLevelcomboBox.SelectedIndex == 0)
                Compresion = CompressionMethod.None;
            if (form.ComprLevelcomboBox.SelectedIndex == 1)
                Compresion = CompressionMethod.Deflate;
            if (form.ComprLevelcomboBox.SelectedIndex == 2)
                Compresion = CompressionMethod.BZip2;
            return Compresion;
        }

        private string GetZipPath(EightZip form)
        {
            ZipPath = form.DestinationTextBox.Text;
            return ZipPath;
        }

        private string[] GetFilesToArchive(EightZip form)
        {
            SourcePath = new string[form.SourceTextBox.Lines.Length];
            SourcePath = form.SourceTextBox.Lines;
            return SourcePath;
        }

        private string[] GetFoldersToArchive(EightZip form)
        {
            SourceFoldersPath = new string[form.txtPath.Lines.Length];
            SourceFoldersPath = form.txtPath.Lines;
            return SourceFoldersPath;
        }

        public InputData ColectData(EightZip form, InputData inputs)
        {
            inputs.SourcePath = inputs.GetFilesToArchive(form);
            inputs.ZipPath = inputs.GetZipPath(form);
            inputs.SourceFoldersPath = inputs.GetFoldersToArchive(form);
            if (SourceFoldersPath.Length > 0)
                inputs.UnzipPath = inputs.SourceFoldersPath[0];
            inputs.Compresion = inputs.GetCompressLevel(form);

            return inputs;
        }
    }

}
