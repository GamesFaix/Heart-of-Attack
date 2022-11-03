using UnityEngine; 

namespace HOA { 

	public class Zone {
		public Matrix<Cell> cells;
		public static Size2 size = new Size2(3,3);

		public Zone (Index2 zoneIndex) {
			cells = new Matrix<Cell>(size);
			foreach (Index2 localIndex in size) {
				Index2 globalIndex = (size*zoneIndex) + localIndex;
				cells[localIndex] = new Cell(globalIndex);
			}
		}

		public Cell this[byte x, byte y] {
			get {return cells[x,y];}
			set {cells[x,y] = value;}
		}

		public Cell this[Index2 index] {
			get {return cells[index];}
			set {cells[index] = value;}
		}


		CellGroup ToCellGroup () {
			CellGroup group = new CellGroup();
			foreach (Index2 index in size) {group.Add(cells[index]);}
			return group;
		}

		public bool RandomLegalCell (Token t, out Cell outCell) {
			CellGroup remainingCells = ToCellGroup();

			while (remainingCells.Count > 0){
				Cell cell = remainingCells.Random();
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
