namespace Automation.Sdk.FunctionalTests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    [Binding, UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public sealed class TracerBindings
    {
        private readonly MockLogger _logger;

        public TracerBindings([NotNull] MockLogger logger)
        {
            _logger = logger;
        }

        [When(@"trace messge ""(.*)""")]
        public void TraceMessage([NotNull] string message)
        {
            //using (var tracer = new DefaultTracerClient())
            //{ 
            //    tracer.Consume(message);
            //}

            throw new NotImplementedException();
        }

        [Then(@"log should contain the following messages:")]
        public void CheckTraceMessages([NotNull] Table messages)
        {
            _logger.Messages.Should().Contain(messages.Rows.Select(x => x[0]));
        }
    }
}
