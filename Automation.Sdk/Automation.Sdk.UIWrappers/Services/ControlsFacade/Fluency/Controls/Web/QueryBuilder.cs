namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Web
{
    using System.Collections.Generic;
    using System.Linq;

    public class QueryBuilder
    {
        private readonly IWebMap _webMap;

        public QueryBuilder(IWebMap webMap)
        {
            _webMap = webMap;
        }

        public FindQuery BuildQuery(string caption)
        {
            Stack<WebControlMap> steps = new Stack<WebControlMap>();

            steps.Push(_webMap.Find(caption));

            while (steps.First().Parent != string.Empty)
            {
                steps.Push(_webMap.Find(steps.First().Parent));
            }

            return new FindQuery(caption, steps);
        }
    }
}