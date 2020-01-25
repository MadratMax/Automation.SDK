namespace Automation.Sdk.UIWrappers.Adapters.Operators
{
    using System.Linq;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using FluentAssertions;

    internal sealed class WpfListItemFinder : IListItemFinder
    {
        public UIAListItem FindSelectionItem(UIAListItem[] items, string choiceName)
        {
            return items.SingleOrDefault(item => GetItemName(item).Equals(choiceName));
        }

        private string GetItemName(UIAListItem listItem)
        {
            var textControl = listItem.FindAdvanced<UIAText>();
            textControl.Should().NotBeNull($"choice item \"{listItem}\" should have text control");

            return textControl.Name;
        }
    }
}