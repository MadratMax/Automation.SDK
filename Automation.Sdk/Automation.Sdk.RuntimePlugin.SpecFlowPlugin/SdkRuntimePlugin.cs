namespace Automation.Sdk.RuntimePlugin.SpecFlowPlugin
{
    using BoDi;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow.Bindings;
    using TechTalk.SpecFlow.Plugins;
    using TechTalk.SpecFlow.Tracing;

    public sealed class SdkRuntimePlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters)
        {
            runtimePluginEvents.CustomizeGlobalDependencies += (_, e) => OverrideDependencies(e.ObjectContainer);
        }

        private void OverrideDependencies([NotNull] IObjectContainer container)
        {
            container.RegisterTypeAs<CustomBindingInvoker, IBindingInvoker>();
            container.RegisterTypeAs<StepsTraceListener, ITraceListener>();
        }
    }
}
