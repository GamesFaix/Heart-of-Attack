using UnityEngine; 

namespace HOA { 

	public class Zone {
		public Matrix<Cell> cells;
		public static size2 size = new size2(3,3);

		public Zone () {
			cells = new Matrix<Cell>(size);
		}

		public Cell this[int x, int y] {
			get {return cells[x,y];}
			set {cells[x,y] = value;}
		}

		public Cell this[index2 index] {
			get {return cells[index];}
			set {cells[index] = value;}
		}


		TargetGroup ToTargetGroup () {
			TargetGroup group = new TargetGroup();
			foreach (index2 index in size) {group.Add(cells[index]);}
			return group;
		}

		public bool RandomLegalCell (Token t, out Cell outCell) {
			TargetGroup remainingCells = ToTargetGroup();

			while (remainingCells.Count > 0){
				Cell cell = (Cell)(remainingCells.Random());
				if (!t.Body.CanEnter(cell)) {
					remainingCells.Remove(cell);}
				else {
					outCell = cell;
					return true;
				}
			}
			outCell = default(Cell);
			return false;		
		}
	}
}
