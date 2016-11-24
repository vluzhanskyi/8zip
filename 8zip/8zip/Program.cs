using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using _8zip.Controller;
using _8zip.CustomEvents;
using EventHandler = _8zip.Controller.EventHandler;
using ProgressBar = _8zip.View.ProgressBar;

namespace _8zip
{
    internal class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// string deploymentPackages, string engagePackages, string EngageVersion, string buildVersion, string unzipPath
        [STAThread]
        private static void Main(string[] args)
        {
            string actionType = null;
            string source = null;
            string packageDestination = null;
            string unzipDestination = null;
            args = new[]
            {
                 @"-DU",
                 @"source = D:\GitFolder\NewRepo\8zip\8zip\8zip\bin\Debug",
                  @"Packagedestination = D:\Share_Slav\WatsonNew03112016",
                  @"Unzipdestination = D:\Share_Slav\WatsonNew03112016\Result"
            };
            if (args.Any())
            {
                foreach (var arg in args)
                {
                    try
                    {
                        if (arg.Contains("-"))
                        {
                            actionType = arg;
                        }
                        if (arg.IndexOf("source", StringComparison.InvariantCultureIgnoreCase) >= 0)
                        {
                            source = arg.Substring(arg.IndexOf("=", StringComparison.Ordinal) + 1).Replace(" ", "");
                            Console.WriteLine(source);
                        }
                        if (arg.IndexOf("Packagedestination", StringComparison.InvariantCultureIgnoreCase) >= 0)
                        {
                            packageDestination = arg.Substring(arg.IndexOf("=", StringComparison.Ordinal) + 1).Replace(" ", "");
                            Console.WriteLine(packageDestination);
                        }
                        if (arg.IndexOf("Unzipdestination", StringComparison.InvariantCultureIgnoreCase) >= 0)
                        {
                            unzipDestination = arg.Substring(arg.IndexOf("=", StringComparison.Ordinal) + 1).Replace(" ", "");
                            Console.WriteLine(unzipDestination);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                if (actionType != null && (actionType.Contains("-d") || actionType.Contains("-D")))
                {
                    try
                    {
                        Downloader downloader = new Downloader();
                        EventHandler.ProgressEvent += PrintProgress;
                        if (source != null)
                            foreach (var package in Directory.GetFiles(source, "*.zip"))
                            {
                                downloader.DownloadPackage(package, packageDestination + @"\" + Path.GetFileName(package));
                            }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }

                    if (actionType.Contains("u") || actionType.Contains("U"))
                    {
                        ZipWrapper zip = new ZipWrapper();
                        if (packageDestination != null)
                            foreach (var archive in Directory.GetFiles(packageDestination, "*.zip"))
                            {
                                zip.ExtractFilesFromZip(archive, unzipDestination);
                            }
                    }
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ProgressBar());      
            }
     
        }

        private static void PrintProgress(object sender, ProgressEventArgs e)
        {
                Console.Write(e.Progress);
        }
    }
}