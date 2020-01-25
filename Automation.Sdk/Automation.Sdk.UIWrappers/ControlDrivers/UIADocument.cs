namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Text;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Services;
    using Enums;

    public class UIADocument : UIAEdit
    {
        public UIADocument(AutomationElement automationElement)
            : base(automationElement)
        {
        }

        public string FullText => DocumentRange.GetText(-1);

        /// <summary>
        /// Experimental feature
        /// </summary>
        /// <param name="text">text to type</param>
        /// <param name="expectedText">string to compare after</param>
        public override void Type(string text, string expectedText = null)
        {
            Methods.WaitUntil(() => AutomationElement.FocusedElement.Equals(this.AutomationElement), "Desired input field is not focused");

            var initialText = string.Empty;
            var selectedText = SelectedText;
            var fullText = FullText;

            if (expectedText == null)
            {
                expectedText = text;
            }

            if (selectedText.Equals(fullText))
            {
                initialText = string.Empty;
            }
            else if (selectedText.Equals(string.Empty))
            {
                initialText = fullText;
            }
            else
            {
                initialText = fullText.Replace(selectedText, string.Empty);
            }

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

                Keyboard.Type(charToType);
                Methods.WaitUntil(() => ValueWasModified(ref initialText, c), "");
                initialText += c;
            }

            Methods.WaitUntil(() => FullText.Equals(expectedText), "Value wasn't typed correctly");
        }

        private bool ValueWasModified(ref string initialText, char c)
        {
            string fullNewText = FullText;

            foreach(var c2 in initialText)
            {
                fullNewText = RemoveOnce(fullNewText, c2);
            }

            return fullNewText.Length == 1 && fullNewText[0].Equals(c);
        }

        private string RemoveOnce(string input, char remove)
        {
            StringBuilder str = new StringBuilder();

            int i;

            for (i = 0; i < input.Length; ++i)
            {
                if (input[i] == remove)
                {
                    break;
                }

                str.Append(input[i]);
            }

            str.Append(input.Substring(i + 1));

            return str.ToString();
        }
    }
}
