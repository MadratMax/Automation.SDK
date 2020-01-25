namespace Automation.Sdk.UIWrappers.Adapters.Web
{
    using System.Threading;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.Container;
    using JetBrains.Annotations;
    using Services.Keyboard;
    using OpenQA.Selenium.Interactions;

    [UsedImplicitly]
    [AutoRegister(Interface = typeof(IClickAdapter), RegistrationType = RegistrationType.Instanced, RegistrationName = PlatformContextType.Web)]
    internal class WebClickAdapter : WebAdapterBase, IClickAdapter
    {
        private readonly IModifierKeysStateHolder _modifierKeysStateHolder;
        private readonly IWebDriverContainer _webDriverContainer;

        public WebClickAdapter(
            IElement element,
            ControlAdapterFactory controlAdapterFactory,
            IModifierKeysStateHolder modifierKeysStateHolder,
            IWebDriverContainer webDriverContainer)
            : base(element, controlAdapterFactory)
        {
            _modifierKeysStateHolder = modifierKeysStateHolder;
            _webDriverContainer = webDriverContainer;
        }

        public void Click()
        {
            if (_modifierKeysStateHolder.PressedKeys.Count == 0)
            {
                WebElement.Click();
            }
            else
            {
                var actionChain = new Actions(_webDriverContainer.WebDriver);
                
                foreach (var key in _modifierKeysStateHolder.PressedKeys)
                {
                    actionChain = actionChain.KeyDown(key);
                }

                actionChain = actionChain.Click(WebElement);

                foreach (var key in _modifierKeysStateHolder.PressedKeys)
                {
                    actionChain = actionChain.KeyUp(key);
                }

                actionChain.Perform();
            }
        }

        public void DoubleClickOnSelf(MouseButton button = MouseButton.Left)
        {
            Click();
            Thread.Sleep(50);
            Click();
        }

        public bool IsClickable => WebElement.Displayed;

        public void ClickOnSelf(MouseButton button = MouseButton.Left)
        {
            Click();
        }
    }
}