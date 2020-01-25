namespace Automation.Sdk.UIWrappers.Services.ResourcesServices
{
    using System;
    using JetBrains.Annotations;

    public sealed class DisposableWithPriority : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly int _priority;

        public DisposableWithPriority([NotNull] IDisposable disposable, int priority = 0)
        {
            _disposable = disposable;
            _priority = priority;
        }

        public int Priority => _priority;

        public void Dispose()
        {
            _disposable.Dispose();            
        }
    }
}