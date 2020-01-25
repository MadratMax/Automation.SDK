namespace Automation.Sdk.Bindings
{
    using Automation.Sdk.UIWrappers.Services.Container;
    using Automation.Sdk.UIWrappers.Services.ScreenCapturing;
    using JetBrains.Annotations;
    using Microsoft.Practices.Unity;
    using TechTalk.SpecFlow;

    [Binding]
    public class ScreenCaptureAddon
    {
        private readonly IScreenCapture _screenCapture;

        public ScreenCaptureAddon()
        {
            _screenCapture = ContainerProvider.Container.Resolve<IScreenCapture>();
        }

        [AfterStep(Order = 0)]
        [UsedImplicitly]
        public void SaveScreenshot()
        {
            _screenCapture.StoreScreenshot(ScenarioContext.Current.StepContext.StepInfo.Text);
        }
    }
}