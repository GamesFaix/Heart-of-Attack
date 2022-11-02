using HOA.Tokens;
using HOA.Map;
using UnityEngine;

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
	}
}


