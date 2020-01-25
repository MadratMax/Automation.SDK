// ReSharper disable once CheckNamespace
// Hook to auto-use these extensions where applicable
namespace Microsoft.Practices.Unity
{
    using System;
    using Automation.Sdk.UIWrappers.Aspects;
    using FluentAssertions;
    using JetBrains.Annotations;
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// Extensions to UnityContainer which are used to hide lifetime managers 
    /// and simplify services registrations
    /// </summary>
    public static class AutomationContainerExtensions
    {
        [NotNull]
        public static IUnityContainer RegisterSingleton<TInterface, TType>([NotNull] this IUnityContainer container)
            where TType : TInterface
        {
            return RegisterSingleton<TInterface, TType>(container, null);
        }

        [NotNull]
        public static IUnityContainer RegisterSingleton(
            [NotNull] this IUnityContainer container, 
            Type iface, 
            Type type, 
            string dependencyName)
        {
            type.Should().BeAssignableTo(iface);

            return container.RegisterType(iface, type,
                dependencyName,
                new ContainerControlledLifetimeManager(),
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior<LoggingInterceptionBehavior>());
        }

        [NotNull]
        public static IUnityContainer RegisterSingleton<TInterface, TType>([NotNull] this IUnityContainer container, string dependencyName)
            where TType : TInterface
        {
            // We are returning IUnityContainer to support native registration fluent syntax like this:
            // Container
            //     .RegisterType<ISomeInterface, SomeImplementation()
            //     .RegisterInstance<ISomeOtherInterface>(myInstance)....;
            return container.RegisterType<TInterface, TType>(
                dependencyName,
                new ContainerControlledLifetimeManager(),
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior<LoggingInterceptionBehavior>());
        }

        [NotNull]
        public static IUnityContainer RegisterSingleton<TType>([NotNull] this IUnityContainer container)
        {
            return container.RegisterSingleton<TType, TType>();
        }
    }
}
