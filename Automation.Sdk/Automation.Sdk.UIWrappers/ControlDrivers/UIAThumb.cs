namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;

    public class UIAThumb : Element
    {
        public UIAThumb(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public void Move(double x, double y)
        {
            TransformPattern transformPattern = GetPattern<TransformPattern>(TransformPattern.Pattern);
            transformPattern.Move(x, y);
        }

        public void Resize(double width, double height)
        {
            TransformPattern transformPattern = GetPattern<TransformPattern>(TransformPattern.Pattern);
            transformPattern.Resize(width, height);
        }

        public void Rotate(double degrees)
        {
            TransformPattern transformPattern = GetPattern<TransformPattern>(TransformPattern.Pattern);
            transformPattern.Rotate(degrees);
        }

        public UIAImage[] Images => FindAll<UIAImage>();

        public bool CanMove => GetProperty<bool>(TransformPattern.CanMoveProperty);

        public bool CanResize => GetProperty<bool>(TransformPattern.CanResizeProperty);

        public bool CanRotate => GetProperty<bool>(TransformPattern.CanRotateProperty);
    }
}