namespace Automation.Sdk.UIWrappers.Adapters.Desktop
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(ITextAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Desktop)]
    internal sealed class TextAdapter : Element, ITextAdapter
    {
        public TextAdapter([NotNull] IElement element)
            : base(element)
        {
        }

        public string GetText() => FindAdvanced<UIAText>()?.DisplayText;

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