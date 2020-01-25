namespace Automation.Sdk.UIWrappers.Tests
{
    using System;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using FluentAssertions;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CurrentContainerProviderFixture
    {
        private Mock<Logger> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<Logger>(MockBehavior.Loose);
        }

        [Test]
        public void ShouldCreateInstance()
        {
            TestDelegate action = () => CreateInstance();

            Assert.DoesNotThrow(action);
        }

        [TestCase("automationId", "")]
        [TestCase("", "caption")]
        public void ShouldFollowSingleBreadcrumb(string automationId, string caption)
        {
            var instance = CreateInstance();

            instance.Set(new WindowAppearanceBreadcrumbs(automationId, caption, null));

            instance.Get().Should().BeNull();

            instance.Follow(new WindowAppearanceBreadcrumbs(automationId, caption, null));

            TestDelegate action = () => instance.Get();

            Assert.Throws<InvalidOperationException>(action);
        }

        [Test]
        public void ShouldFollowToPreviousElement()
        {
            var instance = CreateInstance();

            instance.Set(new WindowAppearanceBreadcrumbs("test", string.Empty, null));

            var topElement = new WindowAppearanceBreadcrumbs("test2", string.Empty, null);

            instance.Set(topElement);

            instance.Follow(topElement);

            instance.GetAll().Should().HaveCount(1);
        }

        [Test]
        public void ShouldFollowRootElement()
        {
            var instance = CreateInstance();

            instance.Set(new WindowAppearanceBreadcrumbs("test", string.Empty, null));

            instance.Set(new WindowAppearanceBreadcrumbs("test2", string.Empty, null));

            instance.Follow(new WindowAppearanceBreadcrumbs("test", string.Empty, null));

            instance.GetAll().Should().BeEmpty();
        }

        private CurrentContainerProvider CreateInstance()
        {
            return new CurrentContainerProvider(_loggerMock.Object);
        }
    }
}
