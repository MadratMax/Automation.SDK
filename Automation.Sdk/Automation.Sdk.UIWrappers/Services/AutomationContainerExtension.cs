namespace Automation.Sdk.UIWrappers.Services
{
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.ApplicationServices;
    using Automation.Sdk.UIWrappers.Services.Bootstrapping;
    using Automation.Sdk.UIWrappers.Services.ClipboardServices;
    using Automation.Sdk.UIWrappers.Services.Container;
    using Automation.Sdk.UIWrappers.Services.ControlConversion;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop;
    using Automation.Sdk.UIWrappers.Services.ExecutionContext;
    using Automation.Sdk.UIWrappers.Services.FileSystem;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using Automation.Sdk.UIWrappers.Services.Platform;
    using Automation.Sdk.UIWrappers.Services.ResourcesServices;
    using Automation.Sdk.UIWrappers.Services.SearchEngines;
    using Automation.Sdk.UIWrappers.Services.TeamCity;
    using JetBrains.Annotations;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// All Automation SDK services registrations container
    /// </summary>
    [UsedImplicitly]
    public sealed class AutomationContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            //Container.AddNewExtension<TraceDriverContainerExtension>();

            Container.AddNewExtension<Interception>();

            // Small trick: such registration will provide ability to resolve any kind of generic-typed parameter.
            // That means, that with this registration, if we will try to resolve Factory<SomeType>, instance of this 
            // type will be created. Moreover, ContainerControlledLifetimeManager will hold all created types and 
            // manage to pool single instance per each different type of generic parameter.
            Container.RegisterType(typeof(IFactory<>), typeof(Factory<>), new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(IFactory<,>), typeof(Factory<,>), new ContainerControlledLifetimeManager());

            Container
                .RegisterSingleton<IBootstrapConsumer, BootstrapContainer>()
                .RegisterSingleton<IBootstrapContainer, BootstrapContainer>();

            
            // TODO: Instanciate it and make autoregister
            Container.RegisterSingleton<WebSearchEngine>();

            // All these components should be kept context-agnostic
            // TODO: Interface bellow
            Container
                .RegisterSingleton<ResourceConsumer>()
                .RegisterSingleton<ProcessService>()
                .RegisterSingleton<ControlTypeConverter>()
                .RegisterSingleton<TeamCityMessageService>()
                .RegisterSingleton<FileService>()
                .RegisterSingleton<PlatformContextSwitcher>()
                .RegisterSingleton<SafeFileService>()
                .RegisterSingleton<ConfigurationFileService>()
                .RegisterSingleton<ControlNamingMapper>()
                .RegisterSingleton<BackupFileService>()
                .RegisterSingleton<Logger>()
                .RegisterSingleton<ControlFacade>()
                .RegisterSingleton<ControlQueries>()
                .RegisterSingleton<FluentConfiguration>()
                .RegisterSingleton<ClipboardCopyService>()
                .RegisterSingleton<LegacyControlsSearchEngineService>()
                .RegisterSingleton<ProcessResourceMediator>()
                .RegisterSingleton<Shouldly>()
                .RegisterSingleton<ControlAdapterFactory>()
                .RegisterSingleton<IExecutionContextMemento, ExecutionContextStorage>()
                .RegisterSingleton<IExecutionContextProvider, ExecutionContextStorage>();
        }
    }
}