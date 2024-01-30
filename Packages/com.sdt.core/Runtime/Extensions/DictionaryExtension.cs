using System.Collections.Generic;
using UnityEngine;

namespace SDTCore
{
    public static class DictionaryExtension
    {
        /// <summary>
        /// Retrieves the value associated with the specified key in the dictionary, or returns a default value if the key is not found or the retrieved value cannot be cast to type T.
        /// </summary>
        /// <typeparam name="T">The type to which the retrieved value should be cast.</typeparam>
        /// <param name="dictionary">The dictionary from which to retrieve the value.</param>
        /// <param name="key">The key whose associated value should be retrieved.</param>
        /// <param name="otherwise">The default value to return if the key is not found or the retrieved value cannot be cast to type T.</param>
        /// <returns>The value associated with the specified key if found and successfully cast to type T; otherwise, it returns the specified default value.</returns>
        public static T GetValueOr<T>(this Dictionary<object, object> dictionary, object key, T otherwise)
        {
            dictionary.TryGetValue(key, out var value);

            if (value == null)
                return otherwise;

            return value is T castedValue ? castedValue : otherwise;
        }

        /// <summary>
        /// Safely swaps the values associated with two keys in the dictionary, providing specific error messages if either key is not present. 
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dic">The dictionary in which the key values should be swapped.</param>
        /// <param name="fromKey">The key whose value should be swapped with the value associated with toKey.</param>
        /// <param name="toKey">The key whose value should be swapped with the value associated with fromKey.</param>
        public static void ReplaceKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey)
        {
            if (!dic.ContainsKey(fromKey) || !dic.ContainsKey(toKey))
            {
                if (!dic.ContainsKey(fromKey)) 
                    Debug.LogError($"FromKey {fromKey} doesn't exist in the dictionary.");
                if (!dic.ContainsKey(toKey)) 
                    Debug.LogError($"ToKey {toKey} doesn't exist in the dictionary.");
                return;
            }
            (dic[fromKey], dic[toKey]) = (dic[toKey], dic[fromKey]);
        }

        /// <summary>
        /// Safely adds an item to a collection associated with a specified key in the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TItem">The type of items in the collections.</typeparam>
        /// <param name="dic">The dictionary in which the item should be added.</param>
        /// <param name="key">The key indicating the collection to which the item should be added or replaced.</param>
        /// <param name="item">The item to be added to the collection.</param>
        public static void SafeAddToCollectionValue<TKey, TItem>(this IDictionary<TKey, IEnumerable<TItem>> dic, TKey key, TItem item)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = item as IEnumerable<TItem>;
                return;
            }
            dic.Add(key, item as IEnumerable<TItem>);
        }
    }
}