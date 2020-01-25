namespace Automation.Sdk.Bindings.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Disposables;
    using Automation.Sdk.Bindings.Dto;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using Automation.Sdk.UIWrappers.Services.ResourcesServices;
    using FluentAssertions;
    using Microsoft.Test.Input;
    using TechTalk.SpecFlow;
    using UIWrappers.Services.Keyboard;

    /// <summary>
    /// Steps for Keyboard pressing and releasing
    /// </summary>
    [Binding]
    public sealed class KeyboardBindings
    {
        private readonly ResourceConsumer _resourceConsumer;
        private readonly ILogger _logger;
        private readonly IModifierKeysStateHolder _modifierKeysStateHolder;

        public KeyboardBindings(
            ILogger logger,
            ResourceConsumer resourceConsumer,
            IModifierKeysStateHolder modifierKeysStateHolder)
        {
            _logger = logger;
            _resourceConsumer = resourceConsumer;
            _modifierKeysStateHolder = modifierKeysStateHolder;
        }

        [When(@"user hits ""(.*)"" key")]
        public void HitKey(Key key)
        {
            Keyboard.Type(key);
            _modifierKeysStateHolder.ReleaseKey(key);
        }

        [When(@"user hits shortcut ""(.*)""")]
        public void HitShortcut(ShortcutCommand shortcutCommand)
        {
            PressKeys(shortcutCommand);
            ReleaseKeys(shortcutCommand);
        }

        [When(@"user presses ""(.*)"" keys")]
        public void PressKeys(ShortcutCommand shortcutCommand)
        {
            foreach (var key in shortcutCommand.Keys)
            {
                Keyboard.Press(key);
                _modifierKeysStateHolder.PressKey(key);
                _logger.Write($"Pressed key '{key}'");
            }

            _resourceConsumer.Consume(Disposable.Create(Keyboard.Reset));
        }

        [When(@"user releases ""(.*)"" keys")]
        public void ReleaseKeys(ShortcutCommand shortcutCommand)
        {
            foreach (var key in shortcutCommand.Keys)
            {
                Keyboard.Release(key);
                _modifierKeysStateHolder.ReleaseKey(key);
                _logger.Write($"Released key '{key}'");
            }
        }

        [StepArgumentTransformation(@"(.*)")]
        public ShortcutCommand GetShortcutCommand(string shortcutStringCommand)
        {
            var chunks = shortcutStringCommand.Split('+');
            var keys = new List<Key>();

            foreach (var chunk in chunks)
            {
                Key key;
                Enum.TryParse(chunk, true, out key).Should().BeTrue($"keys in bindings should be valid. No such key {chunk}");

                keys.Add(key);
            }

            return new ShortcutCommand(keys.ToArray());
        }
    }
}