using UnityEngine;
using System.Collections.Generic;

namespace HOA.Effects {
	public class Create : Effect {
		public override string ToString () {return "Effect - Create";}
		EToken child; Cell cell;
		
		public Create (Source s, EToken newT, Cell c) {
			source = s; child = newT; cell = c;
		}
		public override void Process() {
			Token newToken;
			if (TokenFactory.Create(source, child, cell, out newToken)) {
				newToken.Display.Effect(EEffect.BIRTH);
				Mixer.Play(SoundLoader.Effect(EEffect.BIRTH));
			}
		}
	}
	
	public class Kill : Effect {
		public override string ToString () {return "Effect - Kill";}
		Token target;
		
		public Kill (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			target.Display.Effect(EEffect.DEATH);
			Mixer.Play(SoundLoader.Effect(EEffect.DEATH));
			if (source.Sequence == default(EffectSeq)) {
				EffectQueue.Add(new Kill2(source, target));
			}
			else {
				source.Sequence.AddToNext(new Kill2(source, target));
			}
		}
	}
	
	public class Destruct : Effect {
		public override string ToString () {return "Effect - Destruct";}
		Token target;
		
		public Destruct (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			target.Display.Effect(EEffect.DESTRUCT);
			Mixer.Play(SoundLoader.Effect(EEffect.DESTRUCT));
			
			if (source.Sequence == default(EffectSeq)) {
				EffectQueue.Add(new Kill2(source, target));
			}
			else {
				source.Sequence.AddToNext(new Kill2(source, target));
			}
		}
	}
	
	public class Kill2 : Effect {
		public override string ToString () {return "Effect - Kill2";}
		Token target;
		
		public Kill2 (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			target.Die(source);
		}
	}
	
	public class Replace : Effect {
		public override string ToString () {return "Effect - Replace";}
		Token target; EToken newToken;
		
		public Replace (Source s, Token t, EToken newT) {
			source = s; target = t; newToken = newT;
		}
		public override void Process() {
			Cell cell = target.Body.Cell;
			target.Die(source, false, false);
			TokenFactory.Create(source, newToken, cell, false);
		}
	}

	public class Detonate : EffectSeq {
		public override string ToString () {return "EffectSeq - Detonate";}
		Token target;
		
		public Detonate (Source s, Token t) {
			source = s; target = t;
			
			list = new List<EffectGroup>();
			
			EffectGroup group = new EffectGroup();
			group.Add(new Detonate1 (new Source(source.Token, this), target));
			list.Add(group);
		}
	}
	
	public class Detonate1 : Effect {
		public override string ToString () {return "Effect - Detonate1";}
		Token target;
		
		public Detonate1 (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			Mixer.Play(SoundLoader.Effect(EEffect.DETONATE));
			target.Display.Effect(EEffect.DETONATE);
			source.Sequence.AddToNext(new Detonate2(source, target));
		}
		
	}
	
	public class Detonate2 : Effect {
		public override string ToString () {return "Effect - Detonate2";}
		Token target;
		
		public Detonate2 (Source s, Token t) {
			source = s; target = t;
			Debug.Log(ToString());
		}
		public override void Process() {
			Debug.Log("processing "+ToString());
			target.Die(source);
		}
	}
}