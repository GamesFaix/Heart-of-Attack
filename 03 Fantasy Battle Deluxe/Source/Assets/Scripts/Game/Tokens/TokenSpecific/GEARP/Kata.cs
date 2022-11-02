using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class Katandroid : Unit {
		public Katandroid(Source s, bool template=false){
			NewLabel(EToken.KATA, s, false, template);
			BuildGround();
			
			NewHealth(25);
			NewWatch(5);	

			arsenal.Add(new AMove(this, Aim.MovePath(4)));
			arsenal.Add(new AKataMove(this));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 8));
			arsenal.Sort();
		}
		public override string Notes () {return "";}
	}

	public class AKataMove : Action {
		
		public AKataMove (Unit u) {
			weight = 1;
			price = new Price(0,0);
			actor = u;
			AddAim(HOA.Aim.MovePath(0));
			
			name = "Sprint";
			desc = "Move "+actor+" to target cell.  \nRange +1 per focus (up to +6).  \n"+actor+" loses all focus.";
			
		}

		public override void Adjust () {
			int bonus = Mathf.Min(actor.FP, 6);
			aim[0] = new Aim (aim[0].AimType, aim[0].TargetClass, aim[0].Purpose, aim[0].Range+bonus);
		}

		public override void UnAdjust () {
			aim[0] = HOA.Aim.MovePath(0);
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();
			actor.SetStat(new Source(actor), EStat.FP, 0, false);

			AEffects.Move(new Source(actor), actor, (Cell)targets[0]);
			Targeter.Reset();
		}
	}

	public class AKataSpin : Action {
		int damage;
		
		public AKataSpin (Price p, Unit u, int d) {
			price = p;
			actor = u;
			AddAim(new Aim (EAim.NEIGHBOR, EClass.UNIT));
			
			damage = d;
			name = "Laser Spin";
			desc = "Do "+d+" damage to target unit, then damage all of target's cellmates and all units clockwise or counterclockwise, reducing damage 50% each time.";
			
		}
		
		public override void Execute (List<ITargetable> targets) {
			Unit u = (Unit)targets[0];
			u.Damage(new Source(actor), damage);
			
			int newDmg = (int)Mathf.Floor(damage*0.5f);
			TokenGroup cellMates = u.CellMates;
			if (cellMates.OnlyClass(EClass.UNIT).Count == 1) {
				Unit next = (Unit)cellMates.OnlyClass(EClass.UNIT)[0];
				next.Damage(new Source(actor), newDmg);
				//select direction
				
			}
			else if (cellMates.OnlyClass(EClass.UNIT).Count > 1) {
				
				
			}
			
			else if (cellMates.OnlyClass(EClass.UNIT).Count > 0) {
				//end
				
			}
			Targeter.Reset();
		}
	}
}