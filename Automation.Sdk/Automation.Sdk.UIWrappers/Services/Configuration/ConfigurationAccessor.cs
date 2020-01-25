namespace Automation.Sdk.UIWrappers.Services.Configuration
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Automation.Sdk.UIWrappers.Aspects;
    using JetBrains.Annotations;

    [AutoRegister]
    [UsedImplicitly]
    public sealed class ConfigurationAccessor : IConfigurationAccessor
    {
        private NameValueCollection _appSettings;
        private string _appSettingsPath;

        public ConfigurationAccessor()
        {
            _appSettings = ConfigurationManager.AppSettings;
            _appSettingsPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
        }

        public bool SetConfigurationFile(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            if (!File.Exists(path + ".config"))
            {
                return false;
            }

            var config = ConfigurationManager.OpenExeConfiguration(path);
            NameValueCollection newSettings = new NameValueCollection();

            foreach (var key in config.AppSettings.Settings.AllKeys)
            {
                var setting = config.AppSettings.Settings[key];
                newSettings.Add(setting.Key, setting.Value);
            }

            _appSettings = newSettings;
            _appSettingsPath = config.FilePath;

            return true;
        }

        public string Get(string key, string defaultValue)
        {
            return Get<string>(key, defaultValue);
        }

        public T Get<T>(string key, T defaultValue)
        {
            if (!_appSettings.AllKeys.Contains(key))
            {
                if (defaultValue == null)
                {
                    throw new ArgumentException($"Key {key} not found in {_appSettingsPath}");
                }

                return defaultValue;
            }

            var value = _appSettings.Get(key);
            var specialFolderType = typeof(Environment.SpecialFolder);
            var specifalFolderNames = Enum.GetNames(specialFolderType);
            var variables = Regex.Matches(value, @"\%(.*?)\%");

            foreach (Match variable in variables)
            {
                var variableName = variable.Groups[1].Value;
                if (Environment.GetEnvironmentVariables().Contains(variableName))
                {
                    value = value.Replace(variable.Value, Environment.GetEnvironmentVariable(variableName));
                }
                else
                {
                    if (specifalFolderNames.Contains(variableName))
                    {
                        value = value.Replace(variable.Value, Environment.GetFolderPath((Environment.SpecialFolder)Enum.Parse(specialFolderType, variableName)));
                    }
                }
            }

            var returnType = typeof(T);

            if (returnType.IsEnum)
            {
                return (T)Enum.Parse(returnType, value);
            }

            return (T)Convert.ChangeType(value, returnType);
        }

        public T Get<T>(string key)
        {
            return Get(key, default(T));
        }
    }
}
