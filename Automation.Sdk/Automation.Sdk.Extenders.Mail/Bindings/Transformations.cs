namespace Automation.Sdk.Extenders.Mail.Bindings
{
    using System.Collections.Generic;
    using System.Linq;
    using Automation.Sdk.Extenders.Mail.Dto;
    using Automation.Sdk.UIWrappers.Services;
    using FluentAssertions;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    [Binding]
    [UsedImplicitly]
    public sealed class Transformations
    {
        [StepArgumentTransformation]
        [UsedImplicitly]
        public MailServer Convert(string server)
        {
            var parsed = new FormattedString(server);
            var chunks = parsed.ToString().Split(':');

            if (chunks.Length > 1)
            {
                return new MailServer(chunks[0], int.Parse(chunks[1]));
            }

            return new MailServer(parsed);
        }

        [StepArgumentTransformation]
        [UsedImplicitly]
        public List<Mailbox> TransformMailbox([NotNull] Table table)
        {
            table.Header.Should().BeEquivalentTo("address", "password");

            return table.Rows
                .Select(tableRow => new Mailbox(
                    new FormattedString(tableRow["address"]),
                    new FormattedString(tableRow["password"])))
                .ToList();
        }

        [StepArgumentTransformation]
        [UsedImplicitly]
        public List<MailboxMailCount> TransformMailboxMailCount([NotNull] Table table)
        {
            table.Header.Should().BeEquivalentTo("address", "password", "count");

            return table.Rows
                .Select(tableRow => new MailboxMailCount(
                    new FormattedString(tableRow["address"]),
                    new FormattedString(tableRow["password"]),
                    int.Parse(new FormattedString(tableRow["count"]))))
                .ToList();
        }
    }
}
