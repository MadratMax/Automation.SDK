namespace Automation.Sdk.UIWrappers.Services.Container
{
    using System;
    using OpenQA.Selenium;

    public interface IWebDriverContainer : IDisposable
    {
        IWebDriver WebDriver { get; }

        void AttachBrowser(IWebDriver driver);
    }
}