namespace Automation.Sdk.Bindings.Steps
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reactive;
    using System.Linq;

    using Automation.Sdk.Bindings.Enums;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.ApplicationServices;

    using FluentAssertions;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;
    
    /// <summary>
    /// Steps for application launching
    /// </summary>
    [Binding]
    public sealed class ApplicationBindings
    {
        private readonly IApplicationInvoker _applicationInvoker;
        private readonly IApplicationInfoContainer _applicationInfoContainer;

        public ApplicationBindings(IApplicationInvoker applicationInvoker,
            IApplicationInfoContainer applicationInfoContainer)
        {
            _applicationInfoContainer = applicationInfoContainer;
            _applicationInvoker = applicationInvoker;
        }

        [Given(@"user starts ""(.*)"" application")]
        [When(@"user starts ""(.*)"" application")]
        public void OpenModule(FormattedString application)
        {
            _applicationInvoker.StartApplication(application, string.Empty);
        }

        [Then(@"application ""(.*)"" should (be|become) closed")]
        public void CloseModule(FormattedString application, AssertPredicate assertPredicate)
        {
            var appInfo = _applicationInfoContainer.Get(application);

            Unit.Default.ShouldBe(
                _ => Process.GetProcessesByName(appInfo.ExeName).Length,
                assertPredicate,
                0,
                $@"Application ""{application}"" should be closed.");
        }

        [Given(@"user starts ""(.*)"" application with arguments:")]
        [When(@"user starts ""(.*)"" application with arguments:")]
        public void OpenModule(FormattedString application, [NotNull] IEnumerable<FormattedString> arguments)
        {
            var argumentsList = arguments.ToList();

            argumentsList.Count.Should().Be(1);

            _applicationInvoker.StartApplication(application, argumentsList.Single());
        }
    }
}