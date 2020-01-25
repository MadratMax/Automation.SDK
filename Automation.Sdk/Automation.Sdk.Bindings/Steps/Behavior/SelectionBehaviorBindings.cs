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
    /// Steps for Selection (ComboBox, ListBox types and elements which can have Selected status) behavior
    /// </summary>
    [Binding]
    public sealed class SelectionBehaviorBindings : BehaviorBindings<ISelectionAdapter>
    {
        public SelectionBehaviorBindings(
            [NotNull] ControlFacade controlFacade, 
            [NotNull] ControlAdapterFactory controlAdapterFactory)
            : base(controlFacade, controlAdapterFactory)
        {
        }

        [When(@"user selects item ""(.*)"" in (.*)")]
        [UsedImplicitly]
        public void SelectItem([NotNull] string item, [CanBeNull] ISelectionAdapter selectionAdapter)
        {
            CheckAdapterForNull(selectionAdapter);
            selectionAdapter.SelectItemByName(item);
        }

        [Then(@"(.*) should (be|become) (selected|not selected)")]
        [UsedImplicitly]
        public void CheckSelection([CanBeNull] ISelectionAdapter selectionAdapter, AssertPredicate predicate, bool selected)
        {
            CheckAdapterForNull(selectionAdapter);
            selectionAdapter.ShouldBe(x => x.IsSelected, predicate, true, $@"element ""{selectionAdapter}"" should {predicate} {(selected ? string.Empty : "not ")}selected");
        }
    }
}