namespace Automation.Sdk.UIWrappers.Services.SearchEngines
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.ControlConversion;

    // TODO: CAPP-228 Remove this class
    public sealed class LegacyControlsSearchEngineService
    {
        private readonly ControlTypeConverterService _controlTypeConverterService;

        public LegacyControlsSearchEngineService(ControlTypeConverterService controlTypeConverterService)
        {
            _controlTypeConverterService = controlTypeConverterService;
        }

        public Element Find(AutomationElement context, ControlType type, string name, string automationId, bool useTimeout, int timeout, TreeScope scope, bool useVirtualization)
        {
            if (!useTimeout)
            {
                timeout = 0;
            }

            Condition cond;

            if (automationId == string.Empty && name != string.Empty)
            {
                cond = new AndCondition(new PropertyCondition(AutomationElementIdentifiers.ControlTypeProperty, type),
                    new PropertyCondition(AutomationElementIdentifiers.NameProperty, name));
            }
            else if (name == string.Empty && automationId != string.Empty)
            {
                cond = new AndCondition(new PropertyCondition(AutomationElementIdentifiers.ControlTypeProperty, type),
                    new PropertyCondition(AutomationElementIdentifiers.AutomationIdProperty, automationId));
            }
            else if (name == string.Empty && automationId == string.Empty)
            {
                cond = new PropertyCondition(AutomationElementIdentifiers.ControlTypeProperty, type);
            }
            else
            {
                cond = new AndCondition(new PropertyCondition(AutomationElementIdentifiers.ControlTypeProperty, type),
                    new PropertyCondition(AutomationElementIdentifiers.NameProperty, name),
                    new PropertyCondition(AutomationElementIdentifiers.AutomationIdProperty, automationId));
            }

            AutomationElement control = null;

            Func<bool> controlIsFound = () =>
            {
                if (useVirtualization && (bool)context.GetCurrentPropertyValue(AutomationElement.IsScrollPatternAvailableProperty))
                {
                    control = FindWithScroll(context, scope, cond);
                }
                else
                {
                    control = context.SafeFindFirst(scope, cond);
                }
                
                return control != null;
            };
            Methods.WaitUntil(controlIsFound, timeout);

            return control == null ? null : _controlTypeConverterService.Convert(control);
        }

        public List<Element> GetChildren(AutomationElement context, ControlType type = null)
        {
            Condition condition;
            if (type == null)
            {
                condition = new OrCondition(new PropertyCondition(AutomationElementIdentifiers.NameProperty, string.Empty), new NotCondition(new PropertyCondition(AutomationElementIdentifiers.NameProperty, string.Empty)));
            }
            else
            {
                condition = new AndCondition(new PropertyCondition(AutomationElementIdentifiers.ControlTypeProperty, type),
                                             new OrCondition(new PropertyCondition(AutomationElementIdentifiers.NameProperty, string.Empty),
                                                             new NotCondition(new PropertyCondition(AutomationElementIdentifiers.NameProperty, string.Empty))));
            }

            var children = context.FindAll(TreeScope.Children, condition);

            List<Element> elements = new List<Element>();

            for (int i = 0; i < children.Count; ++i)
            {
                elements.Add(new Element(children[i]));
            }

            return elements;
        }

        public T FindByCondition<T>(AutomationElement context, Element.UIACondition condition, bool useTimeout, int timeout = 5000, TreeScope scope = TreeScope.Children) where T : Element
        {
            if (!useTimeout)
            {
                timeout = 0;
            }

            T foundElement = null;

            Func<bool> elementIsFound = () =>
            {
                T[] elements = FindAll<T>(context, scope, false);

                foreach (T e in elements)
                {
                    if (condition(e))
                    {
                        foundElement = e;
                        break;
                    }
                }

                return foundElement != null;
            };

            Methods.WaitUntil(elementIsFound, timeout);

            return foundElement;
        }

        /// <summary>
        /// Find all the children based on the the specified control type
        /// </summary>
        /// <typeparam name="T">Control Type</typeparam>
        /// <param name="context">Root element for search</param>
        /// <param name="scope">TreeScope</param>
        /// <param name="expectedToReturnMoreThanZero">If true, then method will wait until count of found elements will be greater than zero</param>
        /// <returns>List of children of type T</returns>
        public T[] FindAll<T>(AutomationElement context, TreeScope scope = TreeScope.Children, bool expectedToReturnMoreThanZero = true) where T : Element
        {
            ControlType type = AutomationFacade.ControlTypeConverter.Convert<T>();

            AutomationElementCollection controls = null;
            var cond = new PropertyCondition(AutomationElementIdentifiers.ControlTypeProperty, type);

            Func<bool> elementsIsFound = () =>
            {
                controls = context.FindAll(scope, cond);
                if (controls != null && (controls.Count > 0 || !expectedToReturnMoreThanZero))
                {
                    return true;
                }

                return false;
            };
            Methods.WaitUntil(elementsIsFound, 10000);

            return controls == null ? null : _controlTypeConverterService.Convert<T>(controls);
        }

        public List<T> FindAllByCondition<T>(AutomationElement context, Element.UIACondition condition, bool useTimeout, int timeout = 5000, TreeScope scope = TreeScope.Children) where T : Element
        {
            if (!useTimeout)
            {
                timeout = 0;
            }

            List<T> foundElements = new List<T>();
            T[] elements;

            Func<bool> anyFound = () =>
            {
                elements = FindAll<T>(context, scope, false);

                foreach (T e in elements)
                {
                    if (condition(e))
                    {
                        foundElements.Add(e);
                    }
                }

                return foundElements.Count > 0;
            };
            Methods.WaitUntil(anyFound, timeout);

            return foundElements;
        }

        private AutomationElement FindWithScroll(AutomationElement context, TreeScope scope, Condition cond)
        {
            AutomationElement control = context.SafeFindFirst(scope, cond);

            if (control == null)
            {
                ScrollPattern sp = (ScrollPattern)context.GetCurrentPattern(ScrollPattern.Pattern);
                bool horizontallyScrollable = sp.Current.HorizontallyScrollable;
                bool verticallyScrollable = sp.Current.VerticallyScrollable;

                if (!horizontallyScrollable && !verticallyScrollable)
                {
                    return null;
                }

                //// [AB] 30/06/2016: Some optimization
                //// We may want to scroll down at second try
                //// Because most of the time the fields are given in from-top-to-bottom order
                //// So it may be not needed to start from the top
                //// Especially for table fields in Completion

                if (verticallyScrollable && sp.Current.VerticalScrollPercent < 100)
                {
                    sp.ScrollVertical(ScrollAmount.LargeIncrement);

                    control = context.SafeFindFirst(scope, cond);

                    if (control != null)
                    {
                        return control;
                    }
                }

                double hScrollStarter = horizontallyScrollable ? 0 : -1;
                double vScrollStarter = verticallyScrollable ? 0 : -1;
                sp.SetScrollPercent(hScrollStarter, vScrollStarter);

                if (verticallyScrollable && horizontallyScrollable)
                {
                    while (control == null && sp.Current.VerticalScrollPercent < 100)
                    {
                        sp.SetScrollPercent(0, sp.Current.VerticalScrollPercent);

                        while (control == null && sp.Current.HorizontalScrollPercent < 100)
                        {
                            control = context.SafeFindFirst(scope, cond);

                            if (control == null)
                            {
                                sp.ScrollHorizontal(ScrollAmount.LargeIncrement);
                            }
                        }

                        control = context.SafeFindFirst(scope, cond);

                        if (control == null)
                        {
                            sp.ScrollVertical(ScrollAmount.LargeIncrement);
                        }
                    }
                }
                else if (verticallyScrollable)
                {
                    while (control == null && sp.Current.VerticalScrollPercent < 100)
                    {
                        control = context.SafeFindFirst(scope, cond);

                        if (control == null)
                        {
                            sp.ScrollVertical(ScrollAmount.LargeIncrement);
                        }
                    }

                    control = context.SafeFindFirst(scope, cond);
                }
                else
                {
                    while (control == null && sp.Current.HorizontalScrollPercent < 100)
                    {
                        control = context.SafeFindFirst(scope, cond);

                        if (control == null)
                        {
                            sp.ScrollHorizontal(ScrollAmount.LargeIncrement);
                        }
                    }

                    control = context.SafeFindFirst(scope, cond);
                }
            }

            return control;
        }
    }
}