namespace Automation.Sdk.Bindings.Services
{
    using Automation.Sdk.Bindings.Dto;
    using JetBrains.Annotations;

    /// <summary>
    /// Current step information container
    /// </summary>
    public class StepContextHolder
    {
        private StepContext _currentStep;

        public virtual void Push(int number, [NotNull] string description)
        {
            _currentStep = new StepContext(number, description);
        }

        [CanBeNull]
        public StepContext CurrentStep => _currentStep;
    }
}
