namespace Automation.Sdk.UIWrappers.Adapters.Desktop.Operators
{
    using System.Linq;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IListItemFinder), RegistrationType = RegistrationType.Instanced, RegistrationName = SpecIds.WinFormSignature)]
    internal sealed class WinFormListItemFinder : IListItemFinder
    {
        public UIAListItem FindSelectionItem(UIAListItem[] items, string choiceName)
        {
            return items.SingleOrDefault(choice => choice.Name.Equals(choiceName));
        }
    }
}