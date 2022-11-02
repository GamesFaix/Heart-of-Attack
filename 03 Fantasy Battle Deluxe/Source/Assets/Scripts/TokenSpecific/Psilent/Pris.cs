using UnityEngine;

namespace HOA{
	public class PrismGuard : Unit {
		public PrismGuard(Source s, bool template=false){
			NewLabel(TTYPE.PRIS, s, false, template);
			BuildGround();
			
			NewHealth(15);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 8));
			arsenal.Add(new APrisRefract(new Price(1,1), this, Aim.Shoot(3), 12));
			arsenal.Sort();
		}		
		public override string Notes () {return "Actions targetting "+Name+" have a 50% of missing.";}
		
		public override void Select (Source s) {
			int flip = DiceCoin.Throw(s, DICE.COIN);
			if (flip == 1) {
				SpriteEffect(EFFECT.HEADS);
				GUISelectors.Instance = this;
			}
			else {
				GameLog.Out(s.ToString()+" tried to target "+FullName+" and missed.");
				SpriteEffect(EFFECT.TAILS);
			}
		}
		
		
	}

	public class APrisRefract : Action {
		
		int damage;
		
		public APrisRefract (Price p, Unit u, Aim a, int d) {
			weight = 4;
			actor = u;
			price = p;
			aim = a;
			damage = d;
			
			name = "Refract";
			desc = "50% chance of missing target.\nDo "+d+" damage to all units in target cell.\nIf there are no obstacles in target cell, do reduce damage 50% (rounded up) and damage all units in the next occupied cell in the same direction.  Repeat until damage is 1 or an obstacle is hit.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RPrisRefract(new Source(actor), default(Token), damage));
			}
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
	
}