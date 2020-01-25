namespace Automation.Sdk.UIWrappers.Adapters.Desktop
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.Container;
    using FluentAssertions;
    using JetBrains.Annotations;
    using NUnit.Framework;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IValueAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Desktop)]
    internal sealed class ValueAdapter : Element, IValueAdapter
    {
        public ValueAdapter([NotNull] IElement element)
            : base(element)
        {
        }

        public object Value
        {
            get
            {
                if (GetProperty<bool>(AutomationElement.IsValuePatternAvailableProperty))
                {
                    return GetProperty<object>(ValuePattern.ValueProperty);
                }

                if (GetProperty<bool>(AutomationElement.IsRangeValuePatternAvailableProperty))
                {
                    return GetProperty<object>(RangeValuePattern.ValueProperty);
                }

                Assert.Fail($@"""{this}"" has no any Value patterns available");
                return null;
            }
        }

        public void SetValue(string newValue)
        {
            if (GetProperty<bool>(AutomationElement.IsValuePatternAvailableProperty))
            {
                AutomationFacade.Logger.Write($@"Setting value ""{newValue}"" for ""{this}"" using ValuePattern");

                ValuePattern valuePattern = GetPattern<ValuePattern>(ValuePattern.Pattern);
                valuePattern.SetValue(newValue);
            }
            else if (GetProperty<bool>(AutomationElement.IsRangeValuePatternAvailableProperty))
            {
                AutomationFacade.Logger.Write($@"Setting value ""{newValue}"" for ""{this}"" using RangeValuePattern");
                double newValueConverted = double.Parse(newValue);

                RangeValuePattern rangeValuePattern = GetPattern<RangeValuePattern>(RangeValuePattern.Pattern);
                rangeValuePattern.SetValue(newValueConverted);
            }
            else
            {
                Assert.Fail($@"""{this}"" has no any Value patterns available");
            }
        }

        public TValue GetValue<TValue>()
        {
            var value = Value;
            value.Should().BeAssignableTo(typeof(TValue));

            return (TValue)value;
        }
    }
}