namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;

    public class UIACustom : Element
    {
        private readonly IValueAdapter _valueAdapter;

        public UIACustom(AutomationElement automationElement)
            : base(automationElement)
        {
            _valueAdapter = ControlAdapterFactory.Create<IValueAdapter>(this);
        }

        /// <summary>
        /// Get all child custom controls
        /// </summary>
        /// <returns>List of custom controls</returns>
        public List<UIACustom> GetCustomControls()
        {
            UIACustom[] arrCustoms = FindAll<UIACustom>();
            List<UIACustom> lst = new List<UIACustom>(arrCustoms);
            return lst;
        }

        /// <summary>
        /// Get all child button controls
        /// </summary>
        /// <returns>List of button controls</returns>
        public List<UIAButton> GetButtons()
        {
            UIAButton[] arrButtons = FindAll<UIAButton>();
            List<UIAButton> lst = new List<UIAButton>(arrButtons);
            return lst;
        }

        /// <summary>
        /// Get all the child text block controls
        /// </summary>
        /// <returns>List of text block controls</returns>
        public List<UIAText> GetTexts()
        {
            UIAText[] arrTexts = FindAll<UIAText>();
            List<UIAText> lst = new List<UIAText>(arrTexts);
            return lst;
        }

        /// <summary>
        /// Get all image controls
        /// </summary>
        /// <returns>List of image controls</returns>
        public List<UIAImage> GetImages()
        {
            UIAImage[] arrImage = FindAll<UIAImage>();
            List<UIAImage> lst = new List<UIAImage>(arrImage);
            return lst;
        }

        /// <summary>
        /// Get all radio button controls
        /// </summary>
        /// <returns>List of radio button controls</returns>
        public List<UIARadioButton> GetRadioButtons()
        {
            UIARadioButton[] arrRadioButtons = FindAll<UIARadioButton>();
            List<UIARadioButton> lst = new List<UIARadioButton>(arrRadioButtons);
            return lst;
        }

        public int Row => GetProperty<int>(GridItemPattern.RowProperty);

        public void SetValue(string value)
        {
            _valueAdapter.SetValue(value);
        }

        public string Value => _valueAdapter.GetValue<string>();
    }
}
