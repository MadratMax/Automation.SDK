namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;

    public class UIAGroup : Element
    {
        public UIAGroup(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public bool IsCollapsed
        {
            get
            {
                object pattern;

                if (AutomationElement.TryGetCurrentPattern(ExpandCollapsePattern.Pattern, out pattern))
                {
                    return ((ExpandCollapsePattern)pattern).Current.ExpandCollapseState == ExpandCollapseState.Collapsed;
                }

                return false;
            }
        }

        public bool IsExpanded
        {
            get
            {
                object pattern;

                if (AutomationElement.TryGetCurrentPattern(ExpandCollapsePattern.Pattern, out pattern))
                {
                    return ((ExpandCollapsePattern)pattern).Current.ExpandCollapseState == ExpandCollapseState.Expanded;
                }

                return true;
            }
        }
    }
}
