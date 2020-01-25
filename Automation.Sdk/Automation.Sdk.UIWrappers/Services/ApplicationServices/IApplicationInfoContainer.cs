namespace Automation.Sdk.UIWrappers.Services.ApplicationServices
{
    using System.Collections.Generic;

    public interface IApplicationInfoContainer
    {
        IApplicationInfo Get(string applicationName);

        void Replace(string key, IApplicationInfo appInfo);

        void Add(string key, IApplicationInfo appInfo);

        Dictionary<string, IApplicationInfo> Applications { get; }
    }
}