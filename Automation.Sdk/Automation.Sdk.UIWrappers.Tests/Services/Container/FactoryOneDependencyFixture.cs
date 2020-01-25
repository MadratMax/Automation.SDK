namespace Automation.Sdk.UIWrappers.Tests.Services.Container
{
    using Automation.Sdk.UIWrappers.Services.Container;
    using FluentAssertions;
    using Microsoft.Practices.Unity;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    internal sealed class FactoryOneDependencyFixture
    {
        private Mock<IUnityContainer> _containerMock;
        private MyClass _dependency;

        [SetUp]
        public void SetUp()
        {
            _containerMock = new Mock<IUnityContainer>(MockBehavior.Strict);
            _dependency = new MyClass();
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
            var service = new MyClassWithDependency(_dependency);
            var instance = CreateInstance();

            _containerMock.Setup(x => x.Resolve(typeof(IMyClassWithDependency), null, It.IsAny<ResolverOverride[]>())).Returns(service);

            var actualDependency = instance.Create(_dependency);

            _containerMock.Verify(x => x.Resolve(typeof(IMyClassWithDependency), null, It.IsAny<ResolverOverride[]>()), Times.Once);

            actualDependency.Should().BeSameAs(service);
        }

        [TestCase("test")]
        [TestCase("abc")]
        [TestCase("123456")]
        public void ShouldReturnNamedDependency(string name)
        {
            var service = new MyClassWithDependency(_dependency);
            var instance = CreateInstance();

            _containerMock.Setup(x => x.Resolve(typeof(IMyClassWithDependency), name, It.IsAny<ResolverOverride[]>())).Returns(service);

            var actualDependency = instance.Create(_dependency, name);

            _containerMock.Verify(x => x.Resolve(typeof(IMyClassWithDependency), name, It.IsAny<ResolverOverride[]>()), Times.Once);

            actualDependency.Should().BeSameAs(service);
        }

        private Factory<IMyClassWithDependency, IMyInterface> CreateInstance()
        {
            return new Factory<IMyClassWithDependency, IMyInterface>(_containerMock.Object);
        }

        public class MyClass : IMyInterface { }

        public interface IMyInterface { }

        public class MyClassWithDependency : IMyClassWithDependency
        {
            public MyClassWithDependency(IMyInterface dependency)
            {
                Dependency = dependency;
            }

            public IMyInterface Dependency { get; }
        }

        public interface IMyClassWithDependency
        {
        }
    }
}
