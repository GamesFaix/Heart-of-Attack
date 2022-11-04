using System;
using System.Collections.Generic;

namespace HOA.Stats
{
    public struct Element
    {
        public readonly sbyte normal, current;

        private Element(sbyte normal, sbyte current)
        {
            this.normal = normal;
            this.current = current;
        }

        public Element(sbyte normal)
            : this(normal, normal)
        { }


        public Element Add(sbyte amount)
        { 
            checked 
            { 
                return new Element(normal, (sbyte)(current + amount)); 
            } 
        }

        public Element Set(sbyte amount) 
        { return new Element(normal, amount); }

        public sbyte Changed()
        { checked { return (sbyte)(current.CompareTo(normal)); } }

        public static implicit operator sbyte(Element e) { return e.current; }
        public override string ToString() { return current.ToString(); }

        public static Element operator +(Element e, sbyte s)
        { return e.Add(s); }

        public static Element operator -(Element e, sbyte s)
        { return e.Add((sbyte)(0 - s)); }
    }
}