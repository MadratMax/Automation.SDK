namespace Automation.Sdk.UIWrappers.Adapters
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters.Operators;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Services.Container;
    using Automation.Sdk.UIWrappers.Services.SearchEngines;
    using FluentAssertions;
    using JetBrains.Annotations;

    [UsedImplicitly]
    internal sealed class SelectionAdapter : Element, ISelectionAdapter
    {
        private readonly LegacyControlsSearchEngineService _legacyControlsSearchEngineService;
        private readonly IFactory<IListItemFinder> _listItemFinderFactory;

        public SelectionAdapter([NotNull] AutomationElement element, 
                                [NotNull] LegacyControlsSearchEngineService legacyControlsSearchEngineService,
                                [NotNull] IFactory<IListItemFinder> listItemFinderFactory) 
            : base(element)
        {
            _legacyControlsSearchEngineService = legacyControlsSearchEngineService;
            _listItemFinderFactory = listItemFinderFactory;
        }

        public string Selected
        {
            get
            {
                var selection = GetSelection();
                return selection.Length > 0 ? selection.First().Current.Name : string.Empty;
            }
        }

        public bool CanSelectMultiple => GetProperty<bool>(SelectionPattern.CanSelectMultipleProperty);

        public bool IsSelectionRequired => GetProperty<bool>(SelectionPattern.IsSelectionRequiredProperty);

        public void Select()
        {
            SelectionItemPattern selectionItemPattern = GetPattern<SelectionItemPattern>(SelectionItemPattern.Pattern);
            selectionItemPattern.Select();
        }

        public AutomationElement[] GetSelection()
        {
            SelectionPattern selectionPattern = GetPattern<SelectionPattern>(SelectionPattern.Pattern);
            return selectionPattern.Current.GetSelection();
        }

        public void RemoveFromSelection()
        {
            SelectionItemPattern selectionItemPattern = GetPattern<SelectionItemPattern>(SelectionItemPattern.Pattern);
            selectionItemPattern.RemoveFromSelection();
        }

        public void AddToSelection()
        {
            SelectionItemPattern selectionItemPattern = GetPattern<SelectionItemPattern>(SelectionItemPattern.Pattern);
            selectionItemPattern.AddToSelection();
        }

        public UIAListItem SelectItemByChoiceName(string choiceName)
        {
            if (AutomationElement.Current.ControlType != ControlType.ComboBox 
                && AutomationElement.Current.ControlType != ControlType.List)
            {
                throw new NotImplementedException();
            }

            if (AutomationElement.Current.ControlType == ControlType.ComboBox)
            {
                var expandAdapter = GetAdapter<IExpandAdapter>();
                var focusAdapter = GetAdapter<IFocusAdapter>();

                focusAdapter.SetFocus();
                expandAdapter.Expand();

                var item = PickItem(AutomationElement, choiceName);
                item.Should().NotBeNull($"combo box {Name} should contain item {choiceName}");

                item.ScrollIntoView();
                Thread.Sleep(100);
                item.ClickOn();
                expandAdapter.Collapse();

                return item;
            }
            else if(AutomationElement.Current.ControlType == ControlType.List)
            {
                var item = PickItem(AutomationElement, choiceName);
                item.Should().NotBeNull($"list box {Name} should contain item {choiceName}");

                item.ScrollIntoView();
                item.ClickOn();

                if (item.IsSelected)
                {
                    return item;
                }
            }

            return null;
        }

        public UIAListItem PickItem(AutomationElement listContainer, string choiceName)
        {
            var items = _legacyControlsSearchEngineService.FindAll<UIAListItem>(listContainer, TreeScope.Descendants);
            items.Length.Should().BeGreaterThan(0, $"List container {listContainer} should have items to select");

            var finder = _listItemFinderFactory.Create(listContainer.Current.FrameworkId);
            finder.Should().NotBeNull($"Not implemented frameworkId {listContainer.Current.FrameworkId}");

            return finder.FindSelectionItem(items, choiceName);
        }
    }
}