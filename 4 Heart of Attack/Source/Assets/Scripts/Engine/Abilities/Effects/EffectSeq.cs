using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class EffectSeq : ListSet<EffectSet>, IEffect {

        public string Name { get; protected set; }
        public new Func<string> ToString;
        public Source Source { get; protected set; }
        public Target Target { get; protected set; }
        public int Modifier { get; protected set; }

		public EffectSeq () {}

		public EffectSeq (Source s, Target t = null, int i = 0) {
            Name = "Default EffectSeq name";
            ToString = () => { return Name + " EffectSeq"; };
			Source = s;
            Target = t;
            Modifier = i;
			list = new List<EffectSet>();
		}
		public EffectSeq (Source s, Effect e) : this (s){
			list.Add(new EffectSet(e));
		}
		public EffectSeq (Source s, EffectSet e) : this(s) {
			list.Add(e);
		}


		public void AddToNext (EffectSet e) {
			if (list.Count>1) {
				list[1].Add(e);
			}
			else {list.Add(e);}
		}
		public void AddToNext (Effect e) {
			if (list.Count>1) {
				list[1].Add(e);
			}
			else {list.Add(new EffectSet(e));}
		}

		public void AddToNext (EffectSeq e) {
			Debug.Log(e[0][0]);

			AddToNext(e[0]);
		}

		public EffectSet Pop () {
			if (list.Count > 0) {
				EffectSet e = list[0];
				list.Remove(e);
				return e;
			}
			return new EffectSet();
		}

		public void Process2 () {
			EffectSet top = Pop();
			top.Process2();
		}
	}
}