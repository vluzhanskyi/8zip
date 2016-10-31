using System;
using System.Net;
using System.Windows.Forms;
using _8zip.View;
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
            //var cred = WindowsIdentity.GetCurrent();

            NetworkCredential credential = new NetworkCredential();
            //bool haveAccess = false;
            //DirectoryInfo dir = new DirectoryInfo(@"\\172.28.253.21\build\");
            //try
            //{
            //    var acl = dir.GetAccessControl();
            //    haveAccess = true;
            //}
            //catch (UnauthorizedAccessException uae)
            //{
            //    if (uae.Message.Contains("read-only"))
            //    {
            //        haveAccess = true;
            //    }
            //    else
            //    {
            //        haveAccess = false;
            //    }
            //}

            //if (!haveAccess)
            //{
                
            //}
            credential.UserName = "viacheslavl";
            credential.Domain = "nice.com";
            credential.Password = "1s4l1a0v5%";
            Prompt.PerformSomeActionAsAdmin(credential);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProgressBar());           
        }
       
    }
}