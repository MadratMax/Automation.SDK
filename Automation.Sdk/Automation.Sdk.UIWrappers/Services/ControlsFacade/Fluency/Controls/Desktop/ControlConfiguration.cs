namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop
{
    using System;
    using JetBrains.Annotations;

    /// <summary>
    /// Whole control configuration, including caption, automation Id and so on
    /// </summary>
    public sealed class ControlConfiguration : IControlConfiguration
    {
        private readonly FluentConfiguration _configuration;
        private readonly IControlMapper _map;

        private readonly string _caption;
        private readonly string _automationPropertyValue;
        private readonly AutomationSearchBehavior _automationSearchBehavior;

        private ControlConfiguration _parent;

        public ControlConfiguration(
            [NotNull] FluentConfiguration configuration, 
            [NotNull] IControlMapper map,
            [NotNull] string caption,
            [NotNull] string automationPropertyValue,
            AutomationSearchBehavior automationSearchBehavior)
        {
            _configuration = configuration;
            _map = map;

            _automationSearchBehavior = automationSearchBehavior;
            _caption = caption;
            _automationPropertyValue = automationPropertyValue;

            // Adding currently created control configuration to controls map.
            // In general, it is a code smell to pass "this" as a method parameter,
            // but here it is totally ok because current type responsibility is to 
            // cover mediator functions between configuration setup and control search 
            // components.
            _map.Add(this);
        }

        public string Caption => _caption;

        public string AutomationPropertyValue => _automationPropertyValue;

        public ControlConfiguration Parent => _parent;

        public AutomationSearchBehavior AutomationSearchBehavior => _automationSearchBehavior;

        /// <inheritdoc />
        public IncompleteControlConfiguration Add(string controlCaption)
        {
            return _configuration.Add(controlCaption);
        }

        public IDisposable DefineScope()
        {
            return _map.ConsumeParent(this);
        }

        /// <summary>
        /// Build complete control search command from configured parameters
        /// </summary>
        /// <param name="elementName">Name of the element. Used only for log purposes</param>
        /// <returns>command</returns>
        public ControlFindCommand Build(string elementName)
        {
            if (_automationSearchBehavior != AutomationSearchBehavior.ByVPath)
            {
                throw new NotImplementedException("Update here is new SearchBehavior will be implemented.");
            }

            return new ControlFindCommand(elementName, _parent, _automationPropertyValue);
        }

        public void AddParent(ControlConfiguration parent)
        {
            _parent = parent;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            // !!!!!!!!!!!!!!!!! DO NOT REMOVE !!!!!!!!!!!!!!!!
            // !!!! USED IN UNIT TESTS FOR MOCK COMPARISON !!!!

            // If parameter cannot be cast to ControlConfiguration return false.
            ControlConfiguration p = obj as ControlConfiguration;
            if (p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Caption == p.Caption)
                && (AutomationPropertyValue == p.AutomationPropertyValue)
                && (AutomationSearchBehavior == p.AutomationSearchBehavior);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            // !!!!!!!!!!!!!!!!! DO NOT REMOVE !!!!!!!!!!!!!!!!
            // !!!! USED IN UNIT TESTS FOR MOCK COMPARISON !!!!

            // Common ReSharper-recommended GetHashCode implementation
            // https://www.jetbrains.com/help/resharper/2016.2/Code_Generation__Equality_Members.html
            unchecked
            {
                return (_caption.GetHashCode() * 397) ^ AutomationPropertyValue.GetHashCode() ^ AutomationSearchBehavior.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $@"""{_caption}"" for {_automationPropertyValue} after ""{(_parent != null ? _parent.Caption : "ROOT")}""";
        }
    }
}