using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class EffectSeq : Group<EffectGroup>, IEffect {

		public Source source;

		public EffectSeq () {}

		public EffectSeq (Source s) {
			source = s;
			list = new List<EffectGroup>();
		}
		public EffectSeq (Source s, Effect e) {
			source = s;
			list = new List<EffectGroup>{new EffectGroup(e)};
		}
		public EffectSeq (Source s, EffectGroup e) {
			source = s;
			list = new List<EffectGroup> {e};
		}

		public void AddToNext (EffectGroup e) {
			if (list.Count>1) {
				list[1].Add(e);
			}
			else {list.Add(e);}
		}
		public void AddToNext (Effect e) {
			if (list.Count>1) {
				list[1].Add(e);
			}
			else {list.Add(new EffectGroup(e));}
		}

		public void AddToNext (EffectSeq e) {
			Debug.Log(e[0][0]);

			AddToNext(e[0]);
		}

		public EffectGroup Pop () {
			if (list.Count > 0) {
				EffectGroup e = list[0];
				list.Remove(e);
				return e;
			}
			return new EffectGroup();
		}

		public void Process () {
			EffectGroup top = Pop();
			top.Process();
		}
	}
}