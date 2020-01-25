namespace Automation.Sdk.UIWrappers.Services.ApplicationServices
{
    using System;
    using System.Collections.Generic;
    using Automation.Sdk.UIWrappers.Aspects;

    [AutoRegister]
    public sealed class ApplicationInfoContainer : IApplicationInfoContainer
    {
        private readonly Dictionary<string, IApplicationInfo> _applications;

        public ApplicationInfoContainer()
        {
            _applications = new Dictionary<string, IApplicationInfo>();
        }

        public Dictionary<string, IApplicationInfo> Applications => _applications;

        public IApplicationInfo Get(string applicationName)
        {
            if (_applications.ContainsKey(applicationName))
            {
                return _applications[applicationName];
            }

            throw new ArgumentException($@"Application ""{applicationName}"" is unknown");
        }

        public void Replace(string key, IApplicationInfo appInfo)
        {
            if (_applications.ContainsKey(key))
            {
                _applications.Remove(key);
            }

            Add(key, appInfo);
        }

        public void Add(string key, IApplicationInfo appInfo)
        {
            _applications.Add(key, appInfo);
        }
    }
}