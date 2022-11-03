using UnityEngine;
using System.Collections.Generic;

namespace HOA.Actions {
	
	public class Dart : Task {
		
		int range;

		public override string Desc {get {return "Move "+Parent+" to target cell.";} }
		
		public Dart (Unit parent, int r) {
			Parent = parent;
			Name = "Dart";
			Weight = 1;
			Price = Price.Cheap;
			NewAim(new Aim(ETraj.LINE, ESpecial.CELL, EPurp.MOVE, r));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell endCell = (Cell)targets[0];
		
			CellGroup line = new CellGroup();

			int2 dir = Direction.FromCells(Parent.Body.Cell, endCell);
		
			int length = Length(Parent.Body.Cell, endCell);
		
			Cell c = Parent.Body.Cell;

			for (int i=0; i<length; i++) {
				index2 index = c.Index + dir;
				c = Game.Board.Cell(index);
				line.Add(c);
			}

			foreach (Cell point in line) {
				EffectQueue.Add(new Effects.Move(new Source(Parent), Parent, point));
			}
		}

		int Length (Cell c1, Cell c2) {
			int x = Mathf.Abs(c1.X-c2.X);
			int y = Mathf.Abs(c1.Y-c2.Y);
			return Mathf.Max(x,y);
		}

	}
}
