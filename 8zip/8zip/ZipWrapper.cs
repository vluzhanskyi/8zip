using System;
using System.IO;
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

        public Exception AddFilesToZip(string[] files, string zipPath, CompressionMethod compressLevel)
        {
            try
            {
                if (files[0] != "Define path to files" && zipPath != "Define path to save zip file")
                {
                    if (!File.Exists(zipPath))
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            zip.CompressionMethod = compressLevel;
                            zip.AddFiles(files, "");
                            zip.Save(zipPath);
                        }
                    }

                    else

                    {
                        using (ZipFile zip = ZipFile.Read(zipPath))
                        {

                            try
                            {
                                zip.AddFiles(files, "");
                            }
                            catch (ArgumentException ex)
                            {

                                return ex;
                            }

                            zip.Save(zipPath);
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex;
            }

            return new Exception("Please define data correctly!");
        }

        public Exception ExtractFilesFromZip(string zipPath, string pathToExtract)
        {
            try
            {
                using (ZipFile zip = ZipFile.Read(zipPath))
                {
                    zip.ExtractAll(pathToExtract);
                    zip.ExtractExistingFile = ExtractExistingFileAction.Throw;
                    zip.Save(zipPath);
                }
                return null;
            }
            catch (Exception exception)
            {
                return exception;
            }
            
 //           return new Exception("Invalid extracting path or archive defined");
        }

        internal Exception AddFoldersToZip(string[] source, string zipPath)
        {
            Exception ex = null;
            if (!File.Exists(zipPath))
            {
                try
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        for (int i = 0; i < source.Length; i++)
                            zip.AddDirectory(source[i]);
                        zip.Save(zipPath);                       
                    }
                }
                catch (Exception exception)
                {ex = exception;}
            }
            else
            {
                try
                {
                    using (ZipFile zip = ZipFile.Read(zipPath))
                    {
                        for (int i = 0; i < source.Length; i++)
                            zip.AddDirectory(source[i]);
                        zip.Save(zipPath);
                    }
                }
                catch (Exception exception)
                {ex = exception;}           
            }
            return ex;
        }

    /*    public void CreateFromDirectory(InputData inputs)
        {
            
            foreach (string t in inputs.SourceFoldersPath)
            {
                ZipFile.CreateFromDirectory(t, inputs.ZipPath, inputs.Compresion, false);
            }
        }

        public void CreateFromFile(InputData inputs)
        {
            foreach (string t in inputs.SourcePath)
            {

                using (inputs.Destination = ZipFile.Open(inputs.ZipPath, ZipArchiveMode.Create))
                {
                    inputs.Destination.CreateEntryFromFile(t, "newEntry.txt", inputs.Compresion);
                }
               
           }
        }
        
        public void ExtractFromZipFile(InputData inputs)
        {
            foreach (string t in inputs.SourcePath)
            {
                ZipFile.ExtractToDirectory(inputs.ZipPath, inputs.UnzipPath); 
            }
        }
*/

    }
}
