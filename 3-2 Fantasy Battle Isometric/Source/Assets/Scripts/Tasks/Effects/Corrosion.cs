using UnityEngine; 

namespace HOA.Effects { 

	public class Corrode : Effect {
		public override string ToString () {return "Effect - Corrode";}
		Unit target; int dmg;
		
		public Corrode (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			//Debug.Log("effect corrode");
			int cor = (int)Mathf.Floor(dmg*0.5f);
			target.Damage(source, dmg);
			Mixer.Play(SoundLoader.Effect(EEffect.CORRODE));
			target.Display.Effect(EEffect.COR);
			target.timers.Add(new TCorrosion(target, source.Token, cor));
		}
	}
	
	public class Corrode2 : Effect {
		public override string ToString () {return "Effect - Corrode2";}
		Unit target; int dmg;
		
		public Corrode2 (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			target.AddStat(source, EStat.HP, 0-dmg);
			Mixer.Play(SoundLoader.Effect(EEffect.CORRODE));
			target.Display.Effect(EEffect.CORRODE);
		}
	}
}
