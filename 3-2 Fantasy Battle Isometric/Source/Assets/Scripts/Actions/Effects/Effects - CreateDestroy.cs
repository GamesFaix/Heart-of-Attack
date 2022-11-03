using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public class ECreate : Effect {
		public override string ToString () {return "Effect - Create";}
		EToken child; Cell cell;
		
		public ECreate (Source s, EToken newT, Cell c) {
			source = s; child = newT; cell = c;
		}
		public override void Process() {
			Token newToken;
			if (TokenFactory.Add(child, source, cell, out newToken)) {
				newToken.Display.Effect(EEffect.BIRTH);
				Mixer.Play(SoundLoader.Effect(EEffect.BIRTH));
			}
		}
	}
	
	public class EKill : Effect {
		public override string ToString () {return "Effect - Kill";}
		Token target;
		
		public EKill (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			target.Display.Effect(EEffect.DEATH);
			Mixer.Play(SoundLoader.Effect(EEffect.DEATH));
			if (source.Sequence == default(EffectSeq)) {
				EffectQueue.Add(new EKill2(source, target));
			}
			else {
				source.Sequence.AddToNext(new EKill2(source, target));
			}
		}
	}
	
	public class EDestruct : Effect {
		public override string ToString () {return "Effect - Destruct";}
		Token target;
		
		public EDestruct (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			target.Display.Effect(EEffect.DESTRUCT);
			Mixer.Play(SoundLoader.Effect(EEffect.DESTRUCT));
			
			if (source.Sequence == default(EffectSeq)) {
				EffectQueue.Add(new EKill2(source, target));
			}
			else {
				source.Sequence.AddToNext(new EKill2(source, target));
			}
		}
	}
	
	public class EKill2 : Effect {
		public override string ToString () {return "Effect - Kill2";}
		Token target;
		
		public EKill2 (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			target.Die(source);
		}
	}
	
	public class EReplace : Effect {
		public override string ToString () {return "Effect - Replace";}
		Token target; EToken newToken;
		
		public EReplace (Source s, Token t, EToken newT) {
			source = s; target = t; newToken = newT;
		}
		public override void Process() {
			Cell cell = target.Body.Cell;
			target.Die(source, false, false);
			TokenFactory.Add(newToken, source, cell, false);
		}
	}

}