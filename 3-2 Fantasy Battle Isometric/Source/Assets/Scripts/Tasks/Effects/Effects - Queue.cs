using UnityEngine;
using System.Collections.Generic;

namespace HOA.Effects {

	public class Advance : Effect {		
		bool log;
		public override string ToString () {return "Effect - Advance";}
		public Advance (Source s, bool log=true) {source = s; this.log=log;}
		public override void Process() {
			TurnQueue.Advance();
			if (log) {GameLog.Out(source+" ended the turn.");}
			Mixer.Play(SoundLoader.Effect(EEffect.ADVANCE));
		}
	}	
	
	public class Shuffle : Effect {
		public override string ToString () {return "Effect - Shuffle";}
		public Shuffle (Source s) {source = s;}
		public override void Process() {
			TurnQueue.Shuffle();
			GameLog.Out(source+" shuffled the Queue.");
			Mixer.Play(SoundLoader.Effect(EEffect.SHUFFLE));
		}
	}	
	
	public class Initialize : Effect {
		public override string ToString () {return "Effect - Initialize";}
		public Initialize (Source s) {source = s;}
		public override void Process() {
			TurnQueue.Initialize();
		}
	}

	public class Shift : Effect {
		public override string ToString () {return "Effect - Shift";}
		Unit target; int slots;
		public Shift (Source s, Unit u, int n) {source = s; target = u; slots = n;}
		public override void Process() {
			if (slots > 0) {
				TurnQueue.MoveUp(target, slots);
				GameLog.Out(target+" moved up "+slots+" slot(s) in the Queue.");
				target.Display.Effect(EEffect.STATUP);
				Mixer.Play(SoundLoader.Effect(EEffect.STATUP));
			}
			if (slots < 0) {
				TurnQueue.MoveDown(target, 0-slots); 
				GameLog.Out(target+" moved down "+slots+" slot(s) in the Queue.");
				target.Display.Effect(EEffect.STATDOWN);
				Mixer.Play(SoundLoader.Effect(EEffect.STATDOWN));	
			}
		}
	}
}