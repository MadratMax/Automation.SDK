namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop
{
    /// <summary>
    /// <para>Class to support Selenium-like fluent control mapping</para>
    /// By.Id(id).Virtualized.TreeScope(Scope.Children).OnlyVisible.Type(ControlTypes.DataGrid);
    /// </summary>
    /// <example>By.Id(id).Virtualized.TreeScope(Scope.Children).OnlyVisible.Type(ControlTypes.DataGrid);</example>
    public static class By
    {
        /// <summary>
        /// By ControlType
        /// </summary>
        /// <param name="type">Add ControlType property to search. Use Inspect.exe tool to find actual element properties
        /// See <see cref="ControlTypes"/> enum for details</param>
        /// <returns>VisualPath</returns>
        public static VisualPath Property(ControlTypes type) => new VisualPath().Type(type);

        /// <summary>
        /// By TreeScope
        /// </summary>
        /// <param name="treeScope">Add TreeScope property to search. See <see cref="Scope"/> enum for details</param>
        /// <returns>VisualPath</returns>
        public static VisualPath Property(Scope treeScope) => new VisualPath().TreeScope(treeScope);

        /// <summary>
        /// By AutomationId
        /// </summary>
        /// <param name="automationId">Add AutomationId property to search. Use Inspect.exe tool to find actual element properties</param>
        /// <returns>VisualPath</returns>
        public static VisualPath Id(string automationId) => new VisualPath().Id(automationId);

        /// <summary>
        /// By ClassName
        /// </summary>
        /// <param name="className">Add ClassName property to search. Use Inspect.exe tool to find actual element properties</param>
        /// <returns>VisualPath</returns>
        public static VisualPath ClassName(string className) => new VisualPath().ClassName(className);

        /// <summary>
        /// By FrameworkId
        /// </summary>
        /// <param name="frameworkId">Add FrameworkId property to search. Use Inspect.exe tool to find actual element properties</param>
        /// <returns>VisualPath</returns>
        public static VisualPath FrameworkId(string frameworkId) => new VisualPath().FrameworkId(frameworkId);

        /// <summary>
        /// By Name
        /// </summary>
        /// <param name="name">Add Name property to search. Use Inspect.exe tool to find actual element properties</param>
        /// <returns>VisualPath</returns>
        public static VisualPath Name(string name) => new VisualPath().Name(name);

        /// <summary>
        /// By ItemStatus
        /// </summary>
        /// <param name="itemStatus">Add ItemStatus property to search. Use Inspect.exe tool to find actual element properties</param>
        /// <returns>VisualPath</returns>
        public static VisualPath ItemStatus(string itemStatus) => new VisualPath().ItemStatus(itemStatus);

        /// <summary>
        /// By ControlType
        /// </summary>
        /// <param name="type">Add ControlType property to search. Use Inspect.exe tool to find actual element properties
        /// See <see cref="ControlTypes"/> enum for details</param>
        /// <returns>VisualPath</returns>
        public static VisualPath Type(ControlTypes type) => new VisualPath().Type(type);

        /// <summary>
        /// By IsOffscreen
        /// </summary>
        /// <param name="isOffscreen">Add IsOffscreen property to search. Use Inspect.exe tool to find actual element properties</param>
        /// <returns>VisualPath</returns>
        public static VisualPath IsOffscreen(bool isOffscreen) => new VisualPath().IsOffscreen(isOffscreen);

        /// <summary>
        /// By TreeScope
        /// </summary>
        /// <param name="treeScope">Add TreeScope property to search. See <see cref="Scope"/> enum for details</param>
        /// <returns>VisualPath</returns>
        public static VisualPath TreeScope(Scope treeScope) => new VisualPath().TreeScope(treeScope);

        /// <summary>
        /// By IndexNumber
        /// </summary>
        /// <param name="index">Use this if you need to find specific element from collection of similar elements.
        /// Typically used for Rows in tables</param>
        /// <returns>VisualPath</returns>
        public static VisualPath IndexNumber(int index) => new VisualPath().IndexNumber(index);

        /// <summary>
        /// Use search engine Virtualization workaround to find virtualized WPF elements.
        /// </summary>
        /// <returns>VisualPath</returns>
        public static VisualPath Virtualized => new VisualPath().Virtualized;

        /// <summary>
        /// Search only elements which are on screen and have non-zero size
        /// </summary>
        /// <returns>VisualPath</returns>
        public static VisualPath OnlyVisible => new VisualPath().OnlyVisible;
    }
}