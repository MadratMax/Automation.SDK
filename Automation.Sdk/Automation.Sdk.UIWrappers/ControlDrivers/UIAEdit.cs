namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System;
    using System.Windows.Automation;
    using System.Windows.Automation.Text;
    using Automation.Sdk.UIWrappers.Services;
    using Enums;

    public class UIAEdit : Element
    {
        public UIAEdit (AutomationElement automationElement)
          : base (automationElement)
        {
        }

        [Obsolete("Use UIAEdit.Focused instead")]
        public UIAEdit ()
            : this(AutomationElement.FocusedElement)
        {
        }

        public static UIAEdit Focused => new UIAEdit(AutomationElement.FocusedElement);

        public void SetValue(string value)
        {
            ValuePattern valuePattern = GetPattern<ValuePattern>(ValuePattern.Pattern);
            valuePattern.SetValue(value);
        }

        public string Value => GetProperty<string>(ValuePattern.ValueProperty);

        public string SelectedText => GetSelection()[0].GetText(-1);

        public bool IsReadOnly => GetProperty<bool>(ValuePattern.IsReadOnlyProperty);

        public TextPatternRange[] GetSelection()
        {
            TextPattern textPattern = GetPattern<TextPattern>(TextPattern.Pattern);
            return textPattern.GetSelection();
        }

        public TextPatternRange[] GetVisibleRanges()
        {
            TextPattern textPattern = GetPattern<TextPattern>(TextPattern.Pattern);
            return textPattern.GetVisibleRanges();
        }

        public TextPatternRange RangeFromChild(AutomationElement childElement)
        {
            TextPattern textPattern = GetPattern<TextPattern>(TextPattern.Pattern);
            return textPattern.RangeFromChild(childElement);
        }

        public TextPatternRange RangeFromPoint(System.Windows.Point screenLocation)
        {
            TextPattern textPattern = GetPattern<TextPattern>(TextPattern.Pattern);
            return textPattern.RangeFromPoint(screenLocation);
        }

        public TextPatternRange DocumentRange
        {
            get
            {
                TextPattern textPattern = GetPattern<TextPattern>(TextPattern.Pattern);
                return textPattern.DocumentRange;
            }
        }

        public int Column => GetProperty<int>(GridItemPattern.ColumnProperty);

        public int ColumnSpan => GetProperty<int>(GridItemPattern.ColumnSpanProperty);

        public int Row => GetProperty<int>(GridItemPattern.RowProperty);

        public int RowSpan => GetProperty<int>(GridItemPattern.RowSpanProperty);

        public AutomationElement ContainingGrid => GetProperty<AutomationElement>(GridItemPattern.ContainingGridProperty);

        public AutomationElement[] ColumnHeaderItems => GetProperty<AutomationElement[]>(TableItemPattern.ColumnHeaderItemsProperty);

        public AutomationElement[] RowHeaderItems => GetProperty<AutomationElement[]>(TableItemPattern.RowHeaderItemsProperty);

        /// <summary>
        /// Experimental feature
        /// </summary>
        /// <param name="text">text to type</param>
        /// <param name="expectedText">string to compare after</param>
        public virtual void Type(string text, string expectedText = null)
        {
            Methods.WaitUntil(() => AutomationElement.FocusedElement.Equals(this.AutomationElement), "Desired input field is not focused");

            var selectedText = SelectedText;

            if (expectedText == null)
            {
                expectedText = text;
            }

            string textBeforeInput = string.Empty;

            foreach (var c in text)
            {
                Methods.WaitUntil(() => AutomationElement.FocusedElement.Equals(this.AutomationElement), "Desired input field lost focus while text was typed");

                string charToType = c.ToString();

                if (c.Equals('+') ||
                    c.Equals('^') ||
                    c.Equals('%') ||
                    c.Equals('~') ||
                    c.Equals('(') ||
                    c.Equals(')') ||
                    c.Equals('[') ||
                    c.Equals(']'))
                {
                    charToType = "{" + c + "}";
                }

                Methods.WaitUntil(() => AutomationElement.FocusedElement.Equals(this.AutomationElement), "Desired input field lost focus while text was typed");
                textBeforeInput = Value;
                Keyboard.Type(charToType);
                Methods.WaitUntil(() => !Value.Equals(textBeforeInput), 500);
            }

            Methods.WaitUntil(() => Value.Equals(expectedText), "Value wasn't typed correctly");
        }
    }
}
