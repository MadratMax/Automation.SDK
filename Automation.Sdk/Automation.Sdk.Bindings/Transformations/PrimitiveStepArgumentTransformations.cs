namespace Automation.Sdk.Bindings.Transformations
{
    using Automation.Sdk.UIWrappers.Services;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    [Binding]
    [UsedImplicitly]
    public sealed class PrimitiveStepArgumentTransformations
    {
        [StepArgumentTransformation(@"(visible|not visible)")]
        [UsedImplicitly]
        public bool TransformVisibility([NotNull] string visibility)
        {
            return visibility == "visible";
        }

        [StepArgumentTransformation(@"(selected|not selected)")]
        [UsedImplicitly]
        public bool TransformSelected([NotNull] string selection)
        {
            return selection == "selected";
        }

        [StepArgumentTransformation]
        [UsedImplicitly]
        public FormattedString GetFormattedString(string input)
        {
            return new FormattedString(input);
        }
    }
}