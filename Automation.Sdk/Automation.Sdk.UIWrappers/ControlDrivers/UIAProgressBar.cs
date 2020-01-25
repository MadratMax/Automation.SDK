namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;

    public class UIAProgressBar : Element
    {
        public UIAProgressBar(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        // Gets the maximum range value supported by the control. 
        public double Maximum
        {
            get 
            {
                RangeValuePattern rangeValuePattern = GetPattern<RangeValuePattern>(RangeValuePattern.Pattern); 
                return rangeValuePattern.Current.Maximum; 
            }
        }

        // Gets the minimum range value supported by the control. 
        public double Mininum
        {
            get
            {
                RangeValuePattern rangeValuePattern = GetPattern<RangeValuePattern>(RangeValuePattern.Pattern);
                return rangeValuePattern.Current.Minimum;
            }
        }

        // Gets the value of the control.  
        public double Value
        {
            get
            {
                RangeValuePattern rangeValuePattern = GetPattern<RangeValuePattern>(RangeValuePattern.Pattern);
                return rangeValuePattern.Current.Value;
            }
        }

        // Gets the value that is added to or subtracted from the Value  property when a small change is made, such as with an arrow key.
        public double SmallChange
        {
            get
            {
                RangeValuePattern rangeValuePattern = GetPattern<RangeValuePattern>(RangeValuePattern.Pattern);
                return rangeValuePattern.Current.SmallChange;
            }
        }

        // Gets the value that is added to or subtracted from the Value  property when a large change is made, such as with the PAGE DOWN key.
        public double LargeChange
        {
            get
            {
                RangeValuePattern rangeValuePattern = GetPattern<RangeValuePattern>(RangeValuePattern.Pattern);
                return rangeValuePattern.Current.LargeChange;
            }
        }
    }
}
