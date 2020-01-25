namespace Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Options
{
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Enums;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Interfaces;

    internal class OnlyVisibleOption : ISearchOption
    {
        public OnlyVisibleOption()
        {
            Type = SearchPropertyType.SearchOption;
        }

        public SearchPropertyType Type { get; }

        public void FromString(SearchOptions command, string value)
        {
            command.OnlyVisible = true;
        }
    }
}