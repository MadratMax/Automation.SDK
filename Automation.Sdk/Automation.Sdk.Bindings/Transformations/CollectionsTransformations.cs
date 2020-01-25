namespace Automation.Sdk.Bindings.Transformations
{
    using System.Collections.Generic;
    using System.Linq;
    using Automation.Sdk.Bindings.Dto;
    using Automation.Sdk.UIWrappers.Services;
    using FluentAssertions;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    [Binding]
    [UsedImplicitly]
    public sealed class CollectionsTransformations
    {
        [StepArgumentTransformation]
        [UsedImplicitly]
        public IEnumerable<string> Convert([NotNull] Table table)
        {
            table.Header.Count
                .Should()
                .Be(1, $"table should have single column, but contains {table.Header.Count} columns: {string.Join(",", table.Header)}");

            return table.Rows.Select(tableRow => new FormattedString(tableRow[0]).ToString());
        }

        [StepArgumentTransformation]
        [UsedImplicitly]
        public List<ControlValue> TransformControlValue([NotNull] Table table)
        {
            table.Header.Should().BeEquivalentTo("control", "value");

            return table.Rows.Select(tableRow => new ControlValue(tableRow["control"], tableRow["value"])).ToList();
        }
    }
}
