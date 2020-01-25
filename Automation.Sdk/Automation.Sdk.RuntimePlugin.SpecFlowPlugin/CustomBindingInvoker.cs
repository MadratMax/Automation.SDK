namespace Automation.Sdk.RuntimePlugin.SpecFlowPlugin
{
    using System;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow.Bindings;
    using TechTalk.SpecFlow.Configuration;
    using TechTalk.SpecFlow.ErrorHandling;
    using TechTalk.SpecFlow.Infrastructure;
    using TechTalk.SpecFlow.Tracing;

    public class CustomBindingInvoker : BindingInvoker
    {
        [NotNull] private readonly Logger _logger;

        public CustomBindingInvoker(
            [NotNull] RuntimeConfiguration runtimeConfiguration, 
            [NotNull] IErrorProvider errorProvider,
            [NotNull] Logger logger) 
            : base(runtimeConfiguration, errorProvider)
        {
            _logger = logger;
        }

        public override object InvokeBinding(
            IBinding binding, 
            IContextManager contextManager, 
            object[] arguments, 
            ITestTracer testTracer,
            out TimeSpan duration)
        {
            if (binding is IHookBinding && ((IHookBinding) binding).HookType == HookType.AfterScenario)
            {
                try
                {
                    return base.InvokeBinding(binding, contextManager, arguments, testTracer, out duration);
                }
                catch (Exception exception)
                {
                    _logger.Write($@"[AfterScenario] hook ""{binding.Method}"" has thrown an exception: {exception}");
                    duration = TimeSpan.Zero;
                    return null;
                }
            }

            return base.InvokeBinding(binding, contextManager, arguments, testTracer, out duration);
        }
    }
}