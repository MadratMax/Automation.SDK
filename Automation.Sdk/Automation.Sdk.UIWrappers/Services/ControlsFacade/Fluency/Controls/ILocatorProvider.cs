namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls
{
    using OpenQA.Selenium;

    public interface ILocatorProvider
    {
        int Priority { get; }

        bool IsMatch(string caption);

        By GetLocator(string caption);

        string GetParentCaption(string caption);
    }
}