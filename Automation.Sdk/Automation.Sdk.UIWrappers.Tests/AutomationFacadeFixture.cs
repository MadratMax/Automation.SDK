namespace Automation.Sdk.UIWrappers.Tests
{
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.Container;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public sealed class AutomationFacadeFixture
    {
        [Test]
        public void ShouldCreateApplicationInvoker()
        {
            AutomationFacade.ApplicationInvoker.Should().NotBeNull();
        }

        [Test]
        public void ShouldCreateApplicationInvokerOnce()
        {
            var firstInvoker = AutomationFacade.ApplicationInvoker;
            AutomationFacade.ApplicationInvoker.Should().BeSameAs(firstInvoker);
        }

        [Test]
        public void ShouldBeRecycledByContainer()
        {
            var firstInvoker = AutomationFacade.ApplicationInvoker;
            ContainerProvider.Recycle();
            AutomationFacade.ApplicationInvoker.Should().NotBeSameAs(firstInvoker);
        }

        [Test]
        public void WindowsServiceFacadeOperational()
        {
            var service = AutomationFacade.WindowsServicesAccessor;

            service.IsServiceInstalled("WSearch").Should().BeTrue();
        }
    }
}
