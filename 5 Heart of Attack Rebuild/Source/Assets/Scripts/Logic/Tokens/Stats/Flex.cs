using System;
using System.Collections.Generic;

namespace HOA.To.St
{
    public class Flex<T> : Stat<T>
        where T : struct, IComparable<T>, IEquatable<T>
    {
        Element<T> min;
        public T Min { get { return min.current; } }

        protected Flex(Unit self, string name, T min, T primary)
            : base (self, name, primary)
        {
            min = new Element<T>(min);
        }

        public void AddMin(Func<T, T, T> adder, T amount) { min.Add(adder, amount); }
        public void SetMin(T amount) { min.Set(amount); }
        public int ChangedMin() { return min.Changed(); }

        public static implicit operator Range<T>(Flex<T> flex) { return new Range<T>(flex.min, flex.primary); }

        public override string ToString() { return string.Format("{0}-to-{1}", min, primary); ; }
    }

    public class Flex : Flex<sbyte>
    {
        private Flex(Unit self, string name, sbyte min, sbyte normal)
            : base(self, name, min, normal)
        { }

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

        public static implicit operator Range<sbyte>(Flex f) 
        { return new Range<sbyte>(f.Min, f.primary); }
    }
}