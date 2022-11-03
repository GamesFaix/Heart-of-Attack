using UnityEngine;
using System.Collections.Generic;

namespace HOA.Actions {
	
	public class Dart : Task {
		
		int range;

		public override string desc {get {return "Move "+parent+" to target cell.";} }
		
		public Dart (Unit parent, int range) : base(parent){
			name = "Dart";
			weight = 1;
			aims += Aim.MoveLine(range);
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			Cell endCell = (Cell)targets[0];
		
			CellGroup line = new CellGroup();

			int2 dir = Direction.FromCells(parent.Body.Cell, endCell);
		
			int length = Length(parent.Body.Cell, endCell);
		
			Cell c = parent.Body.Cell;

			for (int i=0; i<length; i++) {
				index2 index = c.Index + dir;
				c = Game.Board.Cell(index);
				line.Add(c);
			}

			foreach (Cell point in line) {
				EffectQueue.Add(new Effects.Move(source, parent, point));
			}
		}

		int Length (Cell c1, Cell c2) {
			int x = Mathf.Abs(c1.X-c2.X);
			int y = Mathf.Abs(c1.Y-c2.Y);
			return Mathf.Max(x,y);
		}

	}
}
