namespace Automation.Sdk.UIWrappers.Services.Logging
{
    using System;
    using JetBrains.Annotations;

    public interface ILogger
    {
        void Write([NotNull] string message);

        void Write([NotNull] Exception exception);
    }
}