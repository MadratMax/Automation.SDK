namespace Automation.Sdk.UIWrappers.Adapters.Desktop
{
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IKeyboardInputAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Desktop)]
    internal sealed class KeyboardInputAdapter : Element, IKeyboardInputAdapter
    {
        public KeyboardInputAdapter(IElement element)
            :base(element)
        {
        }

        public void Type(string input)
        {
            var focusAdapter = GetAdapter<IFocusAdapter>();
            focusAdapter.ShouldBecome(x => x.IsFocused, true, $@"Element ""{this}"" should have keyboard focus");

            var inputEscaped = string.Empty;

            // From: https://msdn.microsoft.com/ru-ru/library/system.windows.forms.sendkeys(v=vs.110).aspx
            // The plus sign (+), caret(^), percent sign(%), tilde(~), and parentheses() have special meanings to SendKeys.
            // To specify one of these characters, enclose it within braces({ }). For example, to specify the plus sign, use "{+}".
            // To specify brace characters, use "{{}" and "{}}".
            // Brackets([ ]) have no special meaning to SendKeys, but you must enclose them in braces.
            foreach (var charToInput in input)
            {
                if (charToInput.Equals('+') ||
                    charToInput.Equals('^') ||
                    charToInput.Equals('%') ||
                    charToInput.Equals('~') ||
                    charToInput.Equals('(') ||
                    charToInput.Equals(')') ||
                    charToInput.Equals('[') ||
                    charToInput.Equals(']'))
                {
                    inputEscaped += "{" + charToInput + "}";
                }
                else
                {
                    inputEscaped += charToInput;
                }
            }

            Keyboard.Type(inputEscaped);
        }

        public void TypeSlowly(string input)
        {
            var focusAdapter = GetAdapter<IFocusAdapter>();

            focusAdapter.ShouldBecome(x => x.IsFocused, true, $@"Element ""{this}"" should have keyboard focus");
            Keyboard.TypeSlowly(input);
        }

        public void Paste(string input)
        {
            var focusAdapter = GetAdapter<IFocusAdapter>();
            focusAdapter.ShouldBecome(x => x.IsFocused, true, $@"Element ""{this}"" should have keyboard focus");

            AutomationFacade.ClipboardCopyService.CopyToClipboard(input);
            Keyboard.Paste();
        }
    }
}