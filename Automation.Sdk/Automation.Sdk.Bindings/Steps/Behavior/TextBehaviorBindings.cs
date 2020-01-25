namespace Automation.Sdk.Bindings.Steps.Behavior
{
    using System;
    using System.Collections.Generic;
    using Automation.Sdk.Bindings.Enums;
    using Automation.Sdk.UIWrappers.Adapters;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade;
    using FluentAssertions;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Steps for Text (ususally Labels) behavior
    /// </summary>
    [Binding]
    public sealed class TextBehaviorBindings : BehaviorBindings<ITextAdapter>
    {
        public TextBehaviorBindings(
            [NotNull] ControlFacade controlFacade,
            [NotNull] ControlAdapterFactory controlAdapterFactory)
            : base(controlFacade, controlAdapterFactory)
        {
        }

        [Then(@"(.*) text should (be|become) ""(.*)""")]
        [UsedImplicitly]
        public void ShouldHaveText([CanBeNull] ITextAdapter textAdapter, AssertPredicate predicate, [NotNull] FormattedString text)
        {
            CheckAdapterForNull(textAdapter);

            var visibilityAdapter = textAdapter.GetAdapter<IVisibilityAdapter>();
            visibilityAdapter.IsVisible.Should().BeTrue($@"element ""{visibilityAdapter}"" should be visible");

            textAdapter.ShouldBe(x => x.DisplayText, predicate, text.ToString(), $@"element ""{textAdapter}"" text should {predicate} ""{text}""");
        }

        [Then(@"(.*) text should be")]
        [UsedImplicitly]
        public void ShouldHaveText([CanBeNull] ITextAdapter textAdapter, [NotNull, ItemNotNull] IEnumerable<string> text)
        {
            CheckAdapterForNull(textAdapter);

            var visibilityAdapter = textAdapter.GetAdapter<IVisibilityAdapter>();
            visibilityAdapter.IsVisible.Should().BeTrue($@"element ""{visibilityAdapter}"" should be visible");

            textAdapter.DisplayText
                 .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                 .Should()
                 .BeEquivalentTo(text);
        }
    }
}