namespace Automation.Sdk.UIWrappers.Tests
{
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Services.ControlConversion;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using FluentAssertions;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ControlTypeConverterFixture
    {
        [Test]
        public void ShouldCreateInstance()
        {
            TestDelegate action = () => CreateInstance();

            Assert.DoesNotThrow(action);
        }

        [Test]
        [TestCaseSource(nameof(GetControlTypeNameMap))]
        public void ShouldResolveControlTypeByName(string controlTypeName, ControlType expectedControlType)
        {
            var instance = CreateInstance();

            var type = instance.Resolve(controlTypeName);
            
            type.Should().Be(expectedControlType);
        }

        [Test]
        public void ShouldConvertButtonType()
        {
            var instance = CreateInstance();

            var type = instance.Convert<UIAButton>();

            type.Should().Be(ControlType.Button);
        }

        private static IEnumerable<object[]> GetControlTypeNameMap()
        {
            List<object[]> testCaseValues = new List<object[]>
            {
                new object[] {"combo box", ControlType.ComboBox},
                new object[] {"data grid", ControlType.DataGrid},
                new object[] {"document", ControlType.Document},
                new object[] {"data item", ControlType.DataItem},
                new object[] {"text box", ControlType.Edit},
                new object[] {"group box", ControlType.Group},
                new object[] {"header", ControlType.Header},
                new object[] {"header item", ControlType.HeaderItem},
                new object[] {"link", ControlType.Hyperlink},
                new object[] {"image", ControlType.Image},
                new object[] {"list box", ControlType.List},
                new object[] {"list item", ControlType.ListItem},
                new object[] {"menu bar", ControlType.MenuBar},
                new object[] {"menu", ControlType.Menu},
                new object[] {"menu item", ControlType.MenuItem},
                new object[] {"toolbar", ControlType.ToolBar},
                new object[] {"tooltip", ControlType.ToolTip},
                new object[] {"panel", ControlType.Pane},
                new object[] {"progress bar", ControlType.ProgressBar},
                new object[] {"radio button", ControlType.RadioButton},
                new object[] {"scroll bar", ControlType.ScrollBar},
                new object[] {"slider", ControlType.Slider},
                new object[] {"thumb", ControlType.Thumb},
                new object[] {"spinner", ControlType.Spinner},
                new object[] {"split button", ControlType.SplitButton},
                new object[] {"tab item", ControlType.TabItem},
                new object[] {"table", ControlType.Table},
                new object[] {"label", ControlType.Text},
                new object[] {"tree item", ControlType.TreeItem},
                new object[] {"window", ControlType.Window},
                new object[] {"tab", ControlType.Tab},
                new object[] {"custom", ControlType.Custom}
            };

            return testCaseValues;
        }

        private ControlTypeConverter CreateInstance()
        {
            return new ControlTypeConverter(Mock.Of<ILogger>());
        }
    }
}