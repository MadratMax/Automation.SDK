namespace Automation.Sdk.UIWrappers.Services
{
    public interface IWindowsServicesAccessor
    {
        /// <summary>
        /// Start a window service
        /// </summary>
        /// <param name="serviceName">Name of the window service</param>
        /// <param name="timeoutMilliseconds">Wait for service to start</param>
        /// <returns>True if service is started, otherwise false</returns>
        bool StartService(string serviceName, int timeoutMilliseconds);

        /// <summary>
        /// This method tries to start a service specified by a service name and start parameters. 
        /// Then it waits until the service is running or a timeout occurs.
        /// </summary>
        /// <param name="serviceName">ServiceName</param>
        /// <param name="timeoutMilliseconds">Wait for service to start</param>
        /// <param name="args">Args</param>
        /// <returns>True - started, False otherwise</returns>
        bool StartServiceWithArgs(string serviceName, int timeoutMilliseconds, params string[] args);

        /// <summary>
        /// Stop a window service
        /// </summary>
        /// <param name="serviceName">Name of the window service</param>
        /// <param name="timeoutMilliseconds">Wait for service to stop</param>
        /// <returns>True if service is stopped, otherwise false</returns>
        bool StopService(string serviceName, int timeoutMilliseconds);

        /// <summary>
        /// Restart the window service
        /// </summary>
        /// <param name="serviceName">Name of the window service</param>
        /// <param name="timeoutMilliseconds">Wait for service to restart</param>
        /// <returns>True if service is restarted, otherwise false</returns>
        bool RestartService(string serviceName, int timeoutMilliseconds);

        /// <summary>
        /// Check if the window service exists
        /// </summary>
        /// <param name="serviceName">Name of the window service</param>
        /// <returns>True if the window service exists, false otherwise</returns>
        bool IsServiceInstalled(string serviceName);
    }
}