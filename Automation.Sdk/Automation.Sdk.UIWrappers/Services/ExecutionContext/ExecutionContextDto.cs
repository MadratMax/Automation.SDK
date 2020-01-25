namespace Automation.Sdk.UIWrappers.Services.ExecutionContext
{
    using System;
    using System.Collections.Generic;

    public sealed class ExecutionContextDto
    {
        public ExecutionContextDto()
        {
            Steps = new List<string>();
            Tags = new List<string>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public List<string> Tags { get; private set; }

        public List<string> Steps { get; private set; }
    }
}