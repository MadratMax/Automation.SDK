namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop
{
    using JetBrains.Annotations;

    /// <summary>
    /// Incomplete control configuration which is not intended to do anything by itself,
    /// except of ControlConfiguration providing via For(..) method. Created to support 
    /// fluent syntax.
    /// </summary>
    public class IncompleteControlConfiguration
    {
        private readonly FluentConfiguration _configuration;
        private readonly IControlMapper _map;

        private readonly string _controlCaption;

        public IncompleteControlConfiguration(
            [NotNull] FluentConfiguration configuration, 
            [NotNull] IControlMapper map, 
            [NotNull] string controlCaption)
        {
            _configuration = configuration;
            _map = map;
            _controlCaption = controlCaption;
        }

        public IncompleteControlFindingConfiguration For => new IncompleteControlFindingConfiguration(_configuration, _map, _controlCaption);
    }
}