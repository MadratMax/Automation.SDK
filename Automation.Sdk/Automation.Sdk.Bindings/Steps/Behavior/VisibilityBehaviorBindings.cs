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
    /// Steps for all Visibility status behavior
    /// </summary>
    [Binding]
    public sealed class VisibilityBehaviorBindings : BehaviorBindings<IVisibilityAdapter>
    {
        public VisibilityBehaviorBindings(
            [NotNull] ControlFacade controlFacade, 
            [NotNull] ControlAdapterFactory controlAdapterFactory)
            : base(controlFacade, controlAdapterFactory)
        {
        }

        [Then(@"(.*) should (be|become) (visible|not visible)")]
        [UsedImplicitly]
        public void CheckElementVisibility([CanBeNull] IVisibilityAdapter visibilityAdapter, AssertPredicate predicate, bool visibility)
        {
            ElementShouldBeVisible(visibilityAdapter, predicate, visibility);
        }

        private void ElementShouldBeVisible([CanBeNull] IVisibilityAdapter visibilityAdapter, AssertPredicate predicate, bool visibility)
        {
            if (visibility)
            {
                CheckAdapterForNull(visibilityAdapter);
                visibilityAdapter.ShouldBe(x => x.IsContainsElement && x.IsVisible, predicate, true, $@"element ""{visibilityAdapter}"" should {predicate} visible");
            }
            else
            {
                visibilityAdapter?.ShouldBe(x => x.IsContainsElement && x.IsVisible, predicate, false, $@"element ""{visibilityAdapter}"" should {predicate} not visible");
            }
        }
    }
}