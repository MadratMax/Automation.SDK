namespace Automation.Sdk.UIWrappers.Services.ApplicationServices
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.IO;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Services.ControlConversion;
    using Automation.Sdk.UIWrappers.Services.Logging;

    using JetBrains.Annotations;
    using NUnit.Framework;

    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    // Can be inherited in test suites
    [UsedImplicitly]
    [AutoRegister]
    public class ApplicationInvoker : IApplicationInvoker
    {
        private readonly IApplicationInfoContainer _applicationInfoContainer;
        private readonly IRuntimeWatcher _runtimeWatcher;
        private readonly ProcessService _processService;
        private readonly ControlTypeConverterService _controlTypeConverterService;
        private readonly ILogger _logger;

        // Pinvoke declaration for ShowWindow & GetWindowLong
        private const int SW_SHOWMAXIMIZED = 3;
        private const int GWL_STYLE = -16;
        private const uint WS_MAXIMIZEBOX = 0x00010000;
        

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        public ApplicationInvoker(ProcessService processService,
                                  ControlTypeConverterService controlTypeConverterService,
                                  ILogger logger,
                                  IApplicationInfoContainer applicationInfoContainer,
                                  IRuntimeWatcher runtimeWatcher)
        {
            _runtimeWatcher = runtimeWatcher;
            _processService = processService;
            _controlTypeConverterService = controlTypeConverterService;
            _logger = logger;
            _applicationInfoContainer = applicationInfoContainer;
        }

        public Process StartApplication(IApplicationInfo appInfo, string arguments)
        {
            _logger.Write($@"Starting application ""{appInfo.ApplicationName}""");

            arguments = arguments == string.Empty ? appInfo.AdditionalParameters : string.Join(" ", appInfo.AdditionalParameters, arguments);

            var process = _processService.Start(appInfo.ExePath, arguments);

            _logger.Write($@"Waiting for main window...");
            Methods.WaitUntil(
                () => process.MainWindowHandle != IntPtr.Zero || _runtimeWatcher.IsFailed,
                $@"Application ""{appInfo.ApplicationName}"" have no window",
                60000);

            Assert.IsFalse(_runtimeWatcher.IsFailed, $@"Runtime watcher exception while starting application ""{appInfo.ApplicationName}""");

            _logger.Write($@"Main window appeared");

            try
            {
                var styles = GetWindowLong(process.MainWindowHandle, GWL_STYLE);

                var canBeMaximized = styles & WS_MAXIMIZEBOX;
                if (canBeMaximized == WS_MAXIMIZEBOX)
                {
                    _logger.Write($@"Main window can be maximized. Trying to maximize");
                    ShowWindow(process.MainWindowHandle, SW_SHOWMAXIMIZED);
                }
            }
            catch (Exception e)
            {
                _logger.Write("Cannot maximize window:");
                _logger.Write(e);
            }

            return process;
        }

        public virtual Process StartApplication(string applicationName, string arguments)
        {
            var app = _applicationInfoContainer.Get(applicationName);
            return StartApplication(app, arguments);
        }

        /// <summary>
        /// Launch a module
        /// </summary>
        /// <param name="executable">application</param>
        /// <param name="args">arguments</param>
        /// <param name="name">name of the window (empty string is none)</param>
        /// <param name="automationid">automation id of the window (empty string is none)</param>
        /// <returns>Window object</returns>
        [Obsolete]
        public virtual UIAWindow LaunchUIApp(string executable, string args, string name, string automationid)
        {
            var process = _processService.Start(executable, args);
            var fileName = Path.GetFileName(executable);

            ContextStorage.Add("ModuleProcess", process);

            var availableWindows = new List<string>
            {
                "MultiChoiceDlg",
                "DesktopSetupWindow",
                "DCCClientHost",
                "LoginWindow"
            };

            // TODO: reference Automation ID

            if (automationid != string.Empty)
            {
                availableWindows.Add(automationid);
            }

            UIAWindow uiaWin = null;
            PropertyCondition condition = new PropertyCondition(AutomationElementIdentifiers.ControlTypeProperty, ControlType.Window);
            
            _logger.Write($"{fileName}: Waiting for main window to launch.");

            Methods.Action action = () =>
            {
                AutomationElementCollection topWindows = AutomationElement.RootElement.FindAll(TreeScope.Children, condition);
                UIAWindow[] windows = _controlTypeConverterService.Convert<UIAWindow>(topWindows);

                foreach (var win in windows)
                {
                    if (availableWindows.Contains(win.AutomationId) || win.Name.Equals(name))
                    {
                        uiaWin = win;
                        break;
                    }
                }

                return uiaWin != null;

            };
            long timePassed = Methods.DelayedAction(action, 60000);

            if (uiaWin == null)
            {
                _logger.Write($"{fileName}: Main window not found for 60 seconds.");
                return null;
            }

            _logger.Write($"{fileName}: Main window found after {timePassed}ms");
            _logger.Write($"Window name = {uiaWin.Name}, Automation ID = {uiaWin.AutomationId}");

            ContextStorage.Add(SpecIds.MainWindow, uiaWin);

            try
            {
                uiaWin.SetWindowVisualState(WindowVisualState.Maximized);
            }
            catch
            {
                // ignored
            }

            if (!string.IsNullOrEmpty(automationid))
            {
                Assert.AreEqual(automationid, uiaWin.AutomationId, "Wrong window is opened.  Unexpected automation id returned for the window");
            }

            if (!string.IsNullOrEmpty(name))
            {
                Assert.AreEqual(name, uiaWin.Name, "Wrong window is opened.  Unexpected name returned for the window");
            }

            return uiaWin;
        }
    }
}