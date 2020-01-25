namespace Automation.Sdk.UIWrappers.Services.Bootstrapping
{
    using JetBrains.Annotations;

    public interface IBootstrapConsumer
    {
        void Consume<TBootstrapper>() where TBootstrapper : IBootstrapper;
    }
}