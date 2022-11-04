using System;
using System.Collections.Generic;
using System.Collections;
using HOA.Stats;

namespace HOA.Fargo
{

    public class ArgTable<TKey, TValue> : IEnumerable<Arg<TKey, TValue>>
      //  where TKey : Enum
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

    public static class ArgTable
    {
        public static ArgTable<FN, sbyte> Num(params Arg<FN, sbyte>[] args)
        { return new ArgTable<FN, sbyte>(args); }

        public static ArgTable<FS, Stat> Stat(params Arg<FS, Stat>[] args)
        { return new ArgTable<FS, Stat>(args); }

        public static ArgTable<FX, string> Text(params Arg<FX, string>[] args)
        { return new ArgTable<FX, string>(args); }

        public static ArgTable<FO, bool> Option(params Arg<FO, bool>[] args)
        { return new ArgTable<FO, bool>(args); }

        public static ArgTable<FT, IEntity> Target(params Arg<FT, IEntity>[] args)
        { return new ArgTable<FT, IEntity>(args); }

        public static ArgTable<FF, Predicate<IEntity>> Filter(params Arg<FF, Predicate<IEntity>>[] args)
        { return new ArgTable<FF, Predicate<IEntity>>(args); }

        public static ArgTable<FE, Ef.Builder> Effect(params Arg<FE, Ef.Builder>[] args)
        { return new ArgTable<FE, Ef.Builder>(args); }


    }

}