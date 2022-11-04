using System;
using System.Collections.Generic;

namespace HOA.St
{
	
    public class Capped : Stat
    {
        Element cap;
        public sbyte Cap { get { return cap.current; } }

        private Capped(Unit self, string name, sbyte normal, sbyte cap)
            :base(self, name, normal)
        {
            cap = new Element(cap);
        }

        public override void Add(sbyte amount) 
        {
            if (primary + amount <= cap)
                primary += amount;
            else
                primary.Set(cap);
        }

        public override void Set(sbyte amount) 
        {
            if (amount <= cap)
                primary.Set(amount);
            else
                primary.Set(cap);
        }

        public void AddCap(sbyte amount) { cap += amount; }
        public void SetCap(sbyte amount) { cap.Set(amount); }
        public int ChangedCap() { return cap.Changed(); }

        public void Fill() { primary.Set(cap); }

        public override string ToString() { return string.Format("{0}/{1}", primary, cap); ; }

        #region Builders

        public static Capped Hel(Unit self, sbyte cap)
        { return new Capped(self, "Health", cap, cap); }

        public static Capped En(Unit self, sbyte cap)
        { return new Capped(self, "Energy", 0, cap); }

        public static Capped Def(Unit self, sbyte normal, sbyte cap)
        { return new Capped(self, "Defense", normal, cap); }

        #endregion

        public static Capped operator +(Capped s, sbyte i) { s.Add(i); return s; }
    }
}