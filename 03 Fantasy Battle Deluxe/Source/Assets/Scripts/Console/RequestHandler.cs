using HOA.Players;
using HOA.Map;
using HOA.Tokens;
using HOA.Tokens.Components;
using UnityEngine;

public static class RequestHandler {
	
	public static void Submit (Request r) {
		
		//tokens
		if (r is RMove) { 
			RMove r2 = (RMove)r;
			r2.instance.Enter(r2.cell);
		}
		else if (r is RCreate) { 
			RCreate r2 = (RCreate)r;
			Token newToken;
			TokenFactory.Add(r2.token, r2.source, r2.cell, out newToken);
			newToken.SpriteEffect(EFFECT.BIRTH);
		}
		else if (r is RKill) { 
			RKill r2 = (RKill)r;
			r2.instance.Die(r2.source);
		}
		else if (r is RReplace) { 
			RReplace r2 = (RReplace)r;
			Cell cell = r2.instance.Cell;
			r2.instance.Die(r2.source, false, false);
			TokenFactory.Add(r2.token, r2.source, cell, false);
		}
		else if (r is RAsheArise) { 
			RAsheArise r2 = (RAsheArise)r;
			Cell cell = r2.instance.Cell;
			int hp = ((Unit)r2.instance).HP;
			r2.instance.Die(r2.source, false, false);
			Token newToken;
			TokenFactory.Add(r2.token, r2.source, cell, out newToken, false);
			((Unit)newToken).SetStat(r2.source, STAT.HP, hp);
		}
		
		else if (r is RAddStat) { 
			RAddStat r2 = (RAddStat)r;
			if (r2.instance is Unit) {
				Unit u = (Unit)r2.instance;
				u.AddStat(r2.source, r2.stat, r2.magnitude);
			}
		}
		else if (r is RSetStat) { 
			RSetStat r2 = (RSetStat)r;
			if (r2.instance is Unit) {
				Unit u = (Unit)r2.instance;
				u.SetStat(r2.source, r2.stat, r2.magnitude);
			}
		}
		else if (r is RDamage) {
			RDamage r2 = (RDamage)r;
			if (r2.instance is Unit) {
				Unit u = (Unit)r2.instance;
				u.Damage(r2.source, r2.magnitude);
				u.SpriteEffect(EFFECT.DMG);
			}
		}
		else if (r is RRage) {
			RRage r2 = (RRage)r;
			if (r2.instance is Unit) {
				Unit u = (Unit)r2.instance;
				u.Damage(r2.source, r2.magnitude);
				u.SpriteEffect(EFFECT.DMG);
				Unit actor = (Unit)r2.source.Token;
				actor.Damage(r2.source, (int)Mathf.Floor(r2.magnitude*0.5f));
				actor.SpriteEffect(EFFECT.DMG);
			}
		}
		else if (r is RCorrode) {
			RCorrode r2 = (RCorrode)r;
			int cor = (int)Mathf.Floor(r2.magnitude*0.5f);
			if (r2.instance is Unit) {
				Unit u = (Unit)r2.instance;
				u.Damage(r2.source, r2.magnitude);
				u.AddStat(r2.source, STAT.COR, cor);
				u.SpriteEffect(EFFECT.COR);
			}
		}
		else if (r is RDeathSting) {
			RDeathSting r2 = (RDeathSting)r;
			int cor = (int)Mathf.Floor(r2.magnitude*0.5f);
			if (r2.instance is Unit) {
				Unit u = (Unit)r2.instance;
				u.Damage(r2.source, r2.magnitude);
				u.AddStat(r2.source, STAT.COR, cor);
				u.SpriteEffect(EFFECT.COR);
				Unit actor = (Unit)r2.source.Token;
				actor.Die(r2.source);
			}
		}
		
		else if (r is RLeech) {
			RLeech r2 = (RLeech)r;
			if (r2.instance is Unit) {
				Source s = r2.source;
				Unit u = (Unit)r2.instance;
				int oldHP = u.HP;
				u.Damage(s, r2.magnitude);
				u.SpriteEffect(EFFECT.DMG);
				int dmg = oldHP - u.HP;
				Unit actor = (Unit)(s.Token);
				actor.AddStat(s, STAT.HP, dmg);
				actor.SpriteEffect(EFFECT.STATUP);
			}
		}
		else if (r is RDonate) {
			RDonate r2 = (RDonate)r;
			if (r2.instance is Unit) {
				Source s = r2.source;
				Unit u = (Unit)r2.instance;
				int oldHP = u.HP;
				u.AddStat(s, STAT.HP, r2.magnitude);
				u.SpriteEffect(EFFECT.STATUP);
				int diff = u.HP - oldHP;
				Unit actor = (Unit)(s.Token);
				actor.Damage(s, diff);
				actor.SpriteEffect(EFFECT.STATDOWN);
			}
		}
		else if (r is RExplosion) {
			RExplosion r2 = (RExplosion)r;
			Cell start = r2.cell;
			
			CellGroup affected = new CellGroup();
			CellGroup thisRad = new CellGroup(start);
			CellGroup nextRad = new CellGroup();
			
			int dmg = r2.magnitude;
			
			while (dmg > 0) {
				for (int j=0; j<thisRad.Count; j++) {
					Cell next = thisRad[j];
					
					if (!affected.Contains(next)) {
						foreach (Token t in next.Occupants) {
							t.SpriteEffect(EFFECT.EXP);
							InputBuffer.Submit(new RDamageDest(r2.source, t, dmg));
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
		
		else if (r is RDamageFir) {
			RDamageFir r2 = (RDamageFir)r;
			
			TokenGroup neighbors = r2.instance.Neighbors(true);
			neighbors.Remove(r2.source.Token);

			r2.instance.SpriteEffect(EFFECT.FIRE);
			InputBuffer.Submit(new RDamageDest(r2.source, r2.instance, r2.magnitude));
			
			int newDmg = (int)Mathf.Floor(r2.magnitude * 0.5f);
			foreach (Token t in neighbors) {
				t.SpriteEffect(EFFECT.FIRE);
				InputBuffer.Submit(new RDamageDest(r2.source, t, newDmg));
			}
		}
		else if (r is RDamageFirMax) {
			RDamageFirMax r2 = (RDamageFirMax)r;
			
			TokenGroup affected = new TokenGroup(r2.source.Token);
			TokenGroup thisRad = new TokenGroup(r2.instance);
			TokenGroup nextRad = new TokenGroup();
			
			int dmg = r2.magnitude;
			
			while (dmg > 0) {
				for (int j=0; j<thisRad.Count; j++) {
					Token next = thisRad[j];
					
					if (!affected.Contains(next)) {		
						next.SpriteEffect(EFFECT.FIRE);
						InputBuffer.Submit(new RDamageDest(r2.source, next, dmg));
						
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
		
		
		
		
		else if (r is RDamageDest) {
			RDamageDest r2 = (RDamageDest)r;
			if (r2.instance.IsSpecial(SPECIAL.DEST)) {r2.instance.Die(r2.source);}
			
			else if (r2.instance is Unit) {
				Unit u = (Unit)r2.instance;
				u.Damage(r2.source, r2.magnitude);
			}
		}
		
		
		else if (r is RShock) {
			RShock r2 = (RShock)r;
			if (r2.instance is Unit) {
				Unit u = (Unit)r2.instance;
				u.Damage(r2.source, r2.damage);
				u.AddStat(r2.source, STAT.STUN, r2.stun);
				u.SpriteEffect(EFFECT.STUN);
			}
		}
		else if (r is RLaserSpin) {
			RLaserSpin r2 = (RLaserSpin)r;
			if (r2.instance is Unit) {
				Unit u = (Unit)r2.instance;
				u.Damage(r2.source, r2.damage);
				
				int newDmg = (int)Mathf.Floor(r2.damage*0.5f);
				TokenGroup cellMates = u.CellMates;
				if (cellMates.FilterUnit.Count == 1) {
					Unit next = (Unit)cellMates.FilterUnit[0];
					next.Damage(r2.source, newDmg);
					//select direction
					
				}
				else if (cellMates.FilterUnit.Count > 1) {
					
					
				}
				
				else if (cellMates.FilterObstacle.Count > 0) {
					//end
					
				}
				
				
				
			}
		}
		
		else if (r is RSmasSlam) {
			RSmasSlam r2 = (RSmasSlam)r;
			if (r2.instance is Unit) {
				Unit u = (Unit)r2.instance;
				u.Damage(r2.source, r2.magnitude);
				u.SpriteEffect(EFFECT.DMG);
				TokenGroup neighbors = u.Neighbors(true).FilterUnit;
				foreach (Unit u2 in neighbors) {
					u2.Damage(r2.source, r2.magnitude);
					u2.SpriteEffect(EFFECT.DMG);
				}
			}
		}
		
		
		
		//game
		else if (r is RStart) { 
			RStart r2 = (RStart)r;
			if (Roster.Count > 2) {
				TokenFactory.Reset();
				GameLog.Reset();
				TurnQueue.Reset();
				Board.New(r2.boardSize);	
				GUIBoard.ZoomOut();
				foreach (Player p in Roster.Players()){
					Cell cell = Board.RandomCell;
					TokenFactory.Add(p.King, new Source(p), cell, false);
				}
				TurnQueue.Shuffle(new Source(),false);
				TurnQueue.Advance(false);
				GameLog.Out("New game ready.");
				GUIMaster.Toggle();
			}
			else {GameLog.Debug("Console: Cannot start game with less than 2 players.");}
		}
		else if (r is RQuit) {
			TurnQueue.Reset();
			GameLog.Reset();
			TokenFactory.Reset();
			Board.Reset();
			Roster.Reset();
			GUIMaster.Toggle();
		}
		
		//roster
		else if (r is RRosterAdd) { 
			RRosterAdd r2 = (RRosterAdd)r;
			Roster.Add(r2.player);
		}
		else if (r is RRosterRemove) { 
			RRosterRemove r2 = (RRosterRemove)r;
			Roster.Remove(r2.player);
		}
		else if (r is RRosterNew) { 
			RRosterNew r2 = (RRosterNew)r;
			Roster.New(r2.players);
		}
		else if (r is RRosterAssign) {
			RRosterAssign r2 = (RRosterAssign)r;
			Roster.AssignFaction(r2.player, r2.faction);
		}
		else if (r is RRosterRandom) {
			Roster.ForceRandomFactions();	
		}
		else if (r is RCapture) {
			RCapture r2 = (RCapture)r;
			r2.captor.Capture(r2.captive);
		}

		//queue
		else if (r is RQueueAdvance)  { 
			TurnQueue.Advance(); 
		}
		else if (r is RQueueShuffle) { 
		TurnQueue.Shuffle(r.source, false); 
				
		}
		else if (r is RQueueShift) { 
			RQueueShift r2 = (RQueueShift)r;
			if (r2.instance is Unit) {
				if (r2.magnitude > 0) {TurnQueue.MoveUp( (Unit)r2.instance, r2.magnitude);}
				if (r2.magnitude < 0) {TurnQueue.MoveDown( (Unit)r2.instance, 0-r2.magnitude); }
			}			
		}
		
		//random
		else if (r is RRandom) { 
			RRandom r2 = (RRandom)r;
			DiceCoin.Throw(r2.source, r2.dice);
		}
		
		
		
	}
}
