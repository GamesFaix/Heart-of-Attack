using System;
using System.Collections;
using System.Collections.Generic;

namespace HOA.Collections
{
    public class Ledger<T1, T2> : IEnumerable<T1>
    {
        List<T1> list1;
        List<T2> list2;

        public Ledger(int capacity = 4)
        {
            list1 = new List<T1>(capacity);
            list2 = new List<T2>(capacity);
        }

        public void Add(T1 item1, T2 item2)
        {
            list1.Add(item1);
            list2.Add(item2);
        }

        public bool Contains(T1 item)
        {
            return list1.Contains(item);
        }
        public bool Contains(T2 item)
        {
            return list2.Contains(item);
        }

        public bool Remove(T1 item)
        {
            if (Contains(item))
            {
                list2.Remove(this[item]);
                list1.Remove(item);
                return true;
            }
            return false;
        }
        public bool Remove(T2 item)
        {
            if (Contains(item))
            {
                list1.Remove(this[item]);
                list2.Remove(item);
                return true;
            }
            return false;
        }

        public IEnumerator<T1> GetEnumerator()
        {
            return list1.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T1 this[T2 item]
        {
            get
            {
                return list1[list2.IndexOf(item)];
            }
        }
        public T2 this[T1 item]
        {
            get
            {
                return list2[list1.IndexOf(item)];
            }
        }
    }
}
