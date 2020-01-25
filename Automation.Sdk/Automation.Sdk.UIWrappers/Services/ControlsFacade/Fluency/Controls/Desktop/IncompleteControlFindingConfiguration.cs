namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop
{
    using System.Linq;
    using JetBrains.Annotations;

    public sealed class IncompleteControlFindingConfiguration
    {
        private readonly FluentConfiguration _configuration;
        private readonly IControlMapper _map;
        private readonly string _controlCaption;

        public IncompleteControlFindingConfiguration([NotNull] FluentConfiguration configuration, [NotNull] IControlMapper map, [NotNull] string controlCaption)
        {
            _configuration = configuration;
            _map = map;
            _controlCaption = controlCaption;
        }

        public IControlConfiguration Id([NotNull] string automationId)
        {
            return VPath(automationId);
        }

        public IControlConfiguration Name([NotNull] string automationName)
        {
            return VPath($"[Name={automationName}]");
        }

        public IControlConfiguration VPath([NotNull] string vPath)
        {
            //TODO: When above ID and Name methods will become obsolete the following lines may be moved to Id method.
            if (vPath.First() != '[' && vPath.Last() != ']')
            {
                // default property is AutomationID
                vPath = $"[AutomationID={vPath}]";
            }

            return new ControlConfiguration(_configuration, _map, _controlCaption, vPath, AutomationSearchBehavior.ByVPath);
        }
    }
}