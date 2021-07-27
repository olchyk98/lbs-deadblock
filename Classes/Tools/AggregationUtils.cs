using System;
using System.Collections.Generic;
using System.Linq;

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

                return SplitStringIntoDict(aValue, '>');
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

        /// <summary>
        /// Spits a string into a dictionary.
        /// For Example:
        ///
        /// "variant: super;" with splitter ':' and anEnd ';'
        /// will be parsed into { variant: super }
        /// </summary>
        /// <param name="aTarget">
        /// String that's going to be deconstructed.
        /// </param>
        /// <param name="aSplitter">
        /// Split char (:)
        /// Example: hello: world.
        /// </param>
        /// <param name="anEnd">
        /// End char (;)
        /// Example: hello: world; hello2: there;
        /// </param>
        public static Dictionary<string, string> SplitStringIntoDict(string aTarget, char aSplitter = ':', char anEnd = ';')
        {
            var tempStorage = new Dictionary<string, string>();
            var entries = aTarget.Split(anEnd).Select((f) => f.Trim());

            foreach (string entry in entries)
            {
                if (string.IsNullOrEmpty(entry)) continue;

                string[] pair = entry.Trim().Split(aSplitter).Select((f) => f.Trim()).ToArray();
                if (pair.Length != 2)
                {
                    throw new AggregateException($"Could not split pair, as it contains more than one separator: { entry }");
                }

                tempStorage.Add(pair[0], pair[1]);
            }

            return tempStorage;
        }
    }
}
