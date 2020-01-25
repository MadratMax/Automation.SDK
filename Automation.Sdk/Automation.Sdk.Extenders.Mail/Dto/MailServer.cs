namespace Automation.Sdk.Extenders.Mail.Dto
{
    public class MailServer
    {
        public MailServer(string address, int port)
        {
            Address = address;
            Port = port;
        }

        public MailServer(string address)
            : this(address, 110)
        {
        }

        public string Address { get; }

        public int Port { get; }
    }
}