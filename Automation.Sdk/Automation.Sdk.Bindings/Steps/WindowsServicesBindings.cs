namespace Automation.Sdk.Bindings.Steps
{
    using System.Collections.Generic;
    using Automation.Sdk.Bindings.Enums;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.ApplicationServices;
    using JetBrains.Annotations;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Class to work with services
    /// </summary>
    [UsedImplicitly]
    [Binding]
    public class WindowsServicesBindings : Steps
    {
        private readonly IApplicationInfoContainer _applicationInfoContainer;
        private readonly IWindowsServicesAccessor _windowsServicesAccessor;

        public WindowsServicesBindings(
            IApplicationInfoContainer applicationInfoContainer,
            IWindowsServicesAccessor windowsServicesAccessor)
        {
            _applicationInfoContainer = applicationInfoContainer;
            _windowsServicesAccessor = windowsServicesAccessor;
        }

        [When(@"""(.*)"" service is started with parameters")]
        public void WhenServiceIsStartedWithParameters(FormattedString applicationName, IEnumerable<string> parametersTable)
        {
            var serviceName = GetServiceName(applicationName);
            var parameters = string.Join(" ", parametersTable);

            Assert.IsTrue(_windowsServicesAccessor.StartServiceWithArgs(serviceName, 60000, parameters), $"Service {applicationName} wasn't started in time");
        }

        [Then(@"service ""(.*)"" should (be|become) ""(.*)""")]
        public void ThenServiceShouldBecome(FormattedString applicationName, AssertPredicate assertPredicate, string expectedServiceState)
        {
            var serviceName = GetServiceName(applicationName);
            var shouldBeInstalled = expectedServiceState.ToLower().Equals("installed");

            AutomationFacade.WindowsServicesAccessor.ShouldBe(
                x => x.IsServiceInstalled(serviceName),
                assertPredicate,
                shouldBeInstalled,
                $"Service {serviceName} should {assertPredicate} {(shouldBeInstalled ? "" : "not ")}installed");
        }

        [Given(@"""(.*)"" service is started")]
        [When(@"""(.*)"" service is started")]
        public void StartCustomService(FormattedString applicationName)
        {
            string serviceName = GetServiceName(applicationName);
            Assert.IsNotEmpty(serviceName, "{0} module have no service mode.", applicationName);

            Assert.IsTrue(_windowsServicesAccessor.StartService(serviceName, 20000), $"Service {serviceName} is not started");
        }

        [When(@"""(.*)"" service is stopped")]
        public void StopCustomService(string service)
        {
            string serviceName = GetServiceName(service);

            Assert.IsTrue(_windowsServicesAccessor.StopService(serviceName, 20000), $"Service {serviceName} is not stopped");
        }

        [Given(@"following services are")]
        public void GivenFollowingServicesAre(Table table)
        {
            Methods.CheckSpecFlowTable(table, new[] { "ServiceName", "Condition" }, null, "following services are");

            foreach (var row in table.Rows)
            {
                if (row["Condition"].Equals("stopped"))
                {
                    StopCustomService(row["ServiceName"]);
                }
                else
                {
                    StartCustomService(row["ServiceName"]);
                }
            }
        }

        private string GetServiceName(string applicationName)
        {
            var serviceName = _applicationInfoContainer.Get(applicationName).DefaultServiceName;
            Assert.IsNotEmpty(serviceName, $"{applicationName} module have no service mode.");

            return serviceName;
        }
    }
}