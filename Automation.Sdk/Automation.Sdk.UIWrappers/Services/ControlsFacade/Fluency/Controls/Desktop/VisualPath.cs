namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop
{
    using Constants;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents visual path for Desktop applications UI mapping (the same idea xPath in Web have)
    /// <para>Do not create instance of this class.
    /// Use 'By' static class instead.</para>
    /// By.Id(id).Virtualized.TreeScope(Scope.Children).OnlyVisible.Type(ControlTypes.DataGrid);
    /// </summary>
    /// <example>By.Id(id).Virtualized.TreeScope(Scope.Children).OnlyVisible.Type(ControlTypes.DataGrid);</example>
    public class VisualPath
    {
        private readonly List<string> _searchArguments;
        private string _followedBy;

        /// <summary>
        /// Do not use this directly
        /// </summary>
        internal VisualPath()
        {
            _searchArguments = new List<string>();
        }

        /// <summary>
        /// By ControlType
        /// </summary>
        /// <param name="type">Add ControlType property to search. Use Inspect.exe tool to find actual element properties
        /// See <see cref="ControlTypes"/> enum for details</param>
        /// <returns>VisualPath</returns>
        public VisualPath Propery(ControlTypes type) => Type(type);

        /// <summary>
        /// By TreeScope
        /// </summary>
        /// <param name="treeScope">Add TreeScope property to search. See <see cref="Scope"/> enum for details</param>
        /// <returns>VisualPath</returns>
        public VisualPath Propery(Scope scope) => TreeScope(scope);

        /// <summary>
        /// By AutomationId
        /// </summary>
        /// <param name="automationId">Add AutomationId property to search. Use Inspect.exe tool to find actual element properties</param>
        /// <returns>VisualPath</returns>
        public VisualPath Id(string automationId)
        {
            _searchArguments.Add($"{AutomationSearchProperties.AutomationId}={automationId}");
            return this;
        }

        /// <summary>
        /// By ClassName
        /// </summary>
        /// <param name="className">Add ClassName property to search. Use Inspect.exe tool to find actual element properties</param>
        /// <returns>VisualPath</returns>
        public VisualPath ClassName(string className)
        {
            _searchArguments.Add($"{AutomationSearchProperties.ClassName}={className}");
            return this;
        }

        /// <summary>
        /// By FrameworkId
        /// </summary>
        /// <param name="frameworkId">Add FrameworkId property to search. Use Inspect.exe tool to find actual element properties</param>
        /// <returns>VisualPath</returns>
        public VisualPath FrameworkId(string frameworkId)
        {
            _searchArguments.Add($"{AutomationSearchProperties.FrameworkId}={frameworkId}");
            return this;
        }

        /// <summary>
        /// By Name
        /// </summary>
        /// <param name="name">Add Name property to search. Use Inspect.exe tool to find actual element properties</param>
        /// <returns>VisualPath</returns>
        public VisualPath Name(string name)
        {
            _searchArguments.Add($"{AutomationSearchProperties.Name}={name}");
            return this;
        }

        /// <summary>
        /// By ItemStatus
        /// </summary>
        /// <param name="itemStatus">Add ItemStatus property to search. Use Inspect.exe tool to find actual element properties</param>
        /// <returns>VisualPath</returns>
        public VisualPath ItemStatus(string itemStatus)
        {
            _searchArguments.Add($"{AutomationSearchProperties.ItemStatus}={itemStatus}");
            return this;
        }

        /// <summary>
        /// By ControlType
        /// </summary>
        /// <param name="type">Add ControlType property to search. Use Inspect.exe tool to find actual element properties
        /// See <see cref="ControlTypes"/> enum for details</param>
        /// <returns>VisualPath</returns>
        public VisualPath Type(ControlTypes type)
        {
            var typeName = type.GetAttribute<DisplayAttribute>();
            _searchArguments.Add($"{AutomationSearchProperties.Type}={typeName.Name}");
            return this;
        }

        /// <summary>
        /// By IsOffscreen
        /// </summary>
        /// <param name="isOffscreen">Add IsOffscreen property to search. Use Inspect.exe tool to find actual element properties</param>
        /// <returns>VisualPath</returns>
        public VisualPath IsOffscreen(bool isOffscreen)
        {
            _searchArguments.Add($"{AutomationSearchProperties.IsOffscreen}={isOffscreen}");
            return this;
        }

        /// <summary>
        /// By TreeScope
        /// </summary>
        /// <param name="treeScope">Add TreeScope property to search. See <see cref="Scope"/> enum for details</param>
        /// <returns>VisualPath</returns>
        public VisualPath TreeScope(Scope treeScope)
        {
            _searchArguments.Add($"{AutomationSearchProperties.TreeScope}={treeScope}");
            return this;
        }

        /// <summary>
        /// By IndexNumber
        /// </summary>
        /// <param name="index">Use this if you need to find specific element from collection of similar elements.
        /// Typically used for Rows in tables</param>
        /// <returns>VisualPath</returns>
        public VisualPath IndexNumber(int index)
        {
            _searchArguments.Add($"{AutomationSearchProperties.IndexNumber}={index}");
            return this;
        }

        /// <summary>
        /// Use search engine Virtualization workaround to find virtualized WPF elements.
        /// </summary>
        /// <returns>VisualPath</returns>
        public VisualPath Virtualized
        {
            get
            {
                _searchArguments.Add(AutomationSearchProperties.Virtualized);
                return this;
            }
        }

        /// <summary>
        /// Search only elements which are on screen and have non-zero size
        /// </summary>
        /// <returns>VisualPath</returns>
        public VisualPath OnlyVisible
        {
            get
            {
                _searchArguments.Add(AutomationSearchProperties.OnlyVisible);
                return this;
            }
        }

        /// <summary>
        /// Used to create vpath which goes through multiple elements:
        /// Search "this", Then search "that".
        /// Should be the last call in the sequence.
        /// </summary>
        /// <param name="argument">VisualPath of "that" element</param>
        /// <example>
        /// instance.Add(caption,
        ///     By.Id(id)
        ///        .Then(By.ClassName(caption)
        ///         .Then(By.ItemStatus(id))
        ///        ));
        /// </example>
        public VisualPath Then(VisualPath argument)
        {
            _followedBy = argument.Compile();
            return this;
        }

        /// <summary>
        /// Compiles the visual path to string in order to be passed to search engine
        /// </summary>
        /// <returns>Visual path as a string. Something like
        /// "[Type=window,AutomationID=InvoiceEdit_Panel,TreeScope=Children]=>[AutomationID=Test]=>[Type=button]"
        /// </returns>
        public string Compile()
        {
            var compiled = $"[{string.Join(",", _searchArguments)}]";

            if (!string.IsNullOrWhiteSpace(_followedBy))
            {
                compiled = $"{compiled}=>{_followedBy}";
            }

            return compiled;
        }

        public override string ToString() => Compile();
    }
}