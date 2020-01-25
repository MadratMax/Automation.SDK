namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;

    public class UIAWindow : Element
    {
        public UIAWindow(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        #region Transform Pattern
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

        public void Rotate(double degree)
        {
            TransformPattern transformPattern = GetPattern<TransformPattern>(TransformPattern.Pattern);
            transformPattern.Rotate(degree);
        }

        public bool CanMove => GetProperty<bool>(TransformPattern.CanMoveProperty);

        public bool CanResize => GetProperty<bool>(TransformPattern.CanResizeProperty);

        public bool CanRotate => GetProperty<bool>(TransformPattern.CanRotateProperty);

        #endregion

        #region Window Pattern
        public void SetWindowVisualState(WindowVisualState state)
        {
            WindowPattern windowPattern = GetPattern<WindowPattern>(WindowPattern.Pattern);
            windowPattern.SetWindowVisualState(state);
        }

        public void Close()
        {
            WindowPattern windowPattern = GetPattern<WindowPattern>(WindowPattern.Pattern);
            windowPattern.Close();
        }

        public bool CanMaximize => GetProperty<bool>(WindowPattern.CanMaximizeProperty);

        public bool CanMinimize => GetProperty<bool>(WindowPattern.CanMinimizeProperty);

        public bool IsModal
        {
            get
            {
                try
                {
                    // all windows open on desktop are not having this property and throwing a invalid cast exception.
                    return GetProperty<bool>(WindowPattern.IsModalProperty);
                }
                catch
                {
                    return false;
                }
            }
        }

        public string Title => AutomationElement.Current.Name;

        public bool IsTopmost => GetProperty<bool>(WindowPattern.IsTopmostProperty);

        public WindowVisualState WindowVisualState => GetProperty<WindowVisualState>(WindowPattern.WindowVisualStateProperty);

        public WindowInteractionState WindowInteractionState => GetProperty<WindowInteractionState>(WindowPattern.WindowInteractionStateProperty);

        #endregion

        public void Move(System.Windows.Point point)
        {
            Move(point.X, point.Y);
        }

        public void MoveToCenterOf(Element element)
        {
            if (element == null)
            {
                element = Root;
            }

            var width = BoundingRectangle.Width;
            var height = BoundingRectangle.Height;
            var centerPoint = element.Center;

            centerPoint.X -= width / 2;
            centerPoint.Y -= height / 2;

            Move(centerPoint);
        }
    }
}
