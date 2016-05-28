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
        class InputData
        {
            string SourcePath { get; set; }
            string ZipDestinatonPath { get; set; }
            GZipStream ZipStream;
            ZipArchive Archive;
        }
    }
}
