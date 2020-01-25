namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using JetBrains.Annotations;

    [UsedImplicitly]
    internal sealed class TextAdapter : Element, ITextAdapter
    {
        public TextAdapter([NotNull] AutomationElement element) : base(element) { }

        public string GetText() => FindAdvanced<UIAText>()?.DisplayText;  //// TODO: Was a mistake: it should not throw any exceptions

        public List<string> GetAllText() => FindAll<UIAText>().Select(control => control.DisplayText).ToList();

        public string DisplayText
        {
            get
            {
                if (AutomationElement.Current.ControlType == ControlType.Edit 
                    || AutomationElement.Current.ControlType == ControlType.Text)
                {
                    return AutomationElement.Current.Name;
                }

                return GetText();
            }
        }
    }
}