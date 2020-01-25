namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Services;

    internal sealed class FocusValidator : IFocusValidator
    {
        public bool ElementIsFocused(string expectedAid)
        {
            return AutomationElement.FocusedElement.Current.AutomationId.Equals(expectedAid);
        }

        public void VerifyFocusedElement(Element expectedElement)
        {
            VerifyFocusedElement(expectedElement.AutomationId);
        }

        public void VerifyFocusedElement(string expectedAutomationId)
        {
            expectedAutomationId.ShouldBecome(
                x => x.Equals(AutomationElement.FocusedElement.Current.AutomationId),
                true,
                () => $"Wrong element is in focus. Expected: {expectedAutomationId}. Actual {AutomationElement.FocusedElement.Current.AutomationId}");
        }

        public void VerifyFocusedElementName(string expectedName)
        {
            expectedName.ShouldBecome(
                x => x.Equals(AutomationElement.FocusedElement.Current.Name),
                true,
                () => $"Wrong element is in focus. Expected: {expectedName}. Actual {AutomationElement.FocusedElement.Current.Name}");
        }
    }
}