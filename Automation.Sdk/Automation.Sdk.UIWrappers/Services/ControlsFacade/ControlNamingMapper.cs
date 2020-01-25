namespace Automation.Sdk.UIWrappers.Services.ControlsFacade
{
    using System.Collections.Generic;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class ControlNamingMapper
    {
        private readonly List<ControlConfiguration> _controlConfigurations;

        public ControlNamingMapper()
        {
            _controlConfigurations = new List<ControlConfiguration>();
        }

        public virtual void Add([NotNull] ControlConfiguration configuration)
        {
            _controlConfigurations.Add(configuration);
        }

        [NotNull]
        public virtual ControlFindCommand GetFindCommand([NotNull] string caption)
        {
            // At first we try to find override for requested configuration
            foreach (var controlConfiguration in _controlConfigurations)
            {
                if (controlConfiguration.Caption == caption)
                {
                    return controlConfiguration.Build(caption);
                }
            }

            return ControlFindCommand.BuildDefaultCommand(caption);
        }
    }
}
