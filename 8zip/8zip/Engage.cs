
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
