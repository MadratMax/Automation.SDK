namespace Automation.Sdk.Bindings.Dto
{
    public class TextBoxValue
    {
        private readonly string _value;
        private readonly string _caption;

        public TextBoxValue(string value, string caption)
        {
            _value = value;
            _caption = caption;
        }

        public string Value => _value;

        public string Caption => _caption;
    }
}