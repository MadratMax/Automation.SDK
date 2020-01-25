namespace Automation.Sdk.GenerationPlugin.SpecFlowPlugin
{
    using TechTalk.SpecFlow.Generator.Plugins;
    using TechTalk.SpecFlow.Generator.UnitTestConverter;

    /// <summary>
    /// Generator plugin to support Retry functionality for failed SpecFlow test cases
    /// </summary>
    public class GeneratorPlugin : IGeneratorPlugin
    {
        public void Initialize(GeneratorPluginEvents generatorPluginEvents, GeneratorPluginParameters generatorPluginParameters)
        {
            generatorPluginEvents.RegisterDependencies += (sender, args) =>
                {
                    args.ObjectContainer.RegisterTypeAs<RetryUnitTestFeatureGenerator, IFeatureGenerator>();
                    args.ObjectContainer.RegisterTypeAs<RetryUnitTestFeatureGeneratorProvider, IFeatureGeneratorProvider>("retry");
                };
        }
    }
}