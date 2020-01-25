namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;

    public class UIATabItem : Element
    {
        private readonly ISelectionAdapter _selectionAdapter;
        private readonly ITextAdapter _textAdapter;

        public UIATabItem(AutomationElement element)
            : base(element)
        {
            _selectionAdapter = ControlAdapterFactory.Create<ISelectionAdapter>(this);
            _textAdapter = ControlAdapterFactory.Create<ITextAdapter>(this);
        }

        public void ScrollIntoView()
        {
            ScrollItemPattern scrollItemPattern = GetPattern<ScrollItemPattern>(ScrollItemPattern.Pattern);
            scrollItemPattern.ScrollIntoView();
        }

        public void Select() => _selectionAdapter.Select();

        public override bool IsSelected => _selectionAdapter.IsSelected;

        public string DisplayText => _textAdapter.DisplayText;
    }
}
