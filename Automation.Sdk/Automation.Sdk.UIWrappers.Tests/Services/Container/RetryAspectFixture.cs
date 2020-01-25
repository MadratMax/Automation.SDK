namespace Automation.Sdk.UIWrappers.Tests.Services.Container
{
    using System;
    using Automation.Sdk.UIWrappers.Aspects;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;
    using NUnit.Framework;

    [TestFixture]
    public sealed class RetryAspectFixture
    {
        [Test]
        public void ShouldRetryMethodCall()
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<IExceptionBroker, ExceptionBroker>();
            container.Configure<Interception>().SetDefaultInterceptorFor<IExceptionBroker>(new InterfaceInterceptor());
            var exceptionBroker = container.Resolve<IExceptionBroker>();

            TestDelegate action = () => exceptionBroker.ThrowException("sample");

            Assert.DoesNotThrow(action);
        }

        public class ExceptionBroker : IExceptionBroker
        { 
            private int _counter;

            public void ThrowException(string sampleParameter)
            {
                if (_counter == 0)
                {
                    _counter++;
                    throw new ArgumentException();
                }
            }
        }

        public interface IExceptionBroker
        {
            [Retry]
            void ThrowException(string sampleParameter);
        }
    }
}
