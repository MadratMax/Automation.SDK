
namespace Automation.Sdk.UIWrappers.ControlDrivers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.Adapters;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.Container;
    using FluentAssertions;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// The element.
    /// </summary>
    public class Element : IElement
    {
        private static Element _root;

        public delegate bool UIACondition(Element element);

        private readonly AutomationElement _automationElement;

        private readonly ControlAdapterFactory _controlAdapterFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="automationElement">Original AE element</param>
        public Element(AutomationElement automationElement)
        {
            _automationElement = automationElement;

            PlatformContextType = PlatformContextType.Desktop;

            _controlAdapterFactory = ContainerProvider.Container.Resolve<ControlAdapterFactory>();
        }

        /// <summary>
        /// Constructor
        /// The current focus element
        /// </summary>
        public Element() 
            : this(AutomationElement.FocusedElement)
        {
            AutomationFacade.Logger.Write($"Create element instance from focused element: {this}");
        }

        /// <summary>
        /// Looks very strange, however this is required to support both legacy code and new approach
        /// </summary>
        public Element(IElement element) 
            : this(element.UiElement as AutomationElement)
        {
            FindCommandTitle = element.FindCommandTitle;
            AutomationFacade.Logger.Write($"Create element instance from generic element: {element.FindCommandTitle}");
        }

        public static Element Root
        {
            get
            {
                if (_root == null)
                {
                    RefreshRoot();
                }

                return _root;
            }
        }

        public static void RefreshRoot()
        {
            _root = new Element(AutomationElement.RootElement);
        }

        public void Click()
        {
            ((InvokePattern)_automationElement.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
        }

        public int ProcessId => _automationElement.Current.ProcessId;

        public static Element FocusedElement => new Element();

        public AutomationElement AutomationElement => _automationElement;

        public string AutomationId => _automationElement.Current.AutomationId;

        public Rect BoundingRectangle => _automationElement.Current.BoundingRectangle;

        public ControlType ControlType => _automationElement.Current.ControlType;

        public bool HasFocus => _automationElement.Current.HasKeyboardFocus;

        public string HelpText => _automationElement.Current.HelpText;

        public string ItemStatus => _automationElement.Current.ItemStatus;

        public TAdapter GetAdapter<TAdapter>() where TAdapter : IAdapter
        {
            return ControlAdapterFactory.Create<TAdapter>(this);
        }

        public TPattern GetPattern<TPattern>(AutomationPattern automationPattern) where TPattern : BasePattern
        {
            var pattern = (TPattern)AutomationElement.GetCurrentPattern(automationPattern);
            pattern.Should().NotBeNull();

            return pattern;
        }

        public TObject GetProperty<TObject>(AutomationProperty property, bool ignoreDefaultValue = true)
        {
            return (TObject)AutomationElement.GetCurrentPropertyValue(property, ignoreDefaultValue);
        }

        public bool IsClickable
        {
            get
            {
                System.Windows.Point pt;
                return _automationElement.TryGetClickablePoint(out pt);
            }
        }

        public System.Windows.Point Center
        {
            get
            {
                System.Windows.Point pt = new System.Windows.Point
                {
                    X = BoundingRectangle.Left + BoundingRectangle.Width / 2 + 1,
                    Y = BoundingRectangle.Top + BoundingRectangle.Height / 2 + 1
                };
                return pt;
            }
        }

        public System.Windows.Point LeftTop => new System.Windows.Point(BoundingRectangle.Left + 1, BoundingRectangle.Top + 1);

        public bool IsEnabled => _automationElement.Current.IsEnabled;

        public bool IsFocusable => _automationElement.Current.IsKeyboardFocusable;

        public bool IsOffscreen
        {
            get
            {
                try
                {
                    return _automationElement.Current.IsOffscreen;
                }
                catch (ElementNotAvailableException)
                {
                    return true;
                }
            }
        }

        public bool IsVisible => !IsOffscreen;

        public virtual bool IsSelected => GetProperty<bool>(SelectionItemPattern.IsSelectedProperty);

        public string Name => _automationElement.Current.Name;

        public bool IsAvailable
        {
            get
            {
                // Unfortunately in UIA there are no ways to check that element is still available. So we need to use this:
                try
                {
                    bool enabled = AutomationElement.Current.IsEnabled;
                }
                catch
                {
                    return false;
                }

                return true;
                // End of workaround.
            }
        }

        public Element NextSibling
        {
            get
            {
                var sibling = TreeWalker.ControlViewWalker.GetNextSibling(_automationElement);
                return sibling != null ? new Element(sibling) : null;
            }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        public Element Parent
        {
            get
            {
                var parent = TreeWalker.ControlViewWalker.GetParent(_automationElement);
                return parent != null ? new Element(parent) : null;
            }
        }

        public Element PreviousSibling
        {
            get
            {
                TreeWalker walker = TreeWalker.ControlViewWalker;
                AutomationElement sibling = walker.GetPreviousSibling(_automationElement);
                if (sibling != null)
                {
                    return new Element(sibling);
                }

                return null;
            }
        }

        protected ControlAdapterFactory ControlAdapterFactory => _controlAdapterFactory;

        /// <summary>
        /// Gets the parent of specific type.
        /// </summary>
        public T ParentContainer<T>() where T : Element
        {
            TreeWalker walker = TreeWalker.ControlViewWalker;
            AutomationElement parent = walker.GetParent(_automationElement);

            ControlType type = AutomationFacade.ControlTypeConverter.Convert<T>();

            while (parent != AutomationElement.RootElement && parent != null && parent.Current.ControlType != type)
            {
                parent = walker.GetParent(parent);
            }

            if (parent != null && (parent != AutomationElement.RootElement || type == ControlType.Window))
            {
                return Promote(parent) as T;
            }

            return null;
        }

        public T Find<T>(bool useTimeout = true, int timeout = 5000) where T : Element
        {
            return Find<T>(string.Empty, string.Empty, useTimeout, timeout);
        }

        public T Find<T>(string name, bool useTimeout = true, int timeout = 5000) where T : Element
        {
            return Find<T>(name, string.Empty, useTimeout, timeout);
        }

        public T Find<T>(string name, string automationId, bool useTimeout = true, int timeout = 5000, TreeScope scope = TreeScope.Descendants) where T : Element
        {
            ControlType type = AutomationFacade.ControlTypeConverter.Convert<T>();
            return Find(type, name, automationId, useTimeout, timeout, scope, false) as T;
        }

        public T FindVirtualizedControl<T>(string name, string automationId, bool useTimeout, int timeout, TreeScope scope) where T : Element
        {
            ControlType type = AutomationFacade.ControlTypeConverter.Convert<T>();
            return Find(type, name, automationId, useTimeout, timeout, scope, true) as T;
        }

        public List<T> FindAllByCondition<T>(UIACondition condition, bool useTimeout, int timeout = 5000, TreeScope scope = TreeScope.Children) where T : Element
        {
            return AutomationFacade.LegacyControlSearchEngineService.FindAllByCondition<T>(_automationElement, condition, useTimeout, timeout, scope);
        }

        public T FindByCondition<T>(UIACondition condition, bool useTimeout, int timeout = 5000, TreeScope scope = TreeScope.Children) where T : Element
        {
            return AutomationFacade.LegacyControlSearchEngineService.FindByCondition<T>(_automationElement, condition, useTimeout, timeout, scope);
        }

        public List<Element> GetChildren(ControlType type = null)
        {
            return AutomationFacade.LegacyControlSearchEngineService.GetChildren(_automationElement, type);
        }

        protected Element Find(ControlType type, string name, string automationId, bool useTimeout, int timeout, TreeScope scope, bool useVirtualization)
        {
            return AutomationFacade.LegacyControlSearchEngineService.Find(_automationElement, type, name, automationId, useTimeout, timeout, scope, useVirtualization);
        }

        /// <summary>
        /// Find all the children based on the the specified control type
        /// </summary>
        /// <typeparam name="T">Control Type</typeparam>
        /// <param name="scope">TreeScope</param>
        /// <param name="expectedToReturnMoreThanZero">If true, then method will wait until count of found elements will be greater than zero</param>
        /// <returns>List of children of type T</returns>
        public T[] FindAll<T>(TreeScope scope = TreeScope.Children, bool expectedToReturnMoreThanZero = true) where T : Element
        {
            return AutomationFacade.LegacyControlSearchEngineService.FindAll<T>(_automationElement, scope, expectedToReturnMoreThanZero);
        }

        public T FindAdvanced<T>(string automationId) where T : Element
        {
            return WalkFind(this, automationId) as T;
        }

        public T FindAdvanced<T>() where T : Element
        {
            return WalkFind<T>(this);
        }

        protected Element WalkFind(Element rootElement, string automationId)
        {
            AutomationElement elementNode = TreeWalker.RawViewWalker.GetFirstChild(rootElement.AutomationElement);

            while (elementNode != null)
            {
                if (automationId == elementNode.Current.AutomationId)
                {
                    return Promote(elementNode);
                }

                elementNode = TreeWalker.RawViewWalker.GetNextSibling(elementNode);
            }

            return null;
        }

        private T WalkFind<T>(Element rootElement) where T : Element
        {
            AutomationElement elementNode = TreeWalker.RawViewWalker.GetFirstChild(rootElement.AutomationElement);

            ControlType type = AutomationFacade.ControlTypeConverter.Convert<T>();

            while (elementNode != null)
            {
                if (elementNode.Current.ControlType.Equals(type))
                {
                    return Promote(elementNode) as T;
                }

                elementNode = TreeWalker.RawViewWalker.GetNextSibling(elementNode);
            }

            return default(T);
        }

        /// <summary>
        /// To promote a AutomationElement to a certain instance of a class, in order to get more specific methods.
        /// </summary>
        private Element Promote(AutomationElement automationElement)
        {
            return AutomationFacade.ControlTypeConverterService.Convert(automationElement);
        }

        /// <summary>
        /// Takes a screenshot of the current element
        /// </summary>
        public Bitmap CaptureImage()
        {
            int width = Convert.ToInt32(AutomationElement.Current.BoundingRectangle.Width);
            int height = Convert.ToInt32(AutomationElement.Current.BoundingRectangle.Height);
            int startX = Convert.ToInt32(AutomationElement.Current.BoundingRectangle.X);
            int startY = Convert.ToInt32(AutomationElement.Current.BoundingRectangle.Y);
            Bitmap bmp = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(startX, startY, 0, 0, new System.Drawing.Size(width, height));
            }

            return bmp;
        }

        /// <summary>
        /// Click on Self, in center point of BoundingRectangle
        /// </summary>
        public void ClickOn(MouseButton b = MouseButton.Left)
        {
            CheckElementAndMoveMouseOverIt();
            Mouse.Click(b);
            ////need to slow it down before the click
            ////500ms is too large for the click delay. Overall downgrade of performance is about 20 minutes.
            Thread.Sleep(100);
        }

        /// <summary>
        /// The double click on Self
        /// </summary>
        public void DoubleClickOn(MouseButton b = MouseButton.Left)
        {
            CheckElementAndMoveMouseOverIt();

            // Mouse .DoubleClick(b); 
            // WPF not fast enough to process programmatical double-clicks
            Mouse.Click(b);
            Thread.Sleep(100);
            Mouse.Click(b);
        }

        /// <summary>
        /// Click on child element of this element
        /// </summary>
        /// <typeparam name="T">UIA control type</typeparam>
        /// <param name="automationID">AutomationID to click</param>
        /// <param name="button">mouse button to click</param>
        /// <returns>True if element is found, false if not</returns>
        public bool ClickOn<T>(string automationID, MouseButton button = MouseButton.Left) where T : Element
        {
            return ClickOn(Find<T>(string.Empty, automationID), button);
        }

        /// <summary>
        /// Click on child element of this element
        /// </summary>
        /// <typeparam name="T">UIA control type</typeparam>
        /// <param name="name">Name to click</param>
        /// <param name="button">mouse button to click</param>
        /// <returns>True if element is found, false if not</returns>
        public bool ClickOnByName<T>(string name, MouseButton button = MouseButton.Left) where T : Element
        {
            return ClickOn(Find<T>(name, string.Empty), button);
        }

        private bool ClickOn(Element e, MouseButton button = MouseButton.Left)
        {
            // TODO: Is this method quietness legit?
            if (e == null)
            {
                return false;
            }

            Methods.WaitUntil(() => e.IsVisible, 10000);
            Methods.WaitUntil(() => e.IsEnabled, 10000);
            if (!e.IsEnabled || !e.IsVisible)
            {
                return false;
            }

            e.ClickOn(button);
            return true;
        }

        /// <summary>
        /// Get the clickable point for the control
        /// </summary>
        /// <returns>x,y coordinate of the control, otherwise 0,0</returns>
        public virtual System.Windows.Point GetClickablePoint()
        {
            return Center;
        }

        /// <summary>
        /// The set focus.
        /// </summary>
        public void SetFocus()
        {
            // TODO: Is this method quietness legit?
            if (IsFocusable)
            {
                _automationElement.SetFocus();
            }

            Thread.Sleep(50);
        }

        protected void CheckElementAndMoveMouseOverIt()
        {
            this.ShouldBecome(x => x.IsVisible, true, $"Element {this} should be visible.");

            var clickablePoint = GetClickablePoint();
            var moveResult = Mouse.MoveTo(clickablePoint);

            if (AutomationFacade.SdkConfiguration.UseWin10MouseWorkaround)
            {
                Mouse.MoveMouseAround(1, 1);
            }

            moveResult.Should().BeTrue($"Move should be moved to position: {clickablePoint.X}:{clickablePoint.Y}");
        }

        public override string ToString()
        {
            try
            {
                if (_automationElement == AutomationElement.RootElement)
                {
                    return "Root Desktop Pane";
                }

                if (FindCommandTitle != null)
                {
                    return FindCommandTitle;
                }

                return $"{ControlType.ProgrammaticName}: AID={AutomationId}; Name={Name}; ItemStatus={ItemStatus};";
            }
            catch
            {
                return base.ToString();
            }
        }

        public object UiElement => _automationElement;

        public string FindCommandTitle { get; }

        public PlatformContextType PlatformContextType { get; }

        public bool IsContainsElement => _automationElement != null;
    }
}