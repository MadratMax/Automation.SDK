namespace Automation.Sdk.UIWrappers.Services.Configuration
{
    public interface IConfigurationAccessor
    {
        T Get<T>(string key);

        T Get<T>(string key, T defaultValue);

        bool SetConfigurationFile(string path);
    }
}