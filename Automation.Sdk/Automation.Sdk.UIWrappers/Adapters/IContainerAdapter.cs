namespace Automation.Sdk.UIWrappers.Adapters
{
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using JetBrains.Annotations;

    public interface IContainerAdapter : IAdapter
    {
        [NotNull]
        Element Control { get; }
    }
}