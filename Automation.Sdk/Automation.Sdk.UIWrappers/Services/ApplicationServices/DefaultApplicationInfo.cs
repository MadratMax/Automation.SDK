namespace Automation.Sdk.UIWrappers.Services.ApplicationServices
{
    public class DefaultApplicationInfo : IApplicationInfo
    {
        private readonly string _applicationName;
        private readonly string _exeName;
        private readonly string _exePath;
        private readonly string _mdfPath;
        private readonly string _tracePath;
        private readonly string _defaultServiceName;
        private readonly string _additionalParameters;

        public DefaultApplicationInfo(
            string applicationName,
            string exeName,
            string exePath,
            string mdfPath,
            string tracePath,
            string defaultServiceName,
            string additionalParameters)
        {
            _applicationName = applicationName;
            _exeName = exeName;
            _exePath = exePath;
            _mdfPath = mdfPath;
            _tracePath = tracePath;
            _defaultServiceName = defaultServiceName;
            _additionalParameters = additionalParameters;
        }

        public string ApplicationName => _applicationName;

        public string ExePath => _exePath;

        public string Mdf => _mdfPath;

        public string TraceDir => _tracePath;

        public string ExeName => _exeName;

        public string MdfPath => _mdfPath;

        public string TracePath => _tracePath;

        public string DefaultServiceName => _defaultServiceName;

        public string AdditionalParameters => _additionalParameters;
    }
}