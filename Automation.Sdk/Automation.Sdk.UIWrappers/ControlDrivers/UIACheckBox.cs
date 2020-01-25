namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;

    public class UIACheckBox : Element
    {
        private readonly IToggleAdapter _toggleAdapter;
        private readonly ITextAdapter _textAdapter;

        public UIACheckBox(AutomationElement element)
            : base(element)
        {
            _toggleAdapter = ControlAdapterFactory.Create<IToggleAdapter>(this);
            _textAdapter = ControlAdapterFactory.Create<ITextAdapter>(this);
        }

        public void Toggle() => _toggleAdapter.Toggle();

        public ToggleState ToggleState => _toggleAdapter.ToggleState;

        public string GetText() => _textAdapter.GetText();

        public List<string> GetAllText() => _textAdapter.GetAllText();

        public bool Checked
        {
            get
            {
                return ToggleState == ToggleState.On;
            }

            set
            {
                if (value && ToggleState == ToggleState.Off || !value && ToggleState == ToggleState.On)
                {
                    Toggle();
                }
            }
        }
    }
}
