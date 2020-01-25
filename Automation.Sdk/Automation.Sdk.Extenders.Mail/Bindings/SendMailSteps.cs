namespace Automation.Sdk.Extenders.Mail.Bindings
{
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using TechTalk.SpecFlow;
    
    [Binding]
    public sealed class SendMailSteps
    {
        private readonly ILogger _logger;
        private readonly string _mailUtilExe = "SendEmail.exe";

        public SendMailSteps(ILogger logger)
        {
            _logger = logger;
        }

        [When(@"email is sent using server ""(.*)"" and ""(.*)"" working directory")]
        public void WhenISendEmailUsingServer(FormattedString server, FormattedString workingDirectory, Table table)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string sendMailUtil = Directory.GetFiles(currentDirectory,
                                                    _mailUtilExe,
                                                    SearchOption.AllDirectories).FirstOrDefault();

            if (sendMailUtil == null)
            {
                throw new Exception($"{_mailUtilExe} not found at {currentDirectory}");
            }

            if (!table.ContainsColumn("From")
                || !table.ContainsColumn("To")
                || !table.ContainsColumn("Subject")
                || !table.ContainsColumn("Message"))
            {
                ScenarioContext.Current.Pending();
            }

            var firstRow = table.Rows[0];

            FormattedString from = firstRow["From"];
            FormattedString to = firstRow["To"];
            FormattedString subject = firstRow["Subject"];
            FormattedString message = firstRow["Message"];
            var mailArgLine = "-s \"{0}\" -f \"{1}\" -t \"{2}\" -u \"{3}\" -m \"{4}\"";
            var attachments = new List<string>();

            if (table.ContainsColumn("Attachments"))
            {
                foreach (var row in table.Rows)
                {
                    attachments.Add(row["Attachments"]);
                }

                mailArgLine += " -a {5}";
                mailArgLine = string.Format(mailArgLine, server, from, to, subject, message, string.Join(" ", attachments));
            }
            else
            {
                mailArgLine = string.Format(mailArgLine, server, from, to, subject, message);
            }

            _logger.Write("Sending Email using: " + sendMailUtil);
            _logger.Write("Arguments: " + mailArgLine);
            ExecProcess(sendMailUtil, mailArgLine, workingDirectory);
        }

        private static void ExecProcess(string executable, string arguments, string workingDirectory = "")
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = executable,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            if (workingDirectory != string.Empty)
            {
                startInfo.WorkingDirectory = workingDirectory;
            }

            // Start the process with the info specified
            process.StartInfo = startInfo;
            process.Start();

            while (!process.StandardOutput.EndOfStream)
            {
                Console.Write(process.StandardOutput.ReadLine());
            }

            // Wait for the process to exit
            Assert.IsTrue(process.WaitForExit(60000), "Failed to execute: " + executable);

            // Close the process
            process.Close();
        }
    }
}