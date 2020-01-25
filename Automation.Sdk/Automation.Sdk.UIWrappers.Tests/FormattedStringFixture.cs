namespace Automation.Sdk.UIWrappers.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NUnit.Framework;
    using UIWrappers.Services;
    using FluentAssertions;

    [TestFixture]
    public class FormattedStringFixture
    {
        [Test]
        public void ShouldUseKeysFromAppConfig()
        {
            var formattedString = new FormattedString("<Key1> Test <Key2>");
            formattedString.ToString().Should().Be("true Test Administrator");
        }

        [Test]
        public void ShouldUseKeysContextStorage()
        {
            ContextStorage.Add("TESTKEY1", "T1");
            ContextStorage.Add("TESTKEY2", "T2");

            var formattedString = new FormattedString("<TESTKEY2> Test <TESTKEY1>");
            formattedString.ToString().Should().Be("T2 Test T1");
        }

        [Test]
        public void ShouldNotThrowExceptions()
        {
            FormattedString formattedString;

            TestDelegate action = () => formattedString = new FormattedString("<Key10> Test <Key20>");
            Assert.DoesNotThrow(action);

            formattedString = new FormattedString("<Key10> Test <Key20>");
            formattedString.ToString().Should().Be("<Key10> Test <Key20>");
        }
    }
}