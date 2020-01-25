namespace Automation.Sdk.UIWrappers.Services.Logging
{
    using System;
    using Automation.Sdk.UIWrappers.Aspects;

    [AutoRegister]
    public class Logger : ILogger
    {
        [LoggerHidden]
        public virtual void Write(string message)
        {
            Console.Out.WriteLine(message);
        }

        [LoggerHidden]
        public virtual void Write(Exception exception)
        {
            Console.Out.WriteLine(exception.ToString());
        }
    }
}
