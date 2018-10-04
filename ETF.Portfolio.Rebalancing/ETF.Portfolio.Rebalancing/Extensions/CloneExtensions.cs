using System;
using System.Collections.Generic;
using System.Linq;

namespace ETF.Portfolio.Rebalancing.Extensions
{
    static class CloneExtensions
    {
        /// <summary>
        /// Clone an existing List and each of it's clonable items
        /// </summary>
        /// <typeparam name="T">The cloneable type of each item</typeparam>
        /// <param name="listToClone">The list to be cloned</param>
        /// <returns>The cloned list</returns>
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        /// <summary>
        /// Clone an existing dictionary and each of it's clonable items
        /// </summary>
        /// <typeparam name="TKey">The type of the key of the dictionary to be cloed</typeparam>
        /// <typeparam name="TValue">The cloneable type of each item</typeparam>
        /// <param name="original">The dictionary to be cloned</param>
        /// <returns>The cloned dictionary</returns>
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count, original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue)entry.Value.Clone());
            }
            return ret;
        }
    }
}
