using System;
using System.Collections.Generic;

namespace HOA.Stats
{

    public class Capped : Stat
    {
        public Capped(sbyte normal, sbyte cap, Action<sbyte> sideEffect = null)
            : base(sideEffect, normal, cap)
        { }

        public void Fill() { values[0].Set(values[1]); }

        public override sbyte Add(sbyte amount, byte index = 0)
        {
            if (index == 0)
                if (values[0] + amount <= values[1])
                    return base.Add(amount, index);
            else
                return base.Add(amount, index);
            return 0;
        }

        public override sbyte Set(sbyte amount, byte index = 0)
        {
            if (index == 0)
                if (amount <= values[1])
                    return base.Set(amount, index);
            else
                return base.Set(amount, index);
            return 0;
        }

        public static Capped operator +(Capped s, sbyte amount) { s.Add(amount, 0); return s; }
        public static Capped operator -(Capped s, sbyte amount) { checked { s.Add((sbyte)(0 - amount), 0); } return s; }

        public override string ToString() { return string.Format("{0}/{1}", values[0], values[1]); ; }

        public static implicit operator sbyte(Capped s) { return s.values[0].current; }

    }
}