namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    public class UIAMenu : Element
    {
        public UIAMenu(AutomationElement element)
            : base(element)
        {
        }

        public List<UIAMenuItem> GetMenuItems()
        {
            UIAMenuItem[] menuItems = FindAll<UIAMenuItem>();
            if (!menuItems.Any())
            { 
                return null;
            }

            List<UIAMenuItem> lstMenuItems = menuItems.ToList();
            return lstMenuItems;
        }
    }
}
