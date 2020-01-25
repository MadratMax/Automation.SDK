namespace Automation.Sdk.UIWrappers.Services.Keyboard
{
    using Microsoft.Test.Input;
    using System.Collections.Generic;

    public interface IModifierKeysStateHolder
    {
        IList<string> PressedKeys { get; }

        void PressKey(Key key);

        void ReleaseKey(Key key);

        void Reset();
    }
}