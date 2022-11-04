using System;
using System.Collections.Generic;

namespace HOA.To.St
{
	
    public class Capped<T> : Stat<T>
        where T: struct, IComparable<T>, IEquatable<T>
    {
        Element<T> cap;
        public T Cap { get { return cap.current; } }

        protected Capped(Unit self, string name, T normal, T cap)
            :base(self, name, normal)
        {
            cap = new Element<T>(cap);
        }

        public override void Add(Func<T, T, T> adder, T amount) 
        {
            if (adder(primary, amount).CompareTo(cap) <= 0)
                primary.Add(adder, amount);
            else
                primary.Set(cap);
        }

        public override void Set(T amount) 
        {
            if (amount.CompareTo(cap.current) <= 0)
                primary.Set(amount);
            else
                primary.Set(cap);
        }

        public void AddCap(Func<T, T, T> adder, T amount) { cap.Add(adder, amount); }
        public void SetCap(T amount) { cap.Set(amount); }
        public int ChangedCap() { return cap.Changed(); }

        public void Fill() { primary.Set(cap); }

        public override string ToString() { return string.Format("{0}/{1}", primary, cap); ; }
	}

    public class Capped : Capped<sbyte>
    {
        private Capped(Unit self, string name, sbyte normal, sbyte cap)
            : base(self, name, normal, cap)
        { }

        public static Capped Hel(Unit self, sbyte cap)
        {
            return new Capped(self, "Health", cap, cap);
        }

        public static Capped En(Unit self, sbyte cap)
        {
            return new Capped(self, "Energy", 0, cap);
        }

        public static Capped Def(Unit self, sbyte normal, sbyte cap)
        {
            return new Capped(self, "Defense", normal, cap);
        }
    }
}