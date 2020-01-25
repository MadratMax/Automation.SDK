namespace Automation.Sdk.UIWrappers.Services.Container
{
    using JetBrains.Annotations;

    public interface IFactory<out TInterface>
    {
        [NotNull]
        TInterface Create([CanBeNull] string dependencyName = null);
    }
}