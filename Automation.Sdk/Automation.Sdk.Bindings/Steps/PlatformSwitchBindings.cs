namespace Automation.Sdk.Bindings.Steps
{
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Platform;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Steps to switch current platform.
    /// </summary>
    [Binding]
    public sealed class PlatformSwitchBindings
    {
        private readonly PlatformContextSwitcher _platformContextSwitcher;

        public PlatformSwitchBindings(PlatformContextSwitcher platformContextSwitcher)
        {
            _platformContextSwitcher = platformContextSwitcher;
        }

        [Given("user working in browser")]
        [When("user working in browser")]
        public void SwitchContextToWeb()
        {
            _platformContextSwitcher.SwitchPlatform(PlatformContextType.Web);
        }

        [Given("user working in desktop")]
        [When("user working in desktop")]
        public void SwitchContextToDesktop()
        {
            _platformContextSwitcher.SwitchPlatform(PlatformContextType.Desktop);
        }

        [Given("user working in mobile")]
        [When("user working in mobile")]
        public void SwitchContextToMobile()
        {
            _platformContextSwitcher.SwitchPlatform(PlatformContextType.Mobile);
        }
    }
}
