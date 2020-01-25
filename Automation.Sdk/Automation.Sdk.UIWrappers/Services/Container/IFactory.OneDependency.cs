namespace Automation.Sdk.UIWrappers.Services.Container
{
    using JetBrains.Annotations;

    public interface IFactory<out TInterface, in TDependency>
    {
        TInterface Create(TDependency dependency, [CanBeNull] string name = null);
    }
}
