namespace Automation.Sdk.UIWrappers.Services.ResourcesServices
{
    using System;
    using System.Diagnostics;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.Services.Logging;

    [AutoRegister]
    public class ProcessKiller : IProcessKiller
    {
        private readonly ILogger _logger;

        public ProcessKiller(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Kill the process
        /// </summary>
        /// <param name="process">Instance of the process</param>
        /// <param name="waitTime">Time to wait before kill command</param>
        /// <returns>True if process was shutted by itself, false if it was killed</returns>
        public bool KillProcess(Process process, int waitTime)
        {
            try
            {
                if (waitTime > 0 && process.WaitForExit(waitTime))
                {
                    return true;
                }
            }
            catch
            {
                // ignored
            }

            try
            {
                _logger.Write($"killing process {process}");
                process.Kill();
            }
            catch (Exception exception)
            {
                _logger.Write($"process can not be killed: {exception}");
            }

            return false;
        }

        /// <summary>
        /// Kill the process
        /// </summary>
        /// <param name="processId">Id of the process</param>
        /// <param name="waitTime">Time to wait before kill command</param>
        /// <returns>True if process was shutted by itself, false if it was killed</returns>
        public bool KillProcess(int processId, int waitTime)
        {
            try
            {
                var process = Process.GetProcessById(processId);
                return KillProcess(process, waitTime);
            }
            catch (Exception exception)
            {
                _logger.Write($"process can not be killed: {exception}");
            }

            // Process already died when we tried to kill it, so probably it was shutdown
            return true;
        }
    }
}