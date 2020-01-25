namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;

    public class UIARadioButton : Element
    {
        private readonly ISelectionAdapter _selectionAdapter;

        public UIARadioButton(AutomationElement element) : base(element)
        {
            _selectionAdapter = ControlAdapterFactory.Create<ISelectionAdapter>(this);
        }

        public void ScrollIntoView()
        {
            ScrollItemPattern scrollItemPattern = GetPattern<ScrollItemPattern>(ScrollItemPattern.Pattern);
            scrollItemPattern.ScrollIntoView();
        }

        public override bool IsSelected => _selectionAdapter.IsSelected;
    }
}