namespace Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Maps
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Services.ControlConversion;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Enums;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Interfaces;

    internal class ControlTypeAutomationPropertyMap : ISearchAutomationProperty
    {
        private readonly ControlTypeConverter _controlTypeConverter;

        public ControlTypeAutomationPropertyMap(AutomationProperty automationProperty, ControlTypeConverter controlTypeConverter)
        {
            Type = SearchPropertyType.AutomationProperty;
            _controlTypeConverter = controlTypeConverter;
            AutomationProperty = automationProperty;
        }

        public object FromString(string value)
        {
            return _controlTypeConverter.Resolve(value);
        }

        public AutomationProperty AutomationProperty { get; }

        public SearchPropertyType Type { get; }
    }
}