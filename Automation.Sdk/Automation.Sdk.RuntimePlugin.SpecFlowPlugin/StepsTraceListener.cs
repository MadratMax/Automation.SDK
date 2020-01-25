namespace Automation.Sdk.RuntimePlugin.SpecFlowPlugin
{
    using System;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Tracing;

    [UsedImplicitly]
    public sealed class StepsTraceListener : ITraceListener
    {
        public void WriteTestOutput([NotNull] string message)
        {
            if (message.StartsWith(@"Given Step"))
            {
                IndentTrace = true;
                SkipNextTrace = true;
                return;
            }
            Console.WriteLine(IndentTrace ? $"    {message}" : message);
        }

        public void WriteToolOutput([NotNull] string message)
        {
            if (SkipNextTrace)
            {
                return;
            }
            string result = $"-> {message}";
            if (IndentTrace)
            {
                result = $"    {result}";
            }
            Console.WriteLine(result);
        }

        private bool IndentTrace
        {
            get
            {
                if (ScenarioContext.Current == null || !ScenarioContext.Current.ContainsKey("indent_trace"))
                {
                    return false;
                }
                return Convert.ToBoolean(ScenarioContext.Current["indent_trace"]);
            }
            set
            {
                ScenarioContext.Current["indent_trace"] = value;
            }
        }

        private bool SkipNextTrace
        {
            get
            {
                if (ScenarioContext.Current == null || !ScenarioContext.Current.ContainsKey("skip_trace"))
                {
                    return false;
                }
                var result = Convert.ToBoolean(ScenarioContext.Current["skip_trace"]);
                ScenarioContext.Current["skip_trace"] = false;
                return result;
            }
            set
            {
                ScenarioContext.Current["skip_trace"] = value;
            }
        }
    }
}