namespace Automation.Sdk.UIWrappers.Services.SearchEngines.VPath.Cache
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;

    internal class VPathCache
    {
        private Dictionary<string, AutomationElement> values;

        public VPathCache()
        {
            values = new Dictionary<string, AutomationElement>();
        }

        public bool Contains(string step)
        {
            return values.Any(x => x.Key.Equals(step));
        }

        public AutomationElement this[string key]
        {
            get
            {
                return values.First(x => x.Key.Equals(key)).Value;
            }
        }

        private string GetFullKey(string key)
        {
            if (values.Count > 0)
            { 
                return string.Join("=>", values.Last().Key, key);
            }

            return key;
        }

        public void Add(string key, AutomationElement element)
        {
            var fullKey = GetFullKey(key);
            values.Add(fullKey, element);
        }
    }
}