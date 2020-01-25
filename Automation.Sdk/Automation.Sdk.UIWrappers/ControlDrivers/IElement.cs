namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using Automation.Sdk.UIWrappers.Enums;

    public interface IElement
    {
        object UiElement { get; }

        string FindCommandTitle { get; }

        PlatformContextType PlatformContextType { get; }
    }
}