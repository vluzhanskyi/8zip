﻿using System;
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

        private Exception AddFilesToZip(string[] source, string zipPath, CompressionMethod compressLevel)
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

        private void ExtractFilesFromZip(string zipPath, string pathToExtract)
        {
            try
            {
                using (var zip = ZipFile.Read(zipPath))
                {
                   zip.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                    if (zip.Name.Contains("SQLAutoSetup"))
                    {
                        var selection = (from cf in zip.Entries
                                         where (cf.FileName).StartsWith("SQLAutoSetup/")
                                         select cf);

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

        private string GetPreLastItem(string[] spStrings, int index)
        {
            var spDict = new Dictionary<int, string>();
            var spDictSorted = new Dictionary<int, string>();
            foreach (var s in spStrings)
            {
                int r;
                if (s.Contains(".zip"))
                {
                    string temp = s.Replace(s.Substring(s.LastIndexOf(".", StringComparison.Ordinal)), "");
                    Int32.TryParse(temp.Substring(temp.Length - index), out r);
                    spDict.Add(r, s);              
                }
                else
                {
                    Int32.TryParse(s.Substring(s.Length - index), out r);
                    spDict.Add(r, s);
                }
                    
            }

            var spList = spDict.Keys.ToList();
            spList.Sort();
            foreach (var key in spList)
            {
                spDictSorted.Add(key, spDict[key]);
            }
            return spDictSorted.Count >= 2 
                ? spDictSorted.Values.ToArray()[spDictSorted.Count - 2] 
                : spDictSorted.Values.ToArray()[0];

            
        }

        private string[] GetSourcePath(double engageVersion, string build)
        {
            string[] source = new string[3];
            if (engageVersion == 6.5)
            {
                source[0] = @"\\172.28.253.21\build\Drop\Engage6.5\Official\Server_side";
                source[1] = @"\\172.28.253.21\build\Drop\Deployment_Products\Official\Engage_6.5";
                string spSource = @"\\172.28.253.21\build\Drop\Engage6.5\Official";
                var servicePacks = Directory.GetDirectories(spSource, "ServicePack*");
                source[2] = string.Format(@"{0}\Engage", GetPreLastItem(servicePacks, 2));
            }
            else if (engageVersion == 6.6 && build != null)
            {
                string[] s = Directory.GetDirectories(@"\\172.28.253.21\build\AutoDeployment\MirageB", string.Format("Daily_{0}*", build));
                 if (s.Length > 0)
                 {
                     source[0] = string.Format(@"{0}\NPS_Deployment\Packages", s[s.Length - 1]);
                 }
                 string[] ndms = Directory.GetFiles(@"\\172.28.253.21\build\Drop\Deployment_Products\Under_RND_testing\Tools\", string.Format("NDM-{0}.zip", build));
                 string[] srts = Directory.GetFiles(@"\\172.28.253.21\build\Drop\Deployment_Products\Under_RND_testing\Tools\", string.Format("SRT-{0}.zip", build));
                 if (ndms.Length > 0 && srts.Length > 0)
                 {
                     source[1] = GetPreLastItem(ndms, 3);
                     source[2] = GetPreLastItem(srts, 3);
                 }
            }
            return source;
        }

        private void GetPackages(string sourcePath, string targetPath, string fileName, bool isRecOnly = true)
        {
            string sourceFile = Path.Combine(sourcePath, fileName);
            string destFile = Path.Combine(targetPath, Path.GetFileName(fileName));
            const long minimalFileSize = 1000;

            if (isRecOnly)
            {
                if (sourceFile.Contains("AIR") || sourceFile.Contains("Applications") || sourceFile.Contains("CTI")
                || sourceFile.Contains("Data Mart") || sourceFile.Contains("Database") || sourceFile.Contains("Interactions Center")
                || sourceFile.Contains("Logger") || sourceFile.Contains("Storage") || sourceFile.Contains("KAI")
                || sourceFile.Contains("SQLAutoSetup2014_Enertprise") || sourceFile.Contains("Reporter") ||
            sourceFile.Contains("xml") || sourceFile.Contains("NDM") || sourceFile.Contains("SRT") || Path.GetFileName(fileName).StartsWith("SP"))
                    File.Copy(sourceFile, destFile, true);
            }
            else
            {
                FileInfo f = new FileInfo(sourceFile);
                if (f.Length > minimalFileSize && !sourceFile.Contains("Thai") && !sourceFile.Contains("Spanish") && !sourceFile.Contains("Russian")
                    && !sourceFile.Contains("Swedish") && !sourceFile.Contains("Cantonese") && !sourceFile.Contains("English")
                    && !sourceFile.Contains("French") && !sourceFile.Contains("German") && !sourceFile.Contains("Italian")
                    && !sourceFile.Contains("Japanese") && !sourceFile.Contains("Korean") && !sourceFile.Contains("Polish")
                    && !sourceFile.Contains("Portuguese") && !sourceFile.Contains("Turkish") && !sourceFile.Contains("Hindi")
                    && !sourceFile.Contains("Hebrew") && !sourceFile.Contains("Biometrics"))
                    File.Copy(sourceFile, destFile, true);
            }
        }

        private string GetFolderToWork(string unZipPath, string subFolder)
        {
            var directory = !unZipPath.Contains("NDM")
                           ? unZipPath
                           : string.Format(@"{0}\{1}", unZipPath, subFolder);
            return directory;
        }

        //public void GetSP(string _unZipPath)
        //{
        //    OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
        //    int maxProgressValue = 0;
        //     BackgroundWorker worker = new BackgroundWorker();
        //        worker.DoWork += (worker1, result) =>
        //        {
        //            var backgroundWorker = worker1 as BackgroundWorker;
        //            if (backgroundWorker != null)
        //                backgroundWorker.WorkerReportsProgress = true;
                    
        //            var source = GetSourcePath(6.5, null)[2];
        //            var packages = Directory.GetFiles(source, "*.zip");
        //            maxProgressValue += packages.Length;
        //            var spDirectory = GetFolderToWork(_unZipPath, "ServicePacks");
        //            OnRaiseMaxProgressValueChangedEvent(new ChangeMaxProgressValueEventArgs(maxProgressValue));
        //            foreach (var archive in packages)
        //            {
        //                GetPackages(source, spDirectory, archive);
        //                OnRaiseProgressEvent(new ProgressEventArgs(1));
        //            }
        //        };
        //        worker.RunWorkerCompleted += (s, p) =>
        //        {
        //            MessageBox.Show(Resources.ProgressBar_GetRecPackButton_Click_Done_);
        //            OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
        //        };
        //        worker.RunWorkerAsync();
        //}

        public string GetDeployment(double engageVersion, string buildVersion)
        {
            OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
            string unZipPath = null;
            string path = null;
            var directory = Directory.GetCurrentDirectory();
            var sources = GetSourcePath(engageVersion, buildVersion);         
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (worker1, result) =>
            {
                if (engageVersion == 6.5)
                {
                    var dPackages = Directory.GetFiles(sources[1], "*.zip");
                    foreach (var archive in dPackages.Where(s => s.Contains("NDM") || s.Contains("SRT")))
                    {
                        GetPackages(sources[1], directory, archive);
                        var dDestination = Directory.GetFiles(directory, "*.zip");
                        OnRaiseProgressEvent(new ProgressEventArgs(1));
                        var archiveName = Path.GetFileNameWithoutExtension(archive);
                        path = string.Format(directory + @"\" + archiveName);
                        if (path.Contains("NDM"))
                        {
                            unZipPath = path;
                        }

                        ExtractFilesFromZip(dDestination[0], path);
                        RemoveZip(dDestination[0]);
                        OnRaiseProgressEvent(new ProgressEventArgs(1));
                    }

                }
                else if (engageVersion == 6.6)
                {
                    
                    foreach (var source in sources.Where(s => s != null))
                    {
                        GetPackages(source, directory, source);
                        OnRaiseProgressEvent(new ProgressEventArgs(1));
                        var archiveName = Path.GetFileNameWithoutExtension(source);
                        var dDestination = string.Format(@"{0}\{1}.zip", directory, archiveName);
                        path = string.Format(directory + @"\" + archiveName);
                        ExtractFilesFromZip(dDestination, path);
                        RemoveZip(dDestination);
                        OnRaiseProgressEvent(new ProgressEventArgs(1));
                    }

                    if (path != null && path.Contains("NDM"))
                    {
                        unZipPath = path;
                    }
                    
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

        public void GetEngagePackages(double engageVersion, bool withSp, string buildVersion, string unZipPath)
        {
            OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
            int maxProgressValue = 1;

            if (engageVersion == 6.5 || withSp)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (worker1, result) =>
                {
                    var backgroundWorker = worker1 as BackgroundWorker;
                    if (backgroundWorker != null)
                        backgroundWorker.WorkerReportsProgress = true;

                    var directory = GetFolderToWork(unZipPath, "Packages");
                    var sources = GetSourcePath(6.5, null);
                    string source = sources[0];
                    var packages = Directory.GetFiles(source, "*.zip");
                    maxProgressValue += packages.Length;
                    OnRaiseMaxProgressValueChangedEvent(new ChangeMaxProgressValueEventArgs(maxProgressValue));

                    if (withSp)
                    {
                        var spDirectory = GetFolderToWork(unZipPath, "ServicePacks");
                        var sps = Directory.GetFiles(sources[2], "*.zip");
                        GetPackages(sources[2], spDirectory, sps[0]);
                        OnRaiseProgressEvent(new ProgressEventArgs(1));
                    }
                    else
                    {
                        OnRaiseProgressEvent(new ProgressEventArgs(2));
                    }
                    
                    foreach (var sp in packages)
                    {
                        GetPackages(source, directory, sp);
                        OnRaiseProgressEvent(new ProgressEventArgs(1));
                    }
                    foreach (var xml in Directory.GetFiles(source, "*.xml"))
                    {
                        GetPackages(source, directory, xml);
                    }

                };
                worker.RunWorkerCompleted += (s, p) =>
                {
                    MessageBox.Show(Resources.ProgressBar_GetRecPackButton_Click_Done_);
                    OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
                };
                worker.RunWorkerAsync();
            }
            if (engageVersion == 6.6)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (worker1, result) =>
                {
                    string source = GetSourcePath(6.6, buildVersion)[0];
                    var packages = Directory.GetFiles(string.Format(source), "*.zip",
                        SearchOption.AllDirectories);
                    OnRaiseMaxProgressValueChangedEvent(new ChangeMaxProgressValueEventArgs(packages.Length));
                    var directory = GetFolderToWork(unZipPath, "Packages");
                    foreach (var item in packages)
                    {
                        GetPackages(source, directory, item, false);
                        OnRaiseProgressEvent(new ProgressEventArgs(1));
                    }
                    foreach (var xml in Directory.GetFiles(source, "*.xml"))
                    {
                        GetPackages(source, directory, xml);
                    }
                };
                worker.RunWorkerCompleted += (s, p) =>
                {
                    MessageBox.Show(Resources.ProgressBar_GetRecPackButton_Click_Done_);
                    OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
                };
                worker.RunWorkerAsync();
            }          
        }

        public void ExtractAllPackages(string unZipPath)
        {
            OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
            if (unZipPath.Length > unZipPath.LastIndexOf("\\", StringComparison.Ordinal) + 1)
            {
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
                BackgroundWorker worker = new BackgroundWorker();
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
                    if (isExtracted)
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
                        OnRaiseUpdateFormEvent(new UpdateFormArgs(true));
                    }

                };
             
                worker.RunWorkerCompleted += (s, p) =>
                {
                    MessageBox.Show(Resources.ProgressBar_GetRecPackButton_Click_Done_);
                    OnRaiseUpdateFormEvent(new UpdateFormArgs(false));
                };
                worker.RunWorkerAsync();
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
