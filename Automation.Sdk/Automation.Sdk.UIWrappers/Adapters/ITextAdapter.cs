namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Collections.Generic;
    using JetBrains.Annotations;

    public interface ITextAdapter : IAdapter
    {
        [CanBeNull]
        string GetText();

        [ItemCanBeNull]
        List<string> GetAllText();

        string DisplayText { get; }
    }
}
