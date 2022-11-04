using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class EffectGroup : Group<Effect>, IEffect {
		public EffectGroup () {list = new List<Effect>();}
		public EffectGroup (Effect e) {list = new List<Effect>{e};}
		public EffectGroup (List<Effect> e) {list = e;}
		public EffectGroup (EffectGroup eg) {
			list = new List<Effect>();
			foreach (Effect e in eg) {list.Add(e);}
		}

		public void Process2 () {
			foreach (Effect e in list) {
				e.Process();
			}
		}
	}
}