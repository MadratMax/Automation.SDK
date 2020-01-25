namespace Automation.Sdk.UIWrappers.Adapters.Desktop
{
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Container;
    using FluentAssertions;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IEnableAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Desktop)]
    public class EnableAdapter : Element, IEnableAdapter
    {
        public EnableAdapter([NotNull] IElement element)
            : base (element)
        {
            element.Should().NotBeNull();
        }

        // TODO: CAPP-229 Remove IsVisible check here and call VisibilityBehaviorBinding after cache will be implemented
        public new bool IsEnabled => IsVisible && base.IsEnabled;
    }
}