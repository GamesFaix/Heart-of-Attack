using HOA.Tokens;
using HOA.Map;
using UnityEngine;
using HOA.Actions;

public class RAsheArise : RInstanceSelect{
	public TTYPE token;
	public RAsheArise (Source s, Token t, TTYPE newT) {source = s; instance = t; token = newT;}

	public override void Grant () {
		Cell cell = instance.Cell;
		int hp = ((Unit)instance).HP;
		instance.Die(source, false, false);
		Token newToken;
		TokenFactory.Add(token, source, cell, out newToken, false);
		((Unit)newToken).SetStat(source, STAT.HP, hp);
		Reset();
	}
}

public class RDamageFirMax : RInstanceSelect {
	public int magnitude;	
	public RDamageFirMax (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}

	public override void Grant () {
		TokenGroup affected = new TokenGroup(source.Token);
		TokenGroup thisRad = new TokenGroup(instance);
		TokenGroup nextRad = new TokenGroup();
		
		int dmg = magnitude;
		
		while (dmg > 0) {
			for (int j=0; j<thisRad.Count; j++) {
				Token next = thisRad[j];
				
				if (!affected.Contains(next)) {		
					next.SpriteEffect(EFFECT.FIRE);
					InputBuffer.Submit(new RDamageDest(source, next, dmg));
					
					foreach (Token t in next.Neighbors(true)) {
						nextRad.Add(t);
					}
					affected.Add(next);
				}
			}
			thisRad = nextRad;
			nextRad = new TokenGroup();
			dmg = (int)Mathf.Floor(dmg * 0.5f);

		}
		Reset();
	}
}

public class RDeathSting: RInstanceSelect {
	public int magnitude;	
	public RDeathSting (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}

	public override void Grant () {
		int cor = (int)Mathf.Floor(magnitude*0.5f);
		if (instance is Unit) {
			Unit u = (Unit)instance;
			u.Damage(source, magnitude);
			u.AddStat(source, STAT.COR, cor);
			u.SpriteEffect(EFFECT.COR);
			Unit actor = (Unit)source.Token;
			actor.Die(source);
		}
		Reset();
	}
}


public class RLaserSpin : RInstanceSelect {
	public int damage;
	public RLaserSpin (Source s, Token t, int d) {source = s; instance = t; damage = d;}

	public override void Grant () {
		if (instance is Unit) {
			Unit u = (Unit)instance;
			u.Damage(source, damage);
			
			int newDmg = (int)Mathf.Floor(damage*0.5f);
			TokenGroup cellMates = u.CellMates;
			if (cellMates.FilterUnit.Count == 1) {
				Unit next = (Unit)cellMates.FilterUnit[0];
				next.Damage(source, newDmg);
				//select direction
				
			}
			else if (cellMates.FilterUnit.Count > 1) {
				
				
			}
			
			else if (cellMates.FilterObstacle.Count > 0) {
				//end
				
			}
		}
		Reset();
	}
}

public class RSmasSlam : RInstanceSelect {
	public int magnitude;	
	public RSmasSlam (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}

	public override void Grant () {
		if (instance is Unit) {
			Unit u = (Unit)instance;
			u.Damage(source, magnitude);
			u.SpriteEffect(EFFECT.DMG);
			TokenGroup neighbors = u.Neighbors(true).FilterUnit;
			foreach (Unit u2 in neighbors) {
				u2.Damage(source, magnitude);
				u2.SpriteEffect(EFFECT.DMG);
			}
		}
		Reset();
	}
}

public class RMetaConsume : RInstanceSelect{
	public RMetaConsume (Source s, Token t) {source = s; instance = t;}
	
	public override void Grant () {
		instance.Die(source);
		Unit u = (Unit)source.Token;
		u.AddStat(source, STAT.HP, 12);
		u.SpriteEffect(EFFECT.STATUP);
		Reset();
	}
}

public class RUltrThrow : RInstanceSelect{
	Aim aim2; int damage;

	public RUltrThrow (Source s, Token t, Aim a2, int dmg) {source = s; instance = t; aim2 = a2; damage = dmg;}
	
	public override void Grant () {
		Debug.Log("killing destructible");
		instance.Die(source);
		Debug.Log("legalizing tokens for attack ("+aim2.ToString()+")");
		Reset();
		Legalizer.Find(source.Token, aim2);

		GUISelectors.DoWithInstance(new RDamage (source, default(Token), damage));
	}
}


public class ROldtSecond : RInstanceSelect {
	public int magnitude;
	public ROldtSecond (Source s, Token t) {source = s; instance = t;}
	
