namespace Automation.Sdk.UIWrappers.Services.ApplicationServices
{
    using System;
    using System.Diagnostics;
    using Automation.Sdk.UIWrappers.ControlDrivers;

    public interface IApplicationInvoker
    {
        [Obsolete]
        UIAWindow LaunchUIApp(string executable, string args, string name, string automationid);

        Process StartApplication(string applicationName, string arguments);

        Process StartApplication(IApplicationInfo appInfo, string arguments);
    }
}