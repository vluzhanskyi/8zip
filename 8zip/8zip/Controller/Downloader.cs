using System;
using System.IO;
using _8zip.CustomEvents;

namespace _8zip.Controller
{
    public class Downloader : EventHandler
    {
        public void DownloadPackage(string sourceFile, string destFile)
        {
            int buflen = 1024;
            byte[] buf = new byte[buflen];
            long totalBytesRead = 0;
            int numReads = 0;
            FileInfo srcFile = new FileInfo(sourceFile);
            long fileLen = srcFile.Length;
            FileInfo dstFile = new FileInfo(destFile);
            if (dstFile.Exists)
            {
                dstFile.Delete();
            }
            try
            {
                using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream destStream = new FileStream(destFile, FileMode.CreateNew, FileAccess.ReadWrite))
                    {
                        while (true)
                        {
                            numReads++;
                            int bytesRead = sourceStream.Read(buf, 0, buflen);
                            if (bytesRead == 0) break;
                            destStream.Write(buf, 0, bytesRead);

                            totalBytesRead += bytesRead;
                            if (numReads % 10 == 0)
                            {
                                var pctDone = totalBytesRead / (double)fileLen;
                                var pctDoneRes = (int)(pctDone * 100);

                                OnProgressEvent(null, new ProgressEventArgs(pctDoneRes, true));
                            }

                            if (bytesRead < buflen) break;
                        }
                        destStream.Close();
                    }
                    sourceStream.Close();
                }
                File.Copy(sourceFile, destFile, true);

            }
            catch (Exception e)
            {
                OnRiseExceptionEvent(null, new ShowExceptionMessageArks(
                    string.Format("Failed to copy {0} to {1} \n Exception: {2}", sourceFile,
                    destFile, e.Message)));
            }
        }

    }
}
