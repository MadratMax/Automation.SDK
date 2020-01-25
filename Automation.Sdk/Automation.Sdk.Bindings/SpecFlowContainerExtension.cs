namespace Automation.Sdk.Bindings
{
    using Automation.Sdk.Bindings.Services;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using Microsoft.Practices.Unity;

    public class SpecFlowContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<Logger, SpecFlowTraceListener>();
        }
    }
}
