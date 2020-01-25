namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using JetBrains.Annotations;

    [UsedImplicitly]
    internal sealed class ToggleAdapter : Element, IToggleAdapter
    {
        public ToggleAdapter([NotNull] AutomationElement element) : base(element) { }

        public void Toggle()
        {
            TogglePattern togglePattern = GetPattern<TogglePattern>(TogglePattern.Pattern);
            togglePattern.Toggle();
        }

        public bool TogglePatternAvailable => GetProperty<bool>(AutomationElement.IsTogglePatternAvailableProperty);

        public ToggleState ToggleState => GetProperty<ToggleState>(TogglePattern.ToggleStateProperty);
    }
}
