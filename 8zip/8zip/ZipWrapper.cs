using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace _8zip
{
    class ZipWrapper
    {
       // public ZipArchive Archive = new ZipArchive();
        
        public void RunMethods(InputData inputs)
        {
            List<int> lstData = new List<int>() {1, 2, 3, 4, 5};
            lstData.ForEach(item => item++);

           // ZipFile.CreateFromDirectory(inputs.);

            /* inputs.Archive.CreateEntryFromFile(inputs.SourcePath, inputs.EntryName, inputs.Compresion);
            inputs.Archive.CreateEntry(inputs.EntryName, inputs.Compresion);
            inputs.Archive.ExtractToDirectory(inputs.ExtractPath);
            */
        }
        
        
    }
}
