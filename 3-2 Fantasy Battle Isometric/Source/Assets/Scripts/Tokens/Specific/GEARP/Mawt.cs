using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public class Mawth : Unit {
		public static Token Instantiate (Source source, bool template) {
			return new Mawth (source, template);
		}

		Mawth (Source s, bool template=false){
			ID = new ID(this, EToken.MAWT, s, false, template);
			Plane = Plane.Air;
			ScaleLarge();
			NewHealth(55);
			NewWatch(3);
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMoveLine(this, 4),
				new AMawtLaser(this),
				new AMawtBomb(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}




	public class EMawtExplosion : EffectSeq {
		public override string ToString () {return "Effect - Mawth Explosion";}
		Cell target; int dmg;
		
		public EMawtExplosion (Source s, Cell c, int n) {
			source = s; target = c; dmg = n;
		
			list = new List<EffectGroup>();

			CellGroup affected = new CellGroup();
			CellGroup thisRad = new CellGroup(target);
			CellGroup nextRad = new CellGroup();
			
			int currentDmg = dmg;

		//	EffectSeq sequence = new EffectSeq();

			while (currentDmg > 0) {
				EffectGroup group = new EffectGroup();
				for (int j=0; j<thisRad.Count; j++) {
					Cell next = thisRad[j];
					
					if (!affected.Contains(next)) {
						if (next.Occupants.Count > 0) {
							group.Add(new EMawtExplosion2 (new Source(source.Token, this), next, currentDmg));
						}
						else {
							group.Add(new EExplosionDummy (new Source (source.Token, this), next));
						}
						foreach (Cell cell in next.Neighbors()) {nextRad.Add(cell);}
						affected.Add(next);
					}
				}
				//sequence.Add(nextEffects);
				//EffectQueue.Interrupt(nextEffects);
				thisRad = nextRad;
				nextRad = new CellGroup();
				currentDmg = (int)Mathf.Floor(currentDmg * 0.5f);
				list.Add(group);
			}
		//	EffectQueue.Add(sequence);
		}
	}
	
	public class EMawtExplosion2 : Effect {
		public override string ToString () {return "Effect - Mawth Explosion2";}
		Cell cell; int dmg;
		
		public EMawtExplosion2 (Source s, Cell c, int n) {
			source = s; cell = c; dmg = n;
		}
		public override void Process() {
			EffectGroup nextEffects = new EffectGroup();
			TokenGroup targets = cell.Occupants.OnlyType(Special.UnitDest);
			
			foreach (Token t in targets) {
				t.Display.Effect(EEffect.EXP);
				Mixer.Play(SoundLoader.Effect(EEffect.EXP));
				if (t.Special.Is(EType.DEST)) {
					source.Sequence.AddToNext(new EDestruct(source, t));
				}
				
				else if (t is Unit && t!=source.Token) {
					Unit u = (Unit)t;
					u.Damage(source, dmg);
				}
			}		
			EffectQueue.Add(nextEffects);
		}
	}
}