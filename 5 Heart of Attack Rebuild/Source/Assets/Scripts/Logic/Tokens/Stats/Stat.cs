using System;
using System.Collections.Generic;

namespace HOA.To.St
{

    public abstract class Stat<T>
        where T : struct, IComparable<T>, IEquatable<T>
    {
        public Unit self { get; private set; }
        public string name { get; private set; }

        protected Element<T> primary;
        public T Current { get { return primary.current; } }

        protected Stat(Unit self, string name, T normal)
        {
            this.self = self;
            this.name = name;
            primary = new Element<T>(normal);
        }

        public virtual void Add(Func<T, T, T> adder, T amount) { primary.Add(adder, amount); }
        public virtual void Set(T amount) { primary.Set(amount); }
        public int Changed() { return primary.Changed(); }

        public static implicit operator T(Stat<T> s) { return s.Current; }
        public override string ToString() { return primary.ToString(); }
    }

    public abstract class Stat : Stat<sbyte>
    {
        protected Stat(Unit self, string name, sbyte normal) 
            : base(self, name, normal)
        { }
    }
}