namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.Collections.Generic;
    using Automation.Sdk.Bindings.Enums;
    using FluentAssertions;
    using NUnit.Framework;

    public static class ShouldBeMonad
    {
        public static TObject ShouldBe<TObject, TResult>(
            this TObject x, 
            Func<TObject, TResult> predicate, 
            AssertPredicate assertPredicate,
            TResult expected,
            string message = "")
        {
            switch (assertPredicate)
            {
                case AssertPredicate.Be: return x.ShouldBe(predicate, expected, message);
                case AssertPredicate.Become: return x.ShouldBecome(predicate, expected, message);
                default: Assert.Inconclusive($@"unsupported assert predicate ""{assertPredicate}"""); break;
            }
            return x;
        }

        public static TObject ShouldNotBe<TObject, TResult>(
            this TObject x,
            Func<TObject, TResult> predicate,
            AssertPredicate assertPredicate,
            TResult expected,
            string message = "")
        {
            switch (assertPredicate)
            {
                case AssertPredicate.Be: return x.ShouldNotBe(predicate, expected, message);
                case AssertPredicate.Become: return x.ShouldBecomeNot(predicate, expected, message);
                default: Assert.Inconclusive($@"unsupported assert predicate ""{assertPredicate}"""); break;
            }
            return x;
        }

        public static TObject ShouldBe<TObject, TResult>(
            this TObject x, 
            Func<TObject, TResult> predicate, 
            TResult expected, 
            string message = "")
        {
            predicate(x).Should().Be(expected, message);
            return x;
        }

        public static TObject ShouldNotBe<TObject, TResult>(
            this TObject x, 
            Func<TObject, TResult> predicate, 
            TResult expected, 
            string message = "")
        {
            predicate(x).Should().NotBe(expected, message);
            return x;
        }

        public static TObject ShouldBecome<TObject, TResult>(
            this TObject x, 
            Func<TObject, TResult> predicate, 
            TResult expected, 
            string message = "")
        {
            return ShouldBecome(x, predicate, expected, () => message);
        }

        public static TObject ShouldBecomeNot<TObject, TResult>(
            this TObject x, 
            Func<TObject, TResult> predicate, 
            TResult expected, 
            string message = "")
        {
            return ShouldBecomeNot(x, predicate, expected, () => message);
        }

        public static TObject ShouldBecome<TObject, TResult>(
            this TObject x, 
            Func<TObject, TResult> predicate, 
            TResult expected, 
            Func<string> failMessageFunction)
        {
            TResult result = default(TResult);
            var success = Methods.WaitUntil(() => EqualityComparer<TResult>.Default.Equals(result = predicate(x), expected), Shouldly.Timeout);

            if (!success)
            {
                result.Should().Be(expected, failMessageFunction());
            }

            return x;
        }

        public static TObject ShouldBecomeNot<TObject, TResult>(
            this TObject x, 
            Func<TObject, TResult> predicate, 
            TResult expected, 
            Func<string> failMessageFunction)
        {
            
            Methods.WaitUntil(() => !EqualityComparer<TResult>.Default.Equals(predicate(x), expected), failMessageFunction, Shouldly.Timeout);
            return x;
        }
    }
}