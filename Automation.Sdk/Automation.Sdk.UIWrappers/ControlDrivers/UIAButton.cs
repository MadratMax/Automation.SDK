namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;

    public class UIAButton : Element
    {
        private readonly IToggleAdapter _toggleAdapter;
        private readonly IExpandAdapter _expandAdapter;
        private readonly ITextAdapter _textAdapter;

        public UIAButton(AutomationElement automationElement)
            : base(automationElement)
        {
            _toggleAdapter = ControlAdapterFactory.Create<IToggleAdapter>(this);
            _expandAdapter = ControlAdapterFactory.Create<IExpandAdapter>(this);
            _textAdapter = ControlAdapterFactory.Create<ITextAdapter>(this);
        }

        public void Toggle() => _toggleAdapter.Toggle();

        public ToggleState ToggleState => _toggleAdapter.ToggleState;

        public void Expand() => _expandAdapter.Expand();

        public void Collapse() => _expandAdapter.Collapse();

        public ExpandCollapseState ExpandCollapseState => _expandAdapter.ExpandCollapseState;

        public string GetText() => _textAdapter.GetText();

        public List<string> GetAllText() => _textAdapter.GetAllText();
    }
}
