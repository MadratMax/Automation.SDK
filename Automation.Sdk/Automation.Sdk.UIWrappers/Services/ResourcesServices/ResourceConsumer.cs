namespace Automation.Sdk.UIWrappers.Services.ResourcesServices
{
    using System;
    using System.Reactive.Disposables;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using JetBrains.Annotations;

    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    // Can be inherited in test suites
    [UsedImplicitly]
    public class ResourceConsumer : IDisposable
    {
        private readonly Logger _logger;

        /// <summary>
        /// Resources anchor. This field holds all heap pointers to allocated resources
        /// Internally, CompositeDisposable is a thread-safe array of IDisposable objects
        /// which are going to be disposed during CompositeDisposable.Dispose()
        /// </summary>
        private readonly CompositeDisposableWithPriority _anchor;

        public ResourceConsumer(Logger logger)
        {
            _logger = logger;
            _anchor = new CompositeDisposableWithPriority();
        }

        public virtual void Consume(IDisposable resource)
        {
            Consume(resource, 0);
        }

        public virtual void Consume(IDisposable resource, int priority)
        {
            // As far as we are going to release resources in test cleanup, than we need to log and "eat"
            // all possible exception here, thats why we are wrapping passed IDisposable to a try-catch.
            _anchor.Add(Disposable.Create(() => SafeDispose(resource)), priority);

            if (_anchor.IsDisposed)
            {
                throw new InvalidOperationException("Attempt to add disposable resource to already disposed ResourceConsumer.");
            }
        }

        public void Dispose()
        {
            _anchor.Dispose();
        }

        private void SafeDispose(IDisposable resource)
        {
            try
            {
                resource.Dispose();
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception exception)
            {
                _logger.Write(exception);
            }
        }
    }
}