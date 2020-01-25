namespace Automation.Sdk.UIWrappers.Services.SearchEngines
{
    using System;
    using Automation.Sdk.UIWrappers.Services.Container;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Web;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using JetBrains.Annotations;
    using OpenQA.Selenium;

    [UsedImplicitly]
    public class WebSearchEngine
    {
        private readonly IWebDriverContainer _webDriverContainer;
        private readonly ILogger _logger;

        public WebSearchEngine(
            IWebDriverContainer webDriverContainer,
            ILogger logger)
        {
            _webDriverContainer = webDriverContainer;
            _logger = logger;
        }

        public IWebElement Find(FindQuery findQuery, int timeoutOverride = -1)
        {
            var timeout = timeoutOverride >= 0 ? timeoutOverride : findQuery.Timeout;

            if (_webDriverContainer.WebDriver == null)
            {
                throw new Exception("WebDriver is not attached yet.");
            }

            ISearchContext context = _webDriverContainer.WebDriver;
            IWebElement element = null;

            while (findQuery.Steps.Count > 0)
            {
                var step = findQuery.Steps.Pop();
                Methods.WaitUntil(() => (element = FindElement(context, step.Locator)) != null,
                                  timeout);

                if (element == null)
                {
                    return null;
                }

                _logger.Write($"Found control by locator: {step.Locator}");
                context = element;
            }

            return context as IWebElement;
        }

        public bool IsAbsent(FindQuery findQuery)
        {
            var control = Find(findQuery, 0);

            if (control == null)
            {
                return true;
            }

            return !IsAvailable(control);
        }

        private bool IsAvailable(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }

        private IWebElement FindElement(ISearchContext context, By locator)
        {
            try
            {
                var element = context.FindElement(locator);
                if (!IsAvailable(element))
                {
                    return null;
                }

                return element;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }
            catch (NotFoundException)
            {
                return null;
            }
        }
    }
}