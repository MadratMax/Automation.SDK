namespace Automation.Sdk.UIWrappers.Services.Adapters
{
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;
    using Microsoft.Practices.Unity;

    public sealed class ControlAdapterFactory
    {
        public TAdapter Create<TAdapter>([NotNull] IElement element)
        {
            var container = ContainerProvider.Container;

            PlatformContextType platform = element.PlatformContextType;

            return container.Resolve<IFactory<TAdapter, IElement>>().Create(element, platform.ToString());
        }
    }
}