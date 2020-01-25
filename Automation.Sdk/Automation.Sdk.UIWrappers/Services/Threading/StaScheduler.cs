namespace Automation.Sdk.UIWrappers.Services.Threading
{
    using System;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading;

    using Automation.Sdk.UIWrappers.Aspects;
    using JetBrains.Annotations;

    [UsedImplicitly]
    [AutoRegister]
    internal sealed class StaScheduler : IStaScheduler
    {
        /// <summary>
        /// Scheduler, which is holding clipboard access in STA context
        /// </summary>
        private readonly IScheduler _scheduler;

        public StaScheduler()
        {
            // Creating scheduler with STA context in backing thread
            // EventLoopScheduler "Represents an object that schedules units of work on a designated thread."
            // https://msdn.microsoft.com/en-us/library/system.reactive.concurrency.eventloopscheduler(v=vs.103).aspx
            _scheduler = new EventLoopScheduler(CreateStaThread);
        }

        public void Schedule(Action action)
        {
            // Multithreading model here is the following:

            // Subject<Unit> is created in main thread
            var subject = new ReplaySubject<Unit>();

            // Here we are posting asynch message to STA thread to execute our delegate
            _scheduler.Schedule(
                // This delegate is executes in STA thread
                () =>
                {
                    // Executing target action in STA thread
                    action();
                    // Sending cross-thread broadcast notification that delegate 
                    // has been executed
                    subject.OnNext(Unit.Default);
                });

            // Here main thread will wait until STA thread will send 
            // broadcast notification
            subject.FirstAsync().Wait();
        }

        private Thread CreateStaThread(ThreadStart start)
        {
            var thread = new Thread(start);
            thread.SetApartmentState(ApartmentState.STA);
            return thread;
        }
    }
}
