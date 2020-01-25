namespace Automation.Sdk.UIWrappers.Adapters.Desktop
{
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IFocusAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Desktop)]
    internal sealed class FocusAdapter : Element, IFocusAdapter
    {
        public FocusAdapter(IElement element) 
            : base(element)
        {
        }

        public bool IsFocused => AutomationElement.Current.HasKeyboardFocus;
    }
}