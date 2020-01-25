namespace Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Interfaces
{
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Options;

    internal interface ISearchOption : ISearchProperty
    {
        void FromString(SearchOptions command, string value);
    }
}