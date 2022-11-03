using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public abstract class Effect : IEffect {
		protected Source source;
		public abstract void Process();
		public abstract override string ToString();
	}

	public class ETails : Effect {
		public override string ToString () {return "Effect - Tails";}
		Token target;
		
		public ETails (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			target.Display.Effect(EEffect.MISS);
			Mixer.Play(SoundLoader.Effect(EEffect.MISS));

		}
	}

	public class ESetStat : Effect {
		public override string ToString () {return "Effect - Set Stat";}
		Unit target; EStat stat; int newValue;
		
		public ESetStat (Source s, Unit u, EStat st, int n) {
			source = s; target = u; stat = st; newValue = n;
		}
		public override void Process() {
			target.SetStat(source, stat, newValue);
		}
	}	
		
	public class EAddStat : Effect {
		public override string ToString () {return "Effect - Add Stat";}
		Unit target; EStat stat; int addValue;
		
		public EAddStat (Source s, Unit u, EStat st, int n) {
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
		
	public class EGetHeart : Effect {
		public override string ToString () {return "Effect - Get Heart";}
		Token target;

		public EGetHeart (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			//source.Player.Capture(target.Owner);
			EffectGroup effects = new EffectGroup();

			foreach (Token token in target.Owner.OwnedUnits) {
				effects.Add(new ESetOwner(source, token, source.Player));
			}
			target.Display.Effect(EEffect.GETHEART);
			Mixer.Play(SoundLoader.Effect(EEffect.GETHEART));
			GameLog.Out(source.Player.ToString() + " acquired the "+target.ToString()); 
			effects.Add(new EKill2 (source, target));
			EffectQueue.Add(effects);
		}
	}

	public class ESetOwner : Effect {
		public override string ToString () {return "Effect - Set Owner";}
		Token target;
		Player owner;

		public ESetOwner (Source s, Token t, Player owner) {
			source = s; target = t; this.owner = owner;
		}

		public override void Process() {
			target.Owner = owner;
			target.Display.Effect(EEffect.OWNER);
			Mixer.Play(SoundLoader.Effect(EEffect.OWNER));
			GameLog.Out(owner.ToString() + " acquired "+target.ToString()); 
		}
	}
}