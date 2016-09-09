using System;

namespace _8zip
{
    public class ProgressEventArgs : EventArgs
    {
        public int Progress { get; set; }
        public ProgressEventArgs(int progress)
        {
            Progress = progress;
        }
    }
}
