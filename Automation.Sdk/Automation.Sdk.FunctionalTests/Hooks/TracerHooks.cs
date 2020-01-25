namespace Automation.Sdk.FunctionalTests
{
    using Automation.Sdk.UIWrappers.Services.Bootstrapping;
    using Automation.Sdk.UIWrappers.Services.Container;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using JetBrains.Annotations;
    using Microsoft.Practices.Unity;
    using TechTalk.SpecFlow;
    

    [Binding, UsedImplicitly]
    public sealed class TracerHooks
    {
        [Scope(Tag = "TracerTest")]
        [BeforeScenario(Order = BootstrapEventOrder.AFTER_CONTAINER_CREATED)]
        public void ReplaceLogger()
        {
            var container = ContainerProvider.Container;

            container.RegisterSingleton<MockLogger>();
            container.RegisterSingleton<ILogger, MockLogger>();
        }
    }
}