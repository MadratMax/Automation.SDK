namespace Automation.Sdk.UIWrappers.Adapters.Desktop
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters.Desktop.Operators;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Services.Container;
    using Automation.Sdk.UIWrappers.Services.SearchEngines;
    using FluentAssertions;
    using JetBrains.Annotations;
    using System.Collections.Generic;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.Enums;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(ISelectionAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Desktop)]
    internal sealed class SelectionAdapter : Element, ISelectionAdapter
    {
        private readonly LegacyControlsSearchEngineService _legacyControlsSearchEngineService;
        private readonly IFactory<IListItemFinder> _listItemFinderFactory;

        public SelectionAdapter([NotNull] IElement element, 
                                [NotNull] LegacyControlsSearchEngineService legacyControlsSearchEngineService,
                                [NotNull] IFactory<IListItemFinder> listItemFinderFactory) 
            : base(element)
        {
            _legacyControlsSearchEngineService = legacyControlsSearchEngineService;
            _listItemFinderFactory = listItemFinderFactory;
        }

        public void Select()
        {
            SelectionItemPattern selectionItemPattern = GetPattern<SelectionItemPattern>(SelectionItemPattern.Pattern);
            selectionItemPattern.Select();
        }

        public string SelectedItem
        {
            get
            {
                var selectedItems = SelectedItems;

                if (selectedItems.Count == 0)
                {
                    return string.Empty;
                }

                return selectedItems[0];
            }
        }

        public List<string> SelectedItems
        {
            get
            {
                SelectionPattern selectionPattern = GetPattern<SelectionPattern>(SelectionPattern.Pattern);
                var selection = selectionPattern.Current.GetSelection();
                var result = selection.Select(x => x.Current.Name).ToList();

                return result;
            }
        }

        public List<string> SelectionItems
        {
            get
            {
                if (AutomationElement.Current.ControlType == ControlType.ComboBox)
                { 
                    var expandAdapter = GetAdapter<IExpandAdapter>();
                    var focusAdapter = GetAdapter<IFocusAdapter>();

                    focusAdapter.SetFocus();
                    expandAdapter.Expand();

                    var items = _legacyControlsSearchEngineService.FindAll<UIAListItem>(AutomationElement, TreeScope.Descendants);

                    expandAdapter.Collapse();
                    return items.Select(x => x.GetText()).ToList();
                }
                else
                {
                    var items = _legacyControlsSearchEngineService.FindAll<UIAListItem>(AutomationElement, TreeScope.Descendants);
                    return items.Select(x => x.GetText()).ToList();
                }
            }
        }

        public bool SelectItemByName(string name)
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

                var item = PickItem(AutomationElement, name);
                item.Should().NotBeNull($@"combo box ""{this}"" should contain item ""{name}""");

                item.ScrollIntoView();
                Thread.Sleep(100);
                item.ClickOn();
                expandAdapter.Collapse();

                return true;
            }
            else if(AutomationElement.Current.ControlType == ControlType.List)
            {
                var item = PickItem(AutomationElement, name);
                item.Should().NotBeNull($@"list box ""{this}"" should contain item ""{name}""");

                item.ScrollIntoView();
                item.ClickOn();

                if (item.IsSelected)
                {
                    return true;
                }
            }

            return false;
        }

        public bool SelectItemByIndex(int index)
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

                var items = _legacyControlsSearchEngineService.FindAll<UIAListItem>(AutomationElement, TreeScope.Descendants);
                items.Length.Should().BeGreaterOrEqualTo(index + 1, $"List container {AutomationElement} should have at least {index + 1} items to select");

                var item = items[index];
                item.Should().NotBeNull($"combo box {Name} should contain item with index {index}");

                item.ScrollIntoView();
                Thread.Sleep(100);
                item.ClickOn();
                expandAdapter.Collapse();

                return true;
            }
            else if (AutomationElement.Current.ControlType == ControlType.List)
            {
                var items = _legacyControlsSearchEngineService.FindAll<UIAListItem>(AutomationElement, TreeScope.Descendants);
                items.Length.Should().BeGreaterOrEqualTo(index + 1, $"List container {AutomationElement} should have at least {index + 1} items to select");

                var item = items[index];
                item.Should().NotBeNull($"combo box {Name} should contain item with index {index}");

                item.ScrollIntoView();
                item.ClickOn();

                if (item.IsSelected)
                {
                    return true;
                }
            }

            return false;
        }

        public bool SelectItemByValue(string value)
        {
            return SelectItemByName(value);
        }

        private UIAListItem PickItem(AutomationElement listContainer, string choiceName)
        {
            var items = _legacyControlsSearchEngineService.FindAll<UIAListItem>(listContainer, TreeScope.Descendants);
            items.Length.Should().BeGreaterThan(0, $"List container {listContainer} should have items to select");

            var finder = _listItemFinderFactory.Create(listContainer.Current.FrameworkId);
            finder.Should().NotBeNull($"Not implemented frameworkId {listContainer.Current.FrameworkId}");

            return finder.FindSelectionItem(items, choiceName);
        }
    }
}