namespace Automation.Sdk.UIWrappers.Services.ControlConversion
{
    using System;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using JetBrains.Annotations;

    internal interface IControlTypeMap<out TUIMapType> where TUIMapType : Element
    {
        Type UIMapType { [NotNull] get; }

        ControlType ControlType { [NotNull] get; }
    }
}