using UnityEngine;

namespace HOA {
	public class Kabutomachine : Unit {
	
		public Kabutomachine(Source s, bool template=false){
			NewLabel(TTYPE.KABU, s, true, template);
			BuildAir();
			AddKing();
			OnDeath = TTYPE.HSIL;
			
			NewHealth(75);
			NewWatch(4);
			
			arsenal.Add(new AMove(this, Aim.MoveLine(5)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 16));
			arsenal.Add(new ACreate(Price.Cheap, this, TTYPE.KATA));
			arsenal.Add(new ACreate(new Price(0,2), this, TTYPE.CARA));
			arsenal.Add(new ACreate(new Price(2,2), this, TTYPE.MAWT));
			arsenal.Add(new AKabuTeleport(this));
			arsenal.Add(new AKabuLaser(this));
			arsenal.Sort();
		}
		public override string Notes () {return "";}
	}

	public class AKabuLaser : Action {
		
		int damage;
		
		public AKabuLaser (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,2);
			aim = Aim.Shoot(20);
			damage = 16;
			
			name = "Gamma Burst";
			desc = "Do "+damage+" damage to all units in target direction";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RKabuLaser(new Source(actor), default(Token), damage));
			}
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

	public class AKabuTeleport : Action {
		Aim aim2;
		
		public AKabuTeleport (Unit u) {
			weight = 4;
			actor = u;
			price = new Price(1,1);
			aim = new Aim(AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, 5);
			aim.TeamOnly = true;
			aim2 = new Aim(AIMTYPE.ARC, TARGET.CELL, CTAR.MOVE, 5);
			
			name = "Warp Drive";
			desc = "Move target teammate (including self) to target cell.\n"+aim2.ToString();
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RTeleport(new Source(actor), default(Token), aim2));
			}
		}
	}
}