namespace Automation.Sdk.UIWrappers.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Automation.Sdk.UIWrappers.Services;
    using JetBrains.Annotations;
    using Microsoft.Practices.Unity.InterceptionExtension;

    [UsedImplicitly]
    public sealed class LoggingInterceptionBehavior : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (input.MethodBase.CustomAttributes.Any(x => x.AttributeType == typeof(LoggerHiddenAttribute)) ||
                input.Target.GetType().CustomAttributes.Any(x => x.AttributeType == typeof(LoggerHiddenAttribute)))
            {
                return getNext()(input, getNext);
            }
            AutomationFacade.Logger.Write(GetDescription(input));
            return getNext()(input, getNext);
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute => true;

        private string GetDescription(IMethodInvocation input)
        {
            var builder = new StringBuilder();
            builder.Append($"{GetRealType(input.Target.GetType()).Name}.{input.MethodBase.Name}(");
            if (input.Arguments.Count != 0)
            {
                builder.Append(input.Arguments.Cast<object>().Select(x => $@"""{x}""").Aggregate((x, y) => $"{x},{y}"));
            }
            builder.Append(");");
            return builder.ToString();
        }

        private Type GetRealType([NotNull] Type wrapperType)
        {
            return wrapperType.Namespace.Contains("DynamicModule") ? GetRealType(wrapperType.BaseType) : wrapperType;
        }
    }
}
