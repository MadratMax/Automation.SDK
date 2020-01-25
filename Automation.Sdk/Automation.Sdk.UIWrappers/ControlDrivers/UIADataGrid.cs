
namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Services;

    public class UIADataGrid : Element
    {
        public UIADataGrid(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        #region Grid Pattern
        public AutomationElement GetItem(int row, int column)
        {
            GridPattern gridPattern = GetPattern<GridPattern>(GridPattern.Pattern);
            return gridPattern.GetItem(row, column);
        }

        public int RowCount => GetProperty<int>(GridPattern.RowCountProperty);

        public int ColumnCount => GetProperty<int>(GridPattern.ColumnCountProperty);

        #endregion

        #region Table Pattern
        public AutomationElement[] GetColumnHeaders()
        {
            TablePattern tablePattern = GetPattern<TablePattern>(TablePattern.Pattern);
            return tablePattern.Current.GetColumnHeaders();
        }

        public AutomationElement[] GetRowHeaders()
        {
            TablePattern tablePattern = GetPattern<TablePattern>(TablePattern.Pattern);
            return tablePattern.Current.GetRowHeaders();
        }

        public AutomationElement[] ColumnHeaders => GetProperty<AutomationElement[]>(TablePattern.ColumnHeadersProperty);

        public AutomationElement[] RowHeaders => GetProperty<AutomationElement[]>(TablePattern.RowHeadersProperty);

        public RowOrColumnMajor RowOrColumnMajor => GetProperty<RowOrColumnMajor>(TablePattern.RowOrColumnMajorProperty);

        #endregion

        #region Scroll Pattern
        public ScrollPattern GetScrollPattern()
        {
            if (!GetProperty<bool>(AutomationElement.IsScrollPatternAvailableProperty, false))
            {
                AutomationFacade.Logger.Write($"{this} has no ScrollPattern available.");
                return null;
            }

            return GetPattern<ScrollPattern>(ScrollPattern.Pattern);
        }

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

        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to horizontally scroll the requested amount.
        /// </summary>
        /// <param name="amount">The requested horizontal scroll amount</param>
        public void ScrollHorizontal(ScrollAmount amount)
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
                scrollPattern.ScrollHorizontal(amount);
            }
            catch (InvalidOperationException)
            {
                // Control not able to scroll in the direction requested;
                // when scrollable property of that direction is False
                // TODO: error handling.
            }
            catch (ArgumentException)
            {
                // If a control supports SmallIncrement values exclusively 
                // for horizontal or vertical scrolling but a LargeIncrement 
                // value (NaN if not supported) is passed in.
                // TODO: error handling.
            }
        }

        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to vertically scroll the requested amount.
        /// </summary>
        /// <param name="amount">The requested percent to scroll up or down ( 0 - Home, 100 - End)</param>
        public void ScrollVertical(ScrollAmount amount)
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
                scrollPattern.ScrollVertical(amount);
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
        public void ScrollVertical(double percent)
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
                scrollPattern.SetScrollPercent(-1, percent);
            }
            catch (InvalidOperationException)
            {
                // Control not able to scroll in the direction requested;
                // when scrollable property of that direction is False
                // TODO: error handling.
            }
            catch (ArgumentException)
            {
                // If a control supports SmallIncrement values exclusively 
                // for horizontal or vertical scrolling but a LargeIncrement 
                // value (NaN if not supported) is passed in.
                // TODO: error handling.
            }
        }

        public bool ScrollTo(Element e, int paddingTop, int paddingLeft, int paddingBottom, int paddingRight, int maxTime)
        {
            var scrollController = GetPattern<ScrollPattern>(ScrollPattern.Pattern);

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

        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to vertically scroll the requested amount.
        /// </summary>
        /// <param name="scrollAmount">The requested vertical scroll amount</param>
        public void ScrollElementVertically(ScrollAmount scrollAmount)
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
                scrollPattern.ScrollVertical(scrollAmount);
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
                scrollPattern.SetScrollPercent(-1, percent);
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

        public bool HorizontallyScrollable => GetProperty<bool>(ScrollPattern.HorizontallyScrollableProperty, false);

        public double HorizontalScrollPercent => GetProperty<double>(ScrollPattern.HorizontalScrollPercentProperty);

        public double HorizontalViewSize => GetProperty<double>(ScrollPattern.HorizontalViewSizeProperty);

        public bool VerticallyScrollable => GetProperty<bool>(ScrollPattern.VerticallyScrollableProperty);

        public double VerticalScrollPercent => GetProperty<double>(ScrollPattern.VerticalScrollPercentProperty);

        public double VerticalViewSize => GetProperty<double>(ScrollPattern.VerticalViewSizeProperty);

        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to scroll to the 'home' position.
        /// </summary>
        public void ScrollHome()
        {
            ScrollPattern scrollPattern = GetScrollPattern();

            scrollPattern?.SetScrollPercent(-1, 0);
        }

        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to scroll to the 'end' position.
        /// </summary>
        public void ScrollEnd()
        {
            ScrollPattern scrollPattern = GetScrollPattern();

            scrollPattern?.SetScrollPercent(-1, 100);
        }

        #endregion

        #region Methods/Properties
        /// <summary>
        /// Returns the header bar (column)
        /// </summary>
        public UIAHeader Header
        {
            get
            {
                UIAHeader header = Find<UIAHeader>();
                return header;
            }
        }

        /// <summary>
        /// Return a list of data items (rows)
        /// </summary>
        public List<UIADataItem> DataItems
        {
            get
            {
                UIADataItem[] dataItems = FindAll<UIADataItem>();
                List<UIADataItem> lstDataItems = new List<UIADataItem>(dataItems);
                return lstDataItems;
            }
        }

        /// <summary>
        /// Return a list of data header items (columns)
        /// </summary>
        public List<UIAHeaderItem> HeaderItems
        {
            get
            {
                UIAHeaderItem[] dataItems = FindAll<UIAHeaderItem>(TreeScope.Descendants);
                List<UIAHeaderItem> lstHeaderItems = new List<UIAHeaderItem>(dataItems);
                return lstHeaderItems;
            }
        }

        /// <summary>
        /// Get the value for the table cell
        /// </summary>
        /// <param name="rowIndex">row index (for example, 1 to ...)</param>
        /// <param name="columnIndex">column index (for example, 1 to ...)</param>
        /// <returns>The value of the table cell</returns>
        public string GetText(int rowIndex, int columnIndex)
        {
            List<UIADataItem> dataItems = DataItems;
            List<UIAHeaderItem> headerItems = Header.HeaderItems;
          
            int rowCount = dataItems.Count;
            int columnCount = headerItems.Count;

            if (rowIndex < 1 || rowIndex > rowCount)
            {
                Trace.WriteLine("Row index provided is out of range = " + rowIndex);
                return string.Empty;
            }

            if (columnIndex < 1 || columnIndex > columnCount)
            {
                Trace.WriteLine("Column index provided is out of range = " + rowIndex);
                return string.Empty;
            }

            UIADataItem item = dataItems[rowIndex - 1];
            string value = item.GetValueByIndex(columnIndex - 1);

            return value;
        }

        /// <summary>
        /// Return a list of selected data items
        /// </summary>
        /// <returns>List of selected data items</returns>
        public List<UIADataItem> GetSelectedItems()
        {
            List<UIADataItem> items = DataItems;
            List<UIADataItem> selectedItems = new List<UIADataItem>();

            foreach (UIADataItem item in items)
            {
                if (item.IsSelected)
                {
                    selectedItems.Add(item);
                }
            }

            return selectedItems;
        }

        #endregion
    }
}