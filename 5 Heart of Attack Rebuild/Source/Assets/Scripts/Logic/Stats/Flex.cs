using System;
using System.Collections.Generic;

namespace HOA.Stats
{
    public class Flex : BiStat 
    {
        public sbyte min { get { return primary.current; } }
        public sbyte max { get { return secondary.current; } }
        private Flex(Unit self, string name, sbyte min, sbyte max)
            : base(self, name, min, max)
        { }

        public static implicit operator Range<sbyte>(Flex flex) { return Range.sb(flex.min, flex.primary); }
        public override string ToString() { return string.Format("{0}-to-{1}", min, primary); ; }

        #region Builders

        public static Flex Rng(Unit self, sbyte min, sbyte max)
        { return new Flex(self, "Range", min, max); }

        public static Flex Rng(Unit self, sbyte max)
        { return Rng(self, 0, max); }

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