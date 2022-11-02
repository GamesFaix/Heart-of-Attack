using UnityEngine;

namespace HOA {
	
	public class Katandroid : Unit {
		public Katandroid(Source s, bool template=false){
			NewLabel(TTYPE.KATA, s, false, template);
			BuildGround();
			
			NewHealth(25);
			NewWatch(5);	
			
			arsenal.Add(new AKataMove(Price.Cheap, this, Aim.MovePath(4)));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 8));
			arsenal.Sort();
		}
		public override string Notes () {return "";}
	}

	public class AKataMove : Action {
		
		public AKataMove (Price p, Unit u, Aim a) {
			weight = 1;
			price = p;
			actor = u;
			aim = a;
			
			name = "Move";
			desc = "Move "+actor+" to target cell.  \nRange +2 per focus (up to +6).  \n"+actor+" loses all focus.";
			
		}
		
		public override void Perform () {
			if (Charge()) {
				int bonus = Mathf.Min(actor.FP*2, 6);
				actor.SetStat(new Source(actor), STAT.FP, 0, false);
				
				aim = new Aim (aim.AimType, aim.Target, aim.CTar, aim.Range+bonus);
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithCell(new RMove(new Source(actor), actor, default(Cell)));
			}
		}
	}

	public class AKataSpin : Action {
		int damage;
		
		public AKataSpin (Price p, Unit u, int d) {
			price = p;
			actor = u;
			aim = new Aim (AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.UNIT);
			
			damage = d;
			name = "Laser Spin";
			desc = "Do "+d+" damage to target unit, then damage all of target's cellmates and all units clockwise or counterclockwise, reducing damage 50% each time.";
			
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RLaserSpin(new Source(actor), default(Token), damage));
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
			Reset();
		}
	}
}