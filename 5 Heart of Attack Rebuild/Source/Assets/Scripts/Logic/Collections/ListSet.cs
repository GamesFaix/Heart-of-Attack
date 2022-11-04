using System.Collections.Generic;
using System.Collections;
using System;

namespace HOA.Collections
{
    /// <summary>
    /// Collection with numbered indecies and no dupicate items.  
    /// Based on inner List.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListSet<T>
        : IEnumerable<T>, ICollection<T>, IList<T>, IMarkable<T>
    {
        #region Properties

        protected List<T> list;
        protected List<T> marked;

        /// <summary>
        /// Accessor for inner List capacity.
        /// </summary>
        public int Capacity
        {
            get { return list.Capacity; }
            protected set { list.Capacity = value; }
        }

        /// <summary>
        /// Accessor for inner List count.
        /// </summary>
        public int Count { get { return list.Count; } }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public bool IsReadOnly { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create empty ListSet
        /// </summary>
        /// <param name="capacity">Capacity of inner list. Defaults to 4.</param>
        public ListSet(int capacity = 4)
        {
            list = new List<T>(capacity);
            marked = new List<T>(1);
        }
        /// <summary>
        /// Create ListSet with one item.
        /// </summary>
        /// <param name="item">Item to be added to List.</param>
        public ListSet(T item)
            : this()
        {
            Add(item);
        }
        /// <summary>
        /// Create ListSet from another collection.
        /// </summary>
        /// <param name="collection">Collection to be added.</param>
        public ListSet(IEnumerable<T> collection)
            : this()
        {
            foreach (T item in collection) 
                Add(item);
        }

        #endregion

        #region Add/Remove

        /// <summary>
        /// Add item to collection. (No duplicates.)
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <param name="alreadyContains">True if addition successful, false if item is duplicate.</param>
        public void Add(T item, out bool alreadyContains)
        {
            alreadyContains = false;
            if (list.Contains(item)) alreadyContains = true;
            else list.Add(item);
        }

        /// <summary>
        /// Add item to collection. (No duplicates.)
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void Add(T item)
        {
            if (!Contains(item))
                list.Add(item);
        }

        /// <summary>
        /// Add a collection to the collection.  (No dupliates.)
        /// </summary>
        /// <param name="collection">Collection to add.</param>
        public void Add(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                Add(item);
        }

        /// <summary>
        /// Insert item at given index.
        /// </summary>
        /// <param name="index">Index to insert at.</param>
        /// <param name="item">Item to insert.</param>
        public void Insert(int index, T item)
        {
            if (!Contains(item))
                list.Insert(index, item);
        }

        /// <summary>
        /// Remove item from collection.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>True if item found, false if not in collection.</returns>
        public bool Remove(T item)
        {
            if (!Contains(item))
                return false;
            list.Remove(item);
            return true;
        }

        /// <summary>
        /// Remove all items in argument collection from ListSet.
        /// </summary>
        /// <param name="collection">Collection to remove.</param>
        public void Remove(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                Remove(item);
        }

        /// <summary>
        /// Remove item at given index.
        /// </summary>
        /// <param name="index">Index to remove.</param>
        public void RemoveAt(int index) { list.RemoveAt(index); }

        /// <summary>
        /// Remove all items from collection.
        /// </summary>
        public void Clear() { list.Clear(); }

        #endregion
 
        #region Fetch

        /// <summary>
        /// Indexer for inner List.  Inner List throws exception if out of range.
        /// </summary>
        /// <param name="index">Numerical index.</param>
        /// <returns>Item at given index, if found.</returns>
        public T this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        /// <summary>
        /// Calls GetEnumerator on inner List.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator() { return list.GetEnumerator(); }
        private IEnumerator GetEnumerator1() { return GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator1(); }
        
        /// <summary>
        /// Calls Contains() on inner List.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item) { return list.Contains(item); }

        /// <summary>
        /// Calls IndexOf(item) on inner List.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item) { return list.IndexOf(item); }

        /// <summary>
        /// Calls List.Random extension method.
        /// </summary>
        /// <returns>Random item from collection</returns>
        public T Random() { return list.Random(); }
       
        #endregion
        
        #region IMarkableCollection

        /// <summary>
        /// Adds item to Marked sublist, if found in collection.
        /// </summary>
        /// <param name="item"></param>
        public void Mark(T item) { if (Contains(item)) marked.Add(item); }
        /// <summary>
        /// Adds item at index to sublist, if in range.
        /// </summary>
        /// <param name="index"></param>
        public void Mark(int index) { if (index >= 0 && index < Count) marked.Add(this[index]); }
        /// <summary>
        /// Adds each item in collection to sublist, if found in collection.
        /// </summary>
        /// <param name="collection"></param>
        public void Mark(IEnumerable<T> collection) { foreach (T item in collection) Mark(item); }

        /// <summary>
        /// Removes item from sublist, if found.
        /// </summary>
        /// <param name="item"></param>
        public void Unmark(T item) { if (marked.Contains(item)) marked.Remove(item); }
        /// <summary>
        /// Removes item at index (in main collection) from sublist, if found.
        /// </summary>
        /// <param name="index"></param>
        public void Unmark(int index) { if (marked.Contains(this[index])) marked.Remove(this[index]); }
        /// <summary>
        /// Removes all items in collection from sublist, if found.
        /// </summary>
        /// <param name="collection"></param>
        public void Unmark(IEnumerable<T> collection) { foreach (T item in collection) Unmark(item); }

        /// <summary>
        /// Removes all items from sublist.
        /// </summary>
        public void UnmarkAll() { marked = new List<T>(1); }

        /// <summary>
        /// Removes all items in sublist from collection.
        /// </summary>
        public void RemoveMarked()
        {
            for (int i = Count - 1; i >= 0; i--)
                if (marked.Contains(this[i]))
                    Remove(this[i]);
        }

        #endregion

        /// <summary>
        /// Calls List.Shuffle extension method.
        /// </summary>
        public void Shuffle() { list.Shuffle(); }

        /// <summary>
        /// Calls Sort() on inner List.
        /// </summary>
        public void Sort() { list.Sort(); }

        /// <summary>
        /// Calls CopyTo(array, index) on inner List.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(T[] array, int index) { list.CopyTo(array, index); }

    }
}
