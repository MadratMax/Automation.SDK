namespace Automation.Sdk.UIWrappers.Adapters.Operators
{
    using System.Linq;
    using Automation.Sdk.UIWrappers.ControlDrivers;

    internal sealed class WinFormListItemFinder : IListItemFinder
    {
        public UIAListItem FindSelectionItem(UIAListItem[] items, string choiceName)
        {
            return items.SingleOrDefault(choice => choice.Name.Equals(choiceName));
        }
    }
}