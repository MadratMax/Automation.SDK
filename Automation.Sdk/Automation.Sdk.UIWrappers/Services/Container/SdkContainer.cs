namespace Automation.Sdk.UIWrappers.Services.Container
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.Services.ResourcesServices;
    using Automation.Sdk.UIWrappers.Services.TeamCity;
    using FluentAssertions;
    using JetBrains.Annotations;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;
    using NUnit.Framework;

    public sealed class SdkContainer : UnityContainer, ISdkContainer
    {
        private ResourceConsumer _resourceConsumer;
        private TeamCityMessageService _teamCityMessageService;

        public void Start()
        {
            _resourceConsumer = this.Resolve<ResourceConsumer>();
            _teamCityMessageService = this.Resolve<TeamCityMessageService>();
            this.Resolve<ProcessResourceMediator>();

            _teamCityMessageService.OpenMessageBlock(TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        /// Important to firstly dispose all the resources so that
        /// dead Disposed services didn't get called again from resourceConsumer
        /// and memory leak from zombie-services apocalypse
        /// </summary>
        public new void Dispose()
        {
            _teamCityMessageService.OpenMessageBlock("CleaningUp");
            _resourceConsumer.Dispose();
            base.Dispose();

            var testName = TestContext.CurrentContext.Test.Name;
            var testStatus = TestContext.CurrentContext.Result.Status;

            _teamCityMessageService.CloseMessageBlock("CleaningUp");
            _teamCityMessageService.CloseMessageBlock(testName);
            _teamCityMessageService.ReportProgressMessage("{0}: {1}", testName, testStatus);
        }

        /// <summary>
        /// Using <see cref="AutoRegisterAttribute"/> attribute on Classes implementing interfaces
        /// we can automate simple registration ( Entity: IEntity ) of Services without explicitly listing them above.
        /// </summary>
        /// <param name="assembly">Assembly to scan. Pass Null to use Assembly.GetExecutingAssembly();</param>
        public void AutoRegisterInterfaces(Assembly assembly)
        {   
            if (assembly == null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }
            
            var types = assembly.GetTypes()
                .Where(type => type.IsClass && type.GetCustomAttribute<AutoRegisterAttribute>() != null);

            foreach (var type in types)
            {
                var registrationInfo = type.GetCustomAttribute<AutoRegisterAttribute>();
                Type iface;

                if (registrationInfo.Interface == null)
                {
                    iface = type.GetInterfaces()
                        .First(i =>
                            i.Namespace != "System");
                }
                else
                {
                    iface = type.GetInterface(registrationInfo.Interface.Name);
                }

                if (registrationInfo.RegistrationType == RegistrationType.Singletone)
                {
                    RegisterSingleton(iface, type, registrationInfo.RegistrationName?.ToString());
                }
                else if (registrationInfo.RegistrationType == RegistrationType.Instanced)
                {
                    RegisterType(iface, type, registrationInfo.RegistrationName?.ToString(), null, new InjectionMember[] {});
                }
                else
                {
                    throw new NotImplementedException(
                        $"Registration of type {registrationInfo.RegistrationType} is not implemented");
                }
            }
        }

        [NotNull]
        public ISdkContainer RegisterSingleton<TType>()
        {
            return RegisterSingleton<TType, TType>();
        }

        [NotNull]
        public ISdkContainer RegisterSingleton<TInterface, TType>()
            where TType : TInterface
        {
            return RegisterSingleton<TInterface, TType>(null);
        }

        [NotNull]
        public ISdkContainer RegisterSingleton<TInterface, TType>(string dependencyName)
            where TType : TInterface
        {
            // We are returning ISdkContainer to support native registration fluent syntax like this:
            // Container
            //     .RegisterType<ISomeInterface, SomeImplementation>()
            //     .RegisterInstance<ISomeOtherInterface>(myInstance)....;
            var iface = typeof(TInterface);
            var type = typeof(TType);

            return RegisterSingleton(iface, type, dependencyName);
        }

        [NotNull]
        public ISdkContainer RegisterSingleton(
            Type iface,
            Type type,
            string dependencyName)
        {
            type.Should().BeAssignableTo(iface);
            RegisterType(
                iface, 
                type,
                dependencyName,
                new ContainerControlledLifetimeManager(),
                new InjectionMember[]
                {
                    new Interceptor<VirtualMethodInterceptor>(),
                    new InterceptionBehavior<LoggingInterceptionBehavior>()
                });

            return this;
        }
    }
}