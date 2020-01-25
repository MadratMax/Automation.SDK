namespace Automation.Sdk.UIWrappers.Services.ApplicationServices
{
    public interface IApplicationInfo
    {
        string ApplicationName { get; }

        string ExeName { get; }

        string ExePath { get; }

        string MdfPath { get; }

        string TracePath { get; }

        string DefaultServiceName { get; }

        string AdditionalParameters { get; }
    }
}