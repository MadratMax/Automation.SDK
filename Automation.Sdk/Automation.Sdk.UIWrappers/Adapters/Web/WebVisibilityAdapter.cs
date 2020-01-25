namespace Automation.Sdk.UIWrappers.Adapters.Web
{
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IVisibilityAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Web)]
    internal class WebVisibilityAdapter : WebAdapterBase, IVisibilityAdapter
    {
        public WebVisibilityAdapter(
            IElement element,
            ControlAdapterFactory controlAdapterFactory)
            : base(element, controlAdapterFactory)
        {
        }

        public bool IsVisible => WebElement != null && WebElement.Displayed;
    }
}