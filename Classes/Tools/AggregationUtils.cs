using System;
using System.Collections.Generic;

namespace Deadblock.Tools
{
    public class AggregationUtils
    {
        /// <summary>
        /// Handles complex conversion to different types.
        /// Works with strings and activates a new instance
        /// with the passed value payload.
        /// Handles specific config types,
        /// such as "key: keyValue", and enum.
        /// </summary>
        /// <param name="aType">
        /// Targeted type, used in activation algorithm.
        /// </param>
        /// <param name="aValue">
        /// Targeted value.
        /// </param>
        /// <returns>
        /// Converted value, represented
        /// as universal object type.
        /// </returns>
        public static object ImpersonateConfigTypeForValue(Type aType, string aValue)
        {
            if (aType.IsEnum)
                return Enum.Parse(aType, aValue);

            // Handle one-line dictionary
            if (aType == typeof(Dictionary<string, string>))
            {
                if (!aValue.Contains('>'))
                {
                    throw new AggregateException("Specified value contains no valid separator. The parts should be separated with '>'. For example, Default > Earth. Contact DEV.");
                }

                return NativeUtils.SplitStringIntoDict(aValue, '>');
            }

            // Simple Generic Type
            return Convert.ChangeType(aValue, aType);
        }

        /// <summary>
        /// Creates a new instance of the specified generic type,
        /// and ASSIGNS (!) every value from the passed dictionary to it.
        /// Takes in the account the specifics of the config file,
        /// and parses it purely according to the standard.
        /// </summary>
        /// <param name="aDictionary">
        /// Source dictionary.
        /// Should have the same properties as the targeted class,
        /// otherwise an error will occure.
        /// All properties from the dictionary
        /// will be converted to the requested by class spec types.
        /// </param>
        /// <returns>
        /// Instance of the created type with
        /// all pushed attributes from the passed dictionary.
        /// </returns>
        public static T CreateInstanceFromConfigBlock<T>(Dictionary<string, string> aDictionary)
        {
            var immediateInstance = (T)Activator.CreateInstance(typeof(T));

            foreach (KeyValuePair<string, string> pair in aDictionary)
            {
                var textureType = immediateInstance.GetType();
                var textureProp = textureType.GetProperty(pair.Key);

                if (textureProp == null)
                {
                    throw new AggregateException($"Could not load create instance from the passed dictionary, as it contains an unexpected key: {pair.Key}. Contact DEV.");
                }

                object typedValue = ImpersonateConfigTypeForValue(textureProp.PropertyType, pair.Value);
                textureProp.SetValue(immediateInstance, typedValue, null);
            }

            return immediateInstance;
        }
    }
}
