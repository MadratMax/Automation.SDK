namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls
{
    using System;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop;
    using JetBrains.Annotations;

    public interface IControlMapper
    {
        void Add([NotNull] ControlConfiguration configuration);

        IDisposable ConsumeParent(ControlConfiguration parent);
    }
}