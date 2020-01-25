namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;
    using Automation.Sdk.UIWrappers.Services;

    public class UIAListItem : Element
    {
        private readonly ISelectionAdapter _selectionAdapter;
        private readonly ITextAdapter _textAdapter;

        public UIAListItem(AutomationElement element) : base (element)
        {
            _selectionAdapter = ControlAdapterFactory.Create<ISelectionAdapter>(this);
            _textAdapter = ControlAdapterFactory.Create<ITextAdapter>(this);
        }

        public void ScrollIntoView()
        {
            if (!GetProperty<bool>(AutomationElement.IsScrollItemPatternAvailableProperty, false))
            {
                var parent = Parent;
                if (!parent.GetProperty<bool>(AutomationElement.IsScrollPatternAvailableProperty, false))
                {
                    return;
                }

                parent.ScrollTo(this);
            }

            ScrollItemPattern scrollItemPattern = GetPattern<ScrollItemPattern>(ScrollItemPattern.Pattern);
            scrollItemPattern.ScrollIntoView();
        }

        public void Select() => _selectionAdapter.Select();

        public override bool IsSelected => _selectionAdapter.IsSelected;

        public string GetText() => _textAdapter.GetText();
    }
}