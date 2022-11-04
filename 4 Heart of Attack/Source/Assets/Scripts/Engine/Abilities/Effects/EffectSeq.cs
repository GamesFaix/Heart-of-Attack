using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class EffectSeq : Group<EffectGroup>, IEffect {

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
			list = new List<EffectGroup>();
		}
		public EffectSeq (Source s, Effect e) : this (s){
			list.Add(new EffectGroup(e));
		}
		public EffectSeq (Source s, EffectGroup e) : this(s) {
			list.Add(e);
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

		public void Process2 () {
			EffectGroup top = Pop();
			top.Process2();
		}
	}
}