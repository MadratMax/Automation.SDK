namespace Automation.Sdk.Bindings.Dto
{
    using System.Collections.Generic;
    using Microsoft.Test.Input;

    public sealed class ShortcutCommand
    { 
        public ShortcutCommand(params Key[] keys)
        {
            Keys = new List<Key>(keys);
        }

        public List<Key> Keys { get; }

        public override string ToString()
        {
            return string.Join("+", Keys);
        }
    }
}