namespace Automation.Sdk.Bindings.Steps
{
    using Automation.Sdk.UIWrappers.Services.FileSystem;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    [Binding]
    [UsedImplicitly]
    public sealed class ConfigurationFileBindings
    {
        private readonly ConfigurationFileService _configurationFileService;

        public ConfigurationFileBindings([NotNull] ConfigurationFileService configurationFileService)
        {
            _configurationFileService = configurationFileService;
        }

        [Then(@"configuration file ""(.*)"" should contain key ""(.*)"" with value ""(.*)""")]
        [UsedImplicitly]
        public void ShouldHaveSetting(string fileName, string key, string value)
        {
            _configurationFileService.VerifySetting(fileName, key, value);
        }

        [Given(@"configuration file ""(.*)"" setting ""(.*)"" is set to value ""(.*)""")]
        [UsedImplicitly]
        public void SetSetting(string fileName, string key, string value)
        {
            _configurationFileService.SetSetting(fileName, key, value);
        }
    }
}
