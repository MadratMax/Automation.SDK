namespace Automation.Sdk.UIWrappers.Services.Threading
{
    using System;
    using JetBrains.Annotations;

    public interface IStaScheduler
    {
        void Schedule([NotNull] Action action);
    }
}