namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Web
{
    using OpenQA.Selenium;

    public class WebControlMap
    {
        private readonly By _locator;
        private readonly string _parent;

        public WebControlMap(By locator, string parent)
        {
            _locator = locator;
            _parent = parent;
        }

        public string Parent => _parent;

        public By Locator => _locator;
    }
}