namespace Automation.Sdk.UIWrappers.Adapters.Web
{
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IEnableAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Web)]
    internal class WebEnableAdapter : WebAdapterBase, IEnableAdapter
    {
        public WebEnableAdapter(
            IElement element,
            ControlAdapterFactory controlAdapterFactory)
            : base(element, controlAdapterFactory)
        {
        }

        public bool IsEnabled => WebElement.Enabled;
    }
}