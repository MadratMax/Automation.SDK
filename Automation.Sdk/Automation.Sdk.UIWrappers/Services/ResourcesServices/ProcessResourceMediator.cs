namespace Automation.Sdk.UIWrappers.Services.ResourcesServices
{
    using System;
    using System.Diagnostics;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using Automation.Sdk.UIWrappers.Services.ApplicationServices;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class ProcessResourceMediator : IDisposable
    {
        private readonly ILogger _logger;

        // This anchor holds subscription itself, if _subscriptionAnchor.Dispose()
        // will be called, than subscription will be closed
        private readonly IDisposable _subscriptionAnchor;

        // This subject raises OnNext when ResourceConsumer will call Dispose for all 
        // disposable anchors holdind by itself. Actually, it will be called during 
        // test cleanup. 
        // There are 3 different types of subjects:
        // Subject<T> - simple stateless object stream 
        private readonly ReplaySubject<int> _killSubject;

        public ProcessResourceMediator(
            [NotNull] ProcessService processService,  
            [NotNull] ILogger logger)
        {
            _logger = logger;

            // Killing subject initialization
            _killSubject = new ReplaySubject<int>();

            // Let RX black magic begin here!
            // 
            // long story short, we have the following data streams enlisted:
            // 1. processService.Processes 
            // this stream fires IDs of processes, which are created via ProcessService
            // output sample: 1, 2, 3
            // 2. _killSubject 
            // this stream returns only single "-1" item and completes after this

            var processIds = processService.Processes
                // here we are removing processService.Processes
                // duplicates from resulting data stream. Obviously, we dont want to try killing 
                // process twice. from timeline out of view, first appearance will appears in the resulting 
                // stream, all further appearances will be ignored
                .Distinct();

            // here we have very tricky thing: object processIds is IObservable<int>, actually
            // it is not a collection of integer process Ids, calculated by Distinct(),
            // it is a pointer to aggregate data stream, which internal logic is defined via
            // Distinct() operation. in another words, we have some kind of lazy
            // IEnumerable on RX manner which will return items only when processService.Processes
            // will start returning some data

            _subscriptionAnchor = _killSubject
                // here Concat(processIds).Skip(1) should be considered together:
                // the purpose here is to get data stream with the exact same values which 
                // processIds local variable got, but we want to get these values right
                // after _killSubject will be completed.
                // to achieve this we are concatenating _killSubject data stream with processIds
                // http://rxmarbles.com/#concat
                // and than, as far as we know, that _killSubject return single synthetic element,
                // we are skipping it via Skip(1)
                .Concat(processIds)
                .Skip(1)
                // and finally, here we have exact IDs in the exact time, so lets call Kill()
                // for each incoming process ID
                .Subscribe(Kill);
        }

        private void KillProcesses()
        {
            // Returning "-1" as a synthetic flag that it is time to release resources
            _killSubject.OnNext(-1);
            _killSubject.OnCompleted();
        }

        private void Kill(int detectedProcess)
        {
            try
            {
                var process = Process.GetProcessById(detectedProcess);
                _logger.Write($"killing process {process}");
                process.Kill();
            }
            catch (Exception exception)
            {
                _logger.Write($"process can not be killed: {exception}");
            }
        }

        public void Dispose()
        {
            // In order to ensure that KillProcesses will be called before 
            // _subscriptionAnchor is destroyed we need to call here KillProcesses()
            KillProcesses();

            // we need to dispose subscription here on container recycle 
            // despite it will be cleaned up anyway by GC. The cause is the following:
            // lets say we have test run with Test1 and Test2
            // in Test1 cleanup Unity container is recycling and recycle all
            // components with container-controlled lifetime manager. In this moment
            // There will be no sync root linked with this instance and next GC flush
            // will collect it. But as far as GC is flushing from time to time, there will 
            // be some delay during which this instance will be kept alive.
            // also, we should take into account that Concat(...).Skip() will be already moved
            // to items from processIds (which means that Kill will be called right after new element
            // appears in processService.Processes or windowService.Processes
            // all that means, that when Test2 will start, in despite of new ProcessResourceMediator
            // instance will be created, newly created processes will be killed immediately right after 
            // start via ProcessResourceMediator instance from Test1
            _subscriptionAnchor.Dispose();
        }
    }
}
