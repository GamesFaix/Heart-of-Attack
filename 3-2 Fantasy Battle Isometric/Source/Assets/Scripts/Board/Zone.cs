using UnityEngine; 

namespace HOA { 

	public class Zone {
		public Cell[,] cells;
		int size;

		public Zone (int size, Cell start) {
			this.size = size;
			cells = new Cell[size,size];

			for (int i=0; i<size; i++) {
				for (int j=0; j<size; j++) {
					cells[i,j] = Board.Cell(start.X+i, start.Y+j);
				}
			}
		}

		public Cell RandomCell() {
			int randomX = Random.Range(0,size-1);
			int randomY = Random.Range(0,size-1);
			return cells[randomX, randomY];
		}

		CellGroup ToCellGroup () {
			CellGroup group = new CellGroup();
			for (int i=0; i<size; i++) {
				for (int j=0; j<size; j++) {
					group.Add(cells[i,j]);
				}
			}
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
