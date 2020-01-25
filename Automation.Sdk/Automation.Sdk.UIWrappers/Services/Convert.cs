
namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Provides functions to convert any values
    /// </summary>
    public static class ConvertFromTo
    {
        /// <summary>
        /// Convert from String to Int
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>Integers array found in the input</returns>
        public static int[] GetIntFromString(string input)
        {
            List<int> intList = new List<int>();

            string[] numbers = Regex.Split(input, @"\D+");
            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    intList.Add(i);
                }
            }

            return intList.ToArray();
        }

        /// <summary>
        /// This function removes restricted chars
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>string without restricted chars</returns>
        public static string RemoveRestrictedChars(string input)
        {
            char[] symbols = { ':', '*', '|', '/', '\\', '?', '"', '<', '>' };
            foreach (char symbol in symbols)
            { 
                input = input.Replace(symbol, ' ');
            }

            return input;
        }

        /// <summary>
        /// Converts string input to a given item in the given enum
        /// </summary>
        /// <typeparam name="T">Enum to parse into</typeparam>
        /// <param name="value">Value to parse into enum</param>
        /// <returns>Enum value</returns>
        public static T StringToEnum<T>(string value) where T : struct, IConvertible
        {
            Type type = typeof(T);

            if (!type.IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return (T)Enum.Parse(type, value, true);
        }
    }
}