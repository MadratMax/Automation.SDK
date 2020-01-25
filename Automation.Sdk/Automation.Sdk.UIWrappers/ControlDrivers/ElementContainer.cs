namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using Automation.Sdk.UIWrappers.Enums;

    public class ElementContainer : IElement
    {
        public ElementContainer(
            object element,
            string findCommandTitle,
            PlatformContextType platformContextType)
        {
            UiElement = element;
            PlatformContextType = platformContextType;
            FindCommandTitle = findCommandTitle;
        }

        public object UiElement { get; }

        public string FindCommandTitle { get; }

        public PlatformContextType PlatformContextType { get; }
    }
}