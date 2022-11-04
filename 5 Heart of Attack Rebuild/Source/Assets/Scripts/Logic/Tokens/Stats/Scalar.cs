using System;
using System.Collections.Generic;

namespace HOA.To.St
{
	
    public class Scalar<T> : Stat<T>
        where T: struct, IComparable<T>, IEquatable<T>
    {
        protected Scalar(Unit self, string name, T normal)
            : base(self, name, normal)
        { }
	}

    public class Scalar : Scalar<sbyte>
    {
        private Scalar(Unit self, string name, sbyte normal)
            : base(self, name, normal)
        { }

        public static Scalar In(Unit self, sbyte normal)
        { return new Scalar(self, "Initiative", normal); }

        public static Scalar Def(Unit self, sbyte normal)
        { return new Scalar(self, "Defense", normal); }

        public static Scalar Fo(Unit self)
        { return new Scalar(self, "Focus", 0); }

        public static Scalar Dam(byte i, Unit self, sbyte normal)
        { return new Scalar(self, "Damage" + i, normal); }

        public static Scalar Dam(Unit self, sbyte normal)
        { return Dam(0, self, normal); }

        public static Scalar Boost(Unit self, sbyte normal)
        { return new Scalar(self, "Boost", normal); }
    }
}