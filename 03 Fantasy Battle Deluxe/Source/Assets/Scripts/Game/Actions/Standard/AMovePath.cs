using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class AMovePath : Action, IMultiMove {


		Cell target;
		int range;
		public int Optional () {return 1;}

		public AMovePath (Unit u, int r) {
			weight = 1;
			actor = u;
			name = "Move";
			desc = "Move "+actor+" to target cell.";

			range = r;
			for (int i=0; i<range; i++) {
				Aim a = new Aim(EAim.NEIGHBOR, EClass.CELL, EPurpose.MOVE) ;
				AddAim(a);
				//Debug.Log(a);
			}


		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			foreach (ITargetable target in targets) {
				EffectQueue.Add(new EMove(new Source(actor), actor, (Cell)target));
			}
			Targeter.Reset();
		}

		public override void Draw (Panel p) {
			GUI.Label(p.LineBox, Name, p.s);
			
			DrawPrice(new Panel(p.LineBox, p.LineH, p.s));

			Aim actual = new Aim(EAim.PATH, EClass.CELL, EPurpose.MOVE, range);
			actual.Draw(new Panel(p.LineBox, p.LineH, p.s));
			
			float descH = (p.H-(p.LineH*2))/p.H;
			//Rect descBox = new Rect(p.x2, p.y2, p.W, descH);
			
			GUI.Label(p.TallBox(descH), Desc());	
			
			
		}
	}
}
