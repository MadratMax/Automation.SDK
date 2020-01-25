
namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using TechTalk.SpecFlow;

    /// <summary>
    /// This class represents the context for accessing the TestUtils class
    /// </summary>
    public class TestUtils
    {
        /// <summary>
        /// Returns the currently running TestID
        /// </summary>
        /// <returns></returns>
        public static string ObtainTestID()
        {
            if (!Methods.IsSpecFlowEnvironment())
            {
                return string.Empty;
            }

            string[] tags = GetScenarioTags();
            string testID = string.Empty;

            foreach (var tag in tags)
            {
                if (tag.StartsWith(SpecIds.TestID))
                {
                    testID = tag;
                    break;
                }
            }

            if (testID == string.Empty)
            {
                Regex tid = new Regex(@"\((.*?)\)");
                testID = tid.Match(ScenarioContext.Current.ScenarioInfo.Title).Groups[1].Value;

                int index = testID.IndexOf(SpecIds.TestID);
                if (index > 0)
                {
                    testID = testID.Substring(index);
                }
            }

            ContextStorage.Add(SpecIds.TestID, testID);

            return testID;
        }

        public static string GetScenarioTagComment(string tagToFind)
        {
            if (!Methods.IsSpecFlowEnvironment())
            {
                return string.Empty;
            }

            foreach (string tag in GetScenarioTags())
            {
                if (tag.StartsWith(tagToFind + "("))
                {
                    Regex reg = new Regex(@"\((.*?)\)");

                    string scenarioTagComment = reg.Match(tag).Groups[1].Value;
                    if (scenarioTagComment.StartsWith("DEFECT_") && !scenarioTagComment.Contains("TODO"))
                    {
                        scenarioTagComment = scenarioTagComment.Replace("DEFECT_", "https://cmajira.lss.emc.com:8443/browse/").Replace("_", "-");
                    }

                    return scenarioTagComment;
                }
            }

            return "Not specified";
        }

        public static string[] GetScenarioTags()
        {
            if (!Methods.IsSpecFlowEnvironment())
            {
                return null;
            }

            return ScenarioContext.Current.ScenarioInfo.Tags;
        }

        public static bool IsCIEnvironment() => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"));

        public static bool ScenarioTagExists(string tagToFind)
        {
            if (!Methods.IsSpecFlowEnvironment())
            {
                return false;
            }

            foreach (string tag in GetScenarioTags())
            {
                if (tag.Equals(tagToFind) || tag.StartsWith(tagToFind + "("))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Kill the process
        /// </summary>
        /// <param name="processName">process name to kill</param>
        public static void KillProcess(string processName)
        {
            string processNameLower = processName.ToLower();
            foreach (Process testHostProc in Process.GetProcesses())
            {
                testHostProc.Exited += myProcess_Exited;

                if (processNameLower.Contains(testHostProc.ProcessName.ToLower()))
                {
                    testHostProc.Kill();

                    testHostProc.WaitForExit(10000);

                    if (!testHostProc.HasExited)
                    {
                        throw new Exception($"The process {processName} is not exited."); 
                    }                   
                }
            }
        }

        public static bool IsDebug()
        {
            return Debugger.IsAttached;
        }

        // Handle Exited event and display process information.
        private static void myProcess_Exited(object sender, EventArgs e)
        {
            Trace.WriteLine($"Process{(sender as Process).ProcessName} is killed.");
        }

        /// <summary>
        /// GetTheProcess
        /// </summary>
        /// <param name="processName">process name to find</param>
        public static List<Process> FindProcess(string processName)
        {
            List<Process> processes = new List<Process>();
            try 
            { 
                foreach (Process testHostProc in Process.GetProcesses())
                {
                    if (processName.ToLower().Contains(testHostProc.ProcessName.ToLower()))
                    {
                        processes.Add(testHostProc);
                    }
                }
            }
            catch (Exception ex)
            {
               Trace.Write(ex);
            }

            return processes;
        }
    }
}