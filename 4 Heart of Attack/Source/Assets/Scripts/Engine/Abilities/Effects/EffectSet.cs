using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class EffectSet : ListSet<Effect>, IEffect {
		
		public EffectSet()
        {
            list = new List<Effect>();
        }

        public EffectSet(Effect e) : this() { Add(e); }

        public void Add(EffectSet set)
        {
            foreach (Effect e in set)
                Add(e);
        }
        
        
        public void Process2 () {
			foreach (Effect e in list) {
				e.Process();
			}
		}


	}
}