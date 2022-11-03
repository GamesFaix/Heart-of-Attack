using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class Katandroid : Unit {
		public Katandroid(Source s, bool template=false){
			ID = new ID(this, EToken.KATA, s, false, template);
			Plane = Plane.Gnd;
			ScaleSmall();
			NewHealth(25);
			NewWatch(5);	
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 4),
				new AStrike(this, 8),
				new AKataSprint(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class AKataSpin : Task {
		public override string Desc {get {return "Do "+damage+" damage to target unit, " +
				"then damage all of target's cellmates " +
					"and all units clockwise or counterclockwise, " +
						"reducing damage 50% each time.";} }

		int damage = 10;
		
		public AKataSpin (Unit parent) {
			Weight = 4;
			Name = "Laser Spin";
			Price = new Price(1,1);
			Parent = parent;
			NewAim(new Aim (ETraj.NEIGHBOR, EType.UNIT));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Unit u = (Unit)targets[0];
			u.Damage(new Source(Parent), damage);
			
			int newDmg = (int)Mathf.Floor(damage*0.5f);
			TokenGroup cellMates = u.Body.CellMates;
			if (cellMates.OnlyType(EType.UNIT).Count == 1) {
				Unit next = (Unit)cellMates.OnlyType(EType.UNIT)[0];
				next.Damage(new Source(Parent), newDmg);
				//select direction
				
			}
			else if (cellMates.OnlyType(EType.UNIT).Count > 1) {
				
				
			}
			
			else if (cellMates.OnlyType(EType.UNIT).Count > 0) {
				//end
				
			}
		}
	}
}