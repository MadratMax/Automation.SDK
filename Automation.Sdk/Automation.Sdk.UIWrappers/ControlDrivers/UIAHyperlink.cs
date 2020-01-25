namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;

    public class UIAHyperlink : Element
    {
        private readonly ITextAdapter _textAdapter;

        public UIAHyperlink(AutomationElement element)
            : base(element)
        {
            _textAdapter = ControlAdapterFactory.Create<ITextAdapter>(this);
        }

        public string GetText() => _textAdapter.GetText();

        public List<string> GetAllText() => _textAdapter.GetAllText();
    }
}
