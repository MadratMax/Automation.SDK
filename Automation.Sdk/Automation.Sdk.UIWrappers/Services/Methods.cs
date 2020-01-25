
namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Text.RegularExpressions;
    using System.Threading;
    using FluentAssertions;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    public static class Methods
    {
        /// <summary>
        /// Delegate for bool returning action
        /// </summary>
        /// <returns>true of false</returns>
        [Obsolete("DelayedAction is deprecated, please use WaitUntil with System.Func<bool> instead.")]
        public delegate bool Action();
        
        /// <summary>
        /// Main Conditional wait
        /// </summary>
        /// <param name="action">action to perform</param>
        /// <param name="maxWait">max wait time in ms</param>
        /// <param name="interval">in ms</param>
        /// <returns>consumed time</returns>
        [Obsolete("DelayedAction is deprecated, please use WaitUntil with Func<bool> instead.")]
        public static long DelayedAction(Action action, int maxWait = 5000, int interval = 100)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (!action() && watch.ElapsedMilliseconds < maxWait)
            {
                Thread.Sleep(interval);
            }

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Main Conditional wait
        /// </summary>
        /// <param name="action">action to perform</param>
        /// <param name="maxWait">max wait time in ms</param>
        /// <param name="interval">query period in ms</param>
        /// <returns>consumed time</returns>
        public static bool WaitUntil(Func<bool> action, int maxWait = 5000, int interval = 100)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            bool result;

            while (!(result = action()) && watch.ElapsedMilliseconds < maxWait)
            {
                Thread.Sleep(interval);
            }

            watch.Stop();

            return result;
        }

        public static TValue WaitValue<TValue>(Func<TValue> predicate, string failMessage, int maxWait = 5000, int interval = 100)
            where TValue : class
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            TValue result;

            while ((result = predicate()) == null && watch.ElapsedMilliseconds < maxWait)
            {
                Thread.Sleep(interval);
            }

            watch.Stop();

            Assert.IsNotNull(result, failMessage);

            return result;
        }

        /// <summary>
        /// Main Conditional wait with assertion.
        /// </summary>
        /// <param name="action">action to perform</param>
        /// <param name="failMessage">fail message is assertion will fail</param>
        /// <param name="maxWait">max wait time in ms</param>
        /// <param name="interval">in ms</param>
        public static void WaitUntil(Func<bool> action, string failMessage, int maxWait = 5000, int interval = 100)
        {
            bool result = WaitUntil(action, maxWait, interval);
            Assert.IsTrue(result, failMessage);
        }

        /// <summary>
        /// Main Conditional wait with assertion with delayed message build procedure.
        /// </summary>
        /// <param name="action">action to perform</param>
        /// <param name="failMessageFuncion">function to generate fail message</param>
        /// <param name="maxWait">max wait time in ms</param>
        /// <param name="interval">in ms</param>
        internal static void WaitUntil(Func<bool> action, Func<string> failMessageFuncion, int maxWait = 5000, int interval = 100)
        {
            var result = WaitUntil(action, maxWait, interval);
            result.Should().BeTrue(failMessageFuncion());
        }

        /// <summary>
        /// Abort the test with Ignore tag.
        /// </summary>
        /// <param name="message">Message to show</param>
        public static void StopTest_NotImplementedYet(string message = "Not implemented yet")
        {
            if (IsSpecFlowEnvironment())
            {
                ScenarioContext.Current.Pending();
            }
            else
            {
                Assert.Inconclusive(message);
            }
        }

        /// <summary>
        /// Wait for Process Idle state
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms687022(v=vs.85).aspx
        /// WaitForInputIdle waits only once for a process to become idle; 
        /// subsequent WaitForInputIdle calls return immediately, whether the process is idle or busy.
        /// </summary>
        /// <param name="process">Process to monitor</param>
        /// <param name="waitTime">time</param>
        /// <param name="assertIdleState">assert in the end</param>
        /// <param name="message">message to show</param>
        public static void WaitForProcessIdle(Process process, int waitTime, bool assertIdleState, string message = "")
        {
            bool waitProgress = true;
            if (!process.HasExited)
            {
                waitProgress = process.WaitForInputIdle(waitTime);
            }

            if (assertIdleState)
            {
                Assert.IsTrue(waitProgress, message);
            }
        }

        /// <summary>
        /// Replaces all placeholders in the string.
        /// Currently supported:
        /// <TestID> - Replaces with TestID
        /// <br> - replaces with NewLine for the SpecFlow tables.
        /// <AppDataLocal>
        /// <CustomOCRNames> - 
        /// <TODAY> - DateTime.Now.ToString("MM/dd/yyyy")
        /// <login>
        /// <domain>
        /// </summary>
        /// <param name="stringToReplace"></param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Issue in StyleCop")]
        public static void ReplacePlaceholders(ref string stringToReplace)
        {
            stringToReplace = new FormattedString(stringToReplace).ToString();
        }

        /// <summary>
        /// Replace %ENVIRONMENT_VARIABLES% in the string
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="target">EnvironmentVariableTarget</param>
        /// <returns>parsed string</returns>
        public static string ReplaceEnvironmentVariables(string input, EnvironmentVariableTarget target = EnvironmentVariableTarget.Machine)
        {
            MatchEvaluator replaceEvaluator = (Match text) =>
            {
                var varaible = text.Value.Substring(1, text.Value.Length - 2);
                var variableValue = Environment.GetEnvironmentVariable(varaible, target);

                if (variableValue == null)
                {
                    throw new InvalidOperationException($"variable {text.Value} is not defined");
                }

                return variableValue;
            };

            var output = Regex.Replace(input, "(%.*?%)", replaceEvaluator);
            return output;
        }

        /// <summary>
        /// Get the domain 
        /// </summary>
        /// <returns>domain</returns>
        public static string GetDomain()
        {
            string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;

            if (domainName == string.Empty)
            {
                return Environment.UserDomainName;
            }

            return domainName;
        }

        /// <summary>
        /// IsSpecFlowEnvironment
        /// </summary>
        /// <returns>true of false</returns>
        public static bool IsSpecFlowEnvironment()
        {
            if (ContextStorage.IsScenarioContextExists())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified value is numeric.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True if the object is numeric</returns>
        public static bool IsNumeric(object value)
        {
            if (value == null || value is DateTime)
            {
                return false;
            }

            if (value is short || value is int || value is long || value is decimal || value is float
                || value is double || value is bool)
            {
                return true;
            }

            try
            {
                if (value is string)
                {
                    double.Parse((string)value);
                }
                else
                {
                    double.Parse(value.ToString());
                }

                return true;
            }
            catch
            {
                //// just dismiss errors but return false
            }

            return false;
        }

        public static void CheckSpecFlowTable(Table table, IEnumerable<string> mandatoryFields, IEnumerable<string> senseFields, string sentence)
        {
            string specFlowSentence = $"{ScenarioContext.Current.CurrentScenarioBlock} {sentence}";
            if (mandatoryFields != null && mandatoryFields.Count() > 0)
            {
                foreach (var field in mandatoryFields)
                {
                    table.Header.Should().Contain(field, $"{field} is mandatory column for sentence: {specFlowSentence}");
                }
            }

            if (senseFields != null && senseFields.Count() > 0)
            {
                table.Header.Should().Contain(column => senseFields.Contains(column), 
                                              $"sentence '{specFlowSentence}' table should have at least one of '{string.Join(",", senseFields)}' fields to have sense");
            }
        }

        public static void ThrowNotImplementedException(bool inconclusive, params object[] values)
        {
            StackTrace st = new StackTrace(1);
            var frame = st.GetFrame(0);
            var method = frame.GetMethod().Module.ScopeName + "." + frame.GetMethod().Name;

            string message = $"Method {method} is not implemented with following params: {string.Join(", ", values)}";

            if (inconclusive)
            {
                StopTest_NotImplementedYet(message);
            }
            else
            {
                throw new NotImplementedException(message);
            }
        }
    }
}
