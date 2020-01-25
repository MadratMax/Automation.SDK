namespace Automation.Sdk.Bindings.Steps.SpecFlowExtenders
{
    using Automation.Sdk.Bindings.Services;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Steps for scenario steps delimeters
    /// </summary>
    /// <example>
    /// Given Step 3 "Create Batch"
    /// </example>
    [Binding]
    public sealed class ScenarioBindings
    {
        private readonly ILogger _logger;
        private readonly StepContextHolder _holder;

        public ScenarioBindings([NotNull] ILogger logger, [NotNull] StepContextHolder holder)
        {
            _logger = logger;
            _holder = holder;
        }

        [Given(@"Step (\d+)")]
        [UsedImplicitly]
        public void StartStep(int stepNumber)
        {
            StartStep(stepNumber, string.Empty);
        }

        [Given(@"Step (\d+) ""(.*)""")]
        [UsedImplicitly]
        public void StartStep(int number, [NotNull] string description)
        {
            _logger.Write($"Step {number}. {description}");
            _holder.Push(number, description);
        }
    }
}
