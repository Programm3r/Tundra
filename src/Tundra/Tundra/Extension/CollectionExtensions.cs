using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Tundra.Extension
{
    /// <summary>
    /// Collection Extensions Class
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// To the observable collection.
        /// </summary>
        /// <typeparam name="T">The type of objects to observe. This type parameter is covariant.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            var c = new ObservableCollection<T>();
            foreach (var e in collection)
                c.Add(e);
            return c;
        }

        /// <summary>
        /// Executes the given action against the given ICollection instance.
        /// </summary>
        /// <typeparam name="T">The type of the ICollection parameter.</typeparam>
        /// <param name="items">The collection the action is performed against.</param>
        /// <param name="action">The action that is performed on each item.</param>
        public static void Each<T>(this ICollection<T> items, Action<T> action)
        {
            foreach (T item in items)
            {
                action(item);
            }
        }

        /// <summary>
        /// Determines whether a parameter is in a given list of parameters.
        /// E.g.. 11.In(1,2,3) will return false.
        /// </summary>
        /// <typeparam name="T">The type of the source parameter.</typeparam>
        /// <param name="source">The item that needs to be checked.</param>
        /// <param name="list">The list that will be checked for the given source.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">source</exception>
        public static bool In<T>(this T source, params T[] list)
        {
            if (source == null) 
                throw new ArgumentNullException("source");
            return list.Contains(source);
        }

        /// <summary>
        /// Determines whether the specified collection has any elements in the sequence.
        /// This method also checks for a null collection.
        /// </summary>
        /// <param name="items">The ICollection of items to check.</param>
        public static bool HasElements(this ICollection items)
        {
            return items != null && items.Count > 0;
        }

        /// <summary>
        /// Iterate over the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration">The enumeration.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
            {
                action(item);
            }
        }
    }
}
