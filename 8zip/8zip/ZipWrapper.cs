using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Ionic.Zip;
using _8zip.Properties;

namespace _8zip
{
    public class ZipWrapper
    {

        public event EventHandler<ProgressEventArgs> ProgesEvent;
        public event EventHandler<ChangeMaxProgressValueEventArgs> MaxValueChangedEvent;
        public event EventHandler<UpdateFormArgs> UpdateFormEvent;
        
        private static List<string> _recPackagesList;
        private static List<string> _notUsedPackagesList;
        private const long MinimalFileSize = 1000;

        internal ZipWrapper()
        {
            _recPackagesList = new List<string>
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
                "SP"
            };

            _notUsedPackagesList = new List<string>
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
                "Biometrics"
            };
        }

        private void ExtractFilesFromZip(string zipPath, string pathToExtract)
        {
            try
            {
                using (var zip = ZipFile.Read(zipPath))
                {
                   zip.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                   if (zip.Name.Contains("SQLAutoSetup") || zip.Name.Contains("SQL Auto Setup"))
                    {
                        var selection = from cf in zip.Entries
                                         where cf.FileName.StartsWith("SQLAutoSetup/") || cf.FileName.StartsWith("SQL Auto Setup Ent 2014")
                                         select cf;

                        selection.ToList().ForEach(entry =>
                        {
                            try
                            {
                                entry.FileName = entry.FileName.Substring(13);
                                entry.Extract(string.Format("{0}\\SqlAutoSetup for Operational", pathToExtract), ExtractExistingFileAction.OverwriteSilently);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }

        }

        private void RemoveZip(string archive)
        {
            try
            {               
                    File.Delete(archive);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void GetPackages(string sourcePath, string targetPath, string fileName, bool isRecOnly = true)
        {
            string sourceFile = Path.Combine(sourcePath, fileName);
            string destFile = Path.Combine(targetPath, Path.GetFileName(fileName));

            if (isRecOnly)
            {
                foreach (var package in _recPackagesList)
                {
                    if (sourceFile.Contains(package))
                    {
                        File.Copy(sourceFile, destFile, true);
                    }
                }
            }
            else
            {
                if (!fileName.Contains("SQLAutoSetup") && !fileName.Contains("SQL Auto Setup"))
                {
                    var f = new FileInfo(sourceFile);
                    var isApproved = _notUsedPackagesList.All(package => !sourceFile.Contains(package) && f.Length >= MinimalFileSize);
                    if (isApproved)
                    {
                       File.Copy(sourceFile, destFile, true);
                    }
                }
                else if (sourceFile.Contains("SQL Auto Setup Ent 2014") || sourceFile.Contains("SQLAutoSetup2014_Enertprise"))
                {
                   File.Copy(sourceFile, destFile, true);
                }
            }
        }

        private string GetFolderToWork(string unZipPath, string subFolder)
        {
            var directory = !unZipPath.Contains("NDM")
                           ? unZipPath
                           : string.Format(@"{0}\{1}", unZipPath, subFolder);
            return directory;
        }

        public string GetDeployment(Engage engage)
        {
            OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
            string unZipPath = null;
            var sources = new Sources(engage);
            var directory = Directory.GetCurrentDirectory();       
            var worker = new BackgroundWorker();
            worker.DoWork += (worker1, result) =>
            {
                foreach (var archive in sources.DEplymentSourcePath)
                {
                    GetPackages(archive, directory, archive);
                    var dDestination = Directory.GetFiles(directory, "*.zip");
                    OnRaiseProgressEvent(new ProgressEventArgs(1));
                    var archiveName = Path.GetFileNameWithoutExtension(archive);
                    var path = string.Format(directory + @"\" + archiveName);
                    if (path.Contains("NDM") || path.Contains("SRT"))
                    {
                        unZipPath = path;
                    }
                    ExtractFilesFromZip(dDestination[0], path);
                    RemoveZip(dDestination[0]);
                    OnRaiseProgressEvent(new ProgressEventArgs(1));
                }
            };
            worker.RunWorkerCompleted += (s, p) =>
            {
                MessageBox.Show(Resources.ProgressBar_GetRecPackButton_Click_Done_);
                OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
            };
            worker.RunWorkerAsync();         
            return unZipPath;
        }

        public void GetEngagePackages(Engage engage, Sources sources, bool isRecOnly, string unZipPath)
        {
            OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
            var maxProgressValue = 1;           
            var directory = GetFolderToWork(unZipPath, "Packages");
            var packages = Directory.GetFiles(sources.PackageSourcePath, "*.zip", SearchOption.AllDirectories);

            var worker = new BackgroundWorker();
            worker.DoWork += (worker1, result) =>
                {
                    var backgroundWorker = worker1 as BackgroundWorker;
                    if (backgroundWorker != null)
                        backgroundWorker.WorkerReportsProgress = true;
                    
                    maxProgressValue += packages.Length;
                    OnRaiseMaxProgressValueChangedEvent(new ChangeMaxProgressValueEventArgs(maxProgressValue));

                    if (engage.WithSp)
                    {
                        var spDirectory = GetFolderToWork(unZipPath, "ServicePacks");
                        var sps = Directory.GetFiles(sources.SpSourcePath, "*.zip");
                        GetPackages(sources.SpSourcePath, spDirectory, sps[0]);
                        OnRaiseProgressEvent(new ProgressEventArgs(1));
                    }
                    else
                    {
                        OnRaiseProgressEvent(new ProgressEventArgs(2));
                    }
                    
                    foreach (var sp in packages)
                    {
                        GetPackages(sources.PackageSourcePath, directory, sp, isRecOnly);
                        OnRaiseProgressEvent(new ProgressEventArgs(1));
                    }

                    foreach (var xml in Directory.GetFiles(sources.PackageSourcePath, "*.xml"))
                    {
                        GetPackages(sources.PackageSourcePath, directory, xml);
                    }

                };
                worker.RunWorkerCompleted += (s, p) =>
                {
                    MessageBox.Show(Resources.ProgressBar_GetRecPackButton_Click_Done_);
                    OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
                };
                worker.RunWorkerAsync();
        }

        public void ExtractAllPackages(string unZipPath)
        {
            OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
            if (unZipPath.Length <= unZipPath.LastIndexOf("\\", StringComparison.Ordinal) + 1) return;
            var directory = unZipPath.Substring(unZipPath.LastIndexOf("\\", StringComparison.Ordinal) + 1).Contains("NDM")
                ? string.Format(unZipPath + @"\Packages")
                : unZipPath;

            var archives = Directory.GetFiles(directory, "*.zip");
            OnRaiseMaxProgressValueChangedEvent(new ChangeMaxProgressValueEventArgs(archives.Length));

            var isExtracted = false;
            if (archives.Length == 0)
            {
                MessageBox.Show(Resources.ProgressBar_ExtractButton_Click___zip_was_not_found);
            }
            var worker = new BackgroundWorker();
            worker.DoWork += (worker1, result) =>
            {
                var backgroundWorker = worker1 as BackgroundWorker;
                if (backgroundWorker != null) backgroundWorker.WorkerReportsProgress = true;

                foreach (var archive in archives)
                {
                    try
                    {
                        ExtractFilesFromZip(archive, directory);
                        OnRaiseProgressEvent(new ProgressEventArgs(1));
                        isExtracted = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Resources.Program_Main_Fail_to_extract + archive + ex.Message);
                    }
                }
                if (!isExtracted) return;
                {
                    DialogResult dialogResult = MessageBox.Show(Resources.ProgressBar_progressBar2_Click_Do_you_want_to_remove_zip_packages_,
                        Resources.ProgressBar_progressBar2_Click_Done_,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        foreach (string archive in archives)
                        {
                            RemoveZip(archive);
                        }
                    }
                }
            };
             
            worker.RunWorkerCompleted += (s, p) =>
            {
                MessageBox.Show(Resources.ProgressBar_GetRecPackButton_Click_Done_);
                OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
            };
            worker.RunWorkerAsync();
        }

        protected virtual void OnRaiseUpdateFormEvent(UpdateFormArgs e)
        {
            EventHandler<UpdateFormArgs> handler = UpdateFormEvent;

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
