namespace Automation.Sdk.UIWrappers.Services.Container
{
    using System;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Provides access to UnityContainer instance
    /// </summary>
    public static class ContainerProvider
    {
        /// <summary>
        /// Lazy-initialized UnityContainer
        /// https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx
        /// </summary>
        private static Lazy<ISdkContainer> _container;

        static ContainerProvider()
        {
            _container = new Lazy<ISdkContainer>(CreateContainer);
        }

        public static ISdkContainer Container => _container.Value;

        /// <summary>
        /// Dispose container and cascade-dispose all services which are holding by it.
        /// Recreates container and re-register all services in it.
        /// </summary>
        public static void Recycle()
        {
            Container.Dispose();
            _container = new Lazy<ISdkContainer>(CreateContainer);
            AutomationFacade.Recycle();

            Console.Out.WriteLine("Automation SDK: Container recycled");
        }

        private static ISdkContainer CreateContainer()
        {
            Console.Out.WriteLine("Automation SDK: New container created");

            var container = new SdkContainer();
            // Adding extension with all Automation SDK services registrations
            container.AddNewExtension<AutomationContainerExtension>();
            container.AutoRegisterInterfaces(null);

            container.Start();

            return container;
        }
    }
}
