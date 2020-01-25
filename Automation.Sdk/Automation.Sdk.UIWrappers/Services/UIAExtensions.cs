
namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Automation;
    using ControlDrivers;
    using System.Reflection;

    public static class UIAExtensions
    {
        public static List<string> AutomationIDList<T>(this List<T> elements) where T: Element
        {
            List<string> list = new List<string>();

            foreach (var element in elements)
            {
                list.Add(element.AutomationId);
            }

            return list;
        }

        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
                where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }

        /// <summary>
        /// Checks if a string contains multiple sub strings
        /// </summary>
        /// <param name="s">String to verify</param>
        /// <param name="chunks">chunks to find</param>
        /// <returns>True if all chunks are found</returns>
        public static bool Contains(this string s, params string[] chunks)
        {
            foreach (var chunk in chunks)
            {
                if (!s.Contains(chunk))
                {
                    return false;
                }
            }

            return true;
        }

        public static AutomationElement SafeFindFirst(this AutomationElement context, TreeScope scope, Condition condition)
        {
            try
            {
                return context.FindFirst(scope, condition);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Scrolls Container Element to target element.
        /// <para>!! Experimental !!</para>
        /// Do not use directly unless you are sure what you are doing.
        /// </summary>
        /// <param name="container">Element which will be scrolled</param>
        /// <param name="target">Element to scroll to</param>
        public static void ScrollTo(this Element container, Element target)
        {
            ScrollTo(container.AutomationElement, target.AutomationElement);
        }

        /// <summary>
        /// Scrolls Container Element to target element.
        /// <para>!! Experimental !!</para>
        /// Do not use directly unless you are sure what you are doing.
        /// </summary>
        /// <param name="container">Element which will be scrolled</param>
        /// <param name="target">Element to scroll to</param>
        public static void ScrollTo(this AutomationElement container, AutomationElement target)
        {
            if (target == null)
            {
                AutomationFacade.Logger.Write("Cannot scroll to target because target is null");
                return;
            }

            if (!(bool)container.GetCurrentPropertyValue(AutomationElement.IsScrollPatternAvailableProperty))
            {
                AutomationFacade.Logger.Write($"Cannot scroll to {target} because container has no ScrollPattern");
                return;
            }

            ScrollPattern containerScrollPattern = (ScrollPattern)container.GetCurrentPattern(ScrollPattern.Pattern);

            var targetRect = target.Current.BoundingRectangle;
            var containerRect = container.Current.BoundingRectangle;

            // TODO: Redesign Virtualized controls scrolling.
            // InnerSize is the size without scrollers and decorative elements
            // Unfortunately you cannot obtain this value if you dont know it beforehand
            // 17 pixels here are ScrollBars width and height.
            // This is actually not very stable since many panels may have decorative elements
            // and scroll bars bigger than 17 px. So this workaround should be someday redesigned.
            var containerInnerSize = new Size(containerRect.Width - (containerScrollPattern.Current.VerticallyScrollable ? 17 : 0),
                                              containerRect.Height - (containerScrollPattern.Current.HorizontallyScrollable ? 17 : 0));

            var targetCenter = new Point(targetRect.X + targetRect.Width / 2, targetRect.Y + targetRect.Height / 2);
            var containerCenter = new Point(containerRect.X + containerRect.Width / 2, containerRect.Y + containerRect.Height / 2);

            Vector pixelsInScrollPercent = new Vector((containerInnerSize.Width / containerScrollPattern.Current.HorizontalViewSize * 100 - containerInnerSize.Width) / 100,
                                                (containerInnerSize.Height / containerScrollPattern.Current.VerticalViewSize * 100 - containerInnerSize.Height) / 100);

            var targetX = targetCenter.X - containerCenter.X + containerScrollPattern.Current.HorizontalScrollPercent * pixelsInScrollPercent.X;
            var targetY = targetCenter.Y - containerCenter.Y + containerScrollPattern.Current.VerticalScrollPercent * pixelsInScrollPercent.Y;

            var horizontalPercent = targetX / pixelsInScrollPercent.X;
            var verticalPercent = targetY / pixelsInScrollPercent.Y;

            verticalPercent = Math.Max(Math.Min(verticalPercent, 100), 0);
            horizontalPercent = Math.Max(Math.Min(horizontalPercent, 100), 0);

            if (!containerScrollPattern.Current.VerticallyScrollable)
            {
                verticalPercent = -1;
            }

            if (!containerScrollPattern.Current.HorizontallyScrollable)
            {
                horizontalPercent = -1;
            }

            containerScrollPattern.SetScrollPercent(horizontalPercent, verticalPercent);
        }
    }
}
