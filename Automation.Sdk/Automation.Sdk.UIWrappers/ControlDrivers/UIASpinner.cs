namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;

    public class UIASpinner : Element
    {
        public UIASpinner(AutomationElement automationElement)
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
    }
}
