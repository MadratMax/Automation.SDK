namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;

    public class UIAHeaderItem : Element
    {
        public UIAHeaderItem(AutomationElement element)
            : base(element)
        {
        }

        #region Methods/Properties

        /// <summary>
        /// Return the Column name
        /// </summary>
        public string LabelName
        {
            get
            {
                UIAText text = GetTextControl();
                if (text == null)
                {
                    return string.Empty;
                }

                return text.Name;
            }
        }

        /// <summary>
        /// Return the Column name
        /// </summary>
        public string LabelHelpText
        {
            get
            {
                UIAText text = GetTextControl();
                if (text == null)
                {
                    return string.Empty;
                }
                else
                {
                    return text.HelpText;
                }
            }
        }

        /// <summary>
        /// Return the Column name
        /// </summary>
        public UIAText GetTextControl()
        {
            return Find<UIAText>();
        }

        public UIAThumb GetThumb()
        {
            UIAThumb thumb = Find<UIAThumb>();
            return thumb;
        }

        #endregion
    }
}
