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
        public void RunMethods(InputData inputs)
        {
            inputs.Archive.CreateEntryFromFile(inputs.SourcePath, inputs.EntryName, inputs.Compresion);
            inputs.Archive.CreateEntry(inputs.EntryName, inputs.Compresion);
            inputs.Archive.ExtractToDirectory(inputs.ExtractPath);
            
        }
        
        
    }
}
