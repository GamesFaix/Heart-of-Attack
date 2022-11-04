using System;
using System.Collections.Generic;

namespace HOA.Stats
{
	
    public class Capped : BiStat
    {
        public sbyte cap { get { return secondary.current; } }

        private Capped(Unit self, string name, sbyte normal, sbyte cap)
            : base(self, name, normal, cap)
        { }

        public override void Add(sbyte amount, byte index = 0) 
        {
            switch (index)
            {
                case 0:
                    if (primary + amount <= secondary)
                        primary += amount;
                    else
                        primary.Set(secondary);
                    break;
                case 1:
                    secondary += amount;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        public override void Set(sbyte amount, byte index = 0) 
        {
            switch (index)
            {
                case 0:
                    if (primary + amount <= secondary)
                        primary.Set(amount);
                    else
                        primary.Set(secondary);
                    break;
                case 1:
                    secondary += amount;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        public void Fill() { primary.Set(secondary); }

        public override string ToString() { return string.Format("{0}/{1}", primary, secondary); ; }

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