namespace Automation.Sdk.UIWrappers.Adapters.Web
{
    using System;
    using System.Collections.Generic;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(ITextAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Web)]
    internal class WebTextAdapter : WebAdapterBase, ITextAdapter
    {
        public WebTextAdapter(
            IElement element,
            ControlAdapterFactory controlAdapterFactory)
            : base(element, controlAdapterFactory)
        {
        }

        public string GetText()
        {
            return WebElement.Text;
        }

        public List<string> GetAllText()
        {
            throw new NotImplementedException();
        }

        public string DisplayText => GetText();
    }
}