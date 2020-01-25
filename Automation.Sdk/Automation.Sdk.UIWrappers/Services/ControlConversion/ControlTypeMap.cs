namespace Automation.Sdk.UIWrappers.Services.ControlConversion
{
    using System;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using JetBrains.Annotations;

    internal sealed class ControlTypeMap<TUIMapType> : IControlTypeMap<TUIMapType> where TUIMapType : Element
    {
        private readonly ControlType _controlType;

        public ControlTypeMap([NotNull] ControlType controlType)
        {
            _controlType = controlType;
        }

        public Type UIMapType => typeof(TUIMapType);

        public ControlType ControlType => _controlType;
    }
}