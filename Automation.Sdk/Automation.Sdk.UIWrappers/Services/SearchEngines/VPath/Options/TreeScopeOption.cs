namespace Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Options
{
    using System;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Enums;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Interfaces;

    internal class TreeScopeOption : ISearchOption
    {
        public TreeScopeOption()
        {
            Type = SearchPropertyType.SearchOption;
        }

        public SearchPropertyType Type { get; }

        public void FromString(SearchOptions command, string value)
        {
            TreeScope parsed;
            Enum.TryParse(value, out parsed);

            command.TreeScope = parsed;
        }
    }
}