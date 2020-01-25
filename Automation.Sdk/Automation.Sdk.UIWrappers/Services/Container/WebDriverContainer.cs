namespace Automation.Sdk.UIWrappers.Services.Container
{
    using Automation.Sdk.UIWrappers.Aspects;
    using OpenQA.Selenium;

    [AutoRegister]
    public class WebDriverContainer : IWebDriverContainer
    {
        private IWebDriver _driver;

        public void Dispose()
        {
        }

        public IWebDriver WebDriver => _driver;

        // This should be redefined in automation Suit for now. 
        // TODO: propagate all missing infrastructure to make it SDK-inner.
        public void AttachBrowser(IWebDriver driver)
        {
            _driver = driver;
        }
    }
}