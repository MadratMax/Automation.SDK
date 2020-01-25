namespace Automation.Sdk.Extenders.Mail.Dto
{
    public class MailboxMailCount
    {
        public MailboxMailCount(string address, string password, int mailCount)
        {
            Address = address;
            Password = password;
            MailCount = mailCount;
        }

        public string Address { get; }

        public string Password { get; }
        
        public int MailCount { get; }
    }
}