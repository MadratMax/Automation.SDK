namespace Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Maps
{
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Services.ControlConversion;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Interfaces;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Options;
    using Constants;

    public class AutomationPropertyMap
    {
        private readonly Dictionary<string, ISearchProperty> _propertyMaps;

        public AutomationPropertyMap(ControlTypeConverter controlTypeConverter)
        {
            _propertyMaps = new Dictionary<string, ISearchProperty>();

            // Automation Properties
            _propertyMaps.Add(AutomationSearchProperties.AutomationId, new StringAutomationPropertyMap(AutomationElementIdentifiers.AutomationIdProperty));
            _propertyMaps.Add(AutomationSearchProperties.ClassName, new StringAutomationPropertyMap(AutomationElementIdentifiers.ClassNameProperty));
            _propertyMaps.Add(AutomationSearchProperties.FrameworkId, new StringAutomationPropertyMap(AutomationElementIdentifiers.FrameworkIdProperty));
            _propertyMaps.Add(AutomationSearchProperties.Name, new StringAutomationPropertyMap(AutomationElementIdentifiers.NameProperty));
            _propertyMaps.Add(AutomationSearchProperties.ItemStatus, new StringAutomationPropertyMap(AutomationElementIdentifiers.ItemStatusProperty));
            _propertyMaps.Add(AutomationSearchProperties.Type, new ControlTypeAutomationPropertyMap(AutomationElementIdentifiers.ControlTypeProperty, controlTypeConverter));
            _propertyMaps.Add(AutomationSearchProperties.IsOffscreen, new BoolAutomationPropertyMap(AutomationElementIdentifiers.IsOffscreenProperty));

            // Search Options
            _propertyMaps.Add(AutomationSearchProperties.TreeScope, new TreeScopeOption());
            _propertyMaps.Add(AutomationSearchProperties.Virtualized, new VirtualizedOption());
            _propertyMaps.Add(AutomationSearchProperties.OnlyVisible, new OnlyVisibleOption());
            _propertyMaps.Add(AutomationSearchProperties.IndexNumber, new IndexNumberOption());
        }

        public ISearchProperty this[string id] => _propertyMaps[id];
    }
}