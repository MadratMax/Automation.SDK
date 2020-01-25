namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;

    public class UIATree : Element
    {
        public UIATree(AutomationElement automationElement)
            : base(automationElement)
        {
        }

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

            // Object doesn't support the ScrollPattern control pattern
            catch (InvalidOperationException)
            {
                return null;
            }

            return scrollPattern;
        }

        /// <summary>
        /// Scroll by horizontal and vertical amount
        /// </summary>
        /// <param name="horizontalAmount">horizontal scroll amount</param>
        /// <param name="verticalAmount">vertical scroll amount</param>
        public void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount)
        {
            ScrollPattern scrollPattern = GetScrollPattern();
            scrollPattern.Scroll(horizontalAmount, verticalAmount);
        }

        /// <summary>
        /// Scroll by horizontal and vertical percentage
        /// </summary>
        /// <param name="horizontalPercent">horizontal percentage</param>
        /// <param name="verticalPercent">vertical percentage</param>
        public void SetScrollPercent(double horizontalPercent, double verticalPercent)
        {
            ScrollPattern scrollPattern = GetScrollPattern();
            scrollPattern.SetScrollPercent(horizontalPercent, verticalPercent);
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

        ///--------------------------------------------------------------------
        /// <summary>
        /// Obtains a ScrollPattern control pattern from an automation 
        /// element and attempts to vertically scroll the requested amount.
        /// </summary>
        /// <param name="intPercent>
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

        #region Selection Pattern
        public AutomationElement[] GetSelection()
        {
            SelectionPattern selectionPattern = GetPattern<SelectionPattern>(SelectionPattern.Pattern);
            return selectionPattern.Current.GetSelection();
        }

        #endregion

        #region TreeItem
        public List<UIATreeItem> GetTreeItems()
        {
            UIATreeItem[] aListItem = FindAll<UIATreeItem>();
            List<UIATreeItem> lst = new List<UIATreeItem>(aListItem);
            return lst;
        }

        public UIATreeItem SelectItemByIndex(int index)
        {
            List<UIATreeItem> treeItems = new List<UIATreeItem>();

            try
            {
                treeItems = GetTreeItems();
                UIATreeItem idxItem = treeItems[index - 1];
                idxItem.ScrollIntoView();
                idxItem.Select();
                return idxItem;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}
