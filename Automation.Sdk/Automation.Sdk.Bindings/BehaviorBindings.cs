
namespace Automation.Sdk.Bindings
{    
    using Automation.Sdk.UIWrappers.Adapters;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade;
    using FluentAssertions;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    public abstract class BehaviorBindings<TAdapter> : TechTalk.SpecFlow.Steps where TAdapter : IAdapter
    {
        private readonly ControlFacade _controlFacade;
        private readonly ControlAdapterFactory _controlAdapterFactory;

        protected BehaviorBindings([NotNull] ControlFacade controlFacade, [NotNull] ControlAdapterFactory controlAdapterFactory)
        {
            _controlFacade = controlFacade;
            _controlAdapterFactory = controlAdapterFactory;
        }

        protected ControlFacade ControlFacade => _controlFacade;

        [StepArgumentTransformation(@"""(.*)""")]
        [UsedImplicitly, CanBeNull]
        public TAdapter GetAdapter([NotNull] string caption)
        {
            // Currently used as VPath
            caption.Should().NotBeNullOrWhiteSpace("control caption should not be null or empty");

            var element = ControlFacade.Get(caption);

            return _controlAdapterFactory.Create<TAdapter>(element);
        }

        protected void CheckAdapterForNull(TAdapter adapter)
        {
            string message = $@"element ""{adapter}"" should exist";
            adapter.Should().NotBeNull(message);
            adapter.IsContainsElement.Should().BeTrue(message);
        }
    }
}