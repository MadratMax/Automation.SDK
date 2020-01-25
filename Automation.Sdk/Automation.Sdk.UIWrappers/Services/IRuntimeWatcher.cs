namespace Automation.Sdk.UIWrappers.Services
{
    using System;

    public interface IRuntimeWatcher
    {
        void Start();

        void Stop();

        bool IsFailed { get; }

        Exception FailReason { get; }
    }
}