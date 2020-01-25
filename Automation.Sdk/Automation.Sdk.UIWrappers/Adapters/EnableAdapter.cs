namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using FluentAssertions;
    using JetBrains.Annotations;

    public class EnableAdapter : Element, IEnableAdapter
    {
        public EnableAdapter([NotNull] AutomationElement automationElement)
            : base (automationElement)
        {
            automationElement.Should().NotBeNull();
        }

        // TODO: CAPP-229 Remove IsVisible check here and call VisibilityBehaviorBinding after cache will be implemented
        public new bool IsEnabled => IsVisible && base.IsEnabled;
    }
}