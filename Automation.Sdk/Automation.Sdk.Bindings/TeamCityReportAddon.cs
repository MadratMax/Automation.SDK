namespace Automation.Sdk.Bindings
{
    using Automation.Sdk.UIWrappers.Services.Bootstrapping;
    using Automation.Sdk.UIWrappers.Services.Container;
    using Automation.Sdk.UIWrappers.Services.TeamCity;

    using JetBrains.Annotations;
    using Microsoft.Practices.Unity;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Addon for TeamCity messaging
    /// </summary>
    [Binding, UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public class TeamCityReportAddon
    {
        private readonly TeamCityMessageService _teamCityMessageService;

        public TeamCityReportAddon()
        {
            _teamCityMessageService = ContainerProvider.Container.Resolve<TeamCityMessageService>();
        }

        [BeforeScenario(Order = BootstrapEventOrder.AFTER_CONTAINER_CREATED)]
        public void OpenMessageBlock()
        {
            _teamCityMessageService.OpenMessageBlock("SetUp");
        }

        [BeforeScenario(Order = BootstrapEventOrder.AFTER_SET_UP_SEQUENCE)]
        public void CloseMessageBlock()
        {
            _teamCityMessageService.CloseMessageBlock("SetUp");
        }
    }
}