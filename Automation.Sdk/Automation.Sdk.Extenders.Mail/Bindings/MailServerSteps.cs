namespace Automation.Sdk.Extenders.Mail.Bindings
{
    using System.Collections.Generic;

    using Automation.Sdk.Extenders.Mail.Dto;
    using Automation.Sdk.Bindings.Enums;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.Logging;

    using OpenPop.Pop3;
    using TechTalk.SpecFlow;
    using System.Reactive;

    [Binding]
    public sealed class MailServerSteps
    {
        private readonly ILogger _logger;

        public MailServerSteps(ILogger logger)
        {
            _logger = logger;
        }

        [Given(@"all mails on server ""(.*)"" are deleted for the following mailboxes")]
        public void DeleteAllMails(MailServer server, List<Mailbox> mailboxes)
        {
            using (Pop3Client client = new Pop3Client())
            {
                foreach (var mailbox in mailboxes)
                { 
                    client.Connect(server.Address, server.Port, false);

                    client.Authenticate(mailbox.Address, mailbox.Password);

                    client.DeleteAllMessages();

                    client.Disconnect();

                    _logger.Write($"Deleted all mails from {mailbox.Address}");
                }
            }
        }

        [Then(@"mail count for the following mailboxes on server ""(.*)"" should (be|become)")]
        public void ShouldHaveMails(MailServer server, AssertPredicate assertPredicate, List<MailboxMailCount> mailboxes)
        {
            foreach (var mailbox in mailboxes)
            {
                Unit.Default.ShouldBe(_ => CheckMailCount(server, mailbox),
                                assertPredicate,
                                true,
                                $"Mailbox {mailbox.Address} should have {mailbox.MailCount} mails");

            }
        }

        private static bool CheckMailCount(MailServer server, MailboxMailCount mailbox)
        {
            using (Pop3Client client = new Pop3Client())
            {
                client.Connect(server.Address, server.Port, false);
                client.Authenticate(mailbox.Address, mailbox.Password);
                return client.GetMessageCount().Equals(mailbox.MailCount);
            }
        }
    }
}