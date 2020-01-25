namespace Automation.Sdk.Bindings.Steps.Behavior
{
    using System.Collections.Generic;
    using Automation.Sdk.Bindings.Dto;
    using Automation.Sdk.UIWrappers.Adapters;
    using Automation.Sdk.UIWrappers.Enums;
    using Automation.Sdk.UIWrappers.Services.Adapters;
    using Automation.Sdk.UIWrappers.Services.ControlsFacade;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using FluentAssertions;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Steps for all Keyboard input behavior
    /// </summary>
    [Binding]
    public sealed class KeyboardInputBehaviorBindings : BehaviorBindings<IKeyboardInputAdapter>
    {
        private readonly ILogger _logger;

        public KeyboardInputBehaviorBindings(
            [NotNull] ControlFacade controlFacade, 
            [NotNull] ControlAdapterFactory controlAdapterFactory,
            [NotNull] ILogger logger)
            : base(controlFacade, controlAdapterFactory)
        {
            _logger = logger;
        }

        [When(@"user enters value ""(.*)"" into (.*)")]
        [UsedImplicitly]
        public void SetValue([NotNull] string value, [CanBeNull] IKeyboardInputAdapter keyboardInputAdapter)
        {
            CheckAdapterForNull(keyboardInputAdapter);

            TypeValue(value, keyboardInputAdapter);

            // Commit input
            Keyboard.Hit(Key.Enter);
        }

        [When(@"user types value ""(.*)"" into (.*)")]
        [UsedImplicitly]
        public void TypeValue([NotNull] string value, [CanBeNull] IKeyboardInputAdapter keyboardInputAdapter)
        {
            CheckAdapterForNull(keyboardInputAdapter);

            ClearValue(keyboardInputAdapter);
            SetFocusToControl(keyboardInputAdapter);

            keyboardInputAdapter.Type(value);
        }

        [When(@"user pastes value ""(.*)"" into (.*)")]
        [UsedImplicitly]
        public void PasteValue([NotNull] string value, [CanBeNull] IKeyboardInputAdapter keyboardInputAdapter)
        {
            CheckAdapterForNull(keyboardInputAdapter);

            ClearValue(keyboardInputAdapter);
            SetFocusToControl(keyboardInputAdapter);

            keyboardInputAdapter.Paste(value);
        }

        [When(@"user enters the following values to controls:")]
        [UsedImplicitly]
        public void SetMultipleValues([NotNull, ItemNotNull] List<ControlValue> values)
        {
            foreach (var controlValue in values)
            {
                When($@"user enters value ""{controlValue.Value}"" into {controlValue.Control}");
            }
        }

        private void SetFocusToControl([NotNull] IKeyboardInputAdapter keyboardInputAdapter)
        {
            var focusAdapter = keyboardInputAdapter.GetAdapter<IFocusAdapter>();
            focusAdapter.SetFocus();

            _logger.Write($"Focus was set to {keyboardInputAdapter}");
        }

        private void ClearValue([NotNull] IKeyboardInputAdapter keyboardInputAdapter)
        {
            var valueAdapter = keyboardInputAdapter.GetAdapter<IValueAdapter>();
            valueAdapter.SetValue(string.Empty);

            _logger.Write($@"Value of element ""{keyboardInputAdapter}"" cleared");
        }
    }
}