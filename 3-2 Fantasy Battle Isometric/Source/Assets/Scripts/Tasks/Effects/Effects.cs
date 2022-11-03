using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public abstract class Effect : IEffect {
		protected Source source;
		public abstract void Process();
		public abstract override string ToString();
	}
}

namespace HOA.Effects {

	public class Miss : Effect {
		public override string ToString () {return "Effect - Miss";}
		Token target;
		
		public Miss (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			target.Display.Effect(EEffect.MISS);
			Mixer.Play(SoundLoader.Effect(EEffect.MISS));

		}
	}

	public class SetStat : Effect {
		public override string ToString () {return "Effect - Set Stat";}
		Unit target; EStat stat; int newValue;
		
		public SetStat (Source s, Unit u, EStat st, int n) {
			source = s; target = u; stat = st; newValue = n;
		}
		public override void Process() {
			target.SetStat(source, stat, newValue);
		}
	}	
		
	public class AddStat : Effect {
		public override string ToString () {return "Effect - Add Stat";}
		Unit target; EStat stat; int addValue;
		
		public AddStat (Source s, Unit u, EStat st, int n) {
			source = s; target = u; stat = st; addValue = n;
		}
		public override void Process() {

			if (addValue > 0) {
				target.Display.Effect(EEffect.STATUP);
				Mixer.Play(SoundLoader.Effect(EEffect.STATUP));
			}
			if (addValue < 0) {
				target.Display.Effect(EEffect.STATDOWN);
				Mixer.Play(SoundLoader.Effect(EEffect.STATDOWN));			
			}


			target.AddStat(source, stat, addValue);
		}
	}			
		
	public class GetHeart : Effect {
		public override string ToString () {return "Effect - Get Heart";}
		Token target;

		public GetHeart (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			//source.Player.Capture(target.Owner);
			EffectGroup effects = new EffectGroup();

			foreach (Token token in target.Owner.OwnedUnits) {
				effects.Add(new SetOwner(source, token, source.Player));
			}
			target.Display.Effect(EEffect.GETHEART);
			Mixer.Play(SoundLoader.Effect(EEffect.GETHEART));
			GameLog.Out(source.Player.ToString() + " acquired the "+target.ToString()); 
			effects.Add(new Kill2 (source, target));
			EffectQueue.Add(effects);
		}
	}

	public class SetOwner : Effect {
		public override string ToString () {return "Effect - Set Owner";}
		Token target;
		Player owner;

		public SetOwner (Source s, Token t, Player owner) {
			source = s; target = t; this.owner = owner;
		}

		public override void Process() {
			target.Owner = owner;
			target.Display.Effect(EEffect.OWNER);
			Mixer.Play(SoundLoader.Effect(EEffect.OWNER));
			GameLog.Out(owner.ToString() + " acquired "+target.ToString()); 
		}
	}

	public class Stick : Effect {
		public override string ToString () {return "Effect - Stick";}
		Tokens.Web parent;
		Unit target;
		
		public Stick (Source s, Unit u) {
			source = s; target = u;
			parent = (Tokens.Web)source.Token; 
		}
		public override void Process() {
			Task move = target.Arsenal.Move;
			if (move != default(Task)) {
				parent.Affected.Add(target, move.aims[0].range);
				move.aims[0].range = 1;
				Mixer.Play(SoundLoader.Effect(EEffect.STICK));
				target.Display.Effect(EEffect.STICK);
			}
		}
	}
}