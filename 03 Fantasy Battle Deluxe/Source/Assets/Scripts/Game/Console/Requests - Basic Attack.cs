using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class RDamage : RInstanceSelect {
		public int magnitude;	
		public RDamage (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}

		public override void Grant () {
			if (instance is Unit) {
				Unit u = (Unit)instance;
				u.Damage(source, magnitude);
				u.SpriteEffect(EEffect.DMG);
			}
			Reset();
		}
	}

	public class RDamagePierce : RInstanceSelect {
		public int magnitude;	
		public RDamagePierce (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
		
		public override void Grant () {
			if (instance is Unit) {
				Unit u = (Unit)instance;
				u.AddStat(source, EStat.HP, -magnitude);
				u.SpriteEffect(EEffect.DMG);
			}
			Reset();
		}
	}

	public class RDamageDest : RInstanceSelect {
		public int magnitude;
		public RDamageDest (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}

		public override void Grant () {
			if (instance.IsClass(EClass.DEST)) {instance.Die(source);}
			else if (instance is Unit) {
				Unit u = (Unit)instance;
				u.Damage(source, magnitude);
				//u.SpriteEffect(EEffect.DMG);
			}
			Reset();
		}
	}

	public class RExplosion : RCellSelect {
		public int magnitude;	
		public RExplosion (Source s, Cell c, int n) {source = s; cell = c; magnitude = n;}
		
		public override void Grant () {
			Cell start = cell;
			
			CellGroup affected = new CellGroup();
			CellGroup thisRad = new CellGroup(start);
			CellGroup nextRad = new CellGroup();
			
			int dmg = magnitude;
			
			while (dmg > 0) {
				for (int j=0; j<thisRad.Count; j++) {
					Cell next = thisRad[j];
					
					if (!affected.Contains(next)) {
						TokenGroup targets = next.Occupants.OnlyClass(new List<EClass> {EClass.UNIT, EClass.DEST});

						foreach (Token t in targets) {
							t.SpriteEffect(EEffect.EXP);
							InputBuffer.Submit(new RDamageDest(source, t, dmg));
						}
						foreach (Cell c in next.Neighbors()) {nextRad.Add(c);}
						affected.Add(next);
					}
				}
				thisRad = nextRad;
				nextRad = new CellGroup();
				dmg = (int)Mathf.Floor(dmg * 0.5f);
			}
			Reset();
		}
	}

	public class RDamageFir : RInstanceSelect {
		public int magnitude;	
		public RDamageFir (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
		
		public override void Grant () {
			TokenGroup neighbors = instance.Neighbors(true);
			neighbors.Remove(source.Token);
			neighbors = neighbors.OnlyClass(new List<EClass> {EClass.UNIT, EClass.DEST});
			
			instance.SpriteEffect(EEffect.FIRE);
			InputBuffer.Submit(new RDamageDest(source, instance, magnitude));
			
			int newDmg = (int)Mathf.Floor(magnitude * 0.5f);
			foreach (Token t in neighbors) {
				t.SpriteEffect(EEffect.FIRE);
				InputBuffer.Submit(new RDamageDest(source, t, newDmg));
			}
			Reset();
		}
	}

	public class RDamageLaser : RInstanceSelect {
		public int magnitude;
		public RDamageLaser (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}

		public override void Grant () {
			Token actor = source.Token;
			int dmg = magnitude;
			Cell cell = instance.Cell;
			int[] direction = Direction.FromCells(cell, actor.Cell);
			bool stop = false;

			TokenGroup targets;

			while (dmg > 0 && !stop) {
				targets = cell.Occupants;
				if (targets.OnlyClass(EClass.OB).Count > 0) {stop = true; Debug.Log("obstacle hit");}
				foreach (Token t in targets.OnlyClass(EClass.UNIT)) {
					((Unit)t).Damage(source, magnitude);
					t.SpriteEffect(EEffect.LASER);
				}
				if (targets.Count > 0) {dmg = (int)Mathf.Floor(dmg*0.5f);}

				int nextX = cell.X-direction[0];
				int nextY = cell.Y-direction[1];

				if (!Board.HasCell(nextX, nextY, out cell)) {stop = true;}
			}
			Reset();
		}


	}

	public class RLeech : RInstanceSelect {
		public int magnitude;	
		public RLeech (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
		
		public override void Grant () {
			if (instance is Unit) {
				Unit u = (Unit)instance;
				int oldHP = u.HP;
				u.Damage(source, magnitude);
				u.SpriteEffect(EEffect.DMG);
				int dmg = oldHP - u.HP;
				Unit actor = (Unit)(source.Token);
				actor.AddStat(source, EStat.HP, dmg);
				actor.SpriteEffect(EEffect.STATUP);
			}
			Reset();
		}
	}

	public class RDonate : RInstanceSelect {
		public int magnitude;	
		public RDonate (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
		
		public override void Grant () {
			if (instance is Unit) {
				Unit u = (Unit)instance;
				int oldHP = u.HP;
				u.AddStat(source, EStat.HP, magnitude);
				u.SpriteEffect(EEffect.STATUP);
				int diff = u.HP - oldHP;
				Unit actor = (Unit)(source.Token);
				actor.Damage(source, diff);
				actor.SpriteEffect(EEffect.STATDOWN);
			}
			Reset();
		}
	}

	public class RCorrode : RInstanceSelect {
		public int magnitude;	
		public RCorrode (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
		
		public override void Grant () {
			int cor = (int)Mathf.Floor(magnitude*0.5f);
			if (instance is Unit) {
				Unit u = (Unit)instance;
				u.Damage(source, magnitude);
				u.AddStat(source, EStat.COR, cor);
				u.SpriteEffect(EEffect.COR);
			}
			Reset();
		}
	}

	public class RShock : RInstanceSelect {
		public int damage; public int stun;
		public RShock (Source s, Token t, int d, int st) {source = s; instance = t; damage = d; stun = st;}
		
		public override void Grant () {
			if (instance is Unit) {
				Unit u = (Unit)instance;
				u.Damage(source, damage);
				u.AddStat(source, EStat.STUN, stun);
				u.SpriteEffect(EEffect.STUN);
			}
			Reset();
		}
	}

	public class RRage : RInstanceSelect {
		public int magnitude;	
		public RRage (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
		
		public override void Grant () {
			if (instance is Unit) {
				Unit u = (Unit)instance;
				u.Damage(source, magnitude);
				u.SpriteEffect(EEffect.DMG);
				Unit actor = (Unit)source.Token;
				actor.Damage(source, (int)Mathf.Floor(magnitude*0.5f));
				actor.SpriteEffect(EEffect.DMG);
			}
			Reset();
		}
	}
}