using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

namespace HOA
{
    /// <summary>
    /// Collection with numbered indecies and no dupicate items.  
    /// Based on inner List.
    /// </summary>
    public class Set<T> : List<T>, IEquatable<Set<T>>, IEnumerable<T>//, ICollection<T>, IList<T>, IMarkable<T>, 
    {
        const int defaultCapacity = 4;

        #region Constructors

        public Set() : base(defaultCapacity) { }

        /// <summary>Creates empty set.</summary>
        public Set(int capacity) 
            : base(capacity) { }
        
        /// <summary> Creates empty set, 
        /// then calls Add(T) on each argument.</summary>
        public Set(params T[] items) 
            : this (items.Length)
        {
            foreach (T item in items)
                Add(item);
        }
        
        /// <summary> Creates empty set,
        /// then calls Add(T) on each item in collection.</summary>
        public Set(params IEnumerable<T>[] collections) 
            : this(collections.Length * defaultCapacity)
        {
            foreach (IEnumerable<T> col in collections)
                foreach (T item in col) 
                    Add(item);
        }

        #endregion

        #region Add/Remove

        /// <summary> If item not already in list, calls List.Add.</summary>
        public new void Add(T item)
        {
            if (!Contains(item))
                base.Add(item);
        }
        
        /// <summary> If item not already in list, calls List.Add.
        /// Else, outputs true.</summary>
        public void Add(T item, out bool alreadyContains)
        {
            alreadyContains = false;
            if (Contains(item)) 
                alreadyContains = true;
            else 
                base.Add(item);
        }

        /// <summary> Call Add(T) on each item.</summary>
        public void Add(params T[] items)
        {
            foreach (T item in items)
                if (!Contains(item))
                     base.Add(item);
        }

        /// <summary> Call Add(T) on each item in collection.</summary>
        public void Add(IEnumerable<T> collection) 
        { 
            foreach (T item in collection) 
                Add(item); 
        }

        /// <summary> Call Add(T) on each item of a collection of a derived type.</summary>
        public void Add<TDerived>(IEnumerable<TDerived> collection)
            where TDerived : T
        {
            foreach (TDerived item in collection) 
                Add(item); 
        }

        /// <summary> If item not already in list, calls List.Insert.</summary>
        public new void Insert(int index, T item) 
        { 
            if (!Contains(item)) 
                base.Insert(index, item); 
        }

        /// <summary> Calls Remove(T) on each item in collection.</summary>
        public void Remove(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                Remove(item);
        }

        #endregion
       
        /// <summary> Returns random item.
        /// Calls List extension method Random.</summary>
        public T Random() { return ListExtensionMethods.Random(this); }

        /// <summary> Randomizes index order. 
        /// Calls List extension method Shuffle.</summary>
        public void Shuffle() { ListExtensionMethods.Shuffle(this); }

        #region Copy/Cast/Convert

        /// <summary> Call constructor with self as argument.</summary>
        public Set<T> Copy() { return new Set<T>(this); }

        /// <summary> Call constructor with item as argument.</summary>
        public static implicit operator Set<T>(T item) { return new Set<T>(item); }

        #endregion

        #region IEquatable

        /// <summary> List.Equals </summary>
        public bool Equals(Set<T> other) { return base.Equals(other); }

        /// <summary> List.Equals </summary>
        public override bool Equals(object obj) { return base.Equals(obj); }

        /// <summary> List.GetHashCode </summary>
        public override int GetHashCode() { return base.GetHashCode(); }

        #endregion

        public Set<T> Union<TDerived>(IEnumerable<TDerived> collection)
            where TDerived : T
        {
            Set<T> s = Copy();
            s.Add<TDerived>(collection);
            return s;
        }

        

        public Set<T> AppendResult(Func<T, T> f)
        {
            Set<T> s = Copy();
            foreach (T item in s)
                s.Add(f(item));
            return s;
        }

        public Set<T> AppendResult(Func<T, IEnumerable<T>> f)
        {
            Set<T> s = Copy();
            foreach (T item in s)
                s.Add(f(item));
            return s;
        }

        #region Operators

        /// <summary> Union </summary>
        public static Set<T> operator + (Set<T> set1, Set<T> set2) 
        { return new Set<T>(set1.Union<T>(set2)); }

        /*public static Set<T> operator +(Set<T> set, Func<T, T> f)
        { return set.AppendResult(f); }
        */
        public static Set<T> operator +(Set<T> set, Func<T, IEnumerable<T>> f)
        { return set.AppendResult(f); }


        /// <summary> Except </summary>
        public static Set<T> operator - (Set<T> set1, Set<T> set2) 
        { return new Set<T>(set1.Except<T>(set2)); }

        /// <summary> Where </summary>
        public static Set<T> operator / (Set<T> s, Predicate<T> p)
        {
            Func<T, bool> f = (t) => { return p(t); };
            return new Set<T>(s.Where<T>(f));
        }

        /// <summary> SingleOrDefault </summary>
        public static T operator & (Set<T> s, Predicate<T> p)
        {
            Func<T, bool> f = (t) => { return p(t); };
            return s.SingleOrDefault<T>(f); 
        }

        #endregion
    }

    public static class SetExtensionMethods
    {
        /// <summary>Convert set of T to set of T's immediate base class."/></summary>
        public static Set<TBase> Base<T, TBase>(this Set<T> s)
            where T : TBase
        {
            Set<TBase> result = new Set<TBase>();
            foreach (T item in s)
                result.Add(item);
            return result;
        }

        //public static T SingleOrNull<T>(this Set<T> s) { return List<T>.SingleOrNull<T>(s); }

        /// <summary>Input a set of T and func(T, TOut),
        /// return set of output values in analagous order.</summary>
        public static Set<TOut> Map<T, TOut>
            (this Set<T> setIn, Func<T, TOut> f)
        {
            Set<TOut> setOut = new Set<TOut>(setIn.Count);
            foreach (T t in setIn)
                setOut.Add(f(t));
            return setOut;
        }


        /// <summary> Merge a set of collections of T into a single set of T.
        /// (Removes duplicates.)</summary>
        public static Set<T> Merge<E, T>(this Set<E> setIn)
            where E : IEnumerable<T>
        {
            Set<T> setOut = new Set<T>();
            foreach (E collection in setIn)
                foreach (T t in collection)
                    setOut.Add(t);
            return setOut;
        }
    }
}
