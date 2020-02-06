using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.SystemExtensions
{
    /// <summary>
    /// 	Extension methods for all kind of Collections implementing the ICollection&lt;T&gt; interface
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 	Adds a value uniquely to to a collection and returns a value whether the value was added or not.
        /// </summary>
        /// <typeparam name = "T">The generic collection value type</typeparam>
        /// <param name = "collection">The collection.</param>
        /// <param name = "value">The value to be added.</param>
        /// <returns>Indicates whether the value was added or not</returns>
        public static bool AddUnique<T>(this ICollection<T> collection, T value)
        {
            var alreadyHas = collection.Contains(value) ;
            if (!alreadyHas)
            {
                collection.Add(value);
            }
            return alreadyHas;
        }

        /// <summary>
        /// 	Adds a range of value uniquely to a collection and returns the amount of values added.
        /// </summary>
        /// <typeparam name = "T">The generic collection value type.</typeparam>
        /// <param name = "collection">The collection.</param>
        /// <param name = "values">The values to be added.</param>
        /// <returns>The amount if values that were added.</returns>
        public static int AddRangeUnique<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            return values.Count(collection.AddUnique);
        }

        ///<summary>
        ///	Remove an item from the collection with predicate
        ///</summary>
        ///<param name = "collection"></param>
        ///<param name = "predicate"></param>
        ///<typeparam name = "T"></typeparam>
        ///<exception cref = "ArgumentNullException"></exception>
        public static void RemoveWhere<T>(this ICollection<T> collection, Predicate<T> predicate)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            var deleteList = collection.Where(child => predicate(child)).ToList();
            deleteList.ForEach(t => collection.Remove(t));
        }

        /// <summary>
        /// Tests if the collection is empty.
        /// </summary>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        public static bool IsEmpty(this ICollection collection)
        {
            return collection.Count == 0;
        }

        /// <summary>
        /// Tests if the collection is empty.
        /// </summary>
        /// <typeparam name="T">The type of the items in 
        /// the collection.</typeparam>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        /// <summary>
        /// Tests if the collection is empty.
        /// </summary>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        public static bool IsEmpty(this IList collection)
        {
            return collection.Count == 0;
        }

        /// <summary>
        /// Tests if the collection is empty.
        /// </summary>
        /// <typeparam name="T">The type of the items in 
        /// the collection.</typeparam>
        /// <param name="collection">The collection to test.</param>
        /// <returns>True if the collection is empty.</returns>
        public static bool IsEmpty<T>(this IList<T> collection)
        {
            return collection.Count == 0;
        }

        #region [Constants]
        private const int INDEX_OF_SECOND_ELEMENT = 1;
        private const int INDEX_OF_THIRD_ELEMENT = 2;
        private const int INDEX_OF_FOURTH_ELEMENT = 3;
        private const int INDEX_OF_FIFTH_ELEMENT = 4;
        private const int INDEX_OF_SIXTH_ELEMENT = 5;
        private const int INDEX_OF_SEVENTH_ELEMENT = 6;
        #endregion [Constants]


        #region [Public members]
        /// <summary>
        /// Gets the second element in current sequence.
        /// </summary>
        /// <typeparam name="T">The type of element in the sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>The second element of the sequence.</returns>
        public static T Second<T>(this IEnumerable<T> source)
        {
            return GetElementAt(source, INDEX_OF_SECOND_ELEMENT);
        }
        /// <summary>
        /// Gets the third element in current sequence.
        /// </summary>
        /// <typeparam name="T">The type of element in the sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>The third element in the sequence.</returns>
        public static T Third<T>(this IEnumerable<T> source)
        {
            return GetElementAt(source, INDEX_OF_THIRD_ELEMENT);
        }
        /// <summary>
        /// Gets the fourth element in current sequence.
        /// </summary>
        /// <typeparam name="T">The type of element in the sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>The fourth element in the sequence.</returns>
        public static T Fourth<T>(this IEnumerable<T> source)
        {
            return GetElementAt(source, INDEX_OF_FOURTH_ELEMENT);
        }
        /// <summary>
        /// Gets the fifth element in current sequence.
        /// </summary>
        /// <typeparam name="T">The type of element in the sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>The fifth element in the sequence.</returns>
        public static T Fifth<T>(this IEnumerable<T> source)
        {
            return GetElementAt(source, INDEX_OF_FIFTH_ELEMENT);
        }
        /// <summary>
        /// Gets the sixth element in current sequence.
        /// </summary>
        /// <typeparam name="T">The type of element in the sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>The sixth element in the sequence.</returns>
        public static T Sixth<T>(this IEnumerable<T> source)
        {
            return GetElementAt(source, INDEX_OF_SIXTH_ELEMENT);
        }
        /// <summary>
        /// Returns the sixth element in current sequence.
        /// </summary>
        /// <typeparam name="T">The type of element in the sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>The sixth element in the sequence.</returns>
        public static T Seventh<T>(this IEnumerable<T> source)
        {
            return GetElementAt(source, INDEX_OF_SEVENTH_ELEMENT);
        }
        /// <summary>
        /// Returns a slice of the given source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="size">The size.</param>
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> source, int startIndex, int size)
        {
            Guard.AgainstNullReference(source);
            var enumerable = source as IList<T> ?? source.ToList();
            return enumerable.Skip(startIndex).Take(size);
        }
        #endregion [Public members]


        #region [Help members]
        private static T GetElementAt<T>(IEnumerable<T> source, int index)
        {
            var enumerable = source as IList<T> ?? source.ToList();
            if (enumerable.Count < index + 1)
                Guard.Throw<ArgumentOutOfRangeException>();
            return enumerable.ElementAt(index);
        }
        #endregion [Help members]
    }
}
