using System;
using System.IO;
using System.Linq;
using Ionic.Zip;
using System.Collections.Generic;

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
            public bool IsFolder { get; set; }
        }
        private Source[] TestSource(string[] source)
        {
            Source[] SourceToArchive = new Source[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                SourceToArchive[i].Path = source[i];

                if (Directory.Exists(source[i]))
                    SourceToArchive[i].IsFolder = true;
                else
                    SourceToArchive[i].IsFolder = false;                        
            }
            
                return SourceToArchive;
        }

        public Exception AddFilesToZip(string[] source, string zipPath, CompressionMethod compressLevel)
        {
            Exception exception = null;
            Source[] SourceToArchive = TestSource(source);
            try
            {
                if (!File.Exists(zipPath))
                {
                    try
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            zip.CompressionMethod = compressLevel;

                            foreach (Source s in SourceToArchive)
                            {
                                if (!s.IsFolder)
                                    zip.AddFile(s.Path, "");
                                else
                                    zip.AddDirectory(s.Path, "");
                                zip.Save(zipPath);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                }

                else

                {
                    using (ZipFile zip = ZipFile.Read(zipPath))
                    {
                        try
                        {
                            zip.CompressionMethod = compressLevel;

                            foreach (Source s in SourceToArchive)
                            {
                                if (!s.IsFolder)
                                    zip.AddFile(s.Path, "");
                                else
                                    zip.AddDirectory(s.Path, "");
                                zip.Save(zipPath);
                            }

                            zip.Save(zipPath);
                        }
                        catch (ArgumentException ex)
                        {
                            exception = ex;
                        }
                    }
                }
                    return exception;
                }

            catch (Exception ex)
            {
                return ex;
            }
            
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

        /*      internal Exception AddFoldersToZip(string[] source, string zipPath)
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

              public void CreateFromDirectory(InputData inputs)
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
