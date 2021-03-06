namespace Automation.Sdk.UIWrappers.Adapters.Desktop.Operators
{
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using JetBrains.Annotations;

    internal interface IListItemFinder
    {
        [CanBeNull] UIAListItem FindSelectionItem([NotNull] UIAListItem[] items, [NotNull] string choiceName);
    }
}