namespace Automation.Sdk.UIWrappers.Services.Container
{
    using Microsoft.Practices.Unity;

    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    // Can be inherited in test suites
    /// <summary>
    /// Provides possibility to create container-controlled service with
    /// passing single additional dependency to its constructor
    /// </summary>
    /// <typeparam name="TInterface">Service public type</typeparam>
    /// <typeparam name="TDependency">Dependency type</typeparam>
    public class Factory<TInterface, TDependency> : IFactory<TInterface, TDependency>
    {
        private readonly IUnityContainer _container;

        public Factory(IUnityContainer container)
        {
            _container = container;
        }

        public virtual TInterface Create(TDependency dependency, string name = null)
        {
            // By default, when container resolving service, if constructor injection is used, it is recursively resolving its dependencies
            // Than, if we want to pass instance of proper dependency to resolving chain, we are creating DependencyOverride and passing it
            // to Resolve<T> method. Than this proper instance will be injected as defined dependencies, all other dependencies will be
            // resolved from container as usual. For example:
            //
            // class MyClass
            // {
            //     public MyClass(FirstDependency firstDependency, SecondDependency secondDependency) { ... }
            // }
            // 
            // ...
            // 
            // {
            //     _container.Resolve<MyClass>(new DependencyOverride<FirstDependency>(firstDependencyInstance))
            // }
            //
            // Than firstDependencyInstance will be passed to constructor directly and SecondDependency will be gained via 
            // var secondDependency = _container.Resolve<SecondDependency>();
            // Read https://msdn.microsoft.com/en-us/library/ff660920(v=pandp.20).aspx for additional context.
            return _container.Resolve<TInterface>(name, new DependencyOverride<TDependency>(dependency));
        }
    }
}
