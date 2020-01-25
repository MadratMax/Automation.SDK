namespace Automation.Sdk.UIWrappers.Tests
{
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop;
    using FluentAssertions;
    using Moq;
    using NUnit.Framework;
    using System.ComponentModel.DataAnnotations;
    using UIWrappers.Services;
    using UIWrappers.Services.ControlsFacade;

    public sealed class FluentConfigurationFixture
    {
        private Mock<IControlMapper> _namingMapperMock;
        private ControlNamingMapper _realControlNamingMapper;
        private ControlMapper _realControlMapper;

        [SetUp]
        public void SetUp()
        {
            _namingMapperMock = new Mock<IControlMapper>();
        }

        [Test]
        public void ShouldCreateInstance()
        {
            TestDelegate action = () => CreateInstance();

            Assert.DoesNotThrow(action);
        }

        [TestCase("id1", "caption1")]
        [TestCase("id2", "caption2")]
        public void ShouldAddConfiguration(string id, string caption)
        {
            var instance = CreateInstance();

            var configuration = instance.Add(id).For.Id(caption);

            _namingMapperMock.Verify(x => x.Add((ControlConfiguration)configuration), Times.Once);
        }

        [Test]
        public void ShouldAddMultipleConfigurations()
        {
            var instance = CreateInstance();

            instance
                .Add("caption1").For.Id("id1")
                .Add("caption2").For.Id("id2");

            _namingMapperMock.Verify(x => x.Add(It.IsAny<ControlConfiguration>()), Times.Exactly(2));
        }

        [TestCase("caption1")]
        [TestCase("caption4")]
        public void ShouldNotAddIncompleteConfig(string caption)
        {
            var instance = CreateInstance();

            instance.Add(caption);

            _namingMapperMock.Verify(x => x.Add(It.IsAny<ControlConfiguration>()), Times.Never);
        }

        [TestCase("id1", "caption1")]
        [TestCase("id2", "caption2")]
        public void ShouldRegisterConfigurationProperties(string id, string caption)
        {
            var instance = CreateInstance();

            instance.Add(caption).For.Id(id);

            var expected = new ControlConfiguration(null, Mock.Of<IControlMapper>(), caption, $"[AutomationID={id}]", AutomationSearchBehavior.ByVPath);

            _namingMapperMock.Verify(x => x.Add(expected), Times.Once);
        }

        [TestCase("id1", "caption1")]
        [TestCase("id2", "caption2")]
        public void ShouldRegisterConfigurationPropertiesUsingBy(string id, string caption)
        {
            var instance = CreateInstance();

            instance.Add(caption, By.Id(id));

            var expected = new ControlConfiguration(null, Mock.Of<IControlMapper>(), caption, $"[AutomationID={id}]", AutomationSearchBehavior.ByVPath);

            _namingMapperMock.Verify(x => x.Add(expected), Times.Once);
        }

        [TestCase("id1", "caption1")]
        [TestCase("id2", "caption2")]
        public void ShouldRegisterChainConfigurationUsingBy(string id, string caption)
        {
            var instance = CreateInstance();

            instance.Add(caption, 
                By.Id(id).Then(
                    By.ClassName(caption).Then(
                        By.ItemStatus(id))
                    ));

            var expected = new ControlConfiguration(
                null, 
                Mock.Of<IControlMapper>(), 
                caption, 
                $"[AutomationID={id}]=>[ClassName={caption}]=>[ItemStatus={id}]",
                AutomationSearchBehavior.ByVPath);

            _namingMapperMock.Verify(x => x.Add(expected), Times.Once);
        }

        [Test]
        public void ShouldRegisterParentRelationship()
        {
            var instance = CreateRealInstance();

            using (instance.Add("1", By.Id("I1")).DefineScope())
            {
                using (instance.Add("1.1", By.Id("I1.1")).DefineScope())
                {
                    using (instance.Add("1.1.1", By.Id("I1.1.1")).DefineScope())
                    {

                    }
                }

                using (instance.Add("1.2", By.Id("I1.2")).DefineScope())
                {
                    using (instance.Add("1.2.1", By.Id("I1.2.1")).DefineScope())
                    {
                        instance.Add("1.2.1.1", By.Id("I1.2.1.1"));
                        instance.Add("1.2.1.2", By.Id("I1.2.1.2"));
                    }

                    instance.Add("1.2.2", By.Id("I1.2.2"));
                }
            }
            
            _realControlNamingMapper.GetFindCommand("1").AutomationPropertyValue.Should().Be("[AutomationID=I1]");
            _realControlNamingMapper.GetFindCommand("1.1").AutomationPropertyValue.Should().Be("[AutomationID=I1]=>[AutomationID=I1.1]");
            _realControlNamingMapper.GetFindCommand("1.1.1").AutomationPropertyValue.Should().Be("[AutomationID=I1]=>[AutomationID=I1.1]=>[AutomationID=I1.1.1]");

            _realControlNamingMapper.GetFindCommand("1.2").AutomationPropertyValue.Should().Be("[AutomationID=I1]=>[AutomationID=I1.2]");
            _realControlNamingMapper.GetFindCommand("1.2.1").AutomationPropertyValue.Should().Be("[AutomationID=I1]=>[AutomationID=I1.2]=>[AutomationID=I1.2.1]");
            _realControlNamingMapper.GetFindCommand("1.2.1.1").AutomationPropertyValue.Should().Be("[AutomationID=I1]=>[AutomationID=I1.2]=>[AutomationID=I1.2.1]=>[AutomationID=I1.2.1.1]");
            _realControlNamingMapper.GetFindCommand("1.2.1.2").AutomationPropertyValue.Should().Be("[AutomationID=I1]=>[AutomationID=I1.2]=>[AutomationID=I1.2.1]=>[AutomationID=I1.2.1.2]");

            _realControlNamingMapper.GetFindCommand("1.2.2").AutomationPropertyValue.Should().Be("[AutomationID=I1]=>[AutomationID=I1.2]=>[AutomationID=I1.2.2]");

            // Additional tests for By
            _realControlNamingMapper.GetFindCommand("1").AutomationPropertyValue.Should().Be(By.Id("I1").Compile());
            _realControlNamingMapper.GetFindCommand("1.1").AutomationPropertyValue.Should().Be(By.Id("I1").Then(By.Id("I1.1")).Compile());
            _realControlNamingMapper.GetFindCommand("1.1.1").AutomationPropertyValue.Should().Be(By.Id("I1").Then(By.Id("I1.1").Then(By.Id("I1.1.1"))).Compile());

            _realControlNamingMapper.GetFindCommand("1.2").AutomationPropertyValue.Should().Be(By.Id("I1").Then(By.Id("I1.2")).Compile());
            _realControlNamingMapper.GetFindCommand("1.2.1").AutomationPropertyValue.Should().Be(By.Id("I1").Then(By.Id("I1.2").Then(By.Id("I1.2.1"))).Compile());
            _realControlNamingMapper.GetFindCommand("1.2.1.1").AutomationPropertyValue.Should().Be(By.Id("I1").Then(By.Id("I1.2").Then(By.Id("I1.2.1").Then(By.Id("I1.2.1.1")))).Compile());
            _realControlNamingMapper.GetFindCommand("1.2.1.2").AutomationPropertyValue.Should().Be(By.Id("I1").Then(By.Id("I1.2").Then(By.Id("I1.2.1").Then(By.Id("I1.2.1.2")))).Compile());

            _realControlNamingMapper.GetFindCommand("1.2.2").AutomationPropertyValue.Should().Be(By.Id("I1").Then(By.Id("I1.2").Then(By.Id("I1.2.2"))).Compile());
        }

        [TestCase("id1", "caption1")]
        [TestCase("id2", "caption2")]
        public void ShouldRegisterComplexConfigurationPropertiesUsingBy(string id, string caption)
        {
            var instance = CreateInstance();

            instance.Add(caption,
                By.Id(id)
                .Virtualized
                .TreeScope(Scope.Children)
                .OnlyVisible
                .Type(ControlTypes.DataGrid)
                );

            var expected = new ControlConfiguration(
                null, 
                Mock.Of<IControlMapper>(), 
                caption, 
                $"[AutomationID={id},Virtualized,TreeScope=Children,OnlyVisible,Type=data grid]", 
                AutomationSearchBehavior.ByVPath);

            _namingMapperMock.Verify(x => x.Add(expected), Times.Once);
        }

        [Test]
        [Theory]
        public void ShouldRegisterTypesUsingBy(ControlTypes type)
        {
            var instance = CreateInstance();

            instance.Add("Test", By.Type(type));

            var expected = new ControlConfiguration(
                null,
                Mock.Of<IControlMapper>(),
                "Test",
                $"[Type={type.GetAttribute<DisplayAttribute>().Name}]",
                AutomationSearchBehavior.ByVPath);

            _namingMapperMock.Verify(x => x.Add(expected), Times.Once);
        }

        [TestCase("Name1", "caption1")]
        [TestCase("Name2", "caption2")]
        public void ShouldRegisterConfigurationPropertiesName(string name, string caption)
        {
            var instance = CreateInstance();

            instance.Add(caption).For.Name(name);

            var expected = new ControlConfiguration(null, Mock.Of<IControlMapper>(), caption, $"[Name={name}]", AutomationSearchBehavior.ByVPath);

            _namingMapperMock.Verify(x => x.Add(expected), Times.Once);
        }

        private FluentConfiguration CreateInstance()
        {
            return new FluentConfiguration(_namingMapperMock.Object);
        }

        private FluentConfiguration CreateRealInstance()
        {
            _realControlNamingMapper = new ControlNamingMapper();
            _realControlMapper = new ControlMapper(_realControlNamingMapper);
            return new FluentConfiguration(_realControlMapper);
        }
    }
}
