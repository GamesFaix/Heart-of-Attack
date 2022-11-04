using System;
using System.Collections.Generic;

namespace HOA.St
{
    public class Flex : Stat 
    {
        Element min;
        public sbyte Min { get { return min.current; } }

        private Flex(Unit self, string name, sbyte min, sbyte primary)
            : base (self, name, primary)
        {
            min = new Element(min);
        }

        public void AddMin(sbyte amount) { min += amount; }
        public void SetMin(sbyte amount) { min.Set(amount); }
        public int ChangedMin() { return min.Changed(); }

        public static implicit operator Range<sbyte>(Flex flex) { return Range.sb(flex.min, flex.primary); }
        public override string ToString() { return string.Format("{0}-to-{1}", min, primary); ; }

        #region Builders

        public static Flex Rng(byte i, Unit self, sbyte min, sbyte max)
        { return new Flex(self, "Range" + i, min, max); }

        public static Flex Rng(Unit self, sbyte min, sbyte max)
        { return Rng(0, self, min, max); }

        public static Flex Rng(byte i, Unit self, sbyte max)
        { return Rng(i, self, 0, max); }

        public static Flex Rng(Unit self, sbyte max)
        { return Rng(0, self, 0, max); }

        public static Flex Sel(byte i, Unit self, sbyte min, sbyte max)
        { return new Flex(self, "Selections" + i, min, max); }

        public static Flex Sel(byte i, Unit self, sbyte max)
        { return Sel(i, self, 1, max); }

        public static Flex Sel(Unit self, sbyte min, sbyte max)
        { return Sel(0, self, min, max); }

        public static Flex Sel(Unit self, sbyte max)
        { return Sel(0, self, 1, max); }

        #endregion

        public static Flex operator +(Flex s, sbyte i) { s.Add(i); return s; }
    }
}