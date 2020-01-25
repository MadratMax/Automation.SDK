namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;

    public class UIAComboBox : Element
    {
        private readonly IExpandAdapter _expandAdapter;
        private readonly ISelectionAdapter _selectionAdapter;
        private readonly IValueAdapter _valueAdapter;

        public UIAComboBox (AutomationElement automationElement)
          : base (automationElement)
        {
            _expandAdapter = ControlAdapterFactory.Create<IExpandAdapter>(this);
            _selectionAdapter = ControlAdapterFactory.Create<ISelectionAdapter>(this);
            _valueAdapter = ControlAdapterFactory.Create<IValueAdapter>(this);
        }

        public void SetValue(string value)
        {
            _valueAdapter.SetValue(value);
        }

        public string Value => _valueAdapter.GetValue<string>();

        public void Expand() => _expandAdapter.Expand();

        public void Collapse() => _expandAdapter.Collapse();

        public ExpandCollapseState ExpandCollapseState => _expandAdapter.ExpandCollapseState;

        public string SelectedItem => _selectionAdapter.SelectedItem;

        public List<string> SelectedItems => _selectionAdapter.SelectedItems;

        public List<string> SelectionItems => _selectionAdapter.SelectionItems;

        public bool SelectItemByName(string itemName) => _selectionAdapter.SelectItemByName(itemName);

        public bool SelectItemByValue(string value) => SelectItemByName(value);

        public bool SelectItemByIndex(int index) => _selectionAdapter.SelectItemByIndex(index);
    }
}