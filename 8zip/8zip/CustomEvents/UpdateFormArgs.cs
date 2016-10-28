using System;

namespace _8zip.CustomEvents
{
    public class UpdateFormArgs : EventArgs
    {
        public bool IsOpen { get; set; }
        public UpdateFormArgs(bool state)
        {
            IsOpen = state;
        }
    }
}
