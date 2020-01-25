namespace Automation.Sdk.UIWrappers.Aspects
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    public sealed class RetryCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var result = getNext()(input, getNext);

            if (result.Exception != null)
            {
                result = getNext()(input, getNext);
            }
            return result;
        }

        public int Order { get; set; }
    }
}