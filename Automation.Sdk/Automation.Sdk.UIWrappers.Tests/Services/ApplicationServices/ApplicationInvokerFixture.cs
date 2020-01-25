namespace Automation.Sdk.UIWrappers.Tests.Services.ApplicationServices
{
    using Automation.Sdk.UIWrappers.Services.ControlConversion;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using Automation.Sdk.UIWrappers.Services.ApplicationServices;

    using NUnit.Framework;
    using UIWrappers.Services;
    using UIWrappers.Services.ScreenCapturing;
    using UIWrappers.Services.ResourcesServices;

    [TestFixture]
    internal sealed class ApplicationInvokerFixture
    {
        private bool initialized = false;
        private IApplicationInvoker _applicationInvoker;
        private IApplicationInfoContainer _applicationInfoContainer;

        [SetUp]
        public void SetUp()
        {
            if (initialized)
            {
                return;
            }

            var logger = new Logger();
            var c = new ControlTypeConverter(logger);
            var cs = new ControlTypeConverterService(c);
            var ps = new ProcessService(logger);
            var sc = new ScreenCapture(logger);
            var pk = new ProcessKiller(logger);
            var rw = new RuntimeWatcher(logger, pk, sc);

            _applicationInfoContainer = new ApplicationInfoContainer();
            _applicationInvoker = new ApplicationInvoker(ps, cs, logger, _applicationInfoContainer, rw);

            initialized = true;
        }

        [Test]
        public void ShouldLaunchNotepad()
        {
            var calc = new DefaultApplicationInfo("Notepad", "", @"%windir%\system32\notepad.exe", "", "", "", "");
            _applicationInfoContainer.Add("Notepad", calc);

            var p = _applicationInvoker.StartApplication("Notepad", string.Empty);

            p.CloseMainWindow();
        }
    }
}