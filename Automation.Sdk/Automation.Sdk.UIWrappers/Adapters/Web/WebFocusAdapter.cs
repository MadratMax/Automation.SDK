namespace Automation.Sdk.UIWrappers.Adapters.Web
{
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;
    using OpenQA.Selenium.Interactions;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IFocusAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Web)]
    internal class WebFocusAdapter : WebAdapterBase, IFocusAdapter
    {
        private readonly IWebDriverContainer _webDriverContainer;

        public WebFocusAdapter(
            IElement element,
            ControlAdapterFactory controlAdapterFactory,
            IWebDriverContainer webDriverContainer)
            : base(element, controlAdapterFactory)
        {
            _webDriverContainer = webDriverContainer;
        }

        public bool IsFocused => WebElement.Equals(_webDriverContainer.WebDriver.SwitchTo().ActiveElement());

        public void SetFocus()
        {
            var driver = _webDriverContainer.WebDriver;

            new Actions(driver).MoveToElement(WebElement);
        }
    }
}
