using UnityEngine; 
using System.Collections.Generic;

namespace HOA.Effects { 

	public class Explosion : EffectSeq {
		public override string ToString () {return "EffectSeq - Explosion";}
		Cell target; int dmg;
		
		public Explosion (Source s, Cell c, int n, bool selfImmune=false) {
			source = s; target = c; dmg = n;
			
			list = new List<EffectGroup>();
			
			CellGroup affected = new CellGroup();
			CellGroup thisRad = new CellGroup(target);
			CellGroup nextRad = new CellGroup();
			
			int currentDmg = dmg;

			int i=0;
			while (currentDmg > 0 && i<=2) {
				EffectGroup group = new EffectGroup();
				for (int j=0; j<thisRad.Count; j++) {
					Cell next = thisRad[j];
					
					if (!affected.Contains(next)) {
						if (next.Occupants.Count > 0) {
							group.Add(new Explosion2 (new Source (source.Token, this), next, currentDmg, selfImmune));
						}
						else {
							group.Add(new ExplosionDummy (new Source (source.Token, this), next));
						}
						foreach (Cell cell in next.Neighbors()) {nextRad.Add(cell);}
						affected.Add(next);
					}
				}
				thisRad = nextRad;
				nextRad = new CellGroup();
				currentDmg = (int)Mathf.Floor(currentDmg * 0.5f);
				list.Add(group);
				i++;
			}
		}
	}

	public class ExplosionDummy : Effect {
		public override string ToString () {return "Effect - Explosion Dummy";}
		Cell cell;
		
		public ExplosionDummy (Source s, Cell c) {
			source = s; 
			cell = c;
		}
		
		public override void Process() {
			cell.Display.Effect(EEffect.EXP);
		}
	}

	public class Explosion2 : Effect {
		public override string ToString () {return "Effect - Explosion2";}
		Cell cell; int dmg;
		bool selfImmune;

		public Explosion2 (Source s, Cell c, int n, bool selfImmune) {
			source = s; 
			cell = c; dmg = n;
			this.selfImmune = selfImmune;
		}
		
		public override void Process() {
			TokenGroup targets = cell.Occupants.OnlyType(Special.UnitDest);
			if (selfImmune) {targets.Remove(source.Token);}
			
			foreach (Token t in targets) {
				if (t.Special.Is(ESpecial.DEST)) {
					t.Display.Effect(EEffect.EXP);
					Mixer.Play(SoundLoader.Effect(EEffect.EXP));
					source.Sequence.AddToNext(new Destruct(source, t));
				}
				
				else if (t is Unit) {
					Unit u = (Unit)t;
					if (u.Damage(source, dmg)) {
						t.Display.Effect(EEffect.EXP);
						Mixer.Play(SoundLoader.Effect(EEffect.EXP));
					}
					else {
						t.Display.Effect(EEffect.MISS);
						Mixer.Play(SoundLoader.Effect(EEffect.MISS));
					}
				}
			}		
		}
	}
}
