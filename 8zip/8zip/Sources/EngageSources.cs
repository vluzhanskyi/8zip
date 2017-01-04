using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _8zip.Sources
{

    public class EngageSources
    {
        private const double Epsilon = 0.01;

        public string[] PackageSourcePath { set; get; }
        public string SpSourcePath { set; get; }
        public string[] DEplymentSourcePath { set; get; }
        public List<string> Packages = new List<string>();
        public List<string> XmlList = new List<string>();

        public EngageSources(Engage engage)
        {
            PackageSourcePath = GetEngageSourcePath(engage);
            DEplymentSourcePath = GetDeplymentSourcePath(engage, PackageSourcePath);
            SpSourcePath = PackageSourcePath[2];
            GetPackagesToDownload();
        }

        private string[] GetEngageSourcePath(Engage engage)
        {
            var source = new string[7];
            if (Math.Abs(engage.Version - 6.5) < Epsilon)
            {
                source[0] = PacManconfigs.OfficialSource;
                source[1] = PacManconfigs.OfficialDeploymentSource;
                if (engage.SpVersion == 0)
                {
                    var servicePacks = Directory.GetDirectories(PacManconfigs.OfficialSpSource, "ServicePack*");
                    var sPs = servicePacks.Where(sp => Directory.GetFiles(string.Format(@"{0}\Engage", sp), "*.zip").Length != 0).ToList();
                    source[2] = string.Format(@"{0}\Engage", GetPreLastItem(servicePacks, 2));
                    if (Directory.GetFiles(source[2], "*.zip").Length == 0)
                        source[2] = string.Format(@"{0}\Engage", GetPreLastItem(sPs, 2, true));
                }
                else
                {
                    var servicePacks = Directory.GetDirectories(PacManconfigs.OfficialSpSource, string.Format("ServicePack{0}", engage.SpVersion));
                    source[2] = string.Format(@"{0}\Engage", servicePacks[0]);
                }
            }
            else if (Math.Abs(engage.Version - 6.6) < Epsilon && engage.Buildversion != null)
            {
                var sources = Directory.GetDirectories(PacManconfigs.DailyUPsFolder, string.Format("*{0}*", engage.Buildversion));
                var sharedCsources = Directory.GetDirectories(PacManconfigs.SharedCSource, string.Format("*{0}*", engage.SharedBuild));
                var splashCSources = Directory.GetDirectories(PacManconfigs.SlashCSource,
                    String.Format("*{0}*", engage.SplashBuild));
                var miniBusSources = Directory.GetDirectories(PacManconfigs.MiniBusSource,
                    string.Format("*{0}*", engage.MiniBusBuild));
                var ncaSources = Directory.GetDirectories(PacManconfigs.NcaBuildSource,
                    string.Format("*{0}*", engage.NcaBuildVersion));
                if (engage.Buildversion != String.Empty && sources.Length > 0)
                {
                    source[0] = string.Format(@"{0}\NPS_Deployment\Packages", sources[sources.Length - 1]);
                }
                if (engage.SharedBuild != String.Empty && sharedCsources.Length > 0)
                {
                    source[3] = string.Format(@"{0}\Packages", sharedCsources[sharedCsources.Length - 1]);
                }
                if (engage.SplashBuild != String.Empty && splashCSources.Length > 0)
                {
                    source[4] = string.Format(@"{0}\NPS_Deployment\Packages", splashCSources[splashCSources.Length - 1]);
                }
                if (engage.MiniBusBuild != String.Empty && miniBusSources.Length > 0)
                {
                    source[5] = string.Format(@"{0}\Packages", miniBusSources[miniBusSources.Length - 1]);
                }
                 if (engage.NcaBuildVersion != String.Empty && ncaSources.Length > 0)
                {
                    source[6] = string.Format(@"{0}\NPS_Deployment\Packages", ncaSources[ncaSources.Length - 1]);
                }
                source[1] = PacManconfigs.DailyDeplymentSource; 
            }
            return source;
        }

        private string[] GetDeplymentSourcePath(Engage engage, string[] sources)
        {
           var dPackages = new string[2];
           IEnumerable<string> deplymentSourcePath = null;
           if (Math.Abs(engage.Version - 6.5) < Epsilon)
            {
                dPackages = Directory.GetFiles(sources[1], "*.zip");                
            }
           if (Math.Abs(engage.Version - 6.6) < Epsilon)
            {
                if (engage.Buildversion != String.Empty)
                {
                    dPackages = Directory.GetFiles(PacManconfigs.DailyDeplymentSource, string.Format("*{0}.zip", engage.Buildversion));
                }
            }

            if (!dPackages.Contains(null))
            {
                deplymentSourcePath = dPackages.Where(s => s.Contains("NDM-") || s.Contains("SRT-"));
            }
            if (deplymentSourcePath != null)
            {
                return deplymentSourcePath.ToArray();
            }
            return null;
        }

        private string GetPreLastItem(IEnumerable<string> spStrings, int index, bool isLast = false)
        {
            var spDict = new Dictionary<int, string>();
            foreach (var s in spStrings)
            {
                int r;
                if (s.Contains(".zip"))
                {
                    var temp = s.Replace(s.Substring(s.LastIndexOf(".", StringComparison.Ordinal)), "");
                    int.TryParse(temp.Substring(temp.Length - index), out r);
                    spDict.Add(r, s);
                }
                else
                {
                    int.TryParse(s.Substring(s.Length - index), out r);
                    spDict.Add(r, s);
                }

            }

            var spList = spDict.Keys.ToList();
            spList.Sort();
            var spDictSorted = spList.ToDictionary(key => key, key => spDict[key]);
            if (!isLast)
            {
                return spDictSorted.Count >= 2
                    ? spDictSorted.Values.ToArray()[spDictSorted.Count - 2]
                    : spDictSorted.Values.ToArray()[0];
            }
            return spDictSorted.Count >= 2
                ? spDictSorted.Values.ToArray()[spDictSorted.Count - 1]
                : spDictSorted.Values.ToArray()[0];
        }

        private void GetPackagesToDownload()
        {
            var splashCSources = new List<string>();
            var sharedCsource = new List<string>();
            var miniBusSources = new List<string>();
            var ncaBusSources = new List<string>();
            var ncaCxmls = new List<string>();
            var sharedCxmls = new List<string>();
            var splashCxmls = new List<string>();
            var miniBusxmls = new List<string>();
            if (PackageSourcePath[6] != null)
            {
                ncaBusSources = Directory.GetFiles(PackageSourcePath[6], "*.zip",
                    SearchOption.AllDirectories).ToList();
                ncaCxmls = Directory.GetFiles(PackageSourcePath[6], "*.xml",
                    SearchOption.AllDirectories).ToList();
            }
            if (PackageSourcePath[0] != null)
            {
                Packages = Directory.GetFiles(PackageSourcePath[0], "*.zip",
                    SearchOption.AllDirectories).ToList();
                XmlList = Directory.GetFiles(PackageSourcePath[0], "*.xml",
                    SearchOption.AllDirectories).ToList();
            }
            if (PackageSourcePath[4] != null)
            {
                splashCSources = Directory.GetFiles(PackageSourcePath[4], "*.zip",
                    SearchOption.AllDirectories).ToList();
                splashCxmls = Directory.GetFiles(PackageSourcePath[4], "*.xml",
                    SearchOption.AllDirectories).ToList();

            }
            if (PackageSourcePath[3] != null)
            {
                sharedCsource = Directory.GetFiles(PackageSourcePath[3], "*.zip",
                    SearchOption.AllDirectories).ToList();
                sharedCxmls = Directory.GetFiles(PackageSourcePath[3], "*.xml",
                    SearchOption.AllDirectories).ToList();
            }
            if (PackageSourcePath[5] != null)
            {
                miniBusSources = Directory.GetFiles(PackageSourcePath[5], "*.zip",
                    SearchOption.AllDirectories).ToList();
                miniBusxmls = Directory.GetFiles(PackageSourcePath[5], "*.xml",
                    SearchOption.AllDirectories).ToList();
            }
            Packages.AddRange(sharedCsource);
            Packages.AddRange(splashCSources);
            Packages.AddRange(miniBusSources);
            Packages.AddRange(ncaBusSources);
            XmlList.AddRange(ncaCxmls);
            XmlList.AddRange(sharedCxmls);
            XmlList.AddRange(splashCxmls);
            XmlList.AddRange(miniBusxmls);
        }
    }
}
