namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop
{
    using JetBrains.Annotations;

    public sealed class ControlFindCommand
    {
        private readonly string _elementName;
        private readonly string _automationPropertyValue;
        private readonly AutomationSearchBehavior _automationSearchBehavior;

        public ControlFindCommand([NotNull] string automationPropertyValue)
        {
            _automationPropertyValue = automationPropertyValue;
        }

        public ControlFindCommand(string elementName,
                                  ControlConfiguration parent,
                                  string automationPropertyValue)
        {
            var currentParrent = parent;

            while (currentParrent != null)
            {
                automationPropertyValue = string.Join("=>", currentParrent.AutomationPropertyValue, automationPropertyValue);
                currentParrent = currentParrent.Parent;
            }

            _elementName = elementName;
            _automationPropertyValue = automationPropertyValue;
            _automationSearchBehavior = AutomationSearchBehavior.ByVPath;
        }

        [NotNull]
        public string AutomationPropertyValue => _automationPropertyValue;

        public AutomationSearchBehavior AutomationSearchBehavior => _automationSearchBehavior;

        [CanBeNull]
        public string ParentId { get; }

        public int Timeout => Shouldly.Timeout;

        public string ElementName => _elementName;

        public static ControlFindCommand BuildDefaultCommand([NotNull] string automationId)
        {
            return new ControlFindCommand(automationId);
        }

        public override string ToString()
        {
            return $"AutomationPropertyValue = {AutomationPropertyValue}; "
                   + $"AutomationSearchBehavior = {AutomationSearchBehavior}; "
                   + $"Timeout = {Timeout}; "
                   + $"ParentId={ParentId ?? "\"No parent\""}";
        }
    }
}