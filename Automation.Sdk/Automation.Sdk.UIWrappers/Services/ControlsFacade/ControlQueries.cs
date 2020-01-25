namespace Automation.Sdk.UIWrappers.Services.ControlsFacade
{
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Web;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using Automation.Sdk.UIWrappers.Services.SearchEngines;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class ControlQueries
    {
        private readonly Logger _logger;
        private readonly VPathSearchEngine _vPathSearchEngine;
        private readonly WebSearchEngine _webSearchEngine;

        public ControlQueries([NotNull] Logger logger,
                              [NotNull] VPathSearchEngine vPathSearchEngine,
                              [NotNull] WebSearchEngine webSearchEngine)
        {
            _logger = logger;
            _vPathSearchEngine = vPathSearchEngine;
            _webSearchEngine = webSearchEngine;
        }

        public IElement ByCommand(ControlFindCommand findQuery)
        {
            _logger.Write($@"trying to find element ""{findQuery.ElementName}"" by ""{findQuery.AutomationPropertyValue}""");

            var element = _vPathSearchEngine.Find(findQuery);
            if (element == null)
            {
                return new ElementContainer(null, findQuery.ElementName, PlatformContextType.Desktop);
            }

            _logger.Write($@"element ""{findQuery.ElementName}"" has been found");
            return new ElementContainer(element, findQuery.ElementName, PlatformContextType.Desktop);
        }

        public IElement ByCommand(FindQuery findQuery)
        {
            _logger.Write($@"trying to find element ""{findQuery.ElementName}""");

            var webElement = _webSearchEngine.Find(findQuery);
            if (webElement == null)
            {
                return new ElementContainer(null, findQuery.ElementName, PlatformContextType.Web);
            }

            _logger.Write($@"element ""{findQuery.ElementName}"" has been found");
            return new ElementContainer(webElement, findQuery.ElementName, PlatformContextType.Web);
        }

        public bool IsAbsent(FindQuery findQuery)
        {
            var result = _webSearchEngine.IsAbsent(findQuery);

            if (!result)
            {
                _logger.Write($@"control ""{findQuery.ElementName}"" has been found");
            }

            return result;
        }

        public bool IsAbsent(ControlFindCommand findQuery)
        {
            var result = _vPathSearchEngine.IsAbsent(findQuery);

            if (!result)
            {
                _logger.Write($@"control {findQuery.AutomationSearchBehavior} ""{findQuery.AutomationPropertyValue}"" has been found");
            }

            return result;
        }
    }
}