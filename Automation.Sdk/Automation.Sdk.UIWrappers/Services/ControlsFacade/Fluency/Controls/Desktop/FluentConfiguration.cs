namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop
{
    using JetBrains.Annotations;
    using System;

    [UsedImplicitly]
    public class FluentConfiguration
    {
        private readonly IControlMapper _map;

        public FluentConfiguration([NotNull] IControlMapper map)
        {
            _map = map;
        }

        // Fluent configuration facade which is used as an endpoint for custom configuration
        // TODO: Do not use anymore.
        // Example: fluentConfiguration.Add("my caption").For().Id("my automation id");
        // Example: fluentConfiguration.Add("my caption").For().Name("my automation name");
        [Obsolete]
        public IncompleteControlConfiguration Add([NotNull] string controlCaption)
        {
            return new IncompleteControlConfiguration(this, _map, controlCaption);
        }

        /// <summary>
        /// Fluent configuration facade which is used as an endpoint for custom configuration
        /// </summary>
        /// <param name="controlCaption">
        /// <para>Name of the element to use in SpecFlow</para>
        /// <para>User clicks on "[controlCaption]"</para>
        /// </param>
        /// <param name="configuration">
        /// <para>Text representation of visual path to element</para>
        /// <para>"[Type=window,AutomationID=InvoiceEdit_Panel,TreeScope=Children]"</para>
        /// </param>
        /// <returns>Control configuration to support chaining adding</returns>
        public ControlConfiguration Add([NotNull] string controlCaption, string configuration)
        {
            return new ControlConfiguration(this, _map, controlCaption, configuration, AutomationSearchBehavior.ByVPath);
        }

        /// <summary>
        /// Fluent configuration facade which is used as an endpoint for custom configuration
        /// </summary>
        /// <param name="controlCaption">
        /// <para>Name of the element to use in SpecFlow</para>
        /// <para>User clicks on "[controlCaption]"</para>
        /// </param>
        /// <param name="by">Use 'By' static class to start adding visual path to element fluently</param>
        /// <returns>Control configuration to support chaining adding</returns>
        public ControlConfiguration Add([NotNull] string controlCaption, VisualPath by)
        {
            return new ControlConfiguration(this, _map, controlCaption, by.Compile(), AutomationSearchBehavior.ByVPath);
        }
    }
}
