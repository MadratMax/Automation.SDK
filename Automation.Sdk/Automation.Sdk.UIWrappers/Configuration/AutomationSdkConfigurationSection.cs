namespace Automation.Sdk.UIWrappers.Configuration
{
    using System.Configuration;
    using JetBrains.Annotations;

    /// <summary>
    /// Automation SDK configuration section wrapper. 
    /// Supports the following configuration section structure:
    /// <sdk>
    /// </sdk>
    /// Consider https://habrahabr.ru/post/128517/ for additional input.
    /// </summary>
    [UsedImplicitly]
    public sealed class AutomationSdkConfigurationSection : ConfigurationSection
    {
        private static readonly AutomationSdkConfigurationSection SdkSettings =
            ConfigurationManager.GetSection("sdk") as AutomationSdkConfigurationSection;

        /// <summary>
        /// Singleton-way static accessor to loaded configuration section data
        /// </summary>
        public static AutomationSdkConfigurationSection Settings => SdkSettings;
    }
}
