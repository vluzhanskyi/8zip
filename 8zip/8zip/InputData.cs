using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows.Forms;

namespace _8zip
{
    class InputData
    {
        public string SourcePath { get; set; }
        public string ZipPath { get; set; }
        public string EntryName { get; set; }
        public string ExtractPath { get; set; }
        public ZipArchive Archive;
        public CompressionLevel Compresion { get; set; }

    }
}
