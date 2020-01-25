namespace Automation.Sdk.UIWrappers.Services.Bootstrapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using Microsoft.Practices.ObjectBuilder2;
    using Microsoft.Practices.Unity;

    public class BootstrapContainer : IBootstrapConsumer, IBootstrapContainer
    {
        private readonly IUnityContainer _container;
        private readonly List<Func<IBootstrapper>> _items;

        public BootstrapContainer([NotNull] IUnityContainer container)
        {
            _container = container;
            _items = new List<Func<IBootstrapper>>();
        }

        public virtual void Consume<TBootstrapper>() where TBootstrapper : IBootstrapper
        {
            _items.Add(() => _container.Resolve<TBootstrapper>());
        }

        public IEnumerable<IBootstrapper> Items => _items.Select(x => x());

        public void Bootstrap()
        {
            Items.ForEach(x => x.Bootstrap());
        }
    }
}
