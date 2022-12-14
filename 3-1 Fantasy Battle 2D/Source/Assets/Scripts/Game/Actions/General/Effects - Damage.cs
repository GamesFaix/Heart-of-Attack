using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public class EDamage : Effect {
		public override string ToString () {return "Effect - Damage";}
		Unit target; int dmg;
		
		public EDamage (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			target.Damage(source, dmg);
			Mixer.Play(SoundLoader.Effect(EEffect.DMG));
			target.SpriteEffect(EEffect.DMG);
		}
	}
	
	public class EPierce : Effect {
		public override string ToString () {return "Effect - Pierce";}
		Unit target; int dmg;
		
		public EPierce (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			target.AddStat(source, EStat.HP, 0-dmg);
			target.SpriteEffect(EEffect.DMG);
			Mixer.Play(SoundLoader.Effect(EEffect.DMG));
		}
	}
	
	
	public class EExplosion : EffectSeq {
		public override string ToString () {return "EffectSeq - Explosion";}
		Cell target; int dmg;
		
		public EExplosion (Source s, Cell c, int n) {
			source = s; target = c; dmg = n;
			
			list = new List<EffectGroup>(){new EffectGroup()};
			
			CellGroup affected = new CellGroup();
			CellGroup thisRad = new CellGroup(target);
			CellGroup nextRad = new CellGroup();
			
			int currentDmg = dmg;
			
			
			while (currentDmg > 0) {
				EffectGroup group = new EffectGroup();
				for (int j=0; j<thisRad.Count; j++) {
					Cell next = thisRad[j];
					
					if (!affected.Contains(next)) {
						if (next.Occupants.Count > 0) {
							group.Add(new EExplosion2 (new Source (source.Token, this), next, currentDmg));
						}
						else {
							group.Add(new EExplosionDummy (new Source (source.Token, this), next));
						}
						foreach (Cell cell in next.Neighbors()) {nextRad.Add(cell);}
						affected.Add(next);
					}
				}
				thisRad = nextRad;
				nextRad = new CellGroup();
				currentDmg = (int)Mathf.Floor(currentDmg * 0.5f);
				list.Add(group);
			}
		}
	}
	
	public class EExplosionDummy : Effect {
		public override string ToString () {return "Effect - Explosion Dummy";}
		Cell cell;
		
		public EExplosionDummy (Source s, Cell c) {
			source = s; 
			cell = c;
		}
		
		public override void Process() {
			cell.SpriteEffect(EEffect.EXP);
		}
	}
	
	
	public class EExplosion2 : Effect {
		public override string ToString () {return "Effect - Explosion2";}
		Cell cell; int dmg;
		
		public EExplosion2 (Source s, Cell c, int n) {
			source = s; 
			cell = c; dmg = n;
		}
		
		public override void Process() {
			TokenGroup targets = cell.Occupants.OnlyClass(new List<EClass> {EClass.UNIT, EClass.DEST});
			
			foreach (Token t in targets) {
				t.SpriteEffect(EEffect.EXP);
				Mixer.Play(SoundLoader.Effect(EEffect.EXP));
				if (t.IsClass(EClass.DEST)) {
					source.Sequence.AddToNext(new EDestruct(source, t));
				}
				
				else if (t is Unit) {
					Unit u = (Unit)t;
					u.Damage(source, dmg);
				}
			}		
		}
	}
	
	public class EFire : Effect {
		public override string ToString () {return "Effect - Fire";}
		Token target; int dmg;
		
		public EFire (Source s, Token t, int n) {
			source = s; target = t; dmg = n;
		}
		public override void Process() {
			EffectGroup nextEffects = new EffectGroup();
			
			target.SpriteEffect(EEffect.FIRE);
			
			Mixer.Play(SoundLoader.Effect(EEffect.FIRE));	
			
			if (target.IsClass(EClass.DEST)) {
				nextEffects.Add(new EDestruct(source, target));
			}
			else if (target is Unit) {
				Unit u = (Unit)target;
				u.Damage(source, dmg);
			}
			
			TokenGroup neighbors = target.Neighbors(true);
			neighbors.Remove(source.Token);
			neighbors = neighbors.OnlyClass(new List<EClass> {EClass.UNIT, EClass.DEST});
			
			int newDmg = (int)Mathf.Floor(dmg * 0.5f);
			foreach (Token t2 in neighbors) {
				nextEffects.Add(new EFire2(source, t2, newDmg));
			}
			
			EffectQueue.Add(nextEffects);
		}
	}
	
	public class EFire2 : Effect {
		public override string ToString () {return "Effect - Fire2";}
		Token target; int dmg;
		
		public EFire2 (Source s, Token t, int n) {
			source = s; target = t; dmg = n;
		}
		public override void Process() {
			target.SpriteEffect(EEffect.FIRE);
			Mixer.Play(SoundLoader.Effect(EEffect.FIRE));
			
			if (target.IsClass(EClass.DEST)) {
				EffectQueue.Add(new EDestruct (source, target));
			}
			
			else if (target is Unit) {
				Unit u = (Unit)target;
				u.Damage(source, dmg);
			}
		}
	}
	
	public class ELaser : Effect {
		public override string ToString () {return "Effect - Laser";}
		Unit target; int dmg;
		
		public ELaser (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			Token actor = source.Token;
			int currentDmg = dmg;
			Cell cell = target.Cell;
			int[] direction = Direction.FromCells(cell, actor.Cell);
			bool stop = false;
			
			TokenGroup targets;
			
			while (currentDmg > 0 && !stop) {
				targets = cell.Occupants;
				
				TokenGroup blockers = new TokenGroup (targets);
				blockers = blockers.OnlyClass(EClass.OB);
				blockers = blockers.RemovePlane(EPlane.SUNK);
				
				if (blockers.Count > 0) {
					stop = true; 
					Debug.Log("obstacle hit");
				}
				foreach (Token t in targets.OnlyClass(EClass.UNIT)) {
					((Unit)t).Damage(source, currentDmg);
					t.SpriteEffect(EEffect.LASER);
				}
				Mixer.Play(SoundLoader.Effect(EEffect.LASER));
				if (targets.Count > 0) {currentDmg = (int)Mathf.Floor(currentDmg*0.5f);}
				
				int nextX = cell.X-direction[0];
				int nextY = cell.Y-direction[1];
				
				if (!Board.HasCell(nextX, nextY, out cell)) {stop = true;}
			}
		}
	}
	
	
	public class ELeech : Effect {
		public override string ToString () {return "Effect - Leech";}
		Unit target; int dmg;
		
		public ELeech (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			int oldHP = target.HP;
			target.Damage(source, dmg);
			Mixer.Play(SoundLoader.Effect(EEffect.DMG));
			target.SpriteEffect(EEffect.DMG);
			int actualDmg = oldHP - target.HP;
			Unit actor = (Unit)(source.Token);
			actor.AddStat(source, EStat.HP, actualDmg);
			actor.SpriteEffect(EEffect.STATUP);
			Mixer.Play(SoundLoader.Effect(EEffect.STATUP));
		}
	}
	
	public class EDonate : Effect {
		public override string ToString () {return "Effect - Donate";}
		Unit target; int dmg;
		
		public EDonate (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			int oldHP = target.HP;
			target.AddStat(source, EStat.HP, dmg);
			target.SpriteEffect(EEffect.STATUP);
			Mixer.Play(SoundLoader.Effect(EEffect.STATUP));
			int diff = target.HP - oldHP;
			Unit actor = (Unit)(source.Token);
			actor.Damage(source, diff);
			actor.SpriteEffect(EEffect.STATDOWN);
			Mixer.Play(SoundLoader.Effect(EEffect.STATDOWN));
		}
	}
	
	public class ECorrode : Effect {
		public override string ToString () {return "Effect - Corrode";}
		Unit target; int dmg;
		
		public ECorrode (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			//Debug.Log("effect corrode");
			int cor = (int)Mathf.Floor(dmg*0.5f);
			target.Damage(source, dmg);
			target.AddStat(source, EStat.COR, cor);
			
			Mixer.Play(SoundLoader.Effect(EEffect.CORRODE));
			target.SpriteEffect(EEffect.COR);
		}
	}
	
	public class ECorrode2 : Effect {
		public override string ToString () {return "Effect - Corrode2";}
		Unit target; int dmg;
		
		public ECorrode2 (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			target.Damage(source, dmg);
			Mixer.Play(SoundLoader.Effect(EEffect.CORRODE));
			target.SpriteEffect(EEffect.CORRODE);
		}
	}
	
	public class EShock : Effect {
		public override string ToString () {return "Effect - Shock";}
		Unit target; int dmg; int stun;
		
		public EShock (Source s, Unit u, int n, int st) {
			source = s; target = u; dmg = n; stun = st;
		}
		public override void Process() {
			target.Damage(source, dmg);
			target.AddStat(source, EStat.STUN, stun);
			target.SpriteEffect(EEffect.STUN);
		}
	}
	
	public class ERage : Effect {
		public override string ToString () {return "Effect - Rage";}
		
		Unit target; int dmg;
		
		public ERage (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			target.Damage(source, dmg);
			target.SpriteEffect(EEffect.DMG);
			Mixer.Play(SoundLoader.Effect(EEffect.DMG));
			Unit actor = (Unit)source.Token;
			actor.Damage(source, (int)Mathf.Floor(dmg*0.5f));
			actor.SpriteEffect(EEffect.DMG);
			Mixer.Play(SoundLoader.Effect(EEffect.DMG));
		}
	}
}