using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	public abstract class Effect {
		protected Source source;
		public abstract void Process();
	}

	public class EDamage : Effect {
		Unit target; int dmg;

		public EDamage (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			target.Damage(source, dmg);
			target.SpriteEffect(EEffect.DMG);
		}
	}

	public class EPierce : Effect {
		Unit target; int dmg;
		
		public EPierce (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			target.AddStat(source, EStat.HP, 0-dmg);
			target.SpriteEffect(EEffect.DMG);
		}
	}

	public class EDamageDest : Effect {
		Token target; int dmg;
		
		public EDamageDest (Source s, Token t, int n) {
			source = s; target = t; dmg = n;
		}
		public override void Process() {
			if (target.IsClass(EClass.DEST)) {target.Die(source);}
			
			else if (target is Unit) {
				Unit u = (Unit)target;
				u.Damage(source, dmg);
			}
		}
	}

	public class EExplosion : Effect {
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
				for (int j=0; j<thisRad.Count; j++) {
					Cell next = thisRad[j];
					List<Effect> nextEffects = new List<Effect>();

					if (!affected.Contains(next)) {
						TokenGroup targets = next.Occupants.OnlyClass(new List<EClass> {EClass.UNIT, EClass.DEST});


						foreach (Token t in targets) {
							t.SpriteEffect(EEffect.EXP);
							nextEffects.Add(new EDamageDest(source, t, currentDmg));
						}
						foreach (Cell c in next.Neighbors()) {nextRad.Add(c);}
						affected.Add(next);
					}
					EffectQueue.Add(nextEffects);
				}
				thisRad = nextRad;
				nextRad = new CellGroup();
				currentDmg = (int)Mathf.Floor(currentDmg * 0.5f);
			}
		}
	}
		
	public class EFire : Effect {
		Token target; int dmg;
		
		public EFire (Source s, Token t, int n) {
			source = s; target = t; dmg = n;
		}
		public override void Process() {
			TokenGroup neighbors = target.Neighbors(true);
			neighbors.Remove(source.Token);
			neighbors = neighbors.OnlyClass(new List<EClass> {EClass.UNIT, EClass.DEST});
			
			target.SpriteEffect(EEffect.FIRE);
			EffectQueue.Add(new EDamageDest(source, target, dmg));

			List<Effect> nextEffects = new List<Effect>();

			int newDmg = (int)Mathf.Floor(dmg * 0.5f);
			foreach (Token t2 in neighbors) {
				t2.SpriteEffect(EEffect.FIRE);
				nextEffects.Add(new EDamageDest(source, t2, newDmg));
			}

			EffectQueue.Add(nextEffects);
		}
	}
		
	public class ELaser : Effect {
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
				if (targets.Count > 0) {currentDmg = (int)Mathf.Floor(currentDmg*0.5f);}
				
				int nextX = cell.X-direction[0];
				int nextY = cell.Y-direction[1];
				
				if (!Board.HasCell(nextX, nextY, out cell)) {stop = true;}
			}
		}
	}


	public class ELeech : Effect {
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
		Unit target; int dmg;
		
		public ECorrode (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			int cor = (int)Mathf.Floor(dmg*0.5f);
			target.Damage(source, dmg);
			target.AddStat(source, EStat.COR, cor);
			target.SpriteEffect(EEffect.COR);
		}
	}

	public class EShock : Effect {
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
		EToken child; Cell cell;
		
		public ECreate (Source s, EToken newT, Cell c) {
			source = s; child = newT; cell = c;
		}
		public override void Process() {
			Token newToken;
			TokenFactory.Add(child, source, cell, out newToken);
			newToken.SpriteEffect(EEffect.BIRTH);
		}
	}

	public class EKill : Effect {
		Token target;
		
		public EKill (Source s, Token t) {
			source = s; target = t;
		}
		public override void Process() {
			target.Die(source);
		}
	}

	public class EReplace : Effect {
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

	public class EShift : Effect {
		Unit target; int slots;
		
		public EShift (Source s, Unit u, int n) {
			source = s; target = u; slots = n;
		}
		public override void Process() {
			if (slots > 0) {TurnQueue.MoveUp(target, slots);}
			if (slots < 0) {TurnQueue.MoveDown(target, 0-slots); }
		}
	}	
		
	public class EMove : Effect {
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

	public class ESetStat : Effect {
		Unit target; EStat stat; int newValue;
		
		public ESetStat (Source s, Unit u, EStat st, int n) {
			source = s; target = u; stat = st; newValue = n;
		}
		public override void Process() {
			target.SetStat(source, stat, newValue);
		}
	}	
		
	public class EAddStat : Effect {
		Unit target; EStat stat; int addValue;
		
		public EAddStat (Source s, Unit u, EStat st, int n) {
			source = s; target = u; stat = st; addValue = n;
		}
		public override void Process() {
			target.AddStat(source, stat, addValue);
		}
	}			
		
	public class EGetHeart : Effect {
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

		public EAdvance (Source s) {
			source = s;
		}
		public override void Process() {
			TurnQueue.Advance();
		}
	}	
		
	public class EShuffle : Effect {
		
		public EShuffle (Source s) {
			source = s;
		}
		public override void Process() {
			TurnQueue.Shuffle(source);
		}
	}	
}