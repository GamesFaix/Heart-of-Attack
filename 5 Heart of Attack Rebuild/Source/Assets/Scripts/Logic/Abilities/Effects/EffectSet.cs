using System;
using System.Text;
using System.Collections.Generic;


namespace HOA.Abilities
{
    public class EffectSet : Set<Effect>, IEffect
    {
        public EffectSet() : base () { }

        public EffectSet(Effect e) : base (e) { }

        public EffectSet(IEnumerable<Effect> e) : base(e) { }

        public Action Process { get { return () => { foreach (Effect e in this) e.Process(); }; } }

        public override string ToString() { return base.ToStringLong(); }
    }
}
