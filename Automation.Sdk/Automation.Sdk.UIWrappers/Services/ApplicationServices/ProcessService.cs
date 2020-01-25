namespace Automation.Sdk.UIWrappers.Services.ApplicationServices
{
    using System;
    using System.Diagnostics;
    using System.Reactive.Subjects;
    using JetBrains.Annotations;
    using Logging;

    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    // Can be inherited in test suites
    [UsedImplicitly]
    public class ProcessService
    {
        private readonly ILogger _logger;
        private readonly ReplaySubject<int> _startedProcesses;

        public ProcessService(ILogger logger)
        {
            _logger = logger;
            _startedProcesses = new ReplaySubject<int>();
        }

        public IObservable<int> Processes => _startedProcesses;

        public virtual Process Start(string name)
        {
            return Start(name, string.Empty);
        }

        public virtual void Register(int processId)
        {
            _startedProcesses.OnNext(processId);
        }

        public virtual Process Start(string path, string arguments, bool useShellExecutable = true)
        {
            var actualPath = Methods.ReplaceEnvironmentVariables(path);

            _logger.Write($@"Starting exe at ""{actualPath}"" with params ""{arguments}""");

            var process = new Process();
            var info = new ProcessStartInfo(actualPath, arguments) {UseShellExecute = useShellExecutable};
            process.StartInfo = info;            

            if (process.Start())
            {
                _startedProcesses.OnNext(process.Id);
            }

            return process;
        }
    }
}