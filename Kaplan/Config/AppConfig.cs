using System.Configuration;

namespace Kaplan.Config
{
    /// <summary>
    /// Strong typed app.config class
    /// Watch for null values
    /// </summary>
    public static class AppConfig
    {
        public static string SourcePath
        {
            get { return ConfigurationManager.AppSettings["SourcePath"]; }
        }

        public static string TargetPath
        {
            get { return ConfigurationManager.AppSettings["TargetPath"]; }
        }

        public static string DirectoryToDeleteFiles
        {
            get { return ConfigurationManager.AppSettings["DirectoryToDeleteFiles"]; }
        }

        public static string[] ServicesArray
        {
            get { return ConfigurationManager.AppSettings["Services"].Split(','); }
        }

        public static string ServicesMachine
        {
            get { return ConfigurationManager.AppSettings["ServicesMachine"]; }
        }
        public static string ZipPass
        {
            get { return ConfigurationManager.AppSettings["ZipPass"]; }
        }
        public static string[] ExtensionsToZip
        {
            get { return ConfigurationManager.AppSettings["ExtensionsToZip"].Split(','); }
        }
        public static int TimeOut
        {
            get { return int.Parse(ConfigurationManager.AppSettings["TimeOut"]); }
        }

    }
}
