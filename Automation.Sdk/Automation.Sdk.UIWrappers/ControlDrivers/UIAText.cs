namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;

    public class UIAText : Element
    {
        private readonly ITextAdapter _textAdapter;

        public UIAText(AutomationElement automationElement)
            : base(automationElement)
        {
            _textAdapter = ControlAdapterFactory.Create<ITextAdapter>(this);
        }
        
        public string DisplayText => _textAdapter.DisplayText;
    }
}