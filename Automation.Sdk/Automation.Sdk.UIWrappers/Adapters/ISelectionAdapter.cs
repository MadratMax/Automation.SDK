namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Collections.Generic;

    public interface ISelectionAdapter : IAdapter
    {
        void Select();

        bool IsSelected { get; }

        string SelectedItem { get; }

        List<string> SelectedItems { get; }

        List<string> SelectionItems { get; }

        bool SelectItemByName(string name);

        bool SelectItemByValue(string value);

        bool SelectItemByIndex(int index);
    }
}