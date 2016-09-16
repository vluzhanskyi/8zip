using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _8zip
{
    public class Sources
    {
        private const string Official65Source = @"\\172.28.253.21\build\Drop\Engage6.5\Official\Server_side";
        private const string Official65SpSource = @"\\172.28.253.21\build\Drop\Engage6.5\Official";
        private const string Official65DeploymentSource = @"\\172.28.253.21\build\Drop\Deployment_Products\Official\Engage_6.5";
        private const string DailyUPsFolder = @"\\172.28.253.21\build\AutoDeployment\MirageB";
        private const string DailyDeplymentSource = @"\\172.28.253.21\build\Drop\Deployment_Products\Under_RND_testing\Tools\";
        private const double Epsilon = 0.01;

        public string PackageSourcePath { set; get; }
        public string SpSourcePath { set; get; }
        public string[] DEplymentSourcePath { set; get; }

        public Sources(Engage engage)
        {
            PackageSourcePath = GetEngageSourcePath(engage)[0];
            DEplymentSourcePath = GetDeplymentSourcePath(engage);
            SpSourcePath = GetEngageSourcePath(engage)[2];
        }

        private string[] GetEngageSourcePath(Engage engage)
        {
            var source = new string[3];
            if (Math.Abs(engage.Version - 6.5) < Epsilon)
            {
                source[0] = Official65Source;
                source[1] = Official65DeploymentSource;
                if (engage.SpVersion == 0)
                {
                    var servicePacks = Directory.GetDirectories(Official65SpSource, "ServicePack*");
                    source[2] = string.Format(@"{0}\Engage", GetPreLastItem(servicePacks, 2));
                }
                else
                {
                    var servicePacks = Directory.GetDirectories(Official65SpSource, string.Format("ServicePack{0}", engage.SpVersion));
                    source[2] = string.Format(@"{0}\Engage", servicePacks[0]);
                }

            }
            else if (Math.Abs(engage.Version - 6.6) < Epsilon && engage.Buildversion != null)
            {
                var sources = Directory.GetDirectories(DailyUPsFolder, string.Format("Daily_{0}*", engage.Buildversion));
                if (sources.Length > 0)
                {
                    source[0] = string.Format(@"{0}\NPS_Deployment\Packages", sources[sources.Length - 1]);
                }
                    source[1] = DailyDeplymentSource; 
            }
            return source;
        }

        public string[] GetDeplymentSourcePath(Engage engage)
        {
           var sources = GetEngageSourcePath(engage);
           var dPackages = new string[2];
           if (Math.Abs(engage.Version - 6.5) < Epsilon)
            {
                dPackages = Directory.GetFiles(sources[1], "*.zip");                
            }
           if (Math.Abs(engage.Version - 6.6) < Epsilon)
            {
                dPackages = Directory.GetFiles(DailyDeplymentSource, string.Format("*{0}.zip", engage.Buildversion));                
            }

            var deplymentSourcePath = dPackages.Where(s => s.Contains("NDM-") || s.Contains("SRT-"));
            return deplymentSourcePath.ToArray();
        }

        private string GetPreLastItem(IEnumerable<string> spStrings, int index)
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
            return spDictSorted.Count >= 2
                ? spDictSorted.Values.ToArray()[spDictSorted.Count - 2]
                : spDictSorted.Values.ToArray()[0];


        }
    }
}
