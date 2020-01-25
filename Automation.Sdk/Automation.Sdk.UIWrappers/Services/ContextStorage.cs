
namespace Automation.Sdk.UIWrappers.Services
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Class for store and read runtime test data
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This code is self-explanatory")]
    public class ContextStorage
    {
        private static Dictionary<string, object> _dictionary;
        private static Dictionary<string, object> _special;

        // Check if running under SpecFlow and if yes use the scenario dictionary. Otherwise, use our own 
        // dictionary in GeneralUISteps.
        public static Dictionary<string, object> Current
        {
            get
            {
                if (IsScenarioContextExists())
                {
                    return ScenarioContext.Current;
                }

                if (_dictionary == null)
                {
                    _dictionary = new Dictionary<string, object>();
                }

                return _dictionary;
            }
        }

        public static Dictionary<string, object> Special
        {
            get
            {
                if (_special == null)
                {
                    _special = new Dictionary<string, object>();
                }

                return _special;
            }
        }

        public static bool IsScenarioContextExists()
        {
            if (ScenarioContext.Current == null)
            {
                return false;
            }

            return true;
        }

        public static bool IsObjectExist(string key)
        {
            return Current.ContainsKey(key);
        }

        public static void Clear()
        {
            if (_dictionary != null)
            {
                _dictionary.Clear();
                _dictionary = null;
            }

            if (ScenarioContext.Current != null)
            {
                ScenarioContext.Current.Clear();
            }
        }

        public static void ClearSpecialValues()
        {
            _special?.Clear();
        }

        public static bool TryGetValue<TValue>(string key, out TValue outValue)
        {
            object value = null;
            bool result = false;

            result = Current.TryGetValue(key, out value);

            if (value != null)
            { 
                outValue = (TValue)value;
            }
            else
            {
                outValue = default(TValue);
            }

            return result;
        }

        public static bool TryGetSpecialValue<TValue>(string key, out TValue outValue)
        {
            object value = null;
            bool result = false;

            result = Special.TryGetValue(key, out value);

            if (value != null)
            {
                outValue = (TValue)value;
            }
            else
            {
                outValue = default(TValue);
            }

            return result;
        }

        /// <summary>
        /// Adds to selected Dictionary the key value pair.
        /// </summary>
        /// <param name="dict">Dictionary object</param>
        /// <param name="key">Name to store the object</param>
        /// <param name="value">Object to be stored</param>
        public static void AddToDictionary(Dictionary<string, object> dict, string key, object value)
        {
            if (dict.ContainsKey(key))
            {
                dict.Remove(key);
            }

            if (value == null)
            { 
                return;
            }

            dict.Add(key, value);
        }

        /// <summary>
        /// Remove selected Dictionary the key value pair.
        /// </summary>
        /// <param name="dict">Dictionary object</param>
        /// <param name="key">Name to store the object</param>
        public static void RemoveFromDictionary(Dictionary<string, object> dict, string key)
        {
            if (dict.ContainsKey(key))
            {
                dict.Remove(key);
            }
        }

        /// <summary>
        /// Cache an object for use in subsequent steps.
        /// </summary>
        /// <param name="key">Object key</param>
        /// <param name="value">Object to cache</param>
        /// <remarks>
        /// If running under SpecFlow, the ScenarioContext dictionary will be used for the cache and is automatically 
        /// disposed of at the end of the scenario. Otherwise, the GeneralUISteps.Current dictionary is used for the 
        /// cache and should be cleared with ClearCache() at the end of the test.
        /// </remarks>
        public static void CacheObject(string key, object value)
        {
            Dictionary<string, object> cache = Current;

            if (cache.ContainsKey(key))
            {
                cache.Remove(key);
            }

            // Only add the object if not null.
            if (value != null)
            {
                cache.Add(key, value);
            }
        }

        /// <summary>
        /// Add an object for use in subsequent steps.
        /// </summary>
        /// <param name="key">Object key</param>
        /// <param name="value">Object to cache</param>
        /// <remarks>
        /// If running under SpecFlow, the ScenarioContext dictionary will be used for the cache and is automatically 
        /// disposed of at the end of the scenario. Otherwise, the GeneralUISteps.Current dictionary is used for the 
        /// cache and should be cleared with ClearCache() at the end of the test.
        /// </remarks>
        public static void Add(string key, object value)
        {
            AddToDictionary(Current, key, value);
        }

        /// <summary>
        /// Remove an object.
        /// </summary>
        /// <param name="key">Object key</param>
        public static void Remove(string key)
        {
            RemoveFromDictionary(Current, key);
        }

        public static void AddSpecialValue(string key, object value)
        {
            AddToDictionary(Special, key, value);
        }
    }
}
