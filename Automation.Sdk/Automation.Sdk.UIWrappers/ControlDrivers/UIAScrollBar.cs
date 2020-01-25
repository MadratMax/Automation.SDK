namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;

    public class UIAScrollBar : Element
    {
        public UIAScrollBar(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        #region RangeValue Pattern
        public void SetValue(double value)
        {
            RangeValuePattern rangeValuePattern = GetPattern<RangeValuePattern>(RangeValuePattern.Pattern);
            rangeValuePattern.SetValue(value);
        }

        public bool IsReadOnly => GetProperty<bool>(RangeValuePattern.IsReadOnlyProperty);

        public double LargeChange => GetProperty<double>(RangeValuePattern.LargeChangeProperty);

        public double SmallChange => GetProperty<double>(RangeValuePattern.SmallChangeProperty);

        public double Maximum => GetProperty<double>(RangeValuePattern.MaximumProperty);

        public double Minimum => GetProperty<double>(RangeValuePattern.MinimumProperty);

        public double Value => GetProperty<double>(RangeValuePattern.ValueProperty);

        #endregion

        #region Scroll Pattern
        public void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount)
        {
            ScrollPattern scrollPattern = GetPattern<ScrollPattern>(ScrollPattern.Pattern);
            scrollPattern.Scroll(horizontalAmount, verticalAmount);
        }

        public void SetScrollPercent(double horizontalPercent, double verticalPercent)
        {
            ScrollPattern scrollPattern = GetPattern<ScrollPattern>(ScrollPattern.Pattern);
            scrollPattern.SetScrollPercent(horizontalPercent, verticalPercent);
        }

        #endregion
    }
}
