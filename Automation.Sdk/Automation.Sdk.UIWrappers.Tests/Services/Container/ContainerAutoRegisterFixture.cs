namespace Automation.Sdk.UIWrappers.Tests.Services.Container
{
    using Automation.Sdk.UIWrappers.Services.ApplicationServices;
    using Automation.Sdk.UIWrappers.Services.Container;
    using Automation.Sdk.UIWrappers.Services.Logging;

    using FluentAssertions;
    using NUnit.Framework;
    using Microsoft.Practices.Unity;

    public class ContainerAutoRegisterFixture
    {
        [Test]
        public void ShouldResolveAutoRegisterTypes()
        {
            var container = ContainerProvider.Container;

            container.Registrations.Should().Contain(x => x.RegisteredType == typeof(ILogger)
                                                          && x.MappedToType == typeof(Logger));
        }

        [Test]
        public void ShouldResolveClassesWithAutoRegisteredTypes()
        {
            var container = ContainerProvider.Container;

            TestDelegate action = () =>
            {
                var applicationInvoker = container.Resolve<IApplicationInvoker>();
            };

            Assert.DoesNotThrow(action);
        }
    }
}
