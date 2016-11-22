using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Ionic.Zip;
using _8zip.CustomEvents;
using _8zip.Properties;
using _8zip.View;

namespace _8zip.Controller
{
    public class ZipWrapper : Processor
    {
        internal ZipWrapper()
        {
            
        }

        protected internal void ExtractFilesFromZip(string zipPath, string pathToExtract)
        {
            try
            {
                using (var zip = ZipFile.Read(zipPath))
                {
                   zip.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                   zip.ExtractProgress += OnRaiseExtractProgressEvent;
                   if (zip.Name.Contains("SQLAutoSetup") || zip.Name.Contains("SQL Auto Setup"))
                    {
                        var selection = from cf in zip.Entries
                                         where cf.FileName.StartsWith("SQLAutoSetup") || cf.FileName.StartsWith("SQL Auto Setup Ent")
                                         select cf;

                        selection.ToList().ForEach(entry =>
                        {
                            try
                            {
                                if (entry.FileName == "SQLAutoSetup" || entry.FileName == "SQL Auto Setup Ent/") return;
                                entry.FileName = entry.FileName.Substring(entry.FileName.IndexOf("/", StringComparison.Ordinal));
                                entry.Extract(string.Format("{0}\\SqlAutoSetup for Operational", pathToExtract), ExtractExistingFileAction.OverwriteSilently);
                            }
                            catch (Exception ex)
                            {
                                OnRiseExceptionEvent(null, new ShowExceptionMessageArks(ex.Message));
                            }

                        });

                    }
                    else
                    {
                        zip.ExtractAll(pathToExtract);
                    }
                    
                }

            }
                catch (ZipException ex)
            {
                if (ex.Message.Contains("already exists"))
                    throw;
            }
            catch (Exception ex)
            {
                OnRiseExceptionEvent(null, new ShowExceptionMessageArks(string.Format(ex.Message)));
            }

        }

        protected internal void RemoveZip(string archive)
        {
            try
            {               
                    File.Delete(archive);
            }
            catch (Exception ex)
            {

                OnRiseExceptionEvent(null, new ShowExceptionMessageArks(string.Format(ex.Message)));
            }
        }

        public void ExtractAllPackages(string unZipPath)
        {
            OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
            if (unZipPath.Length <= unZipPath.LastIndexOf("\\", StringComparison.Ordinal) + 1) return;
            var directory = unZipPath.Substring(unZipPath.LastIndexOf("\\", StringComparison.Ordinal) + 1).Contains("NDM")
                ? string.Format(unZipPath + @"\Packages")
                : unZipPath;
            int i = 0;
            int j = 0;
            var archives = Directory.GetFiles(directory, "*.zip");
            var worker = new BackgroundWorker();
            worker.DoWork += (worker1, result) =>
            {   
                j = archives.Length;
                OnRaiseMaxProgressValueChangedEvent(new ChangeMaxProgressValueEventArgs(archives.Length));

                if (archives.Length == 0)
                {
                    OnRiseExceptionEvent(null, new ShowExceptionMessageArks(Resources.ProgressBar_ExtractButton_Click___zip_was_not_found));
                }
                var backgroundWorker = worker1 as BackgroundWorker;
                if (backgroundWorker != null) backgroundWorker.WorkerReportsProgress = true;

                foreach (var archive in archives)
                {
                    i++;
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                            Actions.Extracting,
                                                                            Path.GetFileNameWithoutExtension(archive), unZipPath));
                    try
                    {
                        ExtractFilesFromZip(archive, directory);
                        OnProgressEvent(new object(), new ProgressEventArgs(1, false));
                    }
                    catch (Exception ex)
                    {
                        OnRiseExceptionEvent(null, new ShowExceptionMessageArks(string.Format("Fail to extract {0} \n {1}", archive, ex.Message)));
                    }
                }

                OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                        string.Empty, string.Empty, unZipPath));                
            };

            worker.RunWorkerCompleted += (s, p) =>
            {
                OnRiseExceptionEvent(null, p.Error != null
                    ? new ShowExceptionMessageArks(string.Format(p.Error.Message))
                    : new ShowExceptionMessageArks("Done!"));

                OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                        string.Empty, string.Empty, unZipPath));
                OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
            };
            worker.RunWorkerAsync();
        }

    }

}
