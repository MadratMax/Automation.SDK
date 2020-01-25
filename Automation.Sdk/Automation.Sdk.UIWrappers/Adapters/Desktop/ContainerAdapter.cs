namespace Automation.Sdk.UIWrappers.Adapters.Desktop
{
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IContainerAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Desktop)]
    internal sealed class ContainerAdapter : Element, IContainerAdapter
    {
        private readonly Element _control;

        public ContainerAdapter(IElement element)
            : base(element)
        {
            _control = this;
        }

        public Element Control => _control;
    }
}