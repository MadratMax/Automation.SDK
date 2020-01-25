namespace Automation.Sdk.UIWrappers.Services.ExecutionContext
{
    using System;
    using JetBrains.Annotations;

    public interface IExecutionContextProvider
    {
        IObservable<ExecutionContextDto> Context { [NotNull] get; }
    }
}