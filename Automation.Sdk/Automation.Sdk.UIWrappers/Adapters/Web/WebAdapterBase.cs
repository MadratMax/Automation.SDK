namespace Automation.Sdk.UIWrappers.Adapters.Web
{
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using OpenQA.Selenium;

    internal class WebAdapterBase : IAdapter
    {
        private readonly ControlAdapterFactory _controlAdapterFactory;
        private readonly IWebElement _webElement;
        private readonly IElement _element;

        public WebAdapterBase(
            IElement element,
            ControlAdapterFactory controlAdapterFactory)
        {
            _controlAdapterFactory = controlAdapterFactory;
            _element = element;
            _webElement = element.UiElement as IWebElement;
        }

        protected IWebElement WebElement => _webElement;

        public TAdapter GetAdapter<TAdapter>() where TAdapter : IAdapter
        {
            return _controlAdapterFactory.Create<TAdapter>(_element);
        }

        public bool IsContainsElement => _webElement != null;

        public override string ToString()
        {
            return _element.FindCommandTitle;
        }
    }
}