namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls
{
    using System;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop;
    using JetBrains.Annotations;

    // Control configuration interface which provides access 
    // to fluent chains and custom configuration parameters.
    // Basic configuration parameters are not intended to be 
    // accessed from it.
    public interface IControlConfiguration
    {
        /// <summary>
        /// Defines children scope to register controls which are
        /// belongs to currently registered one
        /// </summary>
        /// <returns></returns>
        IDisposable DefineScope();

        /// <summary>
        /// Create new control configuration from the very beginning
        /// </summary>
        /// <param name="controlCaption">New control caption</param>
        /// <returns>Incomplete control configuration with successive commands</returns>
        [NotNull]
        IncompleteControlConfiguration Add([NotNull] string controlCaption);
    }
}