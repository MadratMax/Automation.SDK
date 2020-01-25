namespace Automation.Sdk.UIWrappers.Tests.Services.Container
{
    using System.Linq;
    using Automation.Sdk.UIWrappers.Services.Container;
    using FluentAssertions;
    using Microsoft.Practices.Unity;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class FactoryFixture
    {
        private Mock<IUnityContainer> _containerMock;

        [SetUp]
        public void SetUp()
        {
            _containerMock = new Mock<IUnityContainer>(MockBehavior.Strict);
        }

        [Test]
        public void ShouldCreateInstance()
        {
            TestDelegate action = () => CreateInstance();

            Assert.DoesNotThrow(action);
        }

        [Test]
        public void ShouldReturnImplementation()
        {
            var dependency = new MyClass();
            var instance = CreateInstance();

            _containerMock.Setup(x => x.Resolve(typeof(IMyInterface), null)).Returns(dependency);

            var actualDependency = instance.Create();

            _containerMock.Verify(x => x.Resolve(typeof(IMyInterface), null), Times.Once);
            
            actualDependency.Should().BeSameAs(dependency);
        }

        [TestCase("test")]
        [TestCase("abc")]
        [TestCase("123456")]
        public void ShouldReturnNamedDependency(string name)
        {
            var dependency = new MyClass();
            var instance = CreateInstance();

            _containerMock.Setup(x => x.Resolve(typeof(IMyInterface), name)).Returns(dependency);

            var actualDependency = instance.Create(name);

            _containerMock.Verify(x => x.Resolve(typeof(IMyInterface), name), Times.Once);

            actualDependency.Should().BeSameAs(dependency);
        }

        private Factory<IMyInterface> CreateInstance()
        {
            return new Factory<IMyInterface>(_containerMock.Object);
        }

        public class MyClass : IMyInterface { }

        public interface IMyInterface { }
    }
}
