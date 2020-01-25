namespace Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Options
{
    using System.Windows.Automation;

    internal class SearchOptions
    {
        public SearchOptions()
        {
            //Defaults
            TreeScope = TreeScope.Descendants;
            UseVirtualization = false;
            IndexNumber = null;
        }

        public TreeScope TreeScope { get; set; }

        public bool UseVirtualization { get; set; }

        public bool OnlyVisible { get; set; }

        public int? IndexNumber { get; set; }
    }
}