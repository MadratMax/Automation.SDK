namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;

    public class UIAMenuItem : Element
    {
        private readonly IExpandAdapter _expandAdapter;
        private readonly IToggleAdapter _toggleAdapter;
        private readonly ISelectionAdapter _selectionAdapter;

        public UIAMenuItem(AutomationElement automationElement)
            : base(automationElement)
        {
            _toggleAdapter = ControlAdapterFactory.Create<IToggleAdapter>(this);
            _expandAdapter = ControlAdapterFactory.Create<IExpandAdapter>(this);
            _selectionAdapter = ControlAdapterFactory.Create<ISelectionAdapter>(this);
        }

        public void Expand() => _expandAdapter.Expand();

        public void Collapse() => _expandAdapter.Collapse();

        public ExpandCollapseState ExpandCollapseState => _expandAdapter.ExpandCollapseState;

        public void Select() => _selectionAdapter.Select();

        public void Toggle() => _toggleAdapter.Toggle();

        public ToggleState ToggleState => _toggleAdapter.ToggleState;

        public bool TogglePatternAvailable => _toggleAdapter.TogglePatternAvailable;

        public List<UIAMenuItem> GetMenuItems()
        {
            UIAMenuItem[] menuItems = FindAll<UIAMenuItem>();
            if (!menuItems.Any())
                return null;

            List<UIAMenuItem> lstMenuItems = menuItems.ToList<UIAMenuItem>();
            return lstMenuItems;
        }
    }
}
