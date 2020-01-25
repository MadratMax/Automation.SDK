namespace Automation.Sdk.UIWrappers.Services.Container
{
    using JetBrains.Annotations;
    using Microsoft.Practices.Unity;
    
    public class Factory<TInterface> : IFactory<TInterface>
    {
        private readonly IUnityContainer _container;

        public Factory([NotNull] IUnityContainer container)
        {
            _container = container;
        }

        public virtual TInterface Create(string dependencyName = null)
        {
            return _container.Resolve<TInterface>(dependencyName);
        }
    }
}