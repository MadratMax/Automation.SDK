namespace Automation.Sdk.UIWrappers.Adapters
{
    using System.Threading;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services;
    using FluentAssertions;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public sealed class ClickAdapter : Element, IClickAdapter
    {
        private readonly IVisibilityAdapter _visibilityAdapter;

        public ClickAdapter([NotNull] AutomationElement automationElement)
            : base(automationElement)
        {
            _visibilityAdapter = GetAdapter<IVisibilityAdapter>();
        }

        /// <summary>
        /// Click on Self, in center point of BoundingRectangle
        /// </summary>
        public void ClickOnSelf(MouseButton b = MouseButton.Left)
        {
            CheckElement();
            MoveMouseOverElement();

            Mouse.Click(b);
            ////need to slow it down before the click
            ////500ms is too large for the click delay. Overall downgrade of performance is about 20 minutes.
            Thread.Sleep(100);
        }

        /// <summary>
        /// The double click on Self
        /// </summary>
        public void DoubleClickOnSelf(MouseButton b = MouseButton.Left)
        {
            CheckElement();
            MoveMouseOverElement();

            // Mouse .DoubleClick(b); 
            // WPF not fast enough to process programmatical double-clicks
            Mouse.Click(b);
            Thread.Sleep(100);
            Mouse.Click(b);
        }

        private void CheckElement()
        {
            _visibilityAdapter.ShouldBecome(x => x.IsVisible, true, $"Element {this} should be visible.");
        }

        private void MoveMouseOverElement()
        {
            var clickablePoint = GetClickablePoint();
            var moveResult = Mouse.MoveTo(clickablePoint);
            moveResult.Should().BeTrue($"Mouse should be moved to position: {clickablePoint.X}:{clickablePoint.Y}");
        }
    }
}