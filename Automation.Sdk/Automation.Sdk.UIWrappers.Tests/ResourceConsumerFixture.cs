namespace Automation.Sdk.UIWrappers.Tests
{
    using System;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using Automation.Sdk.UIWrappers.Services.ResourcesServices;
    using System.Reactive.Disposables;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ResourceConsumerFixture
    {
        private ResourceConsumer Initialize()
        {
            var logger = new Logger();
            return new ResourceConsumer(logger);
        }

        [Test]
        public void ShouldBeCalledInOrderOfAppearanceFor0Priority()
        {
            var resourceConsumer = Initialize();
            string result = string.Empty;

            resourceConsumer.Consume(Disposable.Create(() => result += "a"));
            resourceConsumer.Consume(Disposable.Create(() => result += "b"));
            resourceConsumer.Consume(Disposable.Create(() => result += "c"));
            resourceConsumer.Consume(Disposable.Create(() => result += "d"));
            resourceConsumer.Consume(Disposable.Create(() => result += "e"));

            resourceConsumer.Dispose();

            result.Should().Be("abcde");
        }

        [Test]
        public void ShouldThrowInCaseIfAlreadyDisposed()
        {
            var resourceConsumer = Initialize();
            string result = string.Empty;

            resourceConsumer.Consume(Disposable.Create(() => result += "a"));
            resourceConsumer.Consume(Disposable.Create(() => result += "b"));
            resourceConsumer.Consume(Disposable.Create(() => result += "c"));

            resourceConsumer.Dispose();
            result.Should().Be("abc");

            TestDelegate action = () => resourceConsumer.Consume(Disposable.Create(() => result += "d"));
            Assert.Throws<InvalidOperationException>(action);

            result.Should().Be("abcd");
        }

        [Test]
        public void ShouldBeCalledInOrderOfPriority()
        {
            var resourceConsumer = Initialize();
            string result = string.Empty;

            resourceConsumer.Consume(Disposable.Create(() => result += "a"), 5);
            resourceConsumer.Consume(Disposable.Create(() => result += "b"), 4);
            resourceConsumer.Consume(Disposable.Create(() => result += "c"), 3);
            resourceConsumer.Consume(Disposable.Create(() => result += "d"), 2);
            resourceConsumer.Consume(Disposable.Create(() => result += "e"), 1);

            resourceConsumer.Dispose();

            result.Should().Be("edcba");
        }

        [Test]
        public void ShouldBeCalledInMixedOrder()
        {
            var resourceConsumer = Initialize();
            string result = string.Empty;

            resourceConsumer.Consume(Disposable.Create(() => result += "o"), 5);
            resourceConsumer.Consume(Disposable.Create(() => result += "a"), 5);
            resourceConsumer.Consume(Disposable.Create(() => result += "b"), 0);
            resourceConsumer.Consume(Disposable.Create(() => result += "c"), 0);
            resourceConsumer.Consume(Disposable.Create(() => result += "z"));
            resourceConsumer.Consume(Disposable.Create(() => result += "x"));
            resourceConsumer.Consume(Disposable.Create(() => result += "d"), 0);
            resourceConsumer.Consume(Disposable.Create(() => result += "e"), 1);
            resourceConsumer.Consume(Disposable.Create(() => result += "f"), 1);

            resourceConsumer.Dispose();

            result.Should().Be("bczxdefoa");
        }

        [Test]
        public void ShouldNotFailInCaseOfException()
        {
            var resourceConsumer = Initialize();
            string result = string.Empty;

            resourceConsumer.Consume(Disposable.Create(() => result += "o"), 5);
            resourceConsumer.Consume(Disposable.Create(() => result += "a"), 5);
            resourceConsumer.Consume(Disposable.Create(() => { throw new Exception("bad disposable 1"); }));
            resourceConsumer.Consume(Disposable.Create(() => result += "b"), 0);
            resourceConsumer.Consume(Disposable.Create(() => result += "c"), 0);
            resourceConsumer.Consume(Disposable.Create(() => result += "z"));
            resourceConsumer.Consume(Disposable.Create(() => { throw new Exception("bad disposable 2"); }));
            resourceConsumer.Consume(Disposable.Create(() => result += "x"));
            resourceConsumer.Consume(Disposable.Create(() => result += "d"), 0);
            resourceConsumer.Consume(Disposable.Create(() => result += "e"), 1);
            resourceConsumer.Consume(Disposable.Create(() => { throw new Exception("bad disposable 3"); }));
            resourceConsumer.Consume(Disposable.Create(() => result += "f"), 1);

            resourceConsumer.Dispose();
            
            result.Should().Be("bczxdefoa");
        }
    }
}
