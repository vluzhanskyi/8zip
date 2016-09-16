using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8zip
{
    public class Engage
    {
        public double Version { set; get; }
        public string Buildversion { set; get; }
        public bool WithSp { set; get; }
        public int SpVersion { set; get;}

        public Engage(double version, string buildVersion, bool withSp)
        {
            Version = version;
            Buildversion = buildVersion;
            WithSp = withSp;
        }
    }

    
}
