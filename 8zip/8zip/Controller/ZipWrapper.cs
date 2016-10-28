using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Ionic.Zip;
using _8zip.CustomEvents;
using _8zip.Properties;
using _8zip.View;

namespace _8zip.Controller
{
    public class ZipWrapper
    {

        public event EventHandler<ProgressEventArgs> ProgesEvent;
        public event EventHandler<ExtractProgressEventArgs> ExtratingProgressEvent;
        public event EventHandler<ChangeMaxProgressValueEventArgs> MaxValueChangedEvent;
        public event EventHandler<UpdateFormArgs> UpdateFormEvent;
        public event EventHandler<UpdatePackageNameArgs> ChangePackageNameEvent;
        public event EventHandler<ShowExceptionMessageArks> ExceptionEvent;
//        private AutoResetEvent _resetEvent = new AutoResetEvent(false);
        protected static List<string> RecPackagesList;
        protected static List<string> NotUsedPackagesList;
        internal const long MinimalFileSize = 1000;

        internal ZipWrapper()
        {
            RecPackagesList = new List<string>
            {
                "AIR",
                "Applications",
                "CTI",
                "Data Mart",
                "Database",
                "Interactions Center",
                "Logger",
                "Storage",
                "KAI",
                "SQLAutoSetup2014_Enertprise",
                "Reporter",
                "xml",
                "NDM",
                "SRT",
                "SP",
                "Authentication Agent",
                "Authentication Spotlight",
                "Engage Search",
                "BUS"
            };

            NotUsedPackagesList = new List<string>
            {
                "Thai",
                "Spanish",
                "Russian",
                "Swedish",
                "Cantonese",
                "English",
                "French",
                "German",
                "Italian",
                "Japanese",
                "Korean",
                "Polish",
                "Portuguese",
                "Turkish",
                "Hindi",
                "Hebrew",
                "Chinese",
                "Biometrics",
                "2012",
                "Standart"
            };
        }

        protected void ExtractFilesFromZip(string zipPath, string pathToExtract)
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
                                OnRiseExceptionEvent(new ShowExceptionMessageArks(ex.Message));
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
                OnRiseExceptionEvent(new ShowExceptionMessageArks(string.Format(ex.Message)));
            }

        }

        protected void RemoveZip(string archive)
        {
            try
            {               
                    File.Delete(archive);
            }
            catch (Exception ex)
            {

                OnRiseExceptionEvent(new ShowExceptionMessageArks(string.Format(ex.Message)));
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
                    OnRiseExceptionEvent(new ShowExceptionMessageArks(Resources.ProgressBar_ExtractButton_Click___zip_was_not_found));
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
                        OnRaiseProgressEvent(new ProgressEventArgs(1, false));
                    }
                    catch (Exception ex)
                    {
                        OnRiseExceptionEvent(new ShowExceptionMessageArks(string.Format("Fail to extract {0} \n {1}", archive, ex.Message)));
                    }
                }
                
                OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                        string.Empty, string.Empty, unZipPath));                
            };

            worker.RunWorkerCompleted += (s, p) =>
            {
                OnRiseExceptionEvent(p.Error != null
                    ? new ShowExceptionMessageArks(string.Format(p.Error.Message))
                    : new ShowExceptionMessageArks("Done!"));
                
              OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                        string.Empty, string.Empty, unZipPath));
                OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
            };
            worker.RunWorkerAsync();
        }

        protected virtual void OnRiseExceptionEvent(ShowExceptionMessageArks e)
        {
            EventHandler<ShowExceptionMessageArks> handler = ExceptionEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRiseChangePackageNameEvent(UpdatePackageNameArgs e)
        {
            EventHandler<UpdatePackageNameArgs> handler = ChangePackageNameEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRaiseUpdateFormEvent(UpdateFormArgs e)
        {
            EventHandler<UpdateFormArgs> handler = UpdateFormEvent;

            if (handler != null)
            {
                handler(this, e);
            }

        }

        private void OnRaiseExtractProgressEvent(object sender, ExtractProgressEventArgs e)
        {
            EventHandler<ExtractProgressEventArgs> handler = ExtratingProgressEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRaiseProgressEvent(ProgressEventArgs e)
        {
            EventHandler<ProgressEventArgs> handler = ProgesEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRaiseMaxProgressValueChangedEvent(ChangeMaxProgressValueEventArgs e)
        {
            EventHandler<ChangeMaxProgressValueEventArgs> handler = MaxValueChangedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

}
