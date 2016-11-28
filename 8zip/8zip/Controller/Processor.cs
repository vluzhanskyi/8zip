using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using _8zip.CustomEvents;
using _8zip.Sources;
using _8zip.View;

namespace _8zip.Controller
{
    public class Processor : EventHandler
    {
        protected static List<string> RecPackagesList;
        protected static List<string> NotUsedPackagesList;
        internal const long MinimalFileSize = 1000;
        private readonly ZipWrapper _unzipper;

        public Processor()

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

            _unzipper = new ZipWrapper();
        }

        public void GetDeployment(Engage engage, string unZipPath, bool isCleanInstallation)
        {
            
            OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
            int i = 0;
            int j = 0;
            var worker = new BackgroundWorker { WorkerSupportsCancellation = true };
            worker.DoWork += (worker1, result) =>
            {
                OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(null, Actions.Initialization, String.Empty, unZipPath));
                var sources = new EngageSources(engage);
                if (sources.DEplymentSourcePath == null) return;
                var directory = Directory.GetCurrentDirectory();
                if (!isCleanInstallation)
                {
                    sources.DEplymentSourcePath = sources.DEplymentSourcePath.Where(e => !e.Contains("SRT")).ToArray();
                }
                j = sources.DEplymentSourcePath.Length;
                OnRaiseMaxProgressValueChangedEvent(new ChangeMaxProgressValueEventArgs(j * 2));
                foreach (var archive in sources.DEplymentSourcePath)
                {
                    i++;
                    var archiveName = Path.GetFileNameWithoutExtension(archive);
                    var sourcePackage = string.Format(directory + @"\" + Path.GetFileName(archive));
                    var path = String.Format(directory + @"\" + archiveName);
                    if (path.Contains("NDM") || path.Contains("SRT"))
                    {
                        unZipPath = path;
                    }
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", i, j),
                    Actions.Downloading,
                    Path.GetFileNameWithoutExtension(archive), unZipPath));

                    GetPackages(archive, directory, archive);
                    OnProgressEvent(new object(), new ProgressEventArgs(1, false));

                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", i, j),
                            Actions.Extracting,
                            Path.GetFileNameWithoutExtension(archive), unZipPath));

                    _unzipper.ExtractFilesFromZip(sourcePackage, path);

                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", i, j),
                        Actions.Removing,
                        Path.GetFileNameWithoutExtension(archive), unZipPath));

                    _unzipper.RemoveZip(sourcePackage);
                    OnProgressEvent(new object(), new ProgressEventArgs(1, false));
                }
            };
            worker.RunWorkerCompleted += (s, p) =>
            {
                OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                        String.Empty,
                                                                        Path.GetFileNameWithoutExtension(String.Empty), unZipPath));
                OnRiseExceptionEvent(null, p.Error != null
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
                OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Empty, Actions.Initialization, String.Empty, unZipPath));
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
                    OnProgressEvent(new object(), new ProgressEventArgs(1, false));
                }
                else
                {
                    OnProgressEvent(new object(), new ProgressEventArgs(2, false));
                }
                foreach (var xml in xmls)
                {
                    i++;
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                            Actions.Downloading,
                                                                            Path.GetFileNameWithoutExtension(xml), unZipPath));
                    GetPackages(xml, directory, xml);
                    OnProgressEvent(new object(), new ProgressEventArgs(1, false));
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
                        _unzipper.ExtractFilesFromZip(package, directory);
                        OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i),
                                                                            Actions.Removing, Path.GetFileNameWithoutExtension(package),
                                                                            unZipPath));
                        _unzipper.RemoveZip(resultedFilePath);
                    }
                    OnProgressEvent(new object(), new ProgressEventArgs(1, false));
                }

            };
            worker.RunWorkerCompleted += (s, p) =>
            {
                if (p.Error == null)
                {
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i), null, null, unZipPath));
                    OnRiseExceptionEvent(null, new ShowExceptionMessageArks("Done!"));
                    OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
                }
                else
                {
                    OnRiseExceptionEvent(null, new ShowExceptionMessageArks(p.Error.Message));
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
                    OnProgressEvent(new object(), new ProgressEventArgs(1, false));
                }

            };
            worker.RunWorkerCompleted += (s, p) =>
            {
                if (p.Error == null)
                {
                    OnRiseChangePackageNameEvent(new UpdatePackageNameArgs(String.Format("{0}/{1}", j, i), null, null,
                        unZipPath));
                    OnRiseExceptionEvent(null, new ShowExceptionMessageArks("Done!"));
                    OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
                }
                else
                {
                    OnRiseExceptionEvent(null, new ShowExceptionMessageArks(p.Error.Message));
                }
            };
            worker.RunWorkerAsync();
        }

        public void UnzipPackages(string unzipPath)
        {
            _unzipper.ExtractAllPackages(unzipPath);
        }

        private void GetPackages(string sourcePath, string targetPath, string fileName, bool isRecOnly = true, bool isCleanInstallation = false)
        {
            if (sourcePath == null || targetPath == null || fileName == null) return;
            string sourceFile = Path.Combine(sourcePath, fileName);
            string destFile = Path.Combine(targetPath, Path.GetFileName(fileName));
            Downloader downloader = new Downloader();
            if (!isCleanInstallation)
            {
                RecPackagesList.Remove("SQLAutoSetup2014_Enertprise");
                NotUsedPackagesList.Add("SQLAutoSetup");
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

        private static string GetFolderToWork(string unZipPath, string subFolder)
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
    }
}
