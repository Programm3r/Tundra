using System;
using System.Collections.Generic;
using System.IO;
using Tundra.Enum;

namespace Tundra.Extension
{
    /// <summary>
    /// Extension Helper Class
    /// </summary>
    public static class ExtensionHelper
    {
        /// <summary>
        /// Applies the specified a.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="a">A.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        public static B Apply<A, B>(this A a, System.Func<A, B> func)
        {
            return func(a);
        }

        /// <summary>
        /// Determines whether a value is between a minimum and maximum value.
        /// </summary>
        /// <typeparam name="T">The type of the value parameter.</typeparam>
        /// <param name="value">The value that needs to be checked.</param>
        /// <param name="low">The inclusive lower boundary.</param>
        /// <param name="high">The inclusive upper boundary.</param>
        public static bool IsBetween<T>(this T value, T low, T high) where T : IComparable<T>
        {
            return value.CompareTo(low) >= 0 && value.CompareTo(high) <= 0;
        }

        /// <summary>
        /// Splits the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="partitionBy">The partition by.</param>
        /// <param name="removeEmptyEntries">if set to <c>true</c> [remove empty entries].</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<TSource>> Split<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> partitionBy, bool removeEmptyEntries = false, int count = -1)
        {
            var yielded = 0;
            var items = new List<TSource>();
            foreach (var item in source)
            {
                if (!partitionBy(item))
                    items.Add(item);
                else if (!removeEmptyEntries || items.Count > 0)
                {
                    yield return items.ToArray();
                    items.Clear();

                    if (count > 0 && ++yielded == count) yield break;
                }
            }

            if (items.Count > 0) yield return items.ToArray();
        }
    }
}
