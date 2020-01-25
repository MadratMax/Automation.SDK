namespace Automation.Sdk.Bindings.Steps.Behavior
{
    using Automation.Sdk.Bindings.Enums;
    using Automation.Sdk.UIWrappers.Adapters;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade;
    using FluentAssertions;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Steps for all Focus status behavior
    /// </summary>
    [Binding]
    public sealed class FocusBehaviorBindings : BehaviorBindings<IFocusAdapter>
    {
        public FocusBehaviorBindings(
            [NotNull] ControlFacade controlFacade, 
            [NotNull] ControlAdapterFactory controlAdapterFactory)
            : base(controlFacade, controlAdapterFactory)
        {
        }

        [When(@"user sets focus into (.*)")]
        [UsedImplicitly]
        public void SetFocus([CanBeNull] IFocusAdapter focusAdapter)
        {
            CheckAdapterForNull(focusAdapter);
            focusAdapter.SetFocus();
        }

        [Then(@"(.*) should (be|become) focused")]
        [UsedImplicitly]
        public void ShouldBeFocused([CanBeNull] IFocusAdapter focusAdapter, AssertPredicate assertPredicate)
        {
            CheckAdapterForNull(focusAdapter);
            focusAdapter.ShouldBe(x => x.IsFocused, assertPredicate, true, $@"element ""{focusAdapter}"" should {assertPredicate} focused");
        }
    }
}