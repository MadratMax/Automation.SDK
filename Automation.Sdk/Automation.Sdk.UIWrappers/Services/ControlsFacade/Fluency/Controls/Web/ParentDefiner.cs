namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Web
{
    using System;

    public class ParentDefiner : IDisposable
    {
        private readonly IWebMap _webMap;
        private readonly string _previousParent;

        public ParentDefiner(IWebMap webMap, string previousParent)
        {
            _webMap = webMap;
            _previousParent = previousParent;
        }

        public void Dispose()
        {
            _webMap.CurrentParent = _previousParent;
        }
    }
}