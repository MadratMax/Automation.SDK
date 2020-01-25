namespace Automation.Sdk.Bindings.Steps
{
    using System.Reactive;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using FluentAssertions;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;
    using UIWrappers.Services.ApplicationServices;
    using UIWrappers.ControlDrivers;

    /// <summary>
    /// Steps to check wheter elemet is appeared or disappeared
    /// </summary>
    [Binding]
    public class PresenceBindings
    {
        private readonly ControlFacade _controlFacade;
        private readonly ProcessService _processService;
        private readonly ILogger _logger;

        public PresenceBindings(
            [NotNull] ILogger logger,
            [NotNull] ControlFacade controlFacade,
            [NotNull] ProcessService processService)
        {
            _logger = logger;
            _controlFacade = controlFacade;
            _processService = processService;
        }

        [Then(@"""(.*)"" should appear"), UsedImplicitly]
        public void FindControl([NotNull] string caption)
        {
            IElement element = null;
            caption.Should().NotBeNullOrWhiteSpace("control caption should not be null or empty");
            Unit.Default.ShouldBecomeNot(_ => (element = _controlFacade.Get(caption)).UiElement, null, $@"element ""{caption}"" should become visible");

            if (element.UiElement is AutomationElement 
                && (element.UiElement as AutomationElement).Current.ControlType == ControlType.Window)
            {
                _processService.Register((element.UiElement as AutomationElement).Current.ProcessId);
            }
        }

        [UsedImplicitly]
        [Then(@"""(.*)"" should disappear")]
        [Then(@"""(.*)"" should become hidden")]
        public void CheckAbsence([NotNull] string caption)
        {
            caption.Should().NotBeNullOrWhiteSpace("control caption should not be null or empty");

            _logger.Write($@"verifying absense of control ""{caption}""");
            Unit.Default.ShouldBecome(_ => _controlFacade.IsAbsent(caption), true, $@"element ""{caption}"" should become hidden");
        }
    }
}