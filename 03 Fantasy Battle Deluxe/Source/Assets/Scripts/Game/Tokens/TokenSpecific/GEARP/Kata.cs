using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class Katandroid : Unit {
		public Katandroid(Source s, bool template=false){
			NewLabel(EToken.KATA, s, false, template);
			BuildGround();
			
			NewHealth(25);
			NewWatch(5);	

			arsenal.Add(new AMovePath(this, 4));
			arsenal.Add(new AKataMove(this));
			arsenal.Add(new AAttack("Melee", Price.Cheap, this, Aim.Melee(), 8));
			arsenal.Sort();
		}
		public override string Notes () {return "";}
	}

	public class AKataMove : Action, IMultiMove {
		Cell target;
		public int Optional () {return 1;}

		public AKataMove (Unit u) {
			weight = 1;
			actor = u;
			price = new Price(0,0);
			name = "Sprint";
			desc = "Move "+actor+" to target cell.  \nRange +1 per focus (up to +6).  \n"+actor+" loses all focus.";

			ResetAim();
			
		}

		public override void Adjust () {
			int bonus = Mathf.Min(actor.FP, 6);
			for (int i=0; i<bonus; i++) {
				aim.Add(new Aim (EAim.NEIGHBOR, EClass.CELL, EPurpose.MOVE));
			}
		}
		
		public override void UnAdjust () {
			ResetAim();
		}

		void ResetAim () {
			aim = new List<Aim>();
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();
			foreach (ITargetable target in targets) {
				EffectQueue.Add(new EMove(new Source(actor), actor, (Cell)target));
			}
			Targeter.Reset();
			actor.SetStat(new Source(actor), EStat.FP, 0);
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));
			
			Aim actual = new Aim(EAim.PATH, EClass.CELL, EPurpose.MOVE, Mathf.Min(6, actor.FP));
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc());	
			
			
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