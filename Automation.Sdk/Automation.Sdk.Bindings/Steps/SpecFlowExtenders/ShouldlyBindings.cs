namespace Automation.Sdk.Bindings.Steps.SpecFlowExtenders
{
    using Automation.Sdk.UIWrappers.Services;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Steps which extends waiting time for other steps
    /// Usage: [Any step] during [number of seconds] seconds
    /// </summary>
    /// <example>
    /// Then batch "<CCIC_BatchName>" should become processed through capture flow during 180 seconds
    /// </example>
    [Binding]
    [UsedImplicitly]
    public sealed class ShouldlyBindings : Steps
    {
        private readonly Shouldly _shouldly;

        public ShouldlyBindings(Shouldly shouldly)
        {
            _shouldly = shouldly;
        }

        [When(@"(.*) during (\d+) seconds")]
        public void ExecuteWhenWithCustomTimeout([NotNull] string binding, int timeout)
        {
            using (_shouldly.Extend(timeout * 1000))
            {
                When(binding);
            }
        }

        [When(@"(.*) during (\d+) seconds")]
        public void ExecuteWhenWithCustomTimeoutWithTable([NotNull] string binding, int timeout, Table tableArg)
        {
            using (_shouldly.Extend(timeout * 1000))
            {
                When(binding, tableArg);
            }
        }

        [Then(@"(.*) during (\d+) seconds")]
        public void ExecuteThenWithCustomTimeout([NotNull] string binding, int timeout)
        {
            using (_shouldly.Extend(timeout * 1000))
            {
                Then(binding);
            }
        }

        [Then(@"(.*) during (\d+) seconds")]
        public void ExecuteThenWithCustomTimeoutWithTable([NotNull] string binding, int timeout, Table tableArg)
        {
            using (_shouldly.Extend(timeout * 1000))
            {
                Then(binding, tableArg);
            }
        }
    }
}