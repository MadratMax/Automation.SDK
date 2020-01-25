namespace Automation.Sdk.UIWrappers.Adapters.Desktop.Operators
{
    using System.Linq;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Container;
    using FluentAssertions;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IListItemFinder), RegistrationType = RegistrationType.Instanced, RegistrationName = SpecIds.WPFSignature)]
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