namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Windows.Automation;
    using ControlDrivers;

    [Obsolete("Consider using Automation.Sdk.UIWrappers.ControlDrivers.Element instead")]
    public class Finder
    {
        private const int Wait = 60000; // ms
        private const int ShortWait = 1000; // ms, needs to be smaller than _wait

        private readonly List<Condition> _conditions = new List<Condition>();
        private readonly AutomationElement _element;

        private ControlType _controlType;

        public Finder(AutomationElement element)
        {
            _element = element;
        }

        public Finder ByName(string name)
        {
            _conditions.Add(new PropertyCondition(AutomationElementIdentifiers.NameProperty, name));
            return this;
        }

        public Finder ByAutomationId(string automationId)
        {
            _conditions.Add(new PropertyCondition(AutomationElementIdentifiers.AutomationIdProperty, automationId));
            return this;
        }

        public Finder ByControlType(ControlType t)
        {
            _conditions.Add(new PropertyCondition(AutomationElementIdentifiers.ControlTypeProperty, t));
            return this;
        }

        public T Find<T>() where T : Element
        {
            _controlType = AutomationFacade.ControlTypeConverter.Convert<T>();
            _conditions.Add(new PropertyCondition(AutomationElementIdentifiers.ControlTypeProperty, _controlType));
            return Find() as T;
        }

        public Element Find(bool shouldWait = true, TreeScope scope = TreeScope.Descendants)
        {
            var condition = _conditions.Count == 1 ? _conditions[0] : new AndCondition(_conditions.ToArray());

            if (shouldWait)
            {
                var timer = new Stopwatch();

                timer.Start();
                while (timer.ElapsedMilliseconds < Wait)
                {
                    var child = _element.FindFirst(scope, condition);

                    if (child != null)
                    {
                        return AutomationFacade.ControlTypeConverterService.Convert(child);
                    }

                    Thread.Sleep(ShortWait);
                    Trace.WriteLine($"Finder retrying. Time is {timer.ElapsedMilliseconds} / {Wait}");
                }

                Trace.WriteLine($"Finder expired at {Wait} ms");
            }
            else
            {
                var child = _element.FindFirst(scope, condition);
                if (child != null)
                {
                    return AutomationFacade.ControlTypeConverterService.Convert(child);
                }
            }

            return null;
        }

        public T[] FindAll<T>() where T : Element
        {
            var condition = _conditions.Count == 1 ? _conditions.Single() : new AndCondition(_conditions.ToArray());

            var timer = new Stopwatch();
            timer.Start();
            while (timer.ElapsedMilliseconds < Wait)
            {
                var controls = _element.FindAll(TreeScope.Children, condition);
                if (controls != null)
                {
                    return AutomationFacade.ControlTypeConverterService.Convert<T>(controls);
                }

                Thread.Sleep(ShortWait);
                Console.WriteLine($"Finder retrying.  Time is {timer.ElapsedMilliseconds} / {Wait}");
            }

            Console.WriteLine($"Finder expired at {Wait} ms");
            return null;
        }

        public void ClearConditions()
        {
            _conditions.Clear();
        }
    }
}
