using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOA.Collections
{
    /// <summary>
    /// Two-dimensional array using index2 and size2
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Matrix<T> : IEnumerable<T>
    {
        #region Properties

        /// <summary>
        /// Number of indecies in each dimension
        /// </summary>
        public size2 Size { get; protected set; }
        /// <summary>
        /// Returns Size.x * Size.y
        /// </summary>
        public int Count { get { return Size.Product; } }

        /// <summary>
        /// Inner array
        /// </summary>
        protected T[,] array;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize inner array to given size, fill with default(T).
        /// </summary>
        /// <param name="size"></param>
        public Matrix(size2 size)
        {
            Size = size;
            array = new T[Size.x, Size.y];
            for (int j = 0; j < Size.y; j++)
                for (int i = 0; i < Size.x; i++)
                    array[i, j] = default(T);
        }

        /// <summary>
        /// Initialize array to given size, fill with list.
        /// Throws exception if list.Count != Matrix.Count
        /// </summary>
        /// <param name="size"></param>
        /// <param name="list"></param>
        public Matrix(size2 size, List<T> list)
        {
            Size = size;
            if (list.Count != Count) 
                throw new Exception("Matrix" + Size + " requires 0 or " + 
                    Count + " entries for construction.");
            array = new T[Size.x, Size.y];

            int listIndex = 0;
            for (int j = 0; j < Size.y; j++)
                for (int i = 0; i < Size.x; i++)
                {
                    array[i, j] = list[listIndex];
                    listIndex++;
                }
        }
        /// <summary>
        /// Initialize array to given size, fill with array.
        /// Throws exception if array.Length != Matrix.Count.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="list"></param>
        public Matrix(size2 size, T[] list) 
            : this(size, new List<T>(list)) { }

        #endregion

        /// <summary>
        /// Basic indexer.
        /// Throws IndexOutOfRangeException if index not found.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[index2 index]
        {
            get
            {
                if (Contains(index))
                    return array[index.x, index.y];
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                if (Contains(index))
                    array[index.x, index.y] = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }
        /// <summary>
        /// Simple indexer.  Throws IndexOutOfRangeException if not found.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public T this[int x, int y]
        {
            get { return this[new index2(x, y)]; }
            set { this[new index2(x, y)] = value; }
        }

        /// <summary>
        /// Iterates through array to check for item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            foreach (index2 index in Size)
                if (this[index] != null)
                    if (this[index].Equals(item)) 
                        return true;
            return false;
        }
        /// <summary>
        /// Iterates through array to check for item.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="i">Index of item if found. Defaults to (0,0).</param>
        /// <returns></returns>
        public bool Contains(T item, out index2 i)
        {
            i = new index2(0, 0);
            foreach (index2 index in Size)
                if (this[index] != null)
                    if (this[index].Equals(item))
                    {
                        i = index;
                        return true;
                    }
            return false;
        }

        /// <summary>
        /// Returns Size.Contains(index)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Contains(index2 index) { return (Size.Contains(index)); }

        /// <summary>
        /// Returns Size.Contains(index)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item">Item at index if true, default if false.</param>
        /// <returns></returns>
        public bool Contains(index2 index, out T item)
        {
            item = default(T);
            if (Size.Contains(index))
            {
                item = this[index];
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// If item is in array, returns its index.  Else, throws exception.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public index2 IndexOf(T item)
        {
            index2 i;
            if (!Contains(item, out i))
                throw new IndexOutOfRangeException();
            else
                return i;
        }

        /// <summary>
        /// Returns a random item from array.
        /// </summary>
        public T Random
        {
            get
            {
                checked
                {
                    int x = HOA.Random.Range(0, Size.x - 1);
                    int y = HOA.Random.Range(0, Size.y - 1);
                    return array[x, y];
                }
            }
        }

        /// <summary>
        /// Iterates through items in array
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int j = 0; j < Size.y; j++)
                for (int i = 0; i < Size.x; i++)
                    yield return array[i, j];
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        /// <summary>
        /// List of items in outer indecies of array
        /// </summary>
        public ListSet<T> Periphery
        {
            get
            {
                ListSet<T> periphery = new ListSet<T>();
                for (int i = 0; i < Size.x; i++) 
                    periphery.Add(array[i, 0]);
                for (int i = 0; i < Size.y; i++) 
                    periphery.Add(array[Size.x - 1, i]);
                for (int i = Size.x - 1; i >= 0; i--) 
                    periphery.Add(array[i, Size.y - 1]);
                for (int i = Size.y - 1; i >= 0; i--) 
                    periphery.Add(array[0, i]);
                return periphery;
            }
        }

        /// <summary>
        /// List of outer indecies of array
        /// </summary>
        public ListSet<index2> PeripheralIndexes
        {
            get
            {
                ListSet<index2> periphery = new ListSet<index2>();
                for (int i = 0; i < Size.x; i++) 
                    periphery.Add(new index2(i, 0)); 
                for (int i = 0; i < Size.y; i++) 
                    periphery.Add(new index2(Size.x - 1, i)); 
                for (int i = Size.x - 1; i >= 0; i--) 
                    periphery.Add(new index2(i, Size.y - 1));
                for (int i = Size.y - 1; i >= 0; i--) 
                    periphery.Add(new index2(0, i));
                return periphery;
            }
        }

        
    }
}
