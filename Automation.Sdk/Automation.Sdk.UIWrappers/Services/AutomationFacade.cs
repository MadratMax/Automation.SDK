namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using Automation.Sdk.UIWrappers.Configuration;
    using Automation.Sdk.UIWrappers.Services.ApplicationServices;
    using Automation.Sdk.UIWrappers.Services.ClipboardServices;
    using Automation.Sdk.UIWrappers.Services.Container;
    using Automation.Sdk.UIWrappers.Services.ControlConversion;
    using Automation.Sdk.UIWrappers.Services.ResourcesServices;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using Automation.Sdk.UIWrappers.Services.ScreenCapturing;
    using Automation.Sdk.UIWrappers.Services.SearchEngines;
    using Automation.Sdk.UIWrappers.Services.TeamCity;

    using Microsoft.Practices.Unity;

    public static class AutomationFacade
    {
        private static Lazy<IApplicationInvoker> _applicationInvokerProvider;
        private static Lazy<ControlTypeConverter> _controlTypeConverterProvider;
        private static Lazy<TeamCityMessageService> _teamCityMessageServiceProvider;
        private static Lazy<ProcessService> _processServiceProvider;
        private static Lazy<WindowsServicesAccessor> _windowsServiceProvider;
        private static Lazy<ILogger> _loggerProvider;
        private static Lazy<ControlTypeConverterService> _controlTypeConverterServiceProvider;
        private static Lazy<LegacyControlsSearchEngineService> _legacyControlSearchEngineProvider;
        private static Lazy<ClipboardCopyService> _clipboardCopyServiceProvider;
        private static Lazy<ScreenCapture> _screenCapture;
        private static Lazy<IProcessKiller> _processKiller;
        private static Lazy<ISdkConfiguration> _sdkConfiguration;

        static AutomationFacade()
        {
            CreateProviders();
        }

        public static IApplicationInvoker ApplicationInvoker => _applicationInvokerProvider.Value;

        public static ControlTypeConverter ControlTypeConverter => _controlTypeConverterProvider.Value;

        public static TeamCityMessageService TeamCityMessageService => _teamCityMessageServiceProvider.Value;

        public static ProcessService ProcessService => _processServiceProvider.Value;

        public static WindowsServicesAccessor WindowsServicesAccessor => _windowsServiceProvider.Value;

        public static ILogger Logger => _loggerProvider.Value;

        public static ControlTypeConverterService ControlTypeConverterService => _controlTypeConverterServiceProvider.Value;

        public static LegacyControlsSearchEngineService LegacyControlSearchEngineService => _legacyControlSearchEngineProvider.Value;

        public static ClipboardCopyService ClipboardCopyService => _clipboardCopyServiceProvider.Value;

        public static ISdkConfiguration SdkConfiguration => _sdkConfiguration.Value;

        public static ScreenCapture ScreenCaptureService => _screenCapture.Value;

        public static IProcessKiller ProcessKillerService => _processKiller.Value;

        public static void Recycle()
        {
            CreateProviders();
        }

        private static void CreateProviders()
        {
            _applicationInvokerProvider = CreateServiceProvider<IApplicationInvoker>();
            _controlTypeConverterProvider = CreateServiceProvider<ControlTypeConverter>();
            _teamCityMessageServiceProvider = CreateServiceProvider<TeamCityMessageService>();
            _processServiceProvider = CreateServiceProvider<ProcessService>();
            _windowsServiceProvider = CreateServiceProvider<WindowsServicesAccessor>();
            _loggerProvider = CreateServiceProvider<ILogger>();
            _controlTypeConverterServiceProvider = CreateServiceProvider<ControlTypeConverterService>();
            _legacyControlSearchEngineProvider = CreateServiceProvider<LegacyControlsSearchEngineService>();
            _clipboardCopyServiceProvider = CreateServiceProvider<ClipboardCopyService>();
            _screenCapture = CreateServiceProvider<ScreenCapture>();
            _processKiller = CreateServiceProvider<IProcessKiller>();
            _sdkConfiguration = CreateServiceProvider<ISdkConfiguration>();
        }

        private static Lazy<TService> CreateServiceProvider<TService>()
        {
            return new Lazy<TService>(() => ContainerProvider.Container.Resolve<TService>());
        }
    }
}