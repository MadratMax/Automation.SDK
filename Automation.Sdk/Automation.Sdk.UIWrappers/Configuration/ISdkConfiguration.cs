namespace Automation.Sdk.UIWrappers.Configuration
{
    public interface ISdkConfiguration
    {
        /// <summary>
        /// Use Win10 mouse issues workarounds or not
        /// </summary>
        bool UseWin10MouseWorkaround { get; }

        /// <summary>
        /// Default timeout for <see cref="Services.Shouldly"/> class
        /// </summary>
        int DefaultWaitTimeout { get; }
    }
}