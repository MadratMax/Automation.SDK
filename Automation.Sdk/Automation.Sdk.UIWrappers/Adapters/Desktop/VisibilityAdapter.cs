namespace Automation.Sdk.UIWrappers.Adapters.Desktop
{
    using System.Windows;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Container;
    using FluentAssertions;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IVisibilityAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Desktop)]
    internal sealed class VisibilityAdapter : Element, IVisibilityAdapter
    {
        public VisibilityAdapter(IElement element)
            : base (element)
        {
            element.Should().NotBeNull();
        }

        public bool IsVisible => UiElement != null && !IsOffscreen && BoundingRectangle != Rect.Empty;
    }
}