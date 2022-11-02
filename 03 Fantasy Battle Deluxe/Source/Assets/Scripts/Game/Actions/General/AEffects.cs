using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class AEffects {

		public static void Damage (Source s, Unit u, int n) {
			u.Damage(s, n);
			u.SpriteEffect(EEffect.DMG);
		}
		
		public static void Pierce (Source s, Unit u, int n) {
			u.AddStat(s, EStat.HP, -n);
			u.SpriteEffect(EEffect.DMG);
		}

		public static void DamageDest (Source s, Token t, int n) {
			if (t.IsClass(EClass.DEST)) {t.Die(s);}

			else if (t is Unit) {
				Unit u = (Unit)t;
				u.Damage(s, n);
			}
		}

		public static void Explosion (Source s, Cell start, int n) {

			CellGroup affected = new CellGroup();
			CellGroup thisRad = new CellGroup(start);
			CellGroup nextRad = new CellGroup();
			
			int dmg = n;
			
			while (dmg > 0) {
				for (int j=0; j<thisRad.Count; j++) {
					Cell next = thisRad[j];
					
					if (!affected.Contains(next)) {
						TokenGroup targets = next.Occupants.OnlyClass(new List<EClass> {EClass.UNIT, EClass.DEST});
						
						foreach (Token t in targets) {
							t.SpriteEffect(EEffect.EXP);
							DamageDest(s, t, dmg);
						}
						foreach (Cell c in next.Neighbors()) {nextRad.Add(c);}
						affected.Add(next);
					}
				}
				thisRad = nextRad;
				nextRad = new CellGroup();
				dmg = (int)Mathf.Floor(dmg * 0.5f);
			}

		}

		public static void Fire (Source s, Token t, int n) {
			TokenGroup neighbors = t.Neighbors(true);
			neighbors.Remove(s.Token);
			neighbors = neighbors.OnlyClass(new List<EClass> {EClass.UNIT, EClass.DEST});
			
			t.SpriteEffect(EEffect.FIRE);
			DamageDest(s, t, n);
			
			int newDmg = (int)Mathf.Floor(n * 0.5f);
			foreach (Token t2 in neighbors) {
				t2.SpriteEffect(EEffect.FIRE);
				DamageDest(s, t2, newDmg);
			}
		}

		public static void Laser (Source s, Unit u, int n) {
			Token actor = s.Token;
			int dmg = n;
			Cell cell = u.Cell;
			int[] direction = Direction.FromCells(cell, actor.Cell);
			bool stop = false;
			
			TokenGroup targets;
			
			while (dmg > 0 && !stop) {
				targets = cell.Occupants;

				TokenGroup blockers = new TokenGroup (targets);
				blockers = blockers.OnlyClass(EClass.OB);
				blockers = blockers.RemovePlane(EPlane.SUNK);

				if (blockers.Count > 0) {
					stop = true; 
					Debug.Log("obstacle hit");
				}
				foreach (Token t in targets.OnlyClass(EClass.UNIT)) {
					((Unit)t).Damage(s, n);
					t.SpriteEffect(EEffect.LASER);
				}
				if (targets.Count > 0) {dmg = (int)Mathf.Floor(dmg*0.5f);}
				
				int nextX = cell.X-direction[0];
				int nextY = cell.Y-direction[1];
				
				if (!Board.HasCell(nextX, nextY, out cell)) {stop = true;}
			}
		}

		public static void Leech (Source s, Unit u, int n) {
			int oldHP = u.HP;
			u.Damage(s, n);
			u.SpriteEffect(EEffect.DMG);
			int dmg = oldHP - u.HP;
			Unit actor = (Unit)(s.Token);
			actor.AddStat(s, EStat.HP, dmg);
			actor.SpriteEffect(EEffect.STATUP);
		}
		
		public static void Donate (Source s, Unit u, int n) {
			int oldHP = u.HP;
			u.AddStat(s, EStat.HP, n);
			u.SpriteEffect(EEffect.STATUP);
			int diff = u.HP - oldHP;
			Unit actor = (Unit)(s.Token);
			actor.Damage(s, diff);
			actor.SpriteEffect(EEffect.STATDOWN);
		}

		public static void Corrode (Source s, Unit u, int n) {
			int cor = (int)Mathf.Floor(n*0.5f);
			u.Damage(s, n);
			u.AddStat(s, EStat.COR, cor);
			u.SpriteEffect(EEffect.COR);
		}
	
		public static void Shock (Source s, Unit u, int n, int st) {
			u.Damage(s, n);
			u.AddStat(s, EStat.STUN, st);
			u.SpriteEffect(EEffect.STUN);
		}

		public static void Rage (Source s, Unit u, int n) {
			u.Damage(s, n);
			u.SpriteEffect(EEffect.DMG);
			Unit actor = (Unit)s.Token;
			actor.Damage(s, (int)Mathf.Floor(n*0.5f));
			actor.SpriteEffect(EEffect.DMG);
		}

		public static void Create (Source s, EToken t, Cell c) {
			Token newToken;
			TokenFactory.Add(t, s, c, out newToken);
			newToken.SpriteEffect(EEffect.BIRTH);
		}

		public static void Kill (Source s, Token t) {
			t.Die(s);
		}

		public static void Replace (Source s, Token t, EToken newT) {
			Cell cell = t.Cell;
			t.Die(s, false, false);
			TokenFactory.Add(newT, s, cell, false);

		}

		public static void Shift (Source s, Unit u, int n) {
			if (n > 0) {TurnQueue.MoveUp(u, n);}
			if (n < 0) {TurnQueue.MoveDown(u, 0-n); }
			
		}

		public static void Move (Source s, Token t, Cell c) {
			Cell oldCell = t.Cell;
			t.SpriteMove(c);
			t.Enter(c);
			Cell newCell = t.Cell;
			GameLog.Out (t+" moved from "+oldCell+" to "+newCell+".");
		}

		public static void SetStat (Source s, Unit u, EStat st, int n) {
			u.SetStat(s, st, n);
		}

		public static void AddStat (Source s, Unit u, EStat st, int n) {
			u.AddStat(s, st, n);
		}

		public static void GetHeart (Source s, Token t) {
			s.Player.Capture(t.Owner);
			GameLog.Out(s.Player.ToString() + " acquired the "+t.ToString()); 
			t.Die(s);
		}

		public static void Advance (Source s) {
			TurnQueue.Advance();
		}

		public static void Shuffle (Source s) {
			TurnQueue.Shuffle(s);
		}
	}
}