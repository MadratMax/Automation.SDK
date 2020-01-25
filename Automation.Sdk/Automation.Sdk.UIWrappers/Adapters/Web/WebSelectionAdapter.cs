namespace Automation.Sdk.UIWrappers.Adapters.Web
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;
    using OpenQA.Selenium.Support.UI;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(ISelectionAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Web)]
    internal class WebSelectionAdapter : WebAdapterBase, ISelectionAdapter
    {
        private readonly SelectElement _selectElement;

        public WebSelectionAdapter(
            IElement element,
            ControlAdapterFactory controlAdapterFactory)
            : base(element, controlAdapterFactory)
        {
            _selectElement = new SelectElement(WebElement);
        }

        public bool IsSelected
        {
            get
            {
                throw new NotImplementedException();
            }
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
                return _selectElement.AllSelectedOptions.Select(x => x.Text).ToList();
            }
        }

        public List<string> SelectionItems
        {
            get
            {
                return _selectElement.Options.Select(x => x.Text).ToList();
            }
        }

        public void Select()
        {
            throw new NotImplementedException();
        }

        public bool SelectItemByIndex(int index)
        {
            _selectElement.SelectByIndex(index);
            return true;
        }

        public bool SelectItemByValue(string value)
        {
            _selectElement.SelectByValue(value);
            return true;
        }

        public bool SelectItemByName(string name)
        {
            _selectElement.SelectByText(name);
            return true;
        }
    }
}