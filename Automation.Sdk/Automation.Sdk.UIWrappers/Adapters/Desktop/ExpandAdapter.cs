namespace Automation.Sdk.UIWrappers.Adapters.Desktop
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister (Interface = typeof(IExpandAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Desktop)]
    internal sealed class ExpandAdapter : Element, IExpandAdapter
    {
        public ExpandAdapter([NotNull] IElement element)
            : base(element)
        {
        }

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