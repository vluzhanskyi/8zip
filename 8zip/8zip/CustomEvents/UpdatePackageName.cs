using System;

namespace _8zip.CustomEvents
{
   public class UpdatePackageNameArgs : EventArgs
    {
        public string Name { get; set; }
        public string ActionName { get; set; }
        public string ItemNumber { set; get; }
        public string UnzipPath { set; get; }

        public UpdatePackageNameArgs(string item, string action, string newNamae, string unzipPath)
        {
            ItemNumber = item;
            ActionName = action;
            Name = newNamae;
            UnzipPath = unzipPath;
        }
    }
}
