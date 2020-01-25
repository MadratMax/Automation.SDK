namespace Automation.Sdk.UIWrappers.Adapters
{
    public interface IKeyboardInputAdapter : IAdapter
    {
        void Type(string input);

        void Paste(string input);

        void TypeSlowly(string input);
    }
}