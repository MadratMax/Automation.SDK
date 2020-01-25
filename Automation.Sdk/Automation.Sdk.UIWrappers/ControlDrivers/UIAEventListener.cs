namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System.Windows.Automation;

    /// <summary>
    /// When subscribing to UIAutomation Events remember to unsubscribe when done.
    /// *For the time being this should be used on a event by event basis.
    /// Reference: http://msdn.microsoft.com/en-us/library/ms752286.aspx
    /// </summary>
    public class UIAEventListener
    {
        static AutomationElement elementSubscribed, raisedElement;
        static AutomationEvent elementEvent, triggeredEvent;
        static AutomationEventHandler uiaEventHandler;

        public static AutomationEvent TriggeredEvent
        {
            get { return triggeredEvent; }
        }

        public static AutomationElement RaisedElement
        {
            get { return raisedElement; }
        }

        /// <summary>
        /// AutomationEventHandler delegate.
        /// </summary>
        /// <param name="src">Object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnUIAutomationEvent(object src, AutomationEventArgs e)
        {
            // Make sure the element still exists. Elements such as tooltips
            // can disappear before the event is processed.
            try
            {
                raisedElement = src as AutomationElement;
                triggeredEvent = e.EventId;
            }
            catch (ElementNotAvailableException)
            {
                return;
            }

            if (e.EventId == AutomationElement.ToolTipOpenedEvent)
            {
                // TODO Add handling code.
            }
            else
            {
                // TODO Handle any other events that have been subscribed to.
            }
        }

        /// <summary>
        /// Cancel subscription to the event.
        /// </summary>
        public static void Unsubscribe()
        {
            if (uiaEventHandler != null)
            {
                Automation.RemoveAutomationEventHandler(elementEvent, 
                    elementSubscribed, uiaEventHandler);
            }

            // Reset Fields
            elementEvent = null;
            elementSubscribed = null;
            uiaEventHandler = null;
            triggeredEvent = null;
            raisedElement = null;
        }        
        
        /// <summary>
        /// Register an event handler for AutomationEvent on the specified element.
        /// </summary>
        /// <param name="elementButton">The automation element.</param>
        public static void SubscribeTo(AutomationElement elm, AutomationEvent evt)
        {
            if (elm != null)
            {
                Automation.AddAutomationEventHandler(evt, 
                     elm, TreeScope.Descendants, 
                     uiaEventHandler = new AutomationEventHandler(OnUIAutomationEvent));
                elementSubscribed = elm;
                elementEvent = evt;
            }
        }
    }
}
