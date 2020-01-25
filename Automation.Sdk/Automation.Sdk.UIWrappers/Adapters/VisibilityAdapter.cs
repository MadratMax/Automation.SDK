namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Windows;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using FluentAssertions;
    using JetBrains.Annotations;

    [UsedImplicitly]
    internal sealed class VisibilityAdapter : Element, IVisibilityAdapter
    {
        public VisibilityAdapter(AutomationElement automationElement)
            : base (automationElement)
        {
            automationElement.Should().NotBeNull();
        }

        public bool IsVisible => !IsOffscreen && BoundingRectangle != Rect.Empty;

        public override string ToString()
        {
            return $"{ControlType.ProgrammaticName}: AutomationID={AutomationId}; Name={Name}";
        }
    }
}