namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;

    public class UIATreeItem : Element
    {
        private readonly IExpandAdapter _expandAdapter;

        public UIATreeItem(AutomationElement automationElement)
            : base(automationElement)
        {
            _expandAdapter = ControlAdapterFactory.Create<IExpandAdapter>(this);
        }

        #region ScrollItem Pattern
        public void ScrollIntoView()
        {
            ScrollItemPattern scrollItemPattern = GetPattern<ScrollItemPattern>(ScrollItemPattern.Pattern);
            scrollItemPattern.ScrollIntoView();
        }

        #endregion

        #region SelectionItem Pattern
        public void Select()
        {
            SelectionItemPattern selectionItemPattern = GetPattern<SelectionItemPattern>(SelectionItemPattern.Pattern);
            selectionItemPattern.Select();
        }

        public void RemoveFromSelection()
        {
            SelectionItemPattern selectionItemPattern = GetPattern<SelectionItemPattern>(SelectionItemPattern.Pattern);
            selectionItemPattern.RemoveFromSelection();
        }

        public void AddToSelection()
        {
            SelectionItemPattern selectionItemPattern = GetPattern<SelectionItemPattern>(SelectionItemPattern.Pattern);
            selectionItemPattern.AddToSelection();
        }

        #endregion

        #region ExpandCollapse Pattern
        public void Expand()
        {
            _expandAdapter.Expand();
        }

        public void Collapse()
        {
            _expandAdapter.Collapse();
        }

        public ExpandCollapseState ExpandCollapseState => _expandAdapter.ExpandCollapseState;

        #endregion
    }
}
