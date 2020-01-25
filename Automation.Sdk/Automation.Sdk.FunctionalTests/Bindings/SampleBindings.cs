namespace Automation.Sdk.FunctionalTests
{
    using System;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.ApplicationServices;
    using FluentAssertions;
    using TechTalk.SpecFlow;

    [Binding]
    public class SampleBindings
    {
        private readonly ProcessService _processService;

        public SampleBindings(ProcessService processService)
        {
            _processService = processService;
        }

        [Given("sample given step")]
        public void DoSampleGiven()
        {
            Console.WriteLine("sample given");
        }

        [Given(@"application ""(.*)"" is started")]
        public void StartApplication(string application)
        {
            _processService.Start(application);
        }

        [Given(@"string ""(.*)"" should be converted to formatted string")]
        public void GivenStringShouldBeConvertedToFormattedString(FormattedString input)
        {
            input.Should().NotBeNull();
        }

        [Given(@"formatted string ""(.*)"" value should be ""(.*)""")]
        public void GivenFormattedStringValueShouldBe(FormattedString input, string expected)
        {
            input.ToString().Should().Be(expected);
        }

        [Then(@"some step")]
        [When(@"some step")]
        public void WhenSomeStep()
        {
            true.ShouldBecome(x => x, true);
        }

        [Then(@"some step with table")]
        [When(@"some step with table")]
        public void WhenSomeStepWithTable(Table table)
        {
            true.ShouldBecome(x => x, true);
        }
    }
}