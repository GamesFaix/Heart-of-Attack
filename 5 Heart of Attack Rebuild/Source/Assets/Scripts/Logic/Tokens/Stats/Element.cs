using System;
using System.Collections.Generic;

namespace HOA.To.St
{

    public struct Element<T>
        where T : struct, IComparable<T>, IEquatable<T>
    {
        public T normal, current;

        public Element(T normal)
        {
            this.normal = normal;
            current = normal;
        }

        public void Add(Func<T,T,T> adder, T amount) { current = adder(current, amount); }
        public void Set(T amount) { current = amount; }
        public int Changed() { return current.CompareTo(normal); }

        public static implicit operator T(Element<T> s) { return s.current; }
        public override string ToString() { return current.ToString(); }
    
	}
}