namespace Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Options
{
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Enums;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Interfaces;

    internal class IndexNumberOption : ISearchOption
    {
        public const int LAST = -1;

        public IndexNumberOption()
        {
            Type = SearchPropertyType.SearchOption;
        }

        public SearchPropertyType Type { get; }

        public void FromString(SearchOptions command, string value)
        {
            if (value == "LAST")
            {
                command.IndexNumber = LAST;
            }
            else
            {
                command.IndexNumber = int.Parse(value);
            }

            // TODO: When/if count will be implemented please add TRUE flag for FindALL search.
        }
    }
}