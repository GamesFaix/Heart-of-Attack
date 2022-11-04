using System;
using System.Collections.Generic;

namespace HOA.St
{
	
    public class Scalar : Stat
    {
        protected Scalar(Unit self, string name, sbyte normal)
            : base(self, name, normal)
        { }

        #region Builders

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

        #endregion

        public static Scalar operator +(Scalar s, sbyte i) { s.Add(i); return s; }
    }
}