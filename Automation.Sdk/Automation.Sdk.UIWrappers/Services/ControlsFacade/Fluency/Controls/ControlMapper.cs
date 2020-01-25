namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Disposables;
    using Automation.Sdk.UIWrappers.Aspects;
    using JetBrains.Annotations;

    [AutoRegister]
    public class ControlMapper : IControlMapper
    {
        private readonly ControlNamingMapper _mapper;
        private readonly Stack<ControlConfiguration> _parentStack;

        public ControlMapper([NotNull] ControlNamingMapper mapper)
        {
            _mapper = mapper;
            _parentStack = new Stack<ControlConfiguration>();
        }

        public void Add(ControlConfiguration configuration)
        {
            if (_parentStack.Count > 0)
            { 
                configuration.AddParent(_parentStack.Peek());
            }

            _mapper.Add(configuration);
        }

        public IDisposable ConsumeParent(ControlConfiguration parent)
        {
            _parentStack.Push(parent);

            return Disposable.Create(() =>
            {
                _parentStack.Pop();
            });
        }
    }
}