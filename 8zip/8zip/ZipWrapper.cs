using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Ionic.Zip;

namespace _8zip
{
    public class ZipWrapper
    {
        /*    public void RunMethods(InputData inputs)
            {
                List<int> lstData = new List<int>() {1, 2, 3, 4, 5};
                lstData.ForEach(item => item++);

                inputs.Archive.CreateEntryFromFile(inputs.SourcePath, inputs.EntryName, inputs.Compresion);
                inputs.Archive.CreateEntry(inputs.EntryName, inputs.Compresion);
                inputs.Archive.ExtractToDirectory(inputs.ExtractPath);
           
            }
             */

        struct Source
        {
            public string Path { get; set; }
            public string Name { get; set; }
            public bool IsFolder { get; set; }
        }

        private Source[] TestSource(string[] source)
        {
            Source[] sourceToArchive = new Source[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                sourceToArchive[i].Path = source[i];
                sourceToArchive[i].Name = source[i].Split('\\').Last();
                if (Directory.Exists(source[i]))
                    sourceToArchive[i].IsFolder = true;
                else
                    sourceToArchive[i].IsFolder = false;
            }
            return sourceToArchive;
        }

        public Exception AddFilesToZip(string[] source, string zipPath, CompressionMethod compressLevel)
        {
            Exception exception = null;
            Source[] sourceToArchive = TestSource(source);
            try
            {
                ZipFile zip = !File.Exists(zipPath) ? new ZipFile() : ZipFile.Read(zipPath);
                try
                {
                    using (zip)
                    {
                        zip.CompressionMethod = compressLevel;
                        foreach (Source s in sourceToArchive)
                        {
                            if (!s.IsFolder)
                                zip.AddFile(s.Path, "");
                            else
                                zip.AddDirectory(s.Path, s.Name);
                        }
                        zip.Save(zipPath);
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }

            catch (Exception ex)
            {
                exception = ex;
            }
            return exception;
        }

        public void ExtractFilesFromZip(string zipPath, string pathToExtract)
        {
            try
            {
                using (ZipFile zip = ZipFile.Read(zipPath))
                {
                    zip.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                    zip.ExtractAll(pathToExtract);
                }

            }
                catch (ZipException ex)
            {
                if (ex.Message.Contains("already exists"))
                    throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
