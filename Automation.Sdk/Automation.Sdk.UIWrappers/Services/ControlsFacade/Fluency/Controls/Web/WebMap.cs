namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Automation.Sdk.UIWrappers.Aspects;
    using OpenQA.Selenium;

    [AutoRegister]
    public class WebMap : IWebMap
    {
        private readonly Dictionary<string, WebControlMap> _webMap;
        private readonly List<ILocatorProvider> _providers;
        private string _currentParent;

        public WebMap()
        {
            _currentParent = string.Empty;
            _webMap = new Dictionary<string, WebControlMap>();
            _providers = new List<ILocatorProvider>();
        }

        public string CurrentParent
        {
            get { return _currentParent; }
            set { _currentParent = value; }
        }

        public virtual IWebMap Add(string caption, By locator, string parent)
        {
            _webMap.Add(caption, new WebControlMap(locator, parent));

            return this;
        }

        public ParentDefiner DefineChildren()
        {
            var currentParent = _currentParent;
            _currentParent = _webMap.Last().Key;
            return new ParentDefiner(this, currentParent);
        }

        public IWebMap Add(string caption, By locator)
        {
            Add(caption, locator, _currentParent);
            return this;
        }

        public IWebMap Add(ILocatorProvider locatorProvider)
        {
            _providers.Add(locatorProvider);
            return this;
        }

        public WebControlMap Find(string caption)
        {
            if (!_webMap.ContainsKey(caption))
            {
                var provider = _providers
                    .OrderBy(x => x.Priority)
                    .FirstOrDefault(x => x.IsMatch(caption));
                
                if (provider == null)
                {
                    throw new Exception($"No defined control with caption {caption}");
                }

                Add(caption, provider.GetLocator(caption), provider.GetParentCaption(caption));
            }

            return _webMap[caption];
        }
    }
}