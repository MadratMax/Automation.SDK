namespace Automation.Sdk.UIWrappers.Services.Bootstrapping
{
    using System.Collections.Generic;
    using JetBrains.Annotations;

    public interface IBootstrapContainer
    {
        [NotNull, ItemNotNull]
        IEnumerable<IBootstrapper> Items { get; }

        void Bootstrap();
    }
}