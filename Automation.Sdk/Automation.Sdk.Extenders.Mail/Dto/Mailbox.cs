namespace Automation.Sdk.Extenders.Mail.Dto
{
    public class Mailbox
    {
        public Mailbox(string address, string password)
        {
            Address = address;
            Password = password;
        }

        public string Address { get; }

        public string Password { get; }
    }
}