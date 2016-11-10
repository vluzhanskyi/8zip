using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using _8zip.CustomEvents;
using _8zip.Sources;
using _8zip.View;

namespace _8zip.Controller
{
    public class Processor : ZipWrapper
    {
        private void GetPackages(string sourcePath, string targetPath, string fileName, bool isRecOnly = true, bool isCleanInstallation = false)
        {
            if (sourcePath == null || targetPath == null || fileName == null) return;
            string sourceFile = Path.Combine(sourcePath, fileName);
            string destFile = Path.Combine(targetPath, Path.GetFileName(fileName));
            Downloader downloader = new Downloader();
            if (!isCleanInstallation)
            {
                RecPackagesList.Remove("SQLAutoSetup2014_Enertprise");
                NotUsedPackagesList.Add("SQLAutoSetup2014_Enertprise");
            }
            if (isRecOnly)
            {
                foreach (var package in RecPackagesList)
                {
                    if (sourceFile.Contains(package))
                    {
                        downloader.DownloadPackage(sourceFile, destFile);
                    }
                }
            }
            else
            {
                var f = new FileInfo(sourceFile);

                var isApproved =
                    NotUsedPackagesList.All(package => !sourceFile.Contains(package) && f.Length >= MinimalFileSize);

                if (isApproved)
                {
                    downloader.DownloadPackage(sourceFile, destFile);
                }
            }
        }

        private string GetFolderToWork(string unZipPath, string subFolder)
        {
            var directory = !unZipPath.Contains("NDM")
                           ? unZipPath
                           : string.Format(@"{0}\{1}", unZipPath, subFolder);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return directory;
        }

