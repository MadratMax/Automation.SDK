namespace Automation.Sdk.FunctionalTests
{
    using System;
    using System.Collections.Generic;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class MockLogger : ILogger
    {
        private readonly List<string> _messages;

        public MockLogger()
        {
            _messages = new List<string>();
        }

        public IList<string> Messages => _messages;

        public void Write(string message)
        {
            _messages.Add(message);
        }

        public void Write(Exception exception)
        {
            _messages.Add(exception.ToString());
        }
    }
}