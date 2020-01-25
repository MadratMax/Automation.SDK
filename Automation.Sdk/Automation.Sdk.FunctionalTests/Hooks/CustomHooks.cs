namespace Automation.Sdk.FunctionalTests
{
    using System;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class CustomHooks
    {
        [Scope(Tag = "PoisonTest")]
        [AfterScenario]
        public void PoisonTest()
        {
            throw new Exception("poison exception");
        }
    }
}