using System.Collections.Generic;
using System.Collections;
using System;

namespace HOA { 

    public class ListSet<T> 
        : IEnumerable<T>, ICollection<T>, IList<T>, IMarkableCollection<T>
    {

        protected List<T> list;
        protected List<T> marked;

        public ListSet(int capacity = 4) 
        {
            list = new List<T>(capacity);
            marked = new List<T>(1);
        }
        public ListSet(T item) : this()
        {
            Add(item);
        }

        public ListSet(IEnumerable<T> collection) : this()
        {
            foreach (T item in collection) Add(item);
        }



        public int Capacity
        {
            get { return list.Capacity; }
            protected set { list.Capacity = value; }
        }

        public int Count { get { return list.Count; } }
        public bool IsReadOnly { get; private set; }

        public void Add(T item, out bool alreadyContains)
        {
            alreadyContains = false;
            if (list.Contains(item)) alreadyContains = true;
            else list.Add(item);
        }
        public void Add(T item)
        {
            if (!Contains(item)) 
                list.Add(item);
        }

        public void Add(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                Add(item);
        }

        public void Insert(int index, T item) 
        { 
            if (!Contains(item)) 
                list.Insert(index, item); 
        }

        
        public bool Remove(T item)
        {
            if (!Contains(item)) 
                return false;
            list.Remove(item);
            return true;
        }

        public void Remove(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                Remove(item);
        }

        public void RemoveAt(int index) { list.RemoveAt(index); }
        public void Clear() { list.Clear(); }


        public T this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }
        public bool Contains(T item) { return list.Contains(item); }
        public int IndexOf(T item) { return list.IndexOf(item); }
        
        public IEnumerator<T> GetEnumerator() { return list.GetEnumerator(); }
        private IEnumerator GetEnumerator1() { return GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator1(); }

        public void CopyTo(T[] array, int index) { list.CopyTo(array, index); }


        public void Mark(T item) { if (Contains(item)) marked.Add(item); }
        public void Mark(int index) { if (index >= 0 && index < Count) marked.Add(this[index]); }
        public void Mark(IEnumerable<T> collection) { foreach (T item in collection) Mark(item); }

        public void Unmark(T item) { if (marked.Contains(item)) marked.Remove(item); }
        public void Unmark(int index) { if (marked.Contains(this[index])) marked.Remove(this[index]); }
        public void Unmark(IEnumerable<T> collection) { foreach (T item in collection) Unmark(item); }
        
        public void UnmarkAll() { marked = new List<T>(1); }

        public void RemoveMarked()
        {
            for (int i=Count-1; i>=0; i--)
                if (marked.Contains(this[i])) 
                    Remove(this[i]);
        }

        public T Random() { return list.Random(); }
        public void Shuffle() { list.Shuffle(); }
     
        public void Sort () {list.Sort();}

    }
}
