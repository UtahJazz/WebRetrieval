using System.Configuration;

namespace Common
{
    public static class ConfigProvider
    {
        public static string GetStringValue(string fieldName)
        {
            return ConfigurationManager.AppSettings[fieldName];
        }
    }
}
