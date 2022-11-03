using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class AMovePath : Task, IMultiMove {

		public override string Desc {get {return "Move "+Parent+" to target cell.";} }
		
		int range;
		public int Optional () {return 1;}

		public AMovePath (Unit parent, int r) {
			Parent = parent;
			Name = "Move";
			Weight = 1;
			range = r;
			for (int i=0; i<range; i++) {
				Aim a = new Aim(ETraj.NEIGHBOR, EType.CELL, EPurp.MOVE) ;
				AddAim(a);
			}
			Price = Price.Cheap;
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
