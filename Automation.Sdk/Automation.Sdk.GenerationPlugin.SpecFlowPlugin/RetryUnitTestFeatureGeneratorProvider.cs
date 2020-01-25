namespace Automation.Sdk.GenerationPlugin.SpecFlowPlugin
{
    using TechTalk.SpecFlow.Generator.UnitTestConverter;
    using TechTalk.SpecFlow.Parser;

    public class RetryUnitTestFeatureGeneratorProvider : IFeatureGeneratorProvider
    {
        private readonly RetryUnitTestFeatureGenerator _unitTestFeatureGenerator;

        public RetryUnitTestFeatureGeneratorProvider(RetryUnitTestFeatureGenerator unitTestFeatureGenerator)
        {
            _unitTestFeatureGenerator = unitTestFeatureGenerator;
        }

        public bool CanGenerate(SpecFlowDocument document)
        {
            return true;
        }

        public IFeatureGenerator CreateGenerator(SpecFlowDocument document)
        {
            return _unitTestFeatureGenerator;
        }

        public int Priority => PriorityValues.Normal;
    }
}