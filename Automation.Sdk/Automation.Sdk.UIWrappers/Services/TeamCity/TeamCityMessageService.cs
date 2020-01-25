namespace Automation.Sdk.UIWrappers.Services.TeamCity
{
    using System;
    using System.Globalization;
    using Automation.Sdk.UIWrappers.Aspects;

    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    // Can be inherited in test suites
    [LoggerHidden]
    public sealed class TeamCityMessageService
    {
        private const string TeamCityMessagePrefix = "##teamcity";
        private const string BuildStatusCommandName = "buildStatus";

        /// <summary>
        /// Writes the messsage to console.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="keyValue">The key value.</param>
        public void SendStatisticMessage(string keyName, string keyValue)
        {
            Console.WriteLine(BuildStatisticMessage(keyName, keyValue));
        }

        /// <summary>
        /// Appends the text to build status.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void AppendTextToBuildStatus(string format, params object[] args)
        {
            AppendTextToBuildStatus(string.Format(CultureInfo.InvariantCulture, format, args));
        }


        /// <summary>
        /// Appends the text to build status.
        /// </summary>
        /// <param name="text">The text.</param>
        public void AppendTextToBuildStatus(string text)
        {
            SendTeamCityMessageToConsole(BuildStatusCommandName, "text='{{build.status.text}} {0}'", FixValue(text));
        }

        /// <summary>
        /// Sets the build status.
        /// </summary>
        /// <param name="buildStatus">The build status.</param>
        /// <param name="message">The message.</param>
        public void SetBuildStatus(BuildStatus buildStatus, string message)
        {
            SendTeamCityMessageToConsole(BuildStatusCommandName, "status='{0}' text='{{build.status.text}} {1}'",
                GetBuildStatus(buildStatus), FixValue(message));
        }

        /// <summary>
        /// Reports the progress message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ReportProgressMessage(string message)
        {
            SendTeamCityMessageToConsole("progressMessage", "'{0}'", FixValue(message));
        }

        /// <summary>
        /// Reports the progress message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void ReportProgressMessage(string format, params object[] args)
        {
            string message = string.Format(CultureInfo.InvariantCulture, format, args);
            ReportProgressMessage(message);
        }

        /// <summary>
        /// Builds the statistic message.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="keyValue">The key value.</param>
        /// <returns>The message string.</returns>
        public string BuildStatisticMessage(string keyName, string keyValue)
        {
            return BuildTeamcityMessage("buildStatisticValue", "key='{0}' value='{1}'", keyName, keyValue);
        }

        /// <summary>
        /// Opens a message block.
        /// </summary>
        /// <param name="message">The message.</param>
        public void OpenMessageBlock(string message)
        {
            SendTeamCityMessageToConsole("blockOpened", "name='{0}'", FixValue(message));
        }

        /// <summary>
        /// Reports the progress message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorDetails">The error details.</param>
        /// <param name="buildStatus">The build status.</param>
        public void ReportBuildProgressMessage(string message, string errorDetails, BuildStatus buildStatus)
        {
            SendTeamCityMessageToConsole("message", "text='{0}' errorDetails='{1}' status='{2}'", FixValue(message),
                FixValue(errorDetails), GetBuildStatus(buildStatus));
        }

        /// <summary>
        /// Closes a message block.
        /// </summary>
        /// <param name="message">The message.</param>
        public void CloseMessageBlock(string message)
        {
            SendTeamCityMessageToConsole("blockClosed", "name='{0}'", FixValue(message));
        }

        private static void SendTeamCityMessageToConsole(string messageName, string format, params object[] args)
        {
            Console.WriteLine(BuildTeamcityMessage(messageName, format, args));
        }

        private static string BuildTeamcityMessage(string messageName, string format, params object[] args)
        {
            return BuildTeamcityMessage(messageName, string.Format(CultureInfo.InvariantCulture, format, args));
        }

        private static string BuildTeamcityMessage(string messageName, string value)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}[{1} {2}]", TeamCityMessagePrefix, messageName, value);
        }

        private static string FixValue(string value)
        {
            value = value.Replace("|", "||");
            value = value.Replace("'", "|'");
            value = value.Replace("[", "|[");
            value = value.Replace("]", "|]");

            return value;
        }

        private static string GetBuildStatus(BuildStatus buildStatus)
        {
            return buildStatus.ToString().ToUpperInvariant();
        }
    }
}