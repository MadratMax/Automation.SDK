namespace Automation.Sdk.UIWrappers.Adapters.Web
{
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IKeyboardInputAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Web)]
    internal class WebInputAdapter : WebAdapterBase, IKeyboardInputAdapter
    {        
        public WebInputAdapter(
            IElement element,
            ControlAdapterFactory controlAdapterFactory)
            : base(element, controlAdapterFactory)
        {
        }

        public void Type(string input)
        {
            WebElement.SendKeys(input);
        }

        public void Paste(string input)
        {
            var focusAdapter = GetAdapter<IFocusAdapter>();
            focusAdapter.SetFocus();

            AutomationFacade.ClipboardCopyService.CopyToClipboard(input);
            Keyboard.Paste();
        }

        public void TypeSlowly(string input)
        {
            var focusAdapter = GetAdapter<IFocusAdapter>();
            focusAdapter.SetFocus();

            Keyboard.TypeSlowly(input);
        }
    }
}