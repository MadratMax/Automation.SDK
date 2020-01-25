namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Linq;
    using System.Windows.Automation;

    public class UIATab : Element
    {
        public UIATab(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public void ClickOnTab(string tabName)
        {
            Tabs.Single(x => x.DisplayText.Equals(tabName)).ClickOn();
        }

        public UIATabItem[] Tabs => FindAll<UIATabItem>();

        #region Selection Pattern

        public UIATabItem Selected
        {
            get
            {
                SelectionPattern selectionPattern = GetPattern<SelectionPattern>(SelectionPattern.Pattern);

                AutomationElement[] selection = selectionPattern.Current.GetSelection();

                if (selection.Length > 0)
                {
                    return new UIATabItem(selection[0]);
                }

                return null;
            }
        }

        public bool CanSelectMultiple => GetProperty<bool>(SelectionPattern.CanSelectMultipleProperty);

        public bool IsSelectionRequired => GetProperty<bool>(SelectionPattern.IsSelectionRequiredProperty);

        #endregion
    }
}