	public override void Grant () {
		if (instance is Unit) {
			int magnitude = TurnQueue.IndexOf((Unit)instance) - 1;
			TurnQueue.MoveUp((Unit)instance, magnitude);
		}	
		Reset();
	}
}

public class RTeleport : RInstanceSelect{
	Aim aim;
	public RTeleport (Source s, Token t, Aim a) {source = s; instance = t; aim = a;}
	
	public override void Grant () {
		Reset();
		Legalizer.Find(source.Token, aim);
		
		GUISelectors.DoWithCell(new RMove (source, instance, default(Cell)));
	}
}

public class RKabuLaser : RInstanceSelect {
	public int magnitude;
	public RKabuLaser (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
	
	public override void Grant () {
		Token actor = source.Token;
		int dmg = magnitude;
		Cell cell = instance.Cell;
		int[] direction = Direction.FromCells(cell, actor.Cell);
		bool stop = false;
		
		TokenGroup targets;
		
		while (dmg > 0 && !stop) {
			targets = cell.Occupants;
			if (targets.FilterObstacle.Count > 0) {stop = true; Debug.Log("obstacle hit");}
			foreach (Token t in targets.FilterUnit) {
				((Unit)t).Damage(source, magnitude);
				t.SpriteEffect(EFFECT.LASER);
			}
			//if (targets.Count > 0) {dmg = (int)Mathf.Floor(dmg*0.5f);}
			
			int nextX = cell.X-direction[0];
			int nextY = cell.Y-direction[1];
			
			if (!Board.HasCell(nextX, nextY, out cell)) {stop = true;}
		}
		Reset();
	}
	
	
}

public class RPrisRefract : RInstanceSelect {
	public int magnitude;
	public RPrisRefract (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
	
	public override void Grant () {
		Token actor = source.Token;

		int flip = DiceCoin.Throw(source, DICE.COIN);
		if (flip == 1) {
			actor.SpriteEffect(EFFECT.HEADS);

			int dmg = magnitude;
			Cell cell = instance.Cell;
			int[] direction = Direction.FromCells(cell, actor.Cell);
			bool stop = false;
			
			TokenGroup targets;
			
			while (dmg > 0 && !stop) {
				targets = cell.Occupants;
				if (targets.FilterObstacle.Count > 0) {stop = true; Debug.Log("obstacle hit");}
				foreach (Token t in targets.FilterUnit) {
					((Unit)t).Damage(source, magnitude);
					t.SpriteEffect(EFFECT.LASER);
				}
				if (targets.Count > 0) {dmg = (int)Mathf.Floor(dmg*0.5f);}
				
				int nextX = cell.X-direction[0];
				int nextY = cell.Y-direction[1];
				
				if (!Board.HasCell(nextX, nextY, out cell)) {stop = true;}
			}
		}
		else {
			actor.SpriteEffect(EFFECT.TAILS);
			GameLog.Out(actor+" attempts to Refract and misses.");
		}
		Reset();
	}
	
	
}

public class RReprMine : RInstanceSelect{
	public RReprMine (Source s, Token t) {source = s; instance = t;}
	
	public override void Grant () {
		instance.Die(source);
		Unit u = (Unit)source.Token;

		if (u.IN < 7) {
			u.AddStat(source, STAT.IN, 1);
			u.SpriteEffect(EFFECT.STATUP);
		}
		Reset();
	}
}

public class RNecrTouch : RInstanceSelect {
	public int magnitude;	
	public RNecrTouch (Source s, Token t, int n) {source = s; instance = t; magnitude = n;}
	
	public override void Grant () {
		if (instance is Unit) {
			Unit u = (Unit)instance;
			int oldHP = u.HP;
			int def = u.DEF;

			int damage = magnitude - def;
			if (oldHP - damage < 10) {damage = oldHP;}
			if (damage >= oldHP) {
				u.Die(source, false, true);
				Reset();
				Aim aim = new Aim(AIMTYPE.FREE, TARGET.CELL, CTAR.CREATE);
				Legalizer.Find(source.Token, aim, TemplateFactory.Template(TTYPE.CORP));
				GUISelectors.DoWithCell (new RCreate(source, TTYPE.CORP, default(Cell)));
			}
			else {
				u.Damage(source, magnitude);
				u.SpriteEffect(EFFECT.DMG);
			}
		}
		//Reset();
	}
}

public class RRecyCannibal : RInstanceSelect{
	Aim aim2; int damage;
	
	public RRecyCannibal (Source s, Token t) {source = s; instance = t;}
	
	public override void Grant () {
		instance.Die(source);
		Unit actor = (Unit)(source.Token);
		actor.AddStat(source, STAT.MHP, 10);
		actor.AddStat(source, STAT.HP, 10);
		Reset();
	}
}