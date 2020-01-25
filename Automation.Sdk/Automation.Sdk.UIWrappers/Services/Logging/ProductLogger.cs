namespace Automation.Sdk.UIWrappers.Services.Logging
{
    using System;
    using System.Reactive.Linq;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.Services.Bootstrapping;    
    using JetBrains.Annotations;

    /// <summary>
    /// This class automatically resolved at <see cref="BootstrapEventOrder.CONTAINER_TYPES_INSTANTIATION"/> event.
    /// Logger will recieve messages from ITracingDispatcher service.
    /// </summary>
    [AutoRegister]
    [UsedImplicitly]
    internal sealed class ProductLogger : IProductLogger
    {
        //private readonly ITracingDispatcher _dispatcher;
        private readonly IDisposable _subscriptionAnchor;

        //public ProductLogger([NotNull] ILogger logger, [NotNull] ITracingDispatcher dispatcher)
        //{
        //    _dispatcher = dispatcher;
        //    _subscriptionAnchor =
        //        _dispatcher.Messages
        //            .Where(x => x.ToLower().Contains("exception"))
        //            .Subscribe(x => logger.Write($"PRODUCT: {x}"));
        //}

        public ProductLogger([NotNull] ILogger logger)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
            //_subscriptionAnchor.Dispose();
            //_dispatcher.Dispose();
        }
    }
}
