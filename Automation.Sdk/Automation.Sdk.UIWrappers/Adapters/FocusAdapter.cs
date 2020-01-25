namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using JetBrains.Annotations;

    [UsedImplicitly]
    internal sealed class FocusAdapter : Element, IFocusAdapter
    {
        public FocusAdapter(AutomationElement automationElement) 
            : base(automationElement)
        {
        }

        public bool IsFocused => AutomationElement.Current.HasKeyboardFocus;
    }
}