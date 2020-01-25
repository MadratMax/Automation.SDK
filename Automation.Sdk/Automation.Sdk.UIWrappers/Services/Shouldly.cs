namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.Reactive.Disposables;
    using Automation.Sdk.UIWrappers.Configuration;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using JetBrains.Annotations;
    using NUnit.Framework;

    [UsedImplicitly]
    public sealed class Shouldly
    {
        private readonly ILogger _logger;

        private static int _timeout;
        private static bool _freshExtension = false;

        public Shouldly(ILogger logger,
            ISdkConfiguration configuration)
        {
            _logger = logger;
            _timeout = configuration.DefaultWaitTimeout;
        }

        public IDisposable Extend(int customTimeout)
        {
            _logger.Write($"Timeout changed to {customTimeout}");
            var defaultTimeout = Timeout;

            
            Timeout = customTimeout;
            _freshExtension = true;

            return Disposable.Create(() => RestoreTimeout(defaultTimeout));
        }

        public static int Timeout
        {
            get
            {
                _freshExtension = false;
                return _timeout;
            }
            private set
            {
                _timeout = value;
            }
        } 

        private void RestoreTimeout(int previousValue)
        {
            if (_freshExtension)
            {
                Assert.Inconclusive("Shouldy timeout has been extended but not used, please review shouldly monad usage");
            }

            Timeout = previousValue;
            _logger.Write($"Timeout restored to {previousValue}");
        }
    }
}
