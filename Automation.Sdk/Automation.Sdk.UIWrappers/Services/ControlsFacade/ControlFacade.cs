namespace Automation.Sdk.UIWrappers.Services.ControlsFacade
{
    using System;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Web;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class ControlFacade
    {
        private readonly ControlNamingMapper _namingMapper;
        private readonly Logger _logger;
        private readonly ControlQueries _controlQueries;
        private readonly QueryBuilder _queryBuilder;
        private PlatformContextType _currentPlatform;

        public ControlFacade(
            [NotNull] ControlNamingMapper namingMapper, 
            [NotNull] Logger logger, 
            [NotNull] ControlQueries controlQueries,
            [NotNull] QueryBuilder queryBuilder)
        {
            _namingMapper = namingMapper;
            _logger = logger;
            _controlQueries = controlQueries;
            _queryBuilder = queryBuilder;
            CurrentPlatform = PlatformContextType.Desktop;
        }

        public PlatformContextType CurrentPlatform
        {
            get { return _currentPlatform; }
            set { _currentPlatform = value; }
        }

        public virtual bool IsAbsent(string caption)
        {
            if (_currentPlatform == PlatformContextType.Desktop)
            {
                var command = _namingMapper.GetFindCommand(caption);
                _logger.Write($"control find command: {command}");

                if (command.AutomationSearchBehavior != AutomationSearchBehavior.ByVPath)
                {
                    throw new NotImplementedException();
                }

                return _controlQueries.IsAbsent(command);
            }
            else if (_currentPlatform == PlatformContextType.Web)
            {
                var command = _queryBuilder.BuildQuery(caption);
                return _controlQueries.IsAbsent(command);
            }
            else
            {
                throw new Exception($"Unsupported platform: {_currentPlatform}");
            }
        }

        public virtual IElement Get(string caption)
        {
            _logger.Write($@"trying to find control ""{caption}""");

            if (_currentPlatform == PlatformContextType.Desktop)
            { 
                var command = _namingMapper.GetFindCommand(caption);
                _logger.Write($"control find command: {command}");

                if (command.AutomationSearchBehavior != AutomationSearchBehavior.ByVPath)
                {
                    throw new NotImplementedException();
                }

                return _controlQueries.ByCommand(command);
            }
            else if (_currentPlatform == PlatformContextType.Web)
            {
                var command = _queryBuilder.BuildQuery(caption);
                return _controlQueries.ByCommand(command);
            }
            else
            {
                throw new Exception($"Unsupported platform: {_currentPlatform}");
            }
        }
    }
}