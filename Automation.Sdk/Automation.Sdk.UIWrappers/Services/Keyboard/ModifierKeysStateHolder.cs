namespace Automation.Sdk.UIWrappers.Services.Keyboard
{
    using Aspects;
    using Microsoft.Test.Input;
    using System.Collections.Generic;
    using OpenQA.Selenium;

    [AutoRegister]
    public class ModifierKeysStateHolder : IModifierKeysStateHolder
    {
        private readonly IList<string> _pressedKeys;

        public ModifierKeysStateHolder()
        {
            _pressedKeys = new List<string>();
        }

        public IList<string> PressedKeys => _pressedKeys;

        public void PressKey(Key key)
        {
            if (key != Key.Ctrl
                && key != Key.Alt
                && key != Key.Shift)
            {
                return;
            }

            var keyAsString = KeyToString(key);
            if (_pressedKeys.Contains(keyAsString))
            {
                return;
            }

            _pressedKeys.Add(keyAsString);
        }

        public void ReleaseKey(Key key)
        {
            var keyAsString = KeyToString(key);
            if (!_pressedKeys.Contains(keyAsString))
            {
                return;
            }

            _pressedKeys.Remove(keyAsString);
        }

        public void Reset()
        {
            _pressedKeys.Clear();
        }

        private string KeyToString(Key key)
        {
            switch (key)
            {
                case Key.Ctrl:
                    return Keys.Control;
                case Key.Shift:
                    return Keys.Shift;
                case Key.Alt:
                    return Keys.Alt;
                default:
                    return key.ToString();

            }
        }
    }
}
