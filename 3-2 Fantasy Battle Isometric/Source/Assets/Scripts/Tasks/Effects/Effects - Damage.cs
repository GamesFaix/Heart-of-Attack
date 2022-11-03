using UnityEngine;
using System.Collections.Generic;

namespace HOA.Effects {
	public class Damage : Effect {
		public override string ToString () {return "Effect - Damage";}
		Unit target; int dmg;
		
		public Damage (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			if (target.Damage(source, dmg)) {
				Mixer.Play(SoundLoader.Effect(EEffect.DMG));
				target.Display.Effect(EEffect.DMG);
			}
			else {
				Mixer.Play(SoundLoader.Effect(EEffect.MISS));
				target.Display.Effect(EEffect.MISS);
			}
		}
	}
	
	public class Pierce : Effect {
		public override string ToString () {return "Effect - Pierce";}
		Unit target; int dmg;
		
		public Pierce (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			target.AddStat(source, EStat.HP, 0-dmg);
			target.Display.Effect(EEffect.DMG);
			Mixer.Play(SoundLoader.Effect(EEffect.DMG));
		}
	}

	public class Leech : Effect {
		public override string ToString () {return "Effect - Leech";}
		Unit target; int dmg;
		
		public Leech (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			int oldHP = target.HP;
			if (target.Damage(source, dmg)) {
				Mixer.Play(SoundLoader.Effect(EEffect.DMG));
				target.Display.Effect(EEffect.DMG);
				int actualDmg = oldHP - target.HP;
				Unit Parent = (Unit)(source.Token);
				Parent.AddStat(source, EStat.HP, actualDmg);
				Parent.Display.Effect(EEffect.STATUP);
				Mixer.Play(SoundLoader.Effect(EEffect.STATUP));
			}
			else {
				Mixer.Play(SoundLoader.Effect(EEffect.MISS));
				target.Display.Effect(EEffect.MISS);
			}
		}
	}
	
	public class Donate : Effect {
		public override string ToString () {return "Effect - Donate";}
		Unit target; int dmg;
		
		public Donate (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			int oldHP = target.HP;
			target.AddStat(source, EStat.HP, dmg);
			target.Display.Effect(EEffect.STATUP);
			Mixer.Play(SoundLoader.Effect(EEffect.STATUP));
			int diff = target.HP - oldHP;
			Unit Parent = (Unit)(source.Token);
			Parent.Damage(source, diff);
			Parent.Display.Effect(EEffect.STATDOWN);
			Mixer.Play(SoundLoader.Effect(EEffect.STATDOWN));
		}
	}

	public class Shock : Effect {
		public override string ToString () {return "Effect - Shock";}
		Unit target; int dmg; int stun;
		
		public Shock (Source s, Unit u, int n, int st) {
			source = s; target = u; dmg = n; stun = st;
		}
		public override void Process() {
			target.Damage(source, dmg);
			target.AddStat(source, EStat.STUN, stun);
			target.Display.Effect(EEffect.STUN);
		}
	}
	
	public class Rage : Effect {
		public override string ToString () {return "Effect - Rage";}
		
		Unit target; int dmg;
		
		public Rage (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			if (target.Damage(source, dmg)) {
				target.Display.Effect(EEffect.DMG);
				Mixer.Play(SoundLoader.Effect(EEffect.DMG));
			}
			else {
				Mixer.Play(SoundLoader.Effect(EEffect.MISS));
				target.Display.Effect(EEffect.MISS);
			}
			Unit Parent = (Unit)source.Token;
			Parent.Damage(source, (int)Mathf.Floor(dmg*0.5f));
			Parent.Display.Effect(EEffect.DMG);
			Mixer.Play(SoundLoader.Effect(EEffect.DMG));
		}
	}

	public class Waterlog : Effect {
		public override string ToString () {return "Effect - Waterlog";}
		Unit target; int dmg;
		
		public Waterlog (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			target.Damage(source, dmg);
			Mixer.Play(SoundLoader.Effect(EEffect.WATERLOG));
			target.Display.Effect(EEffect.WATERLOG);
		}
	}
}