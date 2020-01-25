namespace Automation.Sdk.UIWrappers.Services.Container
{
    public enum RegistrationType
    {
        /// <summary>
        /// This registration will be used as singletone
        /// </summary>
        Singletone = 1,

        /// <summary>
        /// This registration will resolve type creating new instance
        /// each time it's resolved
        /// For example for registrations which should have per-call lifetime management because underlying types
        /// are strongly connected with context-level object (Automation.Sdk.UIWrappers.ControlDrivers in our case).
        /// That means, that we are unable to store these instance in contextless singletone setup.
        /// Like adapters for UIA or Selenium UIobjects
        /// </summary>
        Instanced = 2,
    }
}