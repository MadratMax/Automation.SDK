namespace Automation.Sdk.UIWrappers.Services.ControlConversion
{
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;

    public class ControlTypeConverterService
    {
        private readonly ControlTypeConverter _controlTypeConverter;

        public ControlTypeConverterService(ControlTypeConverter controlTypeConverter)
        {
            _controlTypeConverter = controlTypeConverter;
        }

        public Element Convert(AutomationElement element)
        {
            return _controlTypeConverter.Create(element);
        }

        public T[] Convert<T>(AutomationElementCollection elementCollection) where T : Element
        {
            T[] convertedCollection = new T[elementCollection.Count];
            for (int i = 0; i < elementCollection.Count; i++)
            {
                convertedCollection[i] = Convert(elementCollection[i]) as T;
            }

            return convertedCollection;
        }
    }
}