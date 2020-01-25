namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Services;

    public class UIAPane : Element
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UIAPane"/> class.
        /// </summary>
        /// <param name="automationElement">The elm.</param>
        public UIAPane(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public bool ScrollTo(Element element, int paddingTop, int paddingLeft, int paddingBottom, int paddingRight, int maxTime)
        {
            var scrollController = GetScrollPattern();

            Func<bool> elementInViewport = () =>
            {
                if (element.BoundingRectangle.Bottom + paddingBottom > this.BoundingRectangle.Bottom)
                {
                    scrollController.ScrollVertical(ScrollAmount.SmallIncrement);
                }
                else if (element.BoundingRectangle.Top - paddingTop < this.BoundingRectangle.Top)
                {
                    scrollController.ScrollVertical(ScrollAmount.SmallDecrement);
                }

                if (element.BoundingRectangle.Right + paddingRight > this.BoundingRectangle.Right)
                {
                    scrollController.ScrollHorizontal(ScrollAmount.SmallIncrement);
                }
                else if (element.BoundingRectangle.Left - paddingLeft < this.BoundingRectangle.Left)
                {
                    scrollController.ScrollHorizontal(ScrollAmount.SmallDecrement);
                }

                return element.BoundingRectangle.Bottom + paddingBottom <= this.BoundingRectangle.Bottom &&
                       element.BoundingRectangle.Top - paddingTop >= this.BoundingRectangle.Top &&
                       element.BoundingRectangle.Right + paddingRight <= this.BoundingRectangle.Right &&
                       element.BoundingRectangle.Left - paddingLeft >= this.BoundingRectangle.Left;
            };

            return Methods.WaitUntil(elementInViewport, maxTime, 10);
        }

        #region Dock Pattern
        public void SetDockPosition(DockPosition position)
        {
            DockPattern dockPattern = GetPattern<DockPattern>(DockPattern.Pattern);
            dockPattern.SetDockPosition(position);
        }

        public DockPosition DockPosition => GetProperty<DockPosition>(DockPattern.DockPositionProperty);

        #endregion

        #region Transform Pattern
        public void Rotate(double degree)
        {
            TransformPattern transformPattern = GetPattern<TransformPattern>(TransformPattern.Pattern);
            transformPattern.Rotate(degree);
        }

        public bool CanRotate => GetProperty<bool>(TransformPattern.CanRotateProperty);

        #endregion

        #region Scroll Pattern
        ///--------------------------------------------------------------------
        /// <summary>
        /// Obtains a ScrollPattern control pattern from an 
        /// automation element.
        /// </summary>
        /// <returns>
        /// A ScrollPattern object.
        /// </returns>
        ///--------------------------------------------------------------------
        public ScrollPattern GetScrollPattern()
        {
            ScrollPattern scrollPattern;

            try
            {
                scrollPattern =
                    AutomationElement.GetCurrentPattern(
                    ScrollPattern.Pattern)
                    as ScrollPattern;
            }
            catch (InvalidOperationException)
            {
                // Object doesn't support the ScrollPattern control pattern
                return null;
            }

            return scrollPattern;
        }

        /// <summary>
        /// Vertical scroll percentage
        /// </summary>
        /// <returns>vertical scroll percentage</returns>
        public double VerticalScrollPercent => GetScrollPattern().Current.VerticalScrollPercent;

        /// <summary>
        /// VerticalViewSize
        /// </summary>
        /// <returns>Vertical View Size in percentage</returns>
        public double VerticalViewSize => GetScrollPattern().Current.VerticalViewSize;

        /// <summary>
        /// HorizontalViewSize
        /// </summary>
        /// <returns>Horizontal View Size in percentage</returns>
        public double HorizontalViewSize => GetScrollPattern().Current.HorizontalViewSize;

        /// <summary>
        /// Horizontal scroll percentage
        /// </summary>
        /// <returns>horizontal scroll percentage</returns>
        public double HorizontalScrollPercent => GetScrollPattern().Current.HorizontalScrollPercent;

        ///--------------------------------------------------------------------
        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to horizontally scroll the requested amount.
        /// </summary>
        /// <param name="hScrollAmount">
        /// The requested horizontal scroll amount.
        /// </param>
        ///--------------------------------------------------------------------
        public void ScrollElementHorizontally(ScrollAmount hScrollAmount)
        {
            ScrollPattern scrollPattern = GetScrollPattern();

            if (scrollPattern == null)
            {
                return;
            }

            if (!scrollPattern.Current.HorizontallyScrollable)
            {
                return;
            }

            try
            {
                scrollPattern.ScrollHorizontal(hScrollAmount);
            }
            catch (InvalidOperationException)
            {
                // Control not able to scroll in the direction requested;
                // when scrollable property of that direction is False
                // TO DO: error handling.
            }
            catch (ArgumentException)
            {
                // If a control supports SmallIncrement values exclusively 
                // for horizontal or vertical scrolling but a LargeIncrement 
                // value (NaN if not supported) is passed in.
                // TO DO: error handling.
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to vertically scroll the requested amount.
        /// </summary>
        /// <param name="vScrollAmount">
        /// The requested vertical scroll amount.
        /// </param>
        ///--------------------------------------------------------------------
        public void ScrollElementVertically(ScrollAmount vScrollAmount)
        {
            ScrollPattern scrollPattern = GetScrollPattern();

            if (scrollPattern == null)
            {
                return;
            }

            if (!scrollPattern.Current.VerticallyScrollable)
            {
                return;
            }

            try
            {
                scrollPattern.ScrollVertical(vScrollAmount);
            }
            catch (InvalidOperationException)
            {
                // Control not able to scroll in the direction requested;
                // when scrollable property of that direction is False
                // TO DO: error handling.
            }
            catch (ArgumentException)
            {
                // If a control supports SmallIncrement values exclusively 
                // for horizontal or vertical scrolling but a LargeIncrement 
                // value (NaN if not supported) is passed in.
                // TO DO: error handling.
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to vertically scroll the requested amount.
        /// </summary>
        /// <param name="intPercent">
        /// The requested percent to scroll up or down ( 0 - Home, 100 - End)
        /// </param>
        ///--------------------------------------------------------------------
        public void ScrollElementVertically(int intPercent)
        {
            ScrollPattern scrollPattern = GetScrollPattern();

            if (scrollPattern == null)
            {
                return;
            }

            if (!scrollPattern.Current.VerticallyScrollable)
            {
                return;
            }

            try
            {
                scrollPattern.SetScrollPercent(-1, intPercent);
            }
            catch (InvalidOperationException)
            {
                // Control not able to scroll in the direction requested;
                // when scrollable property of that direction is False
                // TO DO: error handling.
            }
            catch (ArgumentException)
            {
                // If a control supports SmallIncrement values exclusively 
                // for horizontal or vertical scrolling but a LargeIncrement 
                // value (NaN if not supported) is passed in.
                // TO DO: error handling.
            }
        }

        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to vertically scroll the requested amount. 
        /// </summary>
        /// <param name="percent">The requested percent to scroll up or down ( 0 - Home, 100 - End)</param>
        public void ScrollElementVertically(double percent)
        {
            ScrollElementVertically(Convert.ToInt16(percent));
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to scroll to the 'home' position.
        /// </summary>
        ///--------------------------------------------------------------------
        public void ScrollHome()
        {
            ScrollPattern scrollPattern = GetScrollPattern();

            if (scrollPattern == null)
            {
                return;
            }

            try
            {
                scrollPattern.SetScrollPercent(-1, 0);
            }
            catch (InvalidOperationException)
            {
                // Control not able to scroll in the direction requested;
                // when scrollable property of that direction is False
                // TO DO: error handling.
            }
            catch (ArgumentOutOfRangeException)
            {
                // A value greater than 100 or less than 0 is passed in 
                // (except -1 which is equivalent to NoScroll).
                // TO DO: error handling.
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to scroll to the 'end' position.
        /// </summary>
        ///--------------------------------------------------------------------
        public void ScrollEnd()
        {
            ScrollPattern scrollPattern = GetScrollPattern();

            if (scrollPattern == null)
            {
                return;
            }

            try
            {
                scrollPattern.SetScrollPercent(-1, 100);
            }
            catch (InvalidOperationException)
            {
                // Control not able to scroll in the direction requested;
                // when scrollable property of that direction is False
                // TO DO: error handling.
            }
            catch (ArgumentOutOfRangeException)
            {
                // A value greater than 100 or less than 0 is passed in 
                // (except -1 which is equivalent to NoScroll).
                // TO DO: error handling.
            }
        }
        #endregion
    }
}