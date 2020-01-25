namespace Automation.Sdk.UIWrappers.Adapters.Desktop
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IToggleAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Desktop)]
    internal sealed class ToggleAdapter : Element, IToggleAdapter
    {
        public ToggleAdapter([NotNull] IElement element)
            : base(element)
        {
        }

        public void Toggle()
        {
            TogglePattern togglePattern = GetPattern<TogglePattern>(TogglePattern.Pattern);
            togglePattern.Toggle();
        }

        public bool TogglePatternAvailable => GetProperty<bool>(AutomationElement.IsTogglePatternAvailableProperty);

        public ToggleState ToggleState => GetProperty<ToggleState>(TogglePattern.ToggleStateProperty);
    }
}
