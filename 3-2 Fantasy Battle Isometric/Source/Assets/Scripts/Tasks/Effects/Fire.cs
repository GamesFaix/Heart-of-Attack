using UnityEngine; 

namespace HOA.Effects { 

	public class Fire : Effect {
		public override string ToString () {return "Effect - Fire";}
		Token target; int dmg;
		
		public Fire (Source s, Token t, int n) {
			source = s; target = t; dmg = n;
		}
		public override void Process() {
			EffectGroup nextEffects = new EffectGroup();
			
			if (target.Special.Is(ESpecial.DEST)) {
				nextEffects.Add(new Destruct(source, target));
				target.Display.Effect(EEffect.FIRE);
				Mixer.Play(SoundLoader.Effect(EEffect.FIRE));	
			}
			else if (target is Unit) {
				Unit u = (Unit)target;
				u.AddStat(source, EStat.HP, 0-dmg);
				target.Display.Effect(EEffect.FIRE);
				Mixer.Play(SoundLoader.Effect(EEffect.FIRE));	
			}
			
			TokenGroup neighbors = target.Body.Neighbors(true);
			neighbors.Remove(source.Token);
			neighbors = neighbors.OnlyType(Special.UnitDest);
			
			int newDmg = (int)Mathf.Floor(dmg * 0.5f);
			foreach (Token t2 in neighbors) {
				nextEffects.Add(new Fire2(source, t2, newDmg));
			}
			
			EffectQueue.Add(nextEffects);
		}
	}
	
	public class Fire2 : Effect {
		public override string ToString () {return "Effect - Fire2";}
		Token target; int dmg;
		
		public Fire2 (Source s, Token t, int n) {
			source = s; target = t; dmg = n;
		}
		public override void Process() {
			if (target.Special.Is(ESpecial.DEST)) {
				target.Display.Effect(EEffect.FIRE);
				Mixer.Play(SoundLoader.Effect(EEffect.FIRE));
				EffectQueue.Add(new Destruct (source, target));
			}
			
			else if (target is Unit) {
				Unit u = (Unit)target;
				u.AddStat(source, EStat.HP, 0-dmg);
				target.Display.Effect(EEffect.FIRE);
				Mixer.Play(SoundLoader.Effect(EEffect.FIRE));
			}
		}
	}
}
