using System;
using System.Collections.Generic;
using HOA.Collections;

namespace HOA.Abilities
{
    public class EffectSet : ListSet<Effect>, IEffect
    {
        public EffectSet() { list = new List<Effect>(); }

        public EffectSet(Effect e) : base () { list.Add(e); }

        public EffectSet(IEnumerable<Effect> e) : base() { list.Add(e); }

        public void Add(EffectSet set) { foreach (Effect e in set) Add(e); }

        public Action Process { get { return () => { foreach (Effect e in list) e.Process(); }; } }
    }
}
