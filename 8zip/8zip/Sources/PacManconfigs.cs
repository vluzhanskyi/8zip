using System.Configuration;

namespace _8zip.Sources
{
    public static class PacManconfigs
    {
        public static string OfficialSource = ConfigurationManager.AppSettings.Get("OfficialSource");
        public static string OfficialSpSource = ConfigurationManager.AppSettings.Get("OfficialSpSource");
        public static string OfficialDeploymentSource = ConfigurationManager.AppSettings.Get("OfficialDeploymentSource");
        public static string DailyUPsFolder = ConfigurationManager.AppSettings.Get("DailyUPsFolder");
        public static string DailyDeplymentSource = ConfigurationManager.AppSettings.Get("DailyDeplymentSource");
        public static string SharedCSource = ConfigurationManager.AppSettings.Get("SharedCSource");
        public static string SlashCSource = ConfigurationManager.AppSettings.Get("SlashCSource");
        public static string MiniBusSource = ConfigurationManager.AppSettings.Get("MiniBusSource");
    }
}
