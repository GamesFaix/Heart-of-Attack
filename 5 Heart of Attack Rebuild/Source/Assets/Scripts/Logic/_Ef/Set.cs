using System;
using System.Text;
using System.Collections.Generic;


namespace HOA.Ef
{
    public class Set : Set<Effect>, IEffect
    {
        public Set() : base () { }

        public Set(Effect e) : base (e) { }

        public Set(IEnumerable<Effect> e) : base(e) { }

        public Action Process { get { return () => { foreach (Effect e in this) e.Process(); }; } }

        public override string ToString() { return base.ToStringLong(); }
    }
}
