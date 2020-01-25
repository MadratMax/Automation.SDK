namespace Automation.Sdk.UIWrappers.Services.ClipboardServices
{
    using System.Windows.Forms;
    using Automation.Sdk.UIWrappers.Services.Threading;
    using JetBrains.Annotations;

    public class ClipboardCopyService
    {
        private readonly IStaScheduler _staScheduler;

        public ClipboardCopyService([NotNull] IStaScheduler staScheduler)
        {
            _staScheduler = staScheduler;
        }

        public virtual void CopyToClipboard(string text)
        {
            _staScheduler.Schedule(() => Clipboard.SetText(text));
        }

        public virtual string GetClipboardText()
        {
            string result = string.Empty;

            _staScheduler.Schedule(() => result = Clipboard.GetText());

            return result;
        }
    }
}
