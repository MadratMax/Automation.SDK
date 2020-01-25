namespace Automation.Sdk.UIWrappers.Tests
{
    using Automation.Sdk.UIWrappers.Services.ClipboardServices;
    using Automation.Sdk.UIWrappers.Services.Container;
    using Automation.Sdk.UIWrappers.Services.Threading;
    using FluentAssertions;
    using Microsoft.Practices.Unity;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ClipboardCopyServiceFixture
    {
        [Test]
        public void ShouldCreateInstance()
        {
            TestDelegate action = () => CreateInstance();

            Assert.DoesNotThrow(action);
        }

        [Test]
        public void ShouldCopyTextToClipboard()
        {
            var instance = CreateInstance();

            instance.CopyToClipboard("test123");

            instance.GetClipboardText().Should().Be("test123");
        }

        private ClipboardCopyService CreateInstance()
        {
            var container = ContainerProvider.Container;
            return new ClipboardCopyService(container.Resolve<IStaScheduler>());
        }
    }
}
