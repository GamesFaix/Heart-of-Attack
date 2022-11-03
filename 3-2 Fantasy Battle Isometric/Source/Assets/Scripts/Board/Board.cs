using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public static class Board {
		
		static int min = 2;
		static int max = 20;
		public static int Min {get {return min;} }
		public static int Max {get {return max;} }
		
		static Cell[,] cells = new Cell[1,1];

		public static CellGroup Cells {
			get {
				CellGroup cellGroup = new CellGroup();
				foreach (Cell c in cells) {cellGroup.Add(c);}
				return cellGroup;
			}
		}

		static int cellSize = 25;
		public static int CellSize {get {return cellSize;} }

		public static bool ready = false;
		
		public static void New (int n){
			ready = false;
			
			if (n < min || n > max) {
				GameLog.Debug("Board: New board must be larger than "+min+" and less than "+max+"cell(s).");
				return;
			}

			BorderFactory.Generate(n);
			CellFactory.CreateParent();

			cells = new Cell[n,n];
			for (int x=1; x<=n; x++) {
				for (int y=1; y<=n; y++) {
					cells[x-1,y-1] = new Cell(x,y);		
				}
			}

			CellFactory.AttachPrefabs();

			GameLog.Debug("Board: New ("+n+"x"+n+") board created.");
			ready = true;
		}

		public static int Size {get {return cells.GetLength(0);} }
		
		public static Cell Cell (int x, int y){
			if ((x > 0) && (x <= Size) && (y > 0) && (y <= Size)) {
				return cells[x-1,y-1];
			}
			return default(Cell);
		}
		
		public static bool HasCell (int x, int y, out Cell cell) {
			if (Cell(x,y) != default(Cell)) {
				cell = Cell(x,y);
				return true;
			}
			cell = default(Cell);
			return false;
		}

		public static Cell RandomCell {
			get {
				int randX, randY;
				int max = Size - 1;
				randX = RandomSync.Range(0, max);
				randY = RandomSync.Range(0, max);
				return cells[randX,randY];
			}
		}

		public static bool RandomLegalCell (Token t, out Cell outCell) {
			CellGroup remainingCells = Cells;
			//Debug.Log("starting cellcount "+remainingCells.Count);

			while (remainingCells.Count > 0){
			//	Debug.Log("remaining cells "+remainingCells.Count);
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
		
		public static void Reset () {
			cells = new Cell[1,1];
			ready = false;
			CellFactory.Reset();
			BorderFactory.Reset();
		}
		
		public static void ClearLegal () {
			foreach (Cell cell in cells) {
				cell.Legal = false;
			}
		}
	}
}