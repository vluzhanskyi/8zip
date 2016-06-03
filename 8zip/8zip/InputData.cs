using Ionic.Zip;
using System.Collections.Generic;

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
            if (form.txtPath.Text.Length > 0)
                SourceFoldersPath[0] = form.txtPath.Text;
            return SourceFoldersPath;
        }

        public InputData ColectData(EightZip uiForm, InputData inputs)
        {
            inputs.SourcePath = inputs.GetFilesToArchive(uiForm);
            inputs.ZipPath = inputs.GetZipPath(uiForm);
            inputs.SourceFoldersPath = inputs.GetFoldersToArchive(uiForm);
            inputs.Compresion = inputs.GetCompressLevel(uiForm);
            //========================Should be changed according to UX future changes======
            inputs.SourcePath = ColectSources(inputs.SourceFoldersPath, inputs.SourcePath);
            if (SourceFoldersPath.Length > 0)
                inputs.UnzipPath = inputs.SourceFoldersPath[0];         //for test 

            return inputs;
        }

        private string[] ColectSources(string[] files, string[] folders)
        {
            List<string> listOfStrings = new List<string>();

            foreach (string s in files)
                listOfStrings.Add(s);

            foreach (string p in folders)
                listOfStrings.Add(p);
            
            return listOfStrings.ToArray();
        }
    }

}
