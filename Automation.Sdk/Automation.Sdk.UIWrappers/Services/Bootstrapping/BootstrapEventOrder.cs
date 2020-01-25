namespace Automation.Sdk.UIWrappers.Services.Bootstrapping
{
    /// <summary>
    /// Time diagram of bootstrap events order
    /// used for [BeforeScenario] hooks only
    /// </summary>
    public static class BootstrapEventOrder
    {
        /// <summary>
        /// The time before any other activity is started
        /// Have some specific usages for Logging.
        /// !!! WARNING: No any types are instantiated or added at this point so only static classes can be used !!!
        /// </summary>
        public const int BEGINNING = 0;

        /// <summary>
        /// The time when Container is created.
        /// Must have only SINGLE reference
        /// !!! SHOULD NOT BE USED OUTSIDE OF THE SDK !!!
        /// </summary>
        public const int CONTAINER_CREATION = 90;

        /// <summary>
        /// The time when Container is created but types are not yet instantiated.
        /// Most common time to add/replace extensions in Container 
        /// within custom [BeforeScenario] hooks outside of SDK (between 91 and 99)
        /// </summary>
        public const int AFTER_CONTAINER_CREATED = 95;

        /// <summary>
        /// The time when all types from extensions are instantiated for container 
        /// Must have only SINGLE reference
        /// !!! SHOULD NOT BE USED OUTSIDE OF THE SDK !!!
        /// </summary>
        public const int CONTAINER_TYPES_INSTANTIATION = 100;

        /// <summary>
        /// The time after all container activities are done and it is safe to resolve dependencies in custom classes.
        /// Most common time to trigger custom [BeforeScenario] hooks (between 120 and 150) outside of the SDK.
        /// </summary>
        public const int AFTER_CONTAINER_TYPES_INSTANTIATED = 120;

        /// <summary>
        /// The time when Container set up methods are finished.
        /// (between 120 and 150)
        /// </summary>
        public const int AFTER_SET_UP_SEQUENCE = 150;

        /// <summary>
        /// Default SpecFlow [BeforeScenario] order value.
        /// </summary>
        public const int DEFAULT = 1000;
    }
}