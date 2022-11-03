using System.Collections.Generic;
using UnityEngine;

namespace HOA{
	public class Rook : Unit {
		public Rook(Source s, bool template=false){
			id = new ID(this, EToken.ROOK, s, false, template);
			plane = Plane.Gnd;
			onDeath = EToken.ROCK;
			ScaleMedium();
			NewHealth(20,3);
			NewWatch(3);
			BuildArsenal();
		}		

		protected override void BuildArsenal () {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new ARookMove(this),
				new ARookVolley(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "";}
	}

	public class ARookVolley : Task {
		
		int damage = 12;

		public override string Desc {get {return "Do "+damage+" damage to target unit." +
				"\nMay only be used if neighboring or sharing cell with non-Rook teammate." +
				"\nRange +1 per focus (up to 3).";} }

		public ARookVolley (Unit u) {
			Name = "Volley";
			Weight = 3;
			Parent = u;
			Price = Price.Cheap;
			AddAim(new Aim(ETraj.ARC, EType.UNIT, EPurp.ATTACK, 2, 2));
		}

		public override void Adjust () {
			int bonus = Mathf.Min(Parent.FP, 3);
			aim[0] = new Aim (aim[0].Trajectory, aim[0].Special, aim[0].Range+bonus);
		}
		
		public override void UnAdjust () {
			aim[0] = new Aim(ETraj.ARC, EType.UNIT, EPurp.ATTACK, 2, 2);
		}

		public override bool Restrict () {
			TokenGroup neighbors = Parent.Body.Neighbors(true);
			for (int i=neighbors.Count-1; i>=0; i--) {
				Token t = neighbors[i];
				if (t.Owner == Parent.Owner 
				    && t.ID.Code != EToken.ROOK) {
					return false;
				}
			}
			return true;
		}

		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EDamage(new Source(Parent), (Unit)targets[0], damage));
		}
	}
	
	public class ARookMove : Task, IMultiMove {
		int range = 2;
		public int Optional () {return 1;}

		public override string Desc {get {return "Move "+Parent+" to target cell.";} }

		public ARookMove (Unit u) {
			Name = "Rebuild";
			Weight = 1;
			Parent = u;
			Price = new Price(0,2);

			for (int i=0; i<range; i++) {
				Aim a = new Aim(ETraj.NEIGHBOR, EType.CELL, EPurp.MOVE) ;
				AddAim(a);
			}
		}

		protected override void ExecuteMain (TargetGroup targets) {
			foreach (Target target in targets) {
				EffectQueue.Add(new EMove(new Source(Parent), Parent, (Cell)target));
			}
		}
		
		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));
			
			Aim actual = new Aim(ETraj.PATH, EType.CELL, EPurp.MOVE, range);
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc);	
		}
	}
}
