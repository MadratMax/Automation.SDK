namespace Automation.Sdk.UIWrappers.Services.ExecutionContext
{
    using JetBrains.Annotations;

    public interface IExecutionContextMemento
    {
        void RecordTitle([NotNull] string title);

        void RecordStep([NotNull] string step);

        void RecordTag([NotNull] string tag);
    }
}