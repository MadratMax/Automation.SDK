namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using JetBrains.Annotations;

    [UsedImplicitly]
    internal sealed class ContainerAdapter : Element, IContainerAdapter
    {
        private readonly Element _control;

        public ContainerAdapter(AutomationElement automationElement)
            : base(automationElement)
        {
            _control = this;
        }

        public Element Control => _control;
    }
}