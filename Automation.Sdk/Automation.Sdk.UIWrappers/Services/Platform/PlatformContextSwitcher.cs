namespace Automation.Sdk.UIWrappers.Services.Platform
{
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade;
    using Automation.Sdk.UIWrappers.Services.Logging;

    public class PlatformContextSwitcher
    {
        private readonly ControlFacade _controlFacade;
        private readonly ILogger _logger;
        private PlatformContextType _previousPlatform;

        public PlatformContextSwitcher(
            ControlFacade controlFacade,
            ILogger logger)
        {
            _controlFacade = controlFacade;
            _logger = logger;
        }

        public void SwitchPlatform(PlatformContextType type)
        {
            if (type == _controlFacade.CurrentPlatform)
            {
                return;
            }

            _previousPlatform = _controlFacade.CurrentPlatform;
            _controlFacade.CurrentPlatform = type;
            _logger.Write($"Platform context switched to {type}");
        }

        public void RestorePlatform()
        {
            if (_previousPlatform == _controlFacade.CurrentPlatform)
            {
                return;
            }

            _controlFacade.CurrentPlatform = _previousPlatform;
            _logger.Write($"Platform context restored to {_previousPlatform}");
        }
    }
}