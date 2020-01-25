namespace Automation.Sdk.Bindings.Services
{
    using System;
    using System.Linq;
    using FluentAssertions.Execution;
    using JetBrains.Annotations;

    public class ScenarioException : AssertionFailedException
    {
        private readonly string _stepCaption;
        private readonly Exception _exception;

        public ScenarioException([NotNull] string stepCaption, [NotNull] Exception exception) : base(exception.Message)
        {
            _stepCaption = stepCaption;
            _exception = exception;
        }

        public override string Message =>  _stepCaption + _exception.Message;

        public override string StackTrace
        {
            get
            {
                // Remove all steps in call stack, which are belong to external code
                return _exception
                    .StackTrace
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                    .Where(x => x.Contains("Emc."))
                    .Aggregate((current, next) => current + Environment.NewLine + next);
            }
        }
    }
}
