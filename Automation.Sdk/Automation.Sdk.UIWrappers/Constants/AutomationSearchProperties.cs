namespace Automation.Sdk.UIWrappers.Constants
{
    /// <summary>
    /// Automation Properties from System.Windows.Automation
    /// and some syntetic ones for vPath search engine
    /// </summary>
    class AutomationSearchProperties
    {
        // TODO: This is a temp properties. It's better to redone it with Automation.ProgrammaticName
        public const string AutomationId = "AutomationID";
        public const string ClassName = "ClassName";
        public const string FrameworkId = "FrameworkId";
        public const string Name = "Name";
        public const string ItemStatus = "ItemStatus";
        public const string Type = "Type";
        public const string IsOffscreen = "IsOffscreen";

        // VPATH ones:
        public const string TreeScope = "TreeScope";
        public const string Virtualized = "Virtualized";
        public const string OnlyVisible = "OnlyVisible";
        public const string IndexNumber = "IndexNumber";
    }
}
