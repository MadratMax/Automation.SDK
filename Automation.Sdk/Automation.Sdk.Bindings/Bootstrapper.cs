namespace Automation.Sdk.Bindings
{
    using System.Linq;
    using BoDi;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.Bootstrapping;
    using Automation.Sdk.UIWrappers.Services.Container;
    using Automation.Sdk.UIWrappers.Services.ExecutionContext;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using JetBrains.Annotations;
    using Microsoft.Practices.ObjectBuilder2;
    using Microsoft.Practices.Unity;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Tracing;


    /// <summary>
    /// This is a SpecFlow bootstrapper which is setting up SpecFlow IoC container to work with our services
    /// https://habrahabr.ru/post/131993/
    /// </summary>
    [Binding]
    public sealed class Bootstrapper
    {
        private const string ContainerContextKey = "container";

        /// <summary>
        /// SpecFlow BoDi container
        /// Get rid of https://github.com/techtalk/SpecFlow/wiki/Context-Injection for additional information
        /// </summary>
        private readonly IObjectContainer _specFlowContainer;

        private IExecutionContextMemento _context;
        private IRuntimeWatcher _runtimeWatcher;

        /// <summary>
        /// BoDi container instance is injected to constructor by SpecFlow itself
        /// </summary>
        public Bootstrapper([NotNull] IObjectContainer specFlowContainer)
        {
            _specFlowContainer = specFlowContainer;
        }

        [BeforeScenario(Order = BootstrapEventOrder.CONTAINER_CREATION)]
        [UsedImplicitly]
        public void BootstrapContainer()
        {
            var container = ContainerProvider.Container;

            container.AddNewExtension<SpecFlowContainerExtension>();

            // Unity services should be able to use services from BoDi container
            // but it is very dangerous to transfer all registered services to Unity 
            // because SpecFlow supports their own lifecycle. So, it is necessary to 
            // enlist transfered services explicitly.

            // Here InjectionFactory (http://smarly.net/dependency-injection-in-net/di-containers/unity/configuring-difficult-apis)
            // is used to wrap BoDi services into lightweight lazy-loaded box just to not enforce BoDi services
            // to be contracted directly during boostrapping. Such statement as
            //
            // container.RegisterType<TInterface>(new InjectionFactory(MyMethod));
            //
            // public TInterface MyMethod(object context) { ... }
            //
            // actually means 
            // "Dear MS Unity, if you will need to resolve TInterface, please be free to skip whole registrations 
            // tracing and just call MyMethod to get instance. Sincerely yours, automation development team."
            container.RegisterType<ITraceListener>(new InjectionFactory(_ => _specFlowContainer.Resolve<ITraceListener>()));
        }

        /// <summary>
        /// We are using Unity as our own IoC container to make our services SpecFlow-agnostic
        /// so we need to make UnityContainer a transparent proxy in BoDi container
        /// in scope of our services
        /// </summary>
        [BeforeScenario(Order = BootstrapEventOrder.CONTAINER_TYPES_INSTANTIATION)]
        [UsedImplicitly]
        public void Bootstrap()
        {
            var container = ContainerProvider.Container;

            // Iterate through all components in UnityContainer
            // which are registered as Singleton.
            // ContainerControlledLifetimeManager is synchronizing service lifetime with container lifetime
            foreach (var registration in container.Registrations.Where(x => x.LifetimeManagerType == typeof(ContainerControlledLifetimeManager)))
            {
                var frontEndType = registration.RegisteredType;

                // If current registration is a generic type
                // we can not guarantee that we are able to 
                // instantiate it. For examples look at following  registrations:
                // Container.RegisterType(typeof(Factory<,>), typeof(Factory<,>), new ContainerControlledLifetimeManager());
                if (frontEndType.IsGenericType)
                {
                    continue;
                }

                // Resolving underlying type. Here we can not just create instance via Activator 
                // or something like this because there can be some tricks with factories and proxies
                // http://blog.raffaeu.com/archive/2011/06/12/unity-and-injection-with-factories.aspx
                var implementation = container.Resolve(frontEndType, registration.Name);

                // Registering instance in BoDi container.
                // There is a possibility to register type, but BoDi container is not 
                // supporting any tricks in scope of instance creation. 
                // It is one of major restrictions why we also need to use Unity.
                _specFlowContainer.RegisterInstanceAs(implementation, frontEndType, registration.Name);
            }

            // Adding UnityContainer to SpecFlow scenario context 
            // to provide direct access to it.
            ScenarioContext.Current.Add(ContainerContextKey, container);

            container.Resolve<IProductLogger>();

            _runtimeWatcher = container.Resolve<IRuntimeWatcher>();
            _runtimeWatcher.Start();

            container.Resolve<IBootstrapContainer>().Bootstrap();

            _context = container.Resolve<IExecutionContextMemento>();
            _context.RecordTitle(ScenarioContext.Current.ScenarioInfo.Title);

            ScenarioContext.Current.ScenarioInfo.Tags.ForEach(_context.RecordTag);
        }

        [BeforeStep, UsedImplicitly]
        public void LogStep()
        {
            if (_runtimeWatcher.IsFailed)
            {
                throw _runtimeWatcher.FailReason;
            }

            var info = ScenarioContext.Current.StepContext.StepInfo;
            _context.RecordStep($"{info.StepDefinitionType} {info.Text}");
        }

        /// <summary>
        /// AfterScenario hook which is cleaning all resources 
        /// and caches at scenario level
        /// </summary>
        [AfterScenario, UsedImplicitly]
        public void CleanUp()
        {
            _runtimeWatcher.Stop();

            // It is supposed that all resources are holding by UnityContainer
            // because it is treated as ObjectPool in SpecFlow scenario lifecycle
            // http://www.oodesign.com/object-pool-pattern.html
            ContainerProvider.Recycle();
        }
    }
}
