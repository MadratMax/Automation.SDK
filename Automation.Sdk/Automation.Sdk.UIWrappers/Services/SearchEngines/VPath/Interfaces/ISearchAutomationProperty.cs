namespace Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Interfaces
{
    using System.Windows.Automation;

    internal interface ISearchAutomationProperty : ISearchProperty
    {
        AutomationProperty AutomationProperty { get; }

        object FromString(string value);
    }
}