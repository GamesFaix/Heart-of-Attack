using System;
using System.Collections;
using System.Collections.Generic;

namespace HOA.Collections
{

    public class NestedList<T> : IEnumerable<IList<T>>
    {
        protected List<IList<T>> mainList;

        public int Count { get { return mainList.Count; } }

        protected IList<T> first { get { return mainList[0]; } }
        protected IList<T> last { get { return mainList[mainList.Count - 1]; } }

        #region Constructors

        public NestedList() 
        { 
            mainList = new List<IList<T>>(); 
        }

        public NestedList(T item)
            : this()
        {
            AddToEnd(item);
        }

        public NestedList(IEnumerable<T> collection)
            : this()
        {
            AddToEnd(new List<T>(collection));
        }

        #endregion

        #region Add/Remove/Contains

        public void AddToEnd(T item)
        {
            List<T> subList = new List<T>();
            subList.Add(item);
            mainList.Add(subList);
        }
        public void AddToEnd(IList<T> list) 
        { 
            mainList.Add(list); 
        }
        public void AddToList(int subList, T item) 
        { 
            mainList[subList].Add(item); 
        }
        public void AddToList(int subList, List<T> list) 
        { 
            foreach (T item in list)
                mainList[subList].Add(item); 
        }

        public void AddToLast(T item)
        {
            last.Add(item);
        }
        public void AddToLast(IList<T> collection)
        {
            foreach (T item in collection)
                last.Add(item);
        }


        public bool RemoveFromAll(T item)
        {
            bool result = false;
            foreach (IList<T> sub in mainList)
                if (sub.Remove(item))
                    result = true;
            return result;
        }
        public bool RemoveFromAll(IEnumerable<T> collection)
        {
            bool result = false;
            foreach (IList<T> sub in mainList)
                foreach(T item in collection)
                    if (sub.Remove(item))
                        result = true;
            return result;
        }
        public bool RemoveFromList(int subList, T item)
        {
            return mainList[subList].Remove(item);
        }
        public bool RemoveFromList(int subList, IEnumerable<T> collection)
        {
            bool result = false;
            foreach (T item in collection)
                if (mainList[subList].Remove(item))
                    result = true;
            return result;
        }

        public bool Contains(T item)
        {
            foreach (IList<T> subList in mainList)
                if (subList.Contains(item))
                    return true;
            return false;
        }
        public bool ContainsAll(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                if (!Contains(item))
                    return false;
            return true;
        }

        #endregion

        public void Clear() { mainList.Clear(); }
        public void ClearList(int subList) { mainList[subList].Clear(); }

        #region Iterators/Indexers

        public IList<T> this[int index] { get { return this[index]; } }
        public T this[index2 index] { get { return this[index.x][index.y]; } }

        public IEnumerator<IList<T>> GetEnumerator()
        {
            return mainList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        #endregion

    }
}