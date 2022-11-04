using System.Collections.Generic;
using System.Collections;

namespace HOA.Args
{

    public class ArgTable<TKey, TValue> : IEnumerable<Arg<TKey, TValue>>
    {
        Dictionary<TKey, TValue> dict;

        public ArgTable(int capacity = 0)
        {
            dict = new Dictionary<TKey, TValue>(capacity);
        }

        public ArgTable(params Arg<TKey, TValue>[] args)
            : this(args.Length)
        {
            foreach (Arg<TKey, TValue> a in args)
                dict.Add(a.key, a.value);
        }

        public void Add(Arg<TKey, TValue> a) { dict.Add(a.key, a.value); }

        public bool Remove(Arg<TKey, TValue> a) { return Remove(a.key); }
        public bool Remove(TKey key) { return dict.Remove(key); }

        public bool Contains(Arg<TKey, TValue> a) { return Contains(a.key); }
        public bool Contains(TKey key) { return dict.ContainsKey(key); }

        public TValue this[TKey key]
        {
            get { return dict[key]; }
            set { dict[key] = value; }
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        public IEnumerator<Arg<TKey, TValue>> GetEnumerator()
        {
            foreach (TKey key in dict.Keys)
                yield return new Arg<TKey, TValue>(key, dict[key]);
        }

    }

    public static partial class ArgTable
    {
        public static ArgTable<RN, sbyte> Num(params Arg<RN, sbyte>[] args)
        { return new ArgTable<RN, sbyte>(args); }

        public static ArgTable<RL, string> Text(params Arg<RL, string>[] args)
        { return new ArgTable<RL, string>(args); }

        public static ArgTable<RO, bool> Option(params Arg<RO, bool>[] args)
        { return new ArgTable<RO, bool>(args); }

    }

}