namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;
    using Automation.Sdk.UIWrappers.Services;

    public class UIAList : Element
    {
        private readonly ISelectionAdapter _selectionAdapter;

        public UIAList (AutomationElement element)
          : base (element)
        {
            _selectionAdapter = ControlAdapterFactory.Create<ISelectionAdapter>(this);
        }

        public string SelectedItem => _selectionAdapter.SelectedItem;

        public List<string> SelectedItems => _selectionAdapter.SelectedItems;

        public List<string> SelectionItems => _selectionAdapter.SelectionItems;

        public bool SelectItemByName(string itemName) => _selectionAdapter.SelectItemByName(itemName);

        public bool SelectItemByValue(string value) => SelectItemByName(value);

        /// <summary>
        /// Obtains a ScrollPattern control pattern from an 
        /// automation element.
        /// </summary>
        /// <returns>
        /// A ScrollPattern object.
        /// </returns>
        public ScrollPattern GetScrollPattern()
        {
            ScrollPattern scrollPattern;

            try
            {
                scrollPattern = AutomationElement.GetCurrentPattern(ScrollPattern.Pattern) as ScrollPattern;
            }
            catch (InvalidOperationException)
            {
                // Object doesn't support the ScrollPattern control pattern
                return null;
            }

            return scrollPattern;
        }

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

        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to vertically scroll the requested amount.
        /// </summary>
        /// <param name="intPercent">The requested percent to scroll up or down ( 0 - Home, 100 - End)</param>
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

        public bool HorizontallyScrollable => GetProperty<bool>(ScrollPattern.HorizontallyScrollableProperty);

        public double HorizontalScrollPercent => GetProperty<double>(ScrollPattern.HorizontalScrollPercentProperty);

        public double HorizontalViewSize => GetProperty<double>(ScrollPattern.HorizontalViewSizeProperty);

        public bool VerticallyScrollable => GetProperty<bool>(ScrollPattern.VerticallyScrollableProperty);

        public double VerticalScrollPercent => GetProperty<double>(ScrollPattern.VerticalScrollPercentProperty);

        public double VerticalViewSize => GetProperty<double>(ScrollPattern.VerticalViewSizeProperty);

        /// <summary>
        /// Select an item in the list box.
        /// </summary>
        /// <param name="index">Zero-based index of the item to select.</param>
        /// <returns>The UIAListItem if item successfully selected, otherwise null.</returns>
        public bool SelectItemByIndex(int index) => _selectionAdapter.SelectItemByIndex(index);

        public bool ScrollTo(Element e, int paddingTop, int paddingLeft, int paddingBottom, int paddingRight, int maxTime)
        {
            var scrollController = GetScrollPattern();

            Func<bool> elementInViewport = () =>
            {
                if (e.BoundingRectangle.Bottom + paddingBottom > this.BoundingRectangle.Bottom)
                {
                    scrollController.ScrollVertical(ScrollAmount.SmallIncrement);
                }
                else if (e.BoundingRectangle.Top - paddingTop < this.BoundingRectangle.Top)
                {
                    scrollController.ScrollVertical(ScrollAmount.SmallDecrement);
                }

                if (e.BoundingRectangle.Right + paddingRight > this.BoundingRectangle.Right)
                {
                    scrollController.ScrollHorizontal(ScrollAmount.SmallIncrement);
                }
                else if (e.BoundingRectangle.Left - paddingLeft < this.BoundingRectangle.Left)
                {
                    scrollController.ScrollHorizontal(ScrollAmount.SmallDecrement);
                }

                return e.BoundingRectangle.Bottom + paddingBottom <= this.BoundingRectangle.Bottom &&
                       e.BoundingRectangle.Top - paddingTop >= this.BoundingRectangle.Top &&
                       e.BoundingRectangle.Right + paddingRight <= this.BoundingRectangle.Right &&
                       e.BoundingRectangle.Left - paddingLeft >= this.BoundingRectangle.Left;
            };

            return Methods.WaitUntil(elementInViewport, maxTime, 10);
        }
    }
}