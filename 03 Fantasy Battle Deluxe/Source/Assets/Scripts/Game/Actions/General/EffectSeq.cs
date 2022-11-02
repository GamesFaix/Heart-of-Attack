using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class EffectSeq : Group<IEffect>, IEffect {
		public EffectSeq () {list = new List<IEffect>();}
		public EffectSeq (Effect e) {list = new List<IEffect>{e};}
		public EffectSeq (List<IEffect> e) {list = e;}
		public EffectSeq (EffectSeq es) {
			list = new List<IEffect>();
			foreach (Effect e in es) {list.Add(e);}
		}

		public IEffect Pop () {
			IEffect e = default(IEffect);
			if (list.Count > 0) {e = list[0];}
			list.Remove(e);
			return e;
		}

		public void Process () {
			IEffect top = Pop();
			top.Process();
		}
	}
}