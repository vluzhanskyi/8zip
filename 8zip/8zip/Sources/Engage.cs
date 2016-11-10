
namespace _8zip.Sources
{
    public class Engage
    {
        public double Version { set; get; }
        public string Buildversion { set; get; }
        public string SplashBuild { set; get; }
        public string SharedBuild { set; get; }
        public string MiniBusBuild { set; get; }
        public string NcaBuildVersion { set; get; }
        public bool WithSp { set; get; }
        public int SpVersion { set; get;}

        public Engage(double version, string buildVersion, string splashB, string sharedC, string minibus, string ncaVersion, bool withSp)
        {
            Version = version;
            Buildversion = buildVersion;
            SplashBuild = splashB;
            SharedBuild = sharedC;
            MiniBusBuild = minibus;
            NcaBuildVersion = ncaVersion;
            WithSp = withSp;
        }
    }

    
}
