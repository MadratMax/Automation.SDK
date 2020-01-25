namespace Automation.Sdk.Bindings.Services
{
    using System;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow.Tracing;

    [UsedImplicitly]
    public class SpecFlowTraceListener : Logger
    {
        private readonly ITraceListener _traceListener;

        public SpecFlowTraceListener([NotNull] ITraceListener traceListener)
        {
            _traceListener = traceListener;
        }

        public override void Write(string message)
        {
            _traceListener.WriteToolOutput(message);
        }

        public override void Write(Exception exception)
        {
            _traceListener.WriteToolOutput(exception.ToString());
        }
    }
}
