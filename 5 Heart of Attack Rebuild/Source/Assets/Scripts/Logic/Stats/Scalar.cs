using System;
using System.Collections.Generic;

namespace HOA.Stats
{

    public class Scalar : Stat
    {
        public Scalar(sbyte normal, Action<sbyte> sideEffect = null)
            : base(sideEffect, new Element(normal))
        { }

        public static implicit operator sbyte(Scalar s) { return s.values[0].current; }

        public override string ToString() { return values[0].ToString(); }

        public static Scalar operator +(Scalar s, sbyte amount) { s.Add(amount, 0); return s; }
        public static Scalar operator -(Scalar s, sbyte amount) { checked { s.Add((sbyte)(0 - amount), 0); } return s; }
    }
}