using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class Katandroid : Unit {
		public Katandroid(Source s, bool template=false){
			id = new ID(this, EToken.KATA, s, false, template);
			plane = Plane.Gnd;
			ScaleSmall();
			NewHealth(25);
			NewWatch(5);	
			BuildArsenal();
		}

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 4),
				new AStrike(this, 8),
				new AKataSprint(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class AKataSprint : Task, IMultiMove {

		public override string Desc {get {return "Move "+Parent+" to target cell.  " +
				"\nRange +1 per focus (up to +6). " +
					"\n"+Parent+" loses all focus.";} }

		public int Optional () {return 1;}

		public AKataSprint (Unit parent) {
			Name = "Sprint";
			Weight = 4;
			Parent = parent;
			Price = Price.Free;
			ResetAim();
		}

		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 6);
			for (int i=0; i<bonus; i++) {
				aim.Add(new Aim (ETraj.NEIGHBOR, EType.CELL, EPurp.MOVE));
			}
		}
		
		public override void UnAdjust () {
			ResetAim();
		}

		void ResetAim () {
			aim = new List<Aim>();
		}

		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new EMove(new Source(Parent), Parent, (Cell)target));
			}
			Parent.SetStat(new Source(Parent), EStat.FP, 0);
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));
			
			Aim actual = new Aim(ETraj.PATH, EType.CELL, EPurp.MOVE, Mathf.Min(6, Parent.FP));
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc);	
			
			
		}
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
			AddAim(new Aim (ETraj.NEIGHBOR, EType.UNIT));
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