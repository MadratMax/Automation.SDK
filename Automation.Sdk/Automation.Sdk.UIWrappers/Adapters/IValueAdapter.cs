namespace Automation.Sdk.UIWrappers.Adapters
{
    public interface IValueAdapter : IAdapter
    {
        object Value { get; }

        TValue GetValue<TValue>();

        void SetValue(string newValue);
    }
}