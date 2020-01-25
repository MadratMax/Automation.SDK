namespace Automation.Sdk.UIWrappers.Adapters
{
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using JetBrains.Annotations;

    public interface IFocusValidator
    {
        bool ElementIsFocused([NotNull] string expectedAid);

        void VerifyFocusedElement([NotNull] Element element);

        void VerifyFocusedElementName([NotNull] string expectedName);

        void VerifyFocusedElement([NotNull] string expectedAutomationId);
    }
}