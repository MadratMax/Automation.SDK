namespace Automation.Sdk.UIWrappers.Tests.Services.Bootstrapping
{
    using Automation.Sdk.UIWrappers.Services.Bootstrapping;
    using FluentAssertions;
    using Microsoft.Practices.Unity;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    internal sealed class BootstrapContainerFixture
    {
        private Mock<IUnityContainer> _containerMock;

        [SetUp]
        public void SetUp()
        {
            _containerMock = new Mock<IUnityContainer>();
        }

        [Test]
        public void ShouldCreateInstance()
        {
            TestDelegate action = () => CreateInstance();

            Assert.DoesNotThrow(action);
        }

        [Test]
        public void ShouldConsumeItem()
        {
            var instance = CreateInstance();
            var item = Mock.Of<IBootstrapper>();

            _containerMock.Setup(x => x.Resolve(typeof(IBootstrapper), null)).Returns(item);

            instance.Consume<IBootstrapper>();

            instance.Items.Should().Equal(item);
        }

        private BootstrapContainer CreateInstance()
        {
            return new BootstrapContainer(_containerMock.Object);
        }
    }
}
