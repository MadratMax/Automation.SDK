namespace Automation.Sdk.Bindings.Dto
{
    using JetBrains.Annotations;

    /// <summary>
    /// DTO object for SpecFlow scenarios steps delimeter
    /// Given Step # [Step description]
    /// and for error messaging after test fail
    /// </summary>
    public sealed class StepContext
    {
        private readonly int _number;
        private readonly string _description;

        public StepContext(int number, [NotNull] string description)
        {
            _number = number;
            _description = description;
        }

        public int Number => _number;

        public string Description => _description;
    }
}
