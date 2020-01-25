namespace Automation.Sdk.UIWrappers.Adapters
{
    using JetBrains.Annotations;

    public interface IAdapter
    {
        [NotNull]
        TAdapter GetAdapter<TAdapter>() where TAdapter : IAdapter;

        bool IsContainsElement { get; }
    }
}