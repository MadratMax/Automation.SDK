namespace Automation.Sdk.UIWrappers.Services.ResourcesServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    public sealed class CompositeDisposableWithPriority : IDisposable
    {
        private readonly object _gate = new object();
        private bool _disposed;
        private readonly List<DisposableWithPriority> _disposables;

        public bool IsDisposed => _disposed;

        public CompositeDisposableWithPriority()
        {
            _disposables = new List<DisposableWithPriority>();
        }

        public void Add([NotNull] IDisposable item, int priority = 0)
        {
            if (item == null)
            { 
                throw new ArgumentNullException("item");
            }

            bool flag;
            lock (_gate)
            {
                flag = _disposed;
                if (!_disposed)
                {
                    _disposables.Add(new DisposableWithPriority(item, priority));
                }
            }

            if (flag)
            {
                item.Dispose();
            }
        }

        public void Dispose()
        {
            DisposableWithPriority[] disposables = null;
            lock (_gate)
            {
                if (!_disposed)
                {
                    _disposed = true;

                    disposables = _disposables
                                              .OrderBy(d => d.Priority)
                                              .ToArray();

                    _disposables.Clear();
                }
            }

            if (disposables != null)
            { 
                foreach (var disposable in disposables)
                {
                    disposable?.Dispose();
                }
            }
        }
    }
}