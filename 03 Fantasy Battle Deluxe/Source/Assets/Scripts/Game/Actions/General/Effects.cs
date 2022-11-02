using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public abstract class Effect : IEffect {
		protected Source source;
		public abstract void Process();
		public abstract override string ToString();
	}

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


	public class EExplosion : Effect {
		public override string ToString () {return "Effect - Explosion";}
		Cell target; int dmg;
		
		public EExplosion (Source s, Cell c, int n) {
			source = s; target = c; dmg = n;
		}
		public override void Process() {
			CellGroup affected = new CellGroup();
			CellGroup thisRad = new CellGroup(target);
			CellGroup nextRad = new CellGroup();
			
			int currentDmg = dmg;
			
			while (currentDmg > 0) {
				EffectGroup nextEffects = new EffectGroup();
				for (int j=0; j<thisRad.Count; j++) {
					Cell next = thisRad[j];

					if (!affected.Contains(next)) {
						if (next.Occupants.Count > 0) {
							nextEffects.Add(new EExplosion2 (source, next, currentDmg));
						}
						foreach (Cell c in next.Neighbors()) {nextRad.Add(c);}
						affected.Add(next);
					}
				}
				EffectQueue.Interrupt(nextEffects);
				thisRad = nextRad;
				nextRad = new CellGroup();
				currentDmg = (int)Mathf.Floor(currentDmg * 0.5f);
			}
		}
	}

	public class EExplosion2 : Effect {
		public override string ToString () {return "Effect - Explosion2";}
		Cell cell; int dmg;
		
		public EExplosion2 (Source s, Cell c, int n) {
			source = s; cell = c; dmg = n;
		}
		public override void Process() {
			EffectGroup nextEffects = new EffectGroup();
			TokenGroup targets = cell.Occupants.OnlyClass(new List<EClass> {EClass.UNIT, EClass.DEST});
			
			foreach (Token t in targets) {
				t.SpriteEffect(EEffect.EXP);
				Mixer.Play(SoundLoader.Effect(EEffect.EXP));
				if (t.IsClass(EClass.DEST)) {
					nextEffects.Add(new EDestruct(source, t));
				}
				
				else if (t is Unit) {
					Unit u = (Unit)t;
					u.Damage(source, dmg);
				}
			}		
			EffectQueue.Interrupt(nextEffects);
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
			target.SpriteEffect(EEffect.DMG);
			int actualDmg = oldHP - target.HP;
			Unit actor = (Unit)(source.Token);
			actor.AddStat(source, EStat.HP, actualDmg);
			actor.SpriteEffect(EEffect.STATUP);
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
			int diff = target.HP - oldHP;
			Unit actor = (Unit)(source.Token);
			actor.Damage(source, diff);
			actor.SpriteEffect(EEffect.STATDOWN);
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
			Unit actor = (Unit)source.Token;
			actor.Damage(source, (int)Mathf.Floor(dmg*0.5f));
			actor.SpriteEffect(EEffect.DMG);
		}
	}

	public class ECreate : Effect {
		public override string ToString () {return "Effect - Create";}
		EToken child; Cell cell;
		
		public ECreate (Source s, EToken newT, Cell c) {
			source = s; child = newT; cell = c;
		}
		public override void Process() {
			Token newToken;
			TokenFactory.Add(child, source, cell, out newToken);
			newToken.SpriteEffect(EEffect.BIRTH);
			
			Mixer.Play(SoundLoader.Effect(EEffect.BIRTH));
		}
	}

	public class EKill : Effect {
		public override string ToString () {return "Effect - Kill";}
		Token target;
		
		public EKill (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			target.SpriteEffect(EEffect.DEATH);
			Mixer.Play(SoundLoader.Effect(EEffect.DEATH));
			EffectQueue.Interrupt(new EKill2(source, target));
		}
	}

	public class EDestruct : Effect {
		public override string ToString () {return "Effect - Destruct";}
		Token target;
		
		public EDestruct (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			target.SpriteEffect(EEffect.DESTRUCT);
			Mixer.Play(SoundLoader.Effect(EEffect.DESTRUCT));

			EffectQueue.Interrupt(new EKill2(source, target));
		}
	}
	
	public class EKill2 : Effect {
		public override string ToString () {return "Effect - Kill2";}
		Token target;
		
		public EKill2 (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			target.Die(source);
		}
	}

	public class EReplace : Effect {
		public override string ToString () {return "Effect - Replace";}
		Token target; EToken newToken;
		
		public EReplace (Source s, Token t, EToken newT) {
			source = s; target = t; newToken = newT;
		}
		public override void Process() {
			Cell cell = target.Cell;
			target.Die(source, false, false);
			TokenFactory.Add(newToken, source, cell, false);
		}
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

	public class EShift : Effect {
		public override string ToString () {return "Effect - Shift";}
		Unit target; int slots;
		
		public EShift (Source s, Unit u, int n) {
			source = s; target = u; slots = n;
		}
		public override void Process() {
			if (slots > 0) {
				TurnQueue.MoveUp(target, slots);
				target.SpriteEffect(EEffect.STATUP);
				
				Mixer.Play(SoundLoader.Effect(EEffect.STATUP));
			}
			if (slots < 0) {
				TurnQueue.MoveDown(target, 0-slots); 
				target.SpriteEffect(EEffect.STATDOWN);
				Mixer.Play(SoundLoader.Effect(EEffect.STATDOWN));	
			}
		}
	}	
		
	public class EMove : Effect {
		public override string ToString () {return "Effect - Move";}
		Token target; Cell cell;
		
		public EMove (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			Cell oldCell = target.Cell;
			target.SpriteMove(cell);
			target.Enter(cell);
			Cell newCell = target.Cell;
			GameLog.Out (target+" moved from "+oldCell+" to "+newCell+".");
		}
	}	

	public class EBurrow : Effect {
		public override string ToString () {return "Effect - Burrow";}
		Token target; Cell cell;
		
		public EBurrow (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			Cell oldCell = target.Cell;

			target.SpriteEffect(EEffect.BURROW);

			EffectQueue.Add(new EBurrow2(source, target, cell));


			GameLog.Out (target+" burrow from "+oldCell+" to "+cell+".");
		}
	}	

	public class EBurrow2 : Effect {
		public override string ToString () {return "Effect - Burrow2";}
		Token target; Cell cell;
		
		public EBurrow2 (Source s, Token t, Cell c) {
			source = s; target = t; cell = c;
		}
		public override void Process() {
			target.Enter(cell);
			target.SpriteEffect(EEffect.BURROW);
		}
	}	
	




	public class ESwap : Effect {
		public override string ToString () {return "Effect - Swap";}
		Token target; Token other;
		
		public ESwap (Source s, Token t, Token t2) {
			source = s; target = t; other = t2;
		}
		public override void Process() {
			target.SpriteMove(other.Cell);
			other.SpriteMove(target.Cell);
			target.Swap(other);
			GameLog.Out (target+" swapped places with "+other+".");
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
			GameLog.Out(source.Player.ToString() + " acquired the "+target.ToString()); 
			target.Die(source);
		}
	}	
			
	public class EAdvance : Effect {		
		public override string ToString () {return "Effect - Advance";}

		public EAdvance (Source s) {
			source = s;
		}
		public override void Process() {
			TurnQueue.Advance();
			Mixer.Play(SoundLoader.Effect(EEffect.ADVANCE));
		}
	}	
		
	public class EShuffle : Effect {
		public override string ToString () {return "Effect - Shuffle";}
		public EShuffle (Source s) {
			source = s;
		}
		public override void Process() {
			TurnQueue.Shuffle(source);
			Mixer.Play(SoundLoader.Effect(EEffect.SHUFFLE));
		}
	}	

	public class EInitialize : Effect {
		public override string ToString () {return "Effect - Initialize";}
		public EInitialize (Source s) {
			source = s;
		}
		public override void Process() {
			TurnQueue.Initialize();
		}
	}
}