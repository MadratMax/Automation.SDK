namespace Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Maps
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Enums;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Interfaces;

    internal class StringAutomationPropertyMap : ISearchAutomationProperty
    {
        public StringAutomationPropertyMap(AutomationProperty automationProperty)
        {
            Type = SearchPropertyType.AutomationProperty;
            AutomationProperty = automationProperty;
        }

        public object FromString(string value)
        {
            return value;
        }

        public AutomationProperty AutomationProperty { get; }

        public SearchPropertyType Type { get; }
    }
}