        public void GetDeployment(Engage engage, string unZipPath, bool isCleanInstallation)
        {
            OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
            int i = 0;
            int j = 0;
            var worker = new BackgroundWorker {WorkerSupportsCancellation = true};
            worker.DoWork += (worker1, result) =>
            {
                OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(null, Actions.Initialization, string.Empty, unZipPath));
                var sources = new EngageSources(engage);
                if (sources.DEplymentSourcePath == null) return;
                var directory = Directory.GetCurrentDirectory();
                if (!isCleanInstallation)
                {
                    sources.DEplymentSourcePath = sources.DEplymentSourcePath.Where(e => !e.Contains("SRT")).ToArray();
                }
                j = sources.DEplymentSourcePath.Length;
                OnRaiseMaxProgressValueChangedEvent(new ChangeMaxProgressValueEventArgs(j*2));
                foreach (var archive in sources.DEplymentSourcePath)
                {
                    i++;
                    var archiveName = Path.GetFileNameWithoutExtension(archive);
                    var path = string.Format(directory + @"\" + archiveName);
                    if (path.Contains("NDM") || path.Contains("SRT"))
                    {
                        unZipPath = path;
                    }
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                        Actions.Downloading,
                        Path.GetFileNameWithoutExtension(archive), directory));
                    GetPackages(archive, directory, archive);
                    OnRaiseProgressEvent(new ProgressEventArgs(1, false));                  
                    var sourcePackage = string.Format(directory + @"\" + Path.GetFileName(archive));                  
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                        Actions.Extracting,
                        Path.GetFileNameWithoutExtension(archive), unZipPath));
                    ExtractFilesFromZip(sourcePackage, path);
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                        Actions.Removing,
                        Path.GetFileNameWithoutExtension(archive), unZipPath));
                    RemoveZip(sourcePackage);
                    OnRaiseProgressEvent(new ProgressEventArgs(1, false));
                }
            };
            worker.RunWorkerCompleted += (s, p) =>
            {
                OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                        string.Empty,
                                                                        Path.GetFileNameWithoutExtension(string.Empty), unZipPath));
                OnRiseExceptionEvent(p.Error != null
                    ? new ShowExceptionMessageArks(p.Error.Message)
                    : new ShowExceptionMessageArks("Done!"));
                OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
            };
            worker.RunWorkerAsync();
        }

        public void GetEngagePackages(Engage engage, string unZipPath, bool isRecOnly, bool isCleanInstallation)
        {
            OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
            var maxProgressValue = 1;
            int i = 0;
            int j = 0;
            var worker = new BackgroundWorker();
            worker.DoWork += (worker1, result) =>
            {
                OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Empty, Actions.Initialization, string.Empty, unZipPath));
                var sources = new EngageSources(engage);
                var xmls = sources.XmlList;
                var packages = sources.Packages;
                var directory = GetFolderToWork(unZipPath, "Packages");

                var backgroundWorker = worker1 as BackgroundWorker;
                if (backgroundWorker != null)
                {
                    backgroundWorker.WorkerReportsProgress = true;
                    backgroundWorker.WorkerSupportsCancellation = true;
                }

                maxProgressValue += packages.Count + xmls.Count;
                OnRaiseMaxProgressValueChangedEvent(new ChangeMaxProgressValueEventArgs(maxProgressValue));
                j = maxProgressValue - 1;
                if (engage.WithSp)
                {
                    j++;
                    i++;
                    var sps = Directory.GetFiles(sources.SpSourcePath, "*.zip");
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                            Actions.Downloading,
                                                                            Path.GetFileNameWithoutExtension(sps[0]), unZipPath));
                    var spDirectory = GetFolderToWork(unZipPath, "ServicePacks");
                    GetPackages(sources.SpSourcePath, spDirectory, sps[0], isRecOnly, isCleanInstallation);
                    OnRaiseProgressEvent(new ProgressEventArgs(1, false));
                }
                else
                {
                    OnRaiseProgressEvent(new ProgressEventArgs(2, false));
                }
                foreach (var xml in xmls)
                {
                    i++;
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                            Actions.Downloading,
                                                                            Path.GetFileNameWithoutExtension(xml), unZipPath));
                    GetPackages(xml, directory, xml);
                    OnRaiseProgressEvent(new ProgressEventArgs(1, false));
                }
                foreach (var package in packages)
                {
                    i++;
                    string resultedFilePath = String.Format(@"{0}\{1}", directory, Path.GetFileName(package));
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                            Actions.Downloading,
                                                                            Path.GetFileNameWithoutExtension(package), unZipPath));
                    GetPackages(package, directory, package, isRecOnly, isCleanInstallation);
                    if (File.Exists(resultedFilePath))
                    {
                        OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                            Actions.Extracting,
                                                                            Path.GetFileNameWithoutExtension(package), unZipPath));
                        ExtractFilesFromZip(package, directory);
                        OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                            Actions.Removing, Path.GetFileNameWithoutExtension(package),
                                                                            unZipPath));
                        RemoveZip(resultedFilePath);
                    }
                    OnRaiseProgressEvent(new ProgressEventArgs(1, false));
                }

            };
            worker.RunWorkerCompleted += (s, p) =>
            {
                if (p.Error == null)
                {
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i), null, null, unZipPath));
                    OnRiseExceptionEvent(new ShowExceptionMessageArks("Done!"));
                    OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
                }
                else
                {
                    OnRiseExceptionEvent(new ShowExceptionMessageArks(p.Error.Message));
                }
            };
            worker.RunWorkerAsync();
        }

        public void GetServicePack(Engage engage, string unZipPath)
        {
            OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
            var maxProgressValue = 1;
            int i = 0;
            int j = 0;
            var worker = new BackgroundWorker();
            worker.DoWork += (worker1, result) =>
            {
                OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Empty, Actions.Initialization,
                    string.Empty, unZipPath));
                var sources = new EngageSources(engage);
                var sps = Directory.GetFiles(sources.SpSourcePath, "*.zip");
                var backgroundWorker = worker1 as BackgroundWorker;
                if (backgroundWorker != null)
                {
                    backgroundWorker.WorkerReportsProgress = true;
                    backgroundWorker.WorkerSupportsCancellation = true;
                }

                maxProgressValue += sps.Length;
                OnRaiseMaxProgressValueChangedEvent(new ChangeMaxProgressValueEventArgs(maxProgressValue));
                j = maxProgressValue - 1;
                if (engage.WithSp)
                {
                    j++;
                    i++;
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                        Actions.Downloading,
                        Path.GetFileNameWithoutExtension(sps[0]), unZipPath));
                    var spDirectory = GetFolderToWork(unZipPath, "ServicePacks");
                    GetPackages(sources.SpSourcePath, spDirectory, sps[0]);
                    OnRaiseProgressEvent(new ProgressEventArgs(1, false));
                }

            };
            worker.RunWorkerCompleted += (s, p) =>
            {
                if (p.Error == null)
                {
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i), null, null,
                        unZipPath));
                    OnRiseExceptionEvent(new ShowExceptionMessageArks("Done!"));
                    OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
                }
                else
                {
                    OnRiseExceptionEvent(new ShowExceptionMessageArks(p.Error.Message));
                }
            };
            worker.RunWorkerAsync();
        }
    }
}
