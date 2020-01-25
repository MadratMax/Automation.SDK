namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Collections.Generic;
    using System.Windows.Automation;

    public class UIADataItem : Element
    {
        public UIADataItem(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        #region GridItem Pattern
        public int Column => GetProperty<int>(GridItemPattern.ColumnProperty);

        public int ColumnSpan => GetProperty<int>(GridItemPattern.ColumnSpanProperty);

        public AutomationElement ContainingGrid => GetProperty<AutomationElement>(GridItemPattern.ContainingGridProperty);

        public int Row => GetProperty<int>(GridItemPattern.RowProperty);

        public int RowSpan => GetProperty<int>(GridItemPattern.RowSpanProperty);

        #endregion

        #region SelectionItem/ScrollItemPattern Pattern
        public void ScrollIntoView()
        {
            ScrollItemPattern scrollItemPattern = GetPattern<ScrollItemPattern>(ScrollItemPattern.Pattern);
            scrollItemPattern.ScrollIntoView();
        }

        public void Select()
        {
            SelectionItemPattern selectionItemPattern = GetPattern<SelectionItemPattern>(SelectionItemPattern.Pattern);
            selectionItemPattern.Select();
        }

        public void RemoveFromSelection()
        {
            SelectionItemPattern selectionItemPattern = GetPattern<SelectionItemPattern>(SelectionItemPattern.Pattern);
            selectionItemPattern.RemoveFromSelection();
        }

        public void AddToSelection()
        {
            SelectionItemPattern selectionItemPattern = GetPattern<SelectionItemPattern>(SelectionItemPattern.Pattern);
            selectionItemPattern.AddToSelection();
        }

        #endregion
        #region Methods/Properties
        /// <summary>
        /// Get a list of table cell values for the row
        /// </summary>
        public List<string> Values
        {
            get 
            {
                UIAText[] arrText = FindAll<UIAText>(TreeScope.Descendants);
                List<string> lstText = new List<string>();
                foreach (UIAText text in arrText)
                {
                    lstText.Add(text.Name);
                }

                return lstText;
            }
        }

        /// <summary>
        /// Get the table cell value by index (0 based)
        /// </summary>
        /// <param name="columnIndex">column index</param>
        /// <returns>table cell value</returns>
        public string GetValueByIndex(int columnIndex)
        {
            List<string> lstValues = Values;
            if (lstValues.Count == 0)
            {
                return string.Empty;
            }

            return lstValues[columnIndex];
        }

        #endregion
    }
}
