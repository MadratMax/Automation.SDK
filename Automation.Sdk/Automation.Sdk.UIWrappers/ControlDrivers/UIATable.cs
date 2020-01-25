namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System;
    using System.Linq;
    using System.Windows.Automation;

    public class UIATable : Element
    {
        public UIATable(AutomationElement automationElement)
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

        #region MultipleView Pattern
        public string GetViewName(int viewId)
        {
            MultipleViewPattern mvp = GetPattern<MultipleViewPattern>(MultipleViewPattern.Pattern);
            return mvp.GetViewName(viewId);
        }

        public void SetCurrentView(int viewId)
        {
            if (!SupportedViews.Contains(viewId))
            { 
                throw new ArgumentOutOfRangeException();
            }

            MultipleViewPattern multipleViewPattern = GetPattern<MultipleViewPattern>(MultipleViewPattern.Pattern);
            multipleViewPattern.SetCurrentView(viewId);
        }

        public int CurrentView => GetProperty<int>(MultipleViewPattern.CurrentViewProperty);

        public int[] SupportedViews => GetProperty<int[]>(MultipleViewPattern.SupportedViewsProperty);

        #endregion

        #region Scroll Pattern
        public void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount)
        {
            ScrollPattern scrollPattern = GetPattern<ScrollPattern>(ScrollPattern.Pattern);
            scrollPattern.Scroll(horizontalAmount, verticalAmount);
        }

        public void ScrollHorizontal(ScrollAmount amount)
        {
            ScrollPattern scrollPattern = GetPattern<ScrollPattern>(ScrollPattern.Pattern);
            scrollPattern.ScrollHorizontal(amount);
        }

        public void ScrollVertical(ScrollAmount amount)
        {
            ScrollPattern scrollPattern = GetPattern<ScrollPattern>(ScrollPattern.Pattern);
            scrollPattern.ScrollVertical(amount);
        }

        public void SetScrollPercent(double horizontalPercent, double verticalPercent)
        {
            ScrollPattern scrollPattern = GetPattern<ScrollPattern>(ScrollPattern.Pattern);
            scrollPattern.SetScrollPercent(horizontalPercent, verticalPercent);
        }

        public bool HorizontallyScrollable => GetProperty<bool>(ScrollPattern.HorizontallyScrollableProperty, false);

        public double HorizontalScrollPercent => GetProperty<double>(ScrollPattern.HorizontalScrollPercentProperty);

        public double HorizontalViewSize => GetProperty<double>(ScrollPattern.HorizontalViewSizeProperty);

        public bool VerticallyScrollable => GetProperty<bool>(ScrollPattern.VerticallyScrollableProperty);

        public double VerticalScrollPercent => GetProperty<double>(ScrollPattern.VerticalScrollPercentProperty);

        public double VerticalViewSize => GetProperty<double>(ScrollPattern.VerticalViewSizeProperty);

        #endregion
    }
}
