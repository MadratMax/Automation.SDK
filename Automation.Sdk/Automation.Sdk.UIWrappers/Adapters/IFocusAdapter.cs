namespace Automation.Sdk.UIWrappers.Adapters
{
    public interface IFocusAdapter : IAdapter
    {
        void SetFocus();

        bool IsFocused { get; }
    }
}