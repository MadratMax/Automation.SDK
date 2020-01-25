namespace Automation.Sdk.UIWrappers.Services.ExecutionContext
{
    using System;
    using System.Reactive.Subjects;
    using JetBrains.Annotations;

    [UsedImplicitly]
    internal sealed class ExecutionContextStorage : IExecutionContextProvider, IExecutionContextMemento
    {
        private readonly BehaviorSubject<ExecutionContextDto> _context;

        public ExecutionContextStorage()
        {
            _context = new BehaviorSubject<ExecutionContextDto>(new ExecutionContextDto { Id =  Guid.NewGuid(), CreateDate = DateTime.Now });
        }

        public IObservable<ExecutionContextDto> Context => _context;

        public void RecordTitle(string title)
        {
            var state = _context.Value;
            state.Title = title;
            _context.OnNext(state);
        }

        public void RecordStep(string step)
        {
            var state = _context.Value;
            state.Steps.Add(step);
            _context.OnNext(state);
        }

        public void RecordTag(string tag)
        {
            var state = _context.Value;
            state.Tags.Add(tag);
            _context.OnNext(state);
        }
    }
}