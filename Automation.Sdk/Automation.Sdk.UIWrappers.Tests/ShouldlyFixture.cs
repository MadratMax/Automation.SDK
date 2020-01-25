namespace Automation.Sdk.UIWrappers.Tests
{
    using Automation.Sdk.UIWrappers.Configuration;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using FluentAssertions;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    internal sealed class ShouldlyFixture
    {
        [Test]
        public void ShouldCreateInstance()
        {
            TestDelegate action = () => CreateInstance();

            Assert.DoesNotThrow(action);
        }

        [Test]
        public void ShouldThrowExceptionOnUnusedExtension()
        {
            TestDelegate action = () =>
            {
                using (CreateInstance().Extend(-1))
                {
                }
            };

            Assert.Throws<InconclusiveException>(action);
        }

        [Test]
        public void ShouldNotThrowExceptionOnExtension()
        {
            TestDelegate action = () =>
            {
                using (CreateInstance().Extend(10))
                {
                    Shouldly.Timeout.Should().Be(10);
                }
            };

            Assert.DoesNotThrow(action);
        }

        private Shouldly CreateInstance()
        {
            return new Shouldly(Mock.Of<ILogger>(), Mock.Of<ISdkConfiguration>());
        }
    }
}
