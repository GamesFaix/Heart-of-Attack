using System;
using System.Collections.Generic;

namespace HOA.St
{
    public struct Element
    {
        public sbyte normal, current;

        public Element(sbyte normal)
        {
            this.normal = normal;
            current = normal;
        }

        public void Add(sbyte amount) { current += amount; }
        public void Set(sbyte amount) { current = amount; }
        public int Changed() { return current.CompareTo(normal); }

        public static implicit operator sbyte(Element e) { return e.current; }
        public override string ToString() { return current.ToString(); }

        public static Element operator +(Element e, sbyte s)
        {
            e.Add(s);
            return e;
        }
    }
}