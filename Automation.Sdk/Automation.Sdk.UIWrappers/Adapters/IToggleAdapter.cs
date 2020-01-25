namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Windows.Automation;

    public interface IToggleAdapter
    {
        void Toggle();

        bool TogglePatternAvailable { get; }

        ToggleState ToggleState { get; }
    }
}
