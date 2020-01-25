namespace Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Options
{
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Enums;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Interfaces;

    internal class VirtualizedOption : ISearchOption
    {
        public VirtualizedOption()
        {
            Type = SearchPropertyType.SearchOption;
        }

        public SearchPropertyType Type { get; }

        public void FromString(SearchOptions command, string value)
        {
            command.UseVirtualization = true;
        }
    }
}