using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class EAdvance : Effect {		
		public override string ToString () {return "Effect - Advance";}
		
		public EAdvance (Source s) {
			source = s;
		}
		public override void Process() {
			TurnQueue.Advance();
			Mixer.Play(SoundLoader.Effect(EEffect.ADVANCE));
		}
	}	
	
	public class EShuffle : Effect {
		public override string ToString () {return "Effect - Shuffle";}
		public EShuffle (Source s) {
			source = s;
		}
		public override void Process() {
			TurnQueue.Shuffle(source);
			Mixer.Play(SoundLoader.Effect(EEffect.SHUFFLE));
		}
	}	
	
	public class EInitialize : Effect {
		public override string ToString () {return "Effect - Initialize";}
		public EInitialize (Source s) {
			source = s;
		}
		public override void Process() {
			TurnQueue.Initialize();
		}
	}

	public class EShift : Effect {
		public override string ToString () {return "Effect - Shift";}
		Unit target; int slots;
		
		public EShift (Source s, Unit u, int n) {
			source = s; target = u; slots = n;
		}
		public override void Process() {
			if (slots > 0) {
				TurnQueue.MoveUp(target, slots);
				target.Display.Effect(EEffect.STATUP);
				
				Mixer.Play(SoundLoader.Effect(EEffect.STATUP));
			}
			if (slots < 0) {
				TurnQueue.MoveDown(target, 0-slots); 
				target.Display.Effect(EEffect.STATDOWN);
				Mixer.Play(SoundLoader.Effect(EEffect.STATDOWN));	
			}
		}
	}
}