using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace INetApp.Extensions
{
    /// <summary>
    /// IE numerable extensions.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="e">E.</param>
        /// <param name="index">Index.</param>
        public static object GetItem(this IEnumerable e, int index)
        {
            var enumerator = e.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext())
            {
                if (i == index)
                    return enumerator.Current;
                i++;
            }
            return null;
        }

        /// <summary>
        /// Safes the item.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="list">List.</param>
        /// <param name="index">Index.</param>
        /// <param name="safe">Safe.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T SafeItem<T>(this IEnumerable<T> list, int index, T safe)
        {
            if (list.GetItem(index) is T result)
                return result;
            else
                return safe;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <returns>The count.</returns>
        /// <param name="e">E.</param>
        public static int GetCount(this IEnumerable e)
        {
            var enumerator = e.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext())
            {
                i++;
            }
            return i;
        }

        /// <summary>
        /// Safes the cast.
        /// </summary>
        /// <returns>The cast.</returns>
        /// <param name="list">List.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static IEnumerable<T> SafeCast<T>(this object[] list)
        {
            var result = new List<T>();

            if (!list.IsNullOrNotElements())
            {
                foreach (var obj in list)
                {
                    if (obj is T val)
                    {
                        result.Add(val);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="e">E.</param>
        public static List<object> GetList(this IEnumerable e)
        {
            var enumerator = e.GetEnumerator();
            var list = new List<object>();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Current);
            }
            return list;
        }

        /// <summary>
        /// Adds all.
        /// </summary>
        /// <param name="e">list</param>
        /// <param name="vals">list to insert</param>
        /// <param name="clear">If set to <c>true</c> clear list.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void AddAll<T>(this IList<T> e, IEnumerable<T> vals, bool clear = false)
        {
            if (vals != null && e != null)
            {
                if (clear)
                    e.Clear();

                foreach (var item in vals)
                    e.Add(item);
            }
        }

        /// <summary>
        /// Indexs the of.
        /// </summary>
        /// <returns>The of.</returns>
        /// <param name="source">Source.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static int IndexOf<T>(this IEnumerable<T> source, T value)
        {
            int index = 0;
            var comparer = EqualityComparer<T>.Default; // or pass in as a parameter
            foreach (T item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Indexs the of.
        /// </summary>
        /// <returns>The of.</returns>
        /// <param name="source">Source.</param>
        /// <param name="value">Value.</param>
        public static int IndexOf(this IEnumerable source, object value)
        {
            if (source != null && value != null)
            {
                int index = 0;
                var comparer = EqualityComparer<object>.Default; // or pass in as a parameter
                foreach (var item in source)
                {
                    if (comparer.Equals(item, value)) return index;
                    index++;
                }
            }
            return -1;
        }

        /// <summary>
        /// Splits for NC haracters.
        /// </summary>
        /// <returns>The for NC haracters.</returns>
        /// <param name="str">String.</param>
        /// <param name="num">Number.</param>
        public static IEnumerable<string> SplitForNCharacters(this string str, double num)
        {
            int k = 0;
            var output = str.ToCharArray()
                            .ToLookup(c => Math.Floor(k++ / num))
                            .Select(e => new String(e.ToArray()));

            return output;
        }

        /// <summary>
        /// Ises the null or not elements.
        /// </summary>
        /// <returns><c>true</c>, if null or not elements was ised, <c>false</c> otherwise.</returns>
        /// <param name="list">List.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static bool IsNullOrNotElements<T>(this IEnumerable<T> list)
        {
            var result = true;

            if (list != null)
            {
                result = !list.Any();
            }

            return result;
        }

        /// <summary>
        /// Ises the null or not elements.
        /// </summary>
        /// <returns><c>true</c>, if null or not elements was ised, <c>false</c> otherwise.</returns>
        /// <param name="list">List.</param>
        public static bool IsNullOrNotElements(this IEnumerable list)
        {
            return !list.HasElements();
        }

        /// <summary>
        /// Has elements.
        /// </summary>
        /// <returns><c>true</c>, if elements was hased, <c>false</c> otherwise.</returns> 
        public static bool HasElements(this IEnumerable list)
        {
            var result = true;

            if (list != null)
            {
                result = list.GetEnumerator().MoveNext();
            }

            return result;
        }

        /// <summary>
        /// Has elements.
        /// </summary>
        /// <returns><c>true</c>, if elements was hased, <c>false</c> otherwise.</returns>
        public static bool HasElements<T>(this IEnumerable<T> list)
        {
            var result = true;

            if (list != null)
            {
                result = list.Any(); // es mas rapido que getcount()
            }

            return result;
        }

        /// <summary>
        /// Ises the same elements.
        /// </summary>
        /// <returns><c>true</c>, if same elements was ised, <c>false</c> otherwise.</returns>
        /// <param name="list">List.</param>
        /// <param name="data">Data.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static bool IsSameElements<T>(this IEnumerable<T> list, object data)
        {
            if (list != null && data is IEnumerable<T> list2)
            {
                return list.Count() == list2.Count();
            }

            return false;
        }

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <param name="enumeration">Enumeration.</param>
        /// <param name="action">Action.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        /// <summary>
        /// Picks the random.
        /// </summary>
        /// <returns>The random.</returns>
        /// <param name="source">Source.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        /// <summary>
        /// Picks the random.
        /// </summary>
        /// <returns>The random.</returns>
        /// <param name="source">Source.</param>
        /// <param name="count">Count.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        /// <summary>
        /// Shuffle the specified source.
        /// </summary>
        /// <returns>The shuffle.</returns>
        /// <param name="source">Source.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        /// <summary>
        /// Get previous item.
        /// </summary>
        /// <typeparam name="T"></typeparam> 
        /// <param name="predicate"></param>
        /// <param name="list"></param>
        /// <returns>previous item or default(T)</returns>
        public static T Previous<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            var previous = default(T);

            if (list != null && predicate != null)
                foreach (var item in list)
                {
                    if (predicate(item))
                        break;

                    previous = item;
                }

            return previous;
        }
    }
}
