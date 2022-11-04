using System;
using System.Collections.Generic;

namespace HOA.Stats
{
	public abstract class Stat
    {
        protected Element[] values;
        protected Action<sbyte> sideEffects;

        protected Stat (Action<sbyte> sideEffects = null, params Element[] values)
        {
            this.sideEffects = sideEffects;
            this.values = values;
        }

        protected Stat(Action<sbyte> sideEffects = null, params sbyte[] values)
        {
            this.sideEffects = sideEffects;
            this.values = new Element[values.Length];
            for (int i = 0; i < values.Length; i++)
                this.values[i] = new Element(values[i]);
        }

        public sbyte this[int i] { get { return values[i].current; } }

        public virtual sbyte Add(sbyte amount, byte index = 0) 
        {
            return Set(values[index] + amount, index);
        }

        public virtual sbyte Set(sbyte amount, byte index = 0)
        {
            sbyte original = values[index];
            values[index] = values[index].Set(amount);
            if (sideEffects != null)
                sideEffects(values[index] - original);
            return values[index];
        }

        public sbyte Changed(byte index = 0) 
        { return values[index].Changed(); }

        public static Stat operator +(Stat s, sbyte amount) { s.Add(amount, 0); return s; }

        public static Stat operator +(Stat s, int2 i)
        {
            checked
            {
                s.Add((sbyte)i.x, 0);
                s.Add((sbyte)i.y, 1);
                return s;
            }
        }

        public static implicit operator sbyte(Stat s) { return s[0]; }
    }
}