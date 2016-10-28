using System;

namespace _8zip.CustomEvents
{
    public class ProgressEventArgs : EventArgs
    {
        public int Progress { get; set; }
        public bool IsCurrent { get; set; }
        public ProgressEventArgs(int progress, bool isCurrent)
        {
            IsCurrent = isCurrent;
            Progress = progress;
        }
    }
}
