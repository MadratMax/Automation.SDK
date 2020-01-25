namespace Automation.Sdk.UIWrappers.Services.Container
{
    using System;
    using System.Reflection;
    using Microsoft.Practices.Unity;

    public interface ISdkContainer : IUnityContainer
    {
        void Start();

        // ReSharper disable once UnusedMember.Global
        void AutoRegisterInterfaces(Assembly assembly);

        ISdkContainer RegisterSingleton<TType>();

        ISdkContainer RegisterSingleton<TInterface, TType>()
            where TType : TInterface;

        ISdkContainer RegisterSingleton<TInterface, TType>(string dependencyName)
            where TType : TInterface;

        ISdkContainer RegisterSingleton(
            Type iface,
            Type type,
            string dependencyName);
    }
}