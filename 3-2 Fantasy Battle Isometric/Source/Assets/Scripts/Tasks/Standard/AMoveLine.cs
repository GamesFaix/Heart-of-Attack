using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class AMoveLine : Task {
		
		int range;

		public override string Desc {get {return "Move "+Parent+" to target cell.";} }
		
		public AMoveLine (Unit parent, int r) {
			Parent = parent;
			Name = "Move";
			Weight = 1;
			Price = Price.Cheap;
			NewAim(new Aim(ETraj.LINE, EType.CELL, EPurp.MOVE, r));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell endCell = (Cell)targets[0];
		
			CellGroup line = new CellGroup();

			Int2 dir = Direction.FromCells(Parent.Body.Cell, endCell);
		
			int length = Length(Parent.Body.Cell, endCell);
		
			Cell c = Parent.Body.Cell;

			for (int i=0; i<length; i++) {
				Int2 index = c.Index + dir;
				c = Game.Board.Cell(index);
				line.Add(c);
			}

			foreach (Cell point in line) {
				EffectQueue.Add(new EMove(new Source(Parent), Parent, point));
			}
		}

		int Length (Cell c1, Cell c2) {
			int x = Mathf.Abs(c1.X-c2.X);
			int y = Mathf.Abs(c1.Y-c2.Y);
			return Mathf.Max(x,y);
		}

	}
}
