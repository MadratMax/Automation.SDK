namespace Automation.Sdk.UIWrappers.Services.SearchEngines.VPath
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Enums;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Interfaces;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Maps;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Options;
    using Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Cache;

    public sealed class VPathSearchEngine
    {
        private readonly AutomationPropertyMap _propertiesMap;
        private readonly ILogger _logger;
        private VPathCache _cache;

        public VPathSearchEngine(
            AutomationPropertyMap propertiesMap,
            ILogger logger)
        {
            _propertiesMap = propertiesMap;
            _logger = logger;
            _cache = new VPathCache();
        }

        public AutomationElement Find(ControlFindCommand command, int timeoutOverride = -1)
        {
            var timeout = timeoutOverride >= 0 ? timeoutOverride : command.Timeout;

            var steps = Regex.Split(command.AutomationPropertyValue, "=>");

            AutomationElement targetElement = null;
            Methods.WaitUntil(() => (targetElement = ExecuteSteps(steps)) != null, timeout);

            return targetElement;
        }

        private AutomationElement ExecuteSteps(string[] steps)
        {
            var context = AutomationElement.RootElement;
            AutomationElement element = null;
            var freshCache = new VPathCache();
            string cacheKey = string.Empty;

            foreach (var step in steps)
            {
                _logger.Write($"Looking for {step}");

                if (cacheKey == string.Empty)
                {
                    cacheKey = step;
                }
                else
                {
                    cacheKey = string.Join("=>", cacheKey, step);
                }

                if (_cache.Contains(cacheKey) && IsAvailable(_cache[cacheKey]))
                {
                    element = _cache[cacheKey];
                    context = element;
                    freshCache.Add(step, element);
                    _logger.Write($"{step} obtained from cache");
                    continue;
                }

                var propertyList = step.Substring(1, step.Length - 2).Split(',');

                SearchOptions options = GetSearchOptions(propertyList);
                Condition findCondition = GetSearchConditions(options, propertyList);

                element = FindControl(context, findCondition, options);

                if (element == null)
                {
                    _logger.Write($"Control {step} not found");
                    return null;
                }

                _logger.Write($"Found control {step}");
                context = element;
                freshCache.Add(step, element);
            }

            _cache = freshCache;
            return element;
        }

        private bool IsAvailable(AutomationElement automationElement)
        {
            try
            {
                if (automationElement == null)
                {
                    return false;
                }

                return !automationElement.Current.IsOffscreen && automationElement.Current.BoundingRectangle != Rect.Empty;
            }
            catch
            {
                return false;
            }
        }

        public bool IsAbsent(ControlFindCommand command)
        {
            var control = Find(command, 0);

            if (control == null)
            {
                return true;
            }

            return !IsAvailable(control);
        }

        private AutomationElement FindControl(AutomationElement rootElement, Condition findCondition, SearchOptions searchOptions)
        {
            AutomationElement automationElement;
            if (searchOptions.UseVirtualization && GetScrollPatternAvailability(rootElement))
            {
                automationElement = FindWithScroll(rootElement, searchOptions.TreeScope, findCondition);

                if (automationElement != null)
                {
                    rootElement.ScrollTo(automationElement);
                }

                // TODO: Further improvements may be required someday. For now this is enough
                if (searchOptions.IndexNumber != null)
                {
                    automationElement = FindByIndex(rootElement, findCondition, searchOptions);

                    if (automationElement != null)
                    {
                        rootElement.ScrollTo(automationElement);
                    }
                }
            }
            else
            {
                if (searchOptions.IndexNumber == null)
                { 
                    automationElement = rootElement.FindFirst(searchOptions.TreeScope, findCondition);
                }
                else
                {
                    automationElement = FindByIndex(rootElement, findCondition, searchOptions);
                }
            }

            // If VisualTree of found window is locked/not updated/etc. we need to re-find window again.
            if (!ValidateElementVisualSubtree(automationElement))
            {
                return null;
            }

            return automationElement;
        }

        private static AutomationElement FindByIndex(AutomationElement rootElement, Condition findCondition, SearchOptions searchOptions)
        {
            var all = rootElement.FindAll(searchOptions.TreeScope, findCondition);
            int index = searchOptions.IndexNumber ?? default(int);
            if (index == IndexNumberOption.LAST)
            {
                index = all.Count - 1;
            }

            if (all.Count < searchOptions.IndexNumber)
            {
                return null;
            }

            return all[index];
        }

        private SearchOptions GetSearchOptions(string[] properties)
        {
            var setup = new SearchOptions();

            foreach (var property in properties)
            {
                var chunks = property.Split('=');
                var name = chunks[0];

                var pmap = _propertiesMap[name];

                if (pmap.Type == SearchPropertyType.SearchOption)
                {
                    var value = chunks.Length == 2 ? chunks[1] : string.Empty;
                    var prop = (ISearchOption)pmap;
                    prop.FromString(setup, value);
                }
            }

            return setup;
        }

        private Condition GetSearchConditions(SearchOptions options, string[] properties)
        {
            List<Condition> conditions = new List<Condition>();

            foreach (var property in properties)
            {
                var chunks = property.Split('=');
                var name = chunks[0];

                var pmap = _propertiesMap[name];

                if (pmap.Type == SearchPropertyType.AutomationProperty)
                {
                    var value = chunks[1];
                    var prop = (ISearchAutomationProperty)pmap;

                    conditions.Add(new PropertyCondition(prop.AutomationProperty, prop.FromString(value)));
                }
            }

            if (options.OnlyVisible)
            {
                conditions.Add(new PropertyCondition(AutomationElementIdentifiers.IsOffscreenProperty, false));
                conditions.Add(new NotCondition(new PropertyCondition(AutomationElementIdentifiers.BoundingRectangleProperty, Rect.Empty)));
            }

            if (conditions.Count == 1)
            {
                return conditions[0];
            }

            return new AndCondition(conditions.ToArray());
        }

        private bool ValidateElementVisualSubtree(AutomationElement control)
        {
            if (control == null)
            {
                return false;
            }

            if (!control.Current.ControlType.Equals(ControlType.Window))
            {
                return true;
            }

            var firstChild = control.FindFirst(TreeScope.Children, Condition.TrueCondition);
            return firstChild != null;
        }

        private bool GetScrollPatternAvailability(AutomationElement searchContext)
        {
            return (bool)searchContext.GetCurrentPropertyValue(AutomationElement.IsScrollPatternAvailableProperty);
        }

        private AutomationElement FindWithScroll(AutomationElement searchContext, TreeScope scope, Condition cond)
        {
            AutomationElement control = searchContext.FindFirst(scope, cond);

            if (control == null)
            {
                ScrollPattern scrollPattern = (ScrollPattern)searchContext.GetCurrentPattern(ScrollPattern.Pattern);
                bool horizontallyScrollable = scrollPattern.Current.HorizontallyScrollable;
                bool verticallyScrollable = scrollPattern.Current.VerticallyScrollable;

                if (!horizontallyScrollable && !verticallyScrollable)
                {
                    return null;
                }

                //// [AB] 30/06/2016: Some optimization
                //// We may want to scroll down at second try
                //// Because most of the time the fields are given in from-top-to-bottom order
                //// So it may be not needed to start from the top
                //// Especially for table fields in Completion

                if (verticallyScrollable && scrollPattern.Current.VerticalScrollPercent < 100)
                {
                    scrollPattern.ScrollVertical(ScrollAmount.LargeIncrement);

                    control = searchContext.FindFirst(scope, cond);

                    if (control != null)
                    {
                        return control;
                    }
                }

                double hScrollStarter = horizontallyScrollable ? 0 : -1;
                double vScrollStarter = verticallyScrollable ? 0 : -1;
                scrollPattern.SetScrollPercent(hScrollStarter, vScrollStarter);

                if (verticallyScrollable && horizontallyScrollable)
                {
                    while (control == null && scrollPattern.Current.VerticalScrollPercent < 100)
                    {
                        scrollPattern.SetScrollPercent(0, scrollPattern.Current.VerticalScrollPercent);

                        while (control == null && scrollPattern.Current.HorizontalScrollPercent < 100)
                        {
                            control = searchContext.FindFirst(scope, cond);

                            if (control == null)
                            {
                                scrollPattern.ScrollHorizontal(ScrollAmount.LargeIncrement);
                            }
                        }

                        control = searchContext.FindFirst(scope, cond);

                        if (control == null)
                        {
                            scrollPattern.ScrollVertical(ScrollAmount.LargeIncrement);
                        }
                    }
                }
                else if (verticallyScrollable)
                {
                    while (control == null && scrollPattern.Current.VerticalScrollPercent < 100)
                    {
                        control = searchContext.FindFirst(scope, cond);

                        if (control == null)
                        {
                            scrollPattern.ScrollVertical(ScrollAmount.LargeIncrement);
                        }
                    }

                    control = searchContext.FindFirst(scope, cond);
                }
                else
                {
                    while (control == null && scrollPattern.Current.HorizontalScrollPercent < 100)
                    {
                        control = searchContext.FindFirst(scope, cond);

                        if (control == null)
                        {
                            scrollPattern.ScrollHorizontal(ScrollAmount.LargeIncrement);
                        }
                    }

                    control = searchContext.FindFirst(scope, cond);
                }
            }

            return control;
        }
    }
}