namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Collections.Generic;
    using System.Windows.Automation;

    public class UIAHeader : Element
    {
        public UIAHeader(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        /// <summary>
        /// Return a list of header items
        /// </summary>
        public List<UIAHeaderItem> HeaderItems
        {
            get
            {
                UIAHeaderItem[] headerItems = FindAll<UIAHeaderItem>();
                List<UIAHeaderItem> lstHeaderItems = new List<UIAHeaderItem>(headerItems);
                return lstHeaderItems;
            }
        }

        /// <summary>
        /// Return a number of columns
        /// </summary>
        public int ColunmCount => HeaderItems.Count;
    }
}
