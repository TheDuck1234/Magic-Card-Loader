using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGLoadingPicFromWebsite.Core
{
    public static class AppSettings
    {
        public static string GetAppSettingXmlPath()
        {
            return ConfigurationSettings.AppSettings["XmlPath"];
        }
        public static string GetAppSettingFile()
        {
            return ConfigurationSettings.AppSettings["FileName"];
        }
        public static string GetAppSettingImagePath()
        {
            return ConfigurationSettings.AppSettings["ImagePath"];
        }
        public static string GetAppSettingCardSite()
        {
            return ConfigurationSettings.AppSettings["CardSite"];
        }
    }
}
