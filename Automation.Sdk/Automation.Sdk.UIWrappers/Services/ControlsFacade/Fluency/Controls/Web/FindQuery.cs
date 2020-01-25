namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Web
{
    using System.Collections.Generic;

    public class FindQuery
    {
        private readonly string _elementName;
        private readonly Stack<WebControlMap> _steps;

        public FindQuery(string elementName, Stack<WebControlMap> steps)
        {
            _elementName = elementName;
            _steps = steps;
        }

        public Stack<WebControlMap> Steps => _steps;

        public string ElementName => _elementName;

        public int Timeout = Shouldly.Timeout;
    }
}