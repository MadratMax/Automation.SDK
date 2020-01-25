namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using Automation.Sdk.UIWrappers.Services.ResourcesServices;
    using Automation.Sdk.UIWrappers.Services.ScreenCapturing;

    [AutoRegister]
    public class RuntimeWatcher : IRuntimeWatcher
    {
        private readonly ILogger _logger;
        private readonly IProcessKiller _processKiller;
        private readonly IScreenCapture _screenCapture;

        private CancellationTokenSource _cancellationTokenSource;
        private bool _failed;
        private Exception _failReason;

        public RuntimeWatcher(
            ILogger logger, 
            IProcessKiller processKiller,
            IScreenCapture screenCapture)
        {
            _logger = logger;
            _processKiller = processKiller;
            _screenCapture = screenCapture;
        }

        public void Start()
        {
            _failed = false;
            _cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => Run(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        public void Run(CancellationToken cancellationToken)
        {
            _logger.Write("RuntimeWatcher started.");
            var failed = false;

            while (!cancellationToken.IsCancellationRequested)
            {
                var faults = Process.GetProcessesByName("WerFault");

                foreach (var fault in faults)
                {
                    try
                    {
                        if (fault.MainWindowHandle != IntPtr.Zero)
                        {
                            failed = true;

                            _failReason = new Exception("Windows Exception occured while running an application.");
                            _logger.Write("Windows Exception occured while running an application.");

                            // Wait 1 seconds to let exception window to be rendered.
                            Thread.Sleep(1000);
                            _screenCapture.StoreScreenshot("WindowsException");
                            _processKiller.KillProcess(fault, 0);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Write(e);
                    }
                }

                if (failed)
                {
                    _failed = true;
                }
            }

            _logger.Write("RuntimeWatcher stopped.");
        }

        public bool IsFailed => _failed;

        public Exception FailReason => _failReason;
    }
}