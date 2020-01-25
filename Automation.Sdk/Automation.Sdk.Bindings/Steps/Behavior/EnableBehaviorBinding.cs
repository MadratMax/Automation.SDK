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
    /// Steps for all Enabled status behavior
    /// </summary>
    [Binding]
    public sealed class EnableBehaviorBinding : BehaviorBindings<IEnableAdapter>
    {
        public EnableBehaviorBinding(
            [NotNull] ControlFacade controlFacade, 
            [NotNull] ControlAdapterFactory controlAdapterFactory)
            : base(controlFacade, controlAdapterFactory)
        {
        }

        [Then(@"(.*) should (be|become) (enabled|disabled)")]
        [UsedImplicitly]
        public void CheckElementStatus([CanBeNull] IEnableAdapter enableAdapter, AssertPredicate predicate, bool expectedStatus)
        {
            // TODO: CAPP-229 Call VisibilityBehaviorBinding after cache will be implemented
            CheckAdapterForNull(enableAdapter);
            enableAdapter.ShouldBe(
                x => x.IsEnabled,
                predicate,
                expectedStatus,
                $@"element ""{enableAdapter}"" should {predicate} {(expectedStatus ? "enabled" : "disabled")}");
        }

        [StepArgumentTransformation(@"(enabled|disabled)")]
        [UsedImplicitly]
        public bool TransformStatus([NotNull] string propertyValue)
        {
            return propertyValue == "enabled";
        }
    }
}