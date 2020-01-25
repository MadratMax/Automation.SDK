namespace Automation.Sdk.UIWrappers.Configuration
{
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.Services.Configuration;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister]
    public class SdkConfiguration : ISdkConfiguration
    {
        public SdkConfiguration(IConfigurationAccessor configurationAccessor)
        {
            UseWin10MouseWorkaround = configurationAccessor.Get("UseWin10MouseWorkaround", false);
            DefaultWaitTimeout = configurationAccessor.Get("DefaultWaitTimeout", 5000);
        }

        /// <inheritdoc />
        public bool UseWin10MouseWorkaround { get; }

        /// <inheritdoc />
        public int DefaultWaitTimeout { get; }
    }
}