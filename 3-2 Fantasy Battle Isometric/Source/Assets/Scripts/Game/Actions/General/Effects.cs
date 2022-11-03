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
			target.SpriteEffect(EEffect.TAILS);
			Mixer.Play(SoundLoader.Effect(EEffect.TAILS));

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
				target.SpriteEffect(EEffect.STATUP);
				Mixer.Play(SoundLoader.Effect(EEffect.STATUP));
			}
			if (addValue < 0) {
				target.SpriteEffect(EEffect.STATDOWN);
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
			source.Player.Capture(target.Owner);
			target.SpriteEffect(EEffect.GETHEART);
			Mixer.Play(SoundLoader.Effect(EEffect.GETHEART));
			GameLog.Out(source.Player.ToString() + " acquired the "+target.ToString()); 
			EffectQueue.Add(new EKill2 (source, target));
		}
	}	
}