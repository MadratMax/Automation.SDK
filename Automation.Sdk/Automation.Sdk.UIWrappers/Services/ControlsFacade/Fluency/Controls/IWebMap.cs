namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls
{
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Web;
    using OpenQA.Selenium;

    public interface IWebMap
    {
        IWebMap Add(string caption, By locator);

        IWebMap Add(ILocatorProvider locatorProvider);

        WebControlMap Find(string caption);

        ParentDefiner DefineChildren();

        string CurrentParent { get; set; }
    }
}