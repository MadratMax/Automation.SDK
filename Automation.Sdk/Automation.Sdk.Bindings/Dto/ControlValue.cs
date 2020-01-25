namespace Automation.Sdk.Bindings.Dto
{
    using JetBrains.Annotations;

    public sealed class ControlValue
    {
        private readonly string _control;
        private readonly string _value;

        public ControlValue([NotNull] string control, [NotNull] string value)
        {
            _value = value;
            _control = control;
        }

        [NotNull]
        public string Value => _value;

        [NotNull]
        public string Control => _control;
    }
}