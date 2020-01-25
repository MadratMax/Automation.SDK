namespace Automation.Sdk.UIWrappers.Adapters.Web
{
    using System.Linq;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.Container;
    using FluentAssertions;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IValueAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Web)]
    internal class WebValueAdapter : WebAdapterBase, IValueAdapter
    {
        public WebValueAdapter(
            IElement element,
            ControlAdapterFactory controlAdapterFactory)
            : base(element, controlAdapterFactory)
        {
        }

        public object Value
        {
            get
            {
                if (WebElement.TagName == "select")
                {
                    var select = new OpenQA.Selenium.Support.UI.SelectElement(WebElement);
                    return string.Join(",", select.AllSelectedOptions.Select(x => x.Text).ToList());
                }

                return WebElement.GetAttribute("value");
            }
        }

        public TValue GetValue<TValue>()
        {
            var value = Value;
            value.Should().BeAssignableTo(typeof(TValue));

            return (TValue)value;
        }

        public void SetValue(string newValue)
        {
            WebElement.Clear();

            var inputAdapter = GetAdapter<IKeyboardInputAdapter>();
            inputAdapter.Type(newValue);
        }
    }
}