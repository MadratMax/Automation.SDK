namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using JetBrains.Annotations;

    [UsedImplicitly]
    internal sealed class ExpandAdapter : Element, IExpandAdapter
    {
        public ExpandAdapter([NotNull] AutomationElement element) : base(element) { }

        public void Expand()
        {
            ExpandCollapsePattern expandCollapsePattern = GetPattern<ExpandCollapsePattern>(ExpandCollapsePattern.Pattern);
            expandCollapsePattern.Expand();
        }

        public void Collapse()
        {
            ExpandCollapsePattern expandCollapsePattern = GetPattern<ExpandCollapsePattern>(ExpandCollapsePattern.Pattern);
            expandCollapsePattern.Collapse();
        }

        public ExpandCollapseState ExpandCollapseState => GetProperty<ExpandCollapseState>(ExpandCollapsePattern.ExpandCollapseStateProperty);
    }
}