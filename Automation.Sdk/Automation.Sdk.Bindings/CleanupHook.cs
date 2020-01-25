namespace Automation.Sdk.Bindings
{
    using Automation.Sdk.Bindings.Services;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class CleanupHook
    {
        private readonly StepContextHolder _holder;

        public CleanupHook([NotNull] StepContextHolder holder)
        {
            _holder = holder;
        }

        [AfterStep(Order = 100)]
        [UsedImplicitly]
        public void Cleanup()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                var caption = string.Empty;
                if (_holder.CurrentStep != null)
                {
                    caption = $"Step {_holder.CurrentStep.Number}. ";
                    if (!string.IsNullOrEmpty(_holder.CurrentStep.Description))
                    {
                        caption += $"{_holder.CurrentStep.Description}. ";
                    }
                }

                caption =  $"{caption}[{ScenarioContext.Current.StepContext?.StepInfo?.Text}]  ";
                throw new ScenarioException(caption, ScenarioContext.Current.TestError);
            }
        }
    }
}
