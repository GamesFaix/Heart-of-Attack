using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class AMoveLine : Action {
		
		Cell target;
		int range;
		
		public AMoveLine (Unit u, int r) {
			weight = 1;
			AddAim(new Aim(ETraj.LINE, EClass.CELL, EPurp.MOVE, r));
			actor = u;
			
			name = "Move";
			desc = "Move "+actor+" to target cell.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			Cell endCell = (Cell)targets[0];
			//Debug.Log("end: "+endCell);

			CellGroup line = new CellGroup();

			int[] dir = Direction.FromCells(actor.Body.Cell, endCell);
			//Debug.Log("direction: "+dir[0]+","+dir[1]);

			int length = Length(actor.Body.Cell, endCell);
			//Debug.Log("length: "+length);

			Cell c = actor.Body.Cell;

			for (int i=0; i<length; i++) {
				int x = c.X + dir[0];
				int y = c.Y + dir[1];
				c = Board.Cell(x,y);
				line.Add(c);
				//Debug.Log("adding "+c+" to line");
			}

			foreach (Cell point in line) {
				EffectQueue.Add(new EMove(new Source(actor), actor, point));
			}
			Targeter.Reset();
		}

		int Length (Cell c1, Cell c2) {
			int x = Mathf.Abs(c1.X-c2.X);
			int y = Mathf.Abs(c1.Y-c2.Y);
			return Mathf.Max(x,y);



		}

	}
}
