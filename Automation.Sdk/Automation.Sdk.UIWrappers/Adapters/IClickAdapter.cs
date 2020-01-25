namespace Automation.Sdk.UIWrappers.Adapters
{
    using Automation.Sdk.UIWrappers.Enums;

    public interface IClickAdapter : IAdapter
    {
        void Click();

        void ClickOnSelf(MouseButton button = MouseButton.Left);

        void DoubleClickOnSelf(MouseButton button = MouseButton.Left);

        bool IsClickable { get; }
    }
}
