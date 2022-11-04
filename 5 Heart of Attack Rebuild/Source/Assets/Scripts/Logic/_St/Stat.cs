using System;
using System.Collections.Generic;

namespace HOA.St
{

    public abstract class Stat
    {
        public Unit self { get; private set; }
        public string name { get; private set; }

        protected Element primary;
        public sbyte Current { get { return primary.current; } }

        protected Stat(Unit self, string name, sbyte normal)
        {
            this.self = self;
            this.name = name;
            primary = new Element(normal);
        }

        public virtual void Add(sbyte amount) { primary += amount; }
        public virtual void Set(sbyte amount) { primary.Set(amount); }
        public int Changed() { return primary.Changed(); }

        public static implicit operator sbyte(Stat s) { return  s.Current; }
        public override string ToString() { return primary.ToString(); }

        public static Stat operator +(Stat s, sbyte i) { s.Add(i); return s; }
    }
}