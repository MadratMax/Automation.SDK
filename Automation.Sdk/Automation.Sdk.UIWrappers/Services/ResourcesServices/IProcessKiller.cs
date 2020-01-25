namespace Automation.Sdk.UIWrappers.Services.ResourcesServices
{
    using System.Diagnostics;

    public interface IProcessKiller
    {
        bool KillProcess(Process process, int waitTime);

        bool KillProcess(int processId, int waitTime);
    }
}