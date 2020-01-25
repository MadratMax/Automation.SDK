namespace Automation.Sdk.Bindings.Steps.Behavior
{
    using Automation.Sdk.UIWrappers.Adapters;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade;
    using FluentAssertions;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Steps for all Click behavior
    /// </summary>
    [Binding]
    public sealed class ClickBehaviorBindings : BehaviorBindings<IClickAdapter>
    {
        public ClickBehaviorBindings(
            [NotNull] ControlFacade controlFacade, 
            [NotNull] ControlAdapterFactory controlAdapterFactory)
            : base(controlFacade, controlAdapterFactory)
        {
        }

        [When(@"user clicks on (.*)")]
        [UsedImplicitly]
        public void ClickOnElement([CanBeNull] IClickAdapter clickAdapter)
        {
            CheckAdapterForNull(clickAdapter);
            clickAdapter.ClickOnSelf();
        }

        [When(@"user right clicks on (.*)")]
        [UsedImplicitly]
        public void RightClickOnElement([CanBeNull] IClickAdapter clickAdapter)
        {
            CheckAdapterForNull(clickAdapter);
            clickAdapter.ClickOnSelf(MouseButton.Right);
        }

        [When(@"user double-clicks on (.*)")]
        [UsedImplicitly]
        public void DoubleClickOnElement([CanBeNull] IClickAdapter clickAdapter)
        {
            CheckAdapterForNull(clickAdapter);
            clickAdapter.DoubleClickOnSelf();
        }
    }
}