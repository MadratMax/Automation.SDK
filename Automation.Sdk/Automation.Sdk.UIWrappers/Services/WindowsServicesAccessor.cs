
namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.ServiceProcess;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using Automation.Sdk.UIWrappers.Services.ResourcesServices;

    /// <summary>
    /// Operates with windows services
    /// </summary>
    [AutoRegister]
    public class WindowsServicesAccessor
        : IWindowsServicesAccessor
    {
        private readonly ResourceConsumer _resourceConsumer;
        private readonly ILogger _logger;

        public WindowsServicesAccessor(
            ResourceConsumer resourceConsumer,
            ILogger logger)
        {
            _resourceConsumer = resourceConsumer;
            _logger = logger;
        }

        /// <summary>
        /// Start a window service
        /// </summary>
        /// <param name="serviceName">Name of the window service</param>
        /// <param name="timeoutMilliseconds">Wait for service to start</param>
        /// <returns>True if service is started, otherwise false</returns>
        public virtual bool StartService(string serviceName, int timeoutMilliseconds)
        {
            return StartServiceWithArgs(serviceName, timeoutMilliseconds);
        }

        /// <summary>
        /// This method tries to start a service specified by a service name and start parameters. 
        /// Then it waits until the service is running or a timeout occurs.
        /// </summary>
        /// <param name="serviceName">ServiceName</param>
        /// <param name="timeoutMilliseconds">Wait for service to start</param>
        /// <param name="args">Args</param>
        /// <returns>True - started, False otherwise</returns>
        public virtual bool StartServiceWithArgs(string serviceName, int timeoutMilliseconds, params string[] args)
        {
            if (!IsServiceInstalled(serviceName))
            {
                throw new Exception($"Service {serviceName} is not installed in the system.");
            }

            var service = new ServiceController(serviceName);
            var timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

            try
            {
                _resourceConsumer.Consume(Disposable.Create(() => StopService(serviceName, 60000)));

                // [AB] 24.11.2017: Bypassing System.ComponentModel.Win32Exception: The service cannot accept control messages at this time
                // https://msdn.microsoft.com/en-us/library/ms833805.aspx
                Methods.WaitUntil(
                    () => CanAccessService(service),
                    () => $"Service {serviceName} has status {GetServiceStatus(service)} so it cannot be started.",
                    60000);

                if (GetServiceStatus(service) == ServiceControllerStatus.Running)
                {
                    _logger.Write($"Service {serviceName} is already running!");
                    return true;
                }

                service.Start(args);
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception e)
            {
                _logger.Write($@"Exception while starting ""{serviceName}"" service. Service status: {GetServiceStatus(service)}. Exception:");
                _logger.Write(e);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Stop a window service
        /// </summary>
        /// <param name="serviceName">Name of the window service</param>
        /// <param name="timeoutMilliseconds">Wait for service to stop</param>
        /// <returns>True if service is stopped, otherwise false</returns>
        public virtual bool StopService(string serviceName, int timeoutMilliseconds)
        {
            if (!IsServiceInstalled(serviceName))
            {
                throw new Exception($"Service {serviceName} is not installed in the system.");
            }

            var service = new ServiceController(serviceName);
            var timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

            try
            {
                // [AB] 24.11.2017: Bypassing System.ComponentModel.Win32Exception: The service cannot accept control messages at this time
                // https://msdn.microsoft.com/en-us/library/ms833805.aspx
                Methods.WaitUntil(
                    () => CanAccessService(service),
                    () => $"Service {serviceName} has status {GetServiceStatus(service)} so it cannot be stopped.",
                    60000);

                if (GetServiceStatus(service) == ServiceControllerStatus.Stopped)
                {
                    _logger.Write($"Service {serviceName} is already stopped!");
                    return true;
                }

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch (Exception e)
            {
                _logger.Write($@"Exception while stopping ""{serviceName}"" service. Service status: {GetServiceStatus(service)}. Exception:");
                _logger.Write(e);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Restart the window service
        /// </summary>
        /// <param name="serviceName">Name of the window service</param>
        /// <param name="timeoutMilliseconds">Wait for service to restart</param>
        /// <returns>True if service is restarted, otherwise false</returns>
        public virtual bool RestartService(string serviceName, int timeoutMilliseconds)
        {
            return StopService(serviceName, timeoutMilliseconds) 
                && StartService(serviceName, timeoutMilliseconds);
        }

        /// <summary>
        /// Check if the window service exists
        /// </summary>
        /// <param name="serviceName">Name of the window service</param>
        /// <returns>True if the window service exists, false otherwise</returns>
        public virtual bool IsServiceInstalled(string serviceName) => ServiceController.GetServices().Any(s => s.ServiceName == serviceName);

        private ServiceControllerStatus GetServiceStatus(ServiceController service)
        {
            service.Refresh();
            return service.Status;
        }

        private bool CanAccessService(ServiceController service)
        {
            var status = GetServiceStatus(service);
            return status == ServiceControllerStatus.Running
                || status == ServiceControllerStatus.Stopped;
        }
    }
}