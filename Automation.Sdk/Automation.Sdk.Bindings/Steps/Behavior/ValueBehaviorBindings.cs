namespace Automation.Sdk.Bindings.Steps.Behavior
{
    using System.Collections.Generic;
    using Automation.Sdk.Bindings.Dto;
    using Automation.Sdk.Bindings.Enums;
    using Automation.Sdk.UIWrappers.Adapters;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade;
    using FluentAssertions;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Steps for all Value behavior
    /// </summary>
    [Binding]
    public sealed class ValueBehaviorBindings : BehaviorBindings<IValueAdapter>
    {
        public ValueBehaviorBindings(
            [NotNull] ControlFacade controlFacade, 
            [NotNull] ControlAdapterFactory controlAdapterFactory)
            : base(controlFacade, controlAdapterFactory)
        {
        }

        [Then(@"(.*) value should (be|become) ""(.*)""")]
        public void CheckValue(
            [CanBeNull] IValueAdapter valueAdapter, 
            AssertPredicate predicate, 
            [NotNull] FormattedString expectedValue)
        {
            CheckAdapterForNull(valueAdapter);
            valueAdapter.ShouldBe(x => x.Value,
                              predicate,
                              expectedValue.ToString(),
                              $@"element ""{valueAdapter}"" value should {predicate} ""{expectedValue}""");
        }

        [Then(@"following controls should have the following values:")]
        [UsedImplicitly]
        public void CheckTextBoxesCollection([NotNull, ItemNotNull] List<ControlValue> values)
        {
            foreach (var value in values)
            {
                Then($@"{value.Control} value should be ""{value.Value}""");
            }
        }

        [When(@"value ""(.*)"" is set into (.*)")]
        public void SetValue([NotNull] string value, [CanBeNull] IValueAdapter valueAdapter)
        {
            CheckAdapterForNull(valueAdapter);
            valueAdapter.SetValue(value);
        }

        [When(@"the following values are set to controls:")]
        [UsedImplicitly]
        public void SetMultipleValues([NotNull, ItemNotNull] List<ControlValue> values)
        {
            foreach (var textBoxValue in values)
            {
                When($@"value ""{textBoxValue.Value}"" is set into {textBoxValue.Control}");
            }
        }
    }
}