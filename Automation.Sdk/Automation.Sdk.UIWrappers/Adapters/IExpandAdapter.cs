namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Windows.Automation;

    public interface IExpandAdapter : IAdapter
    {
        void Expand();

        void Collapse();

        ExpandCollapseState ExpandCollapseState { get; }
    }
}