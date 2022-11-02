using System;
using System.Collections.Generic;
using HOA.Tokens;
using UnityEngine;

namespace HOA.Map {

	public static class Board {
		
		static int min = 2;
		static int max = 20;
		public static int Min () {return min;}
		public static int Max () {return max;}
		
		public static Cell[,] cells = new Cell[1,1];
		
		public static bool ready = false;
		
		public static void New (int n){
			ready = false;
			
			if (n <= min) {
				GameLog.Debug("Board: New board must be larger than "+min+" cell(s).");
				return;
			}
			if (n > max) {
				GameLog.Debug("Board: New board cannot be larger than "+max+"x"+max+" cells.");
				return;
			}
			
			cells = new Cell[n,n];
			for (int x=1; x<=n; x++) {
				for (int y=1; y<=n; y++) {
	
					cells[x-1,y-1] = new Cell(x,y);		
				}
			}
			GameLog.Debug("Board: New ("+n+"x"+n+") board created.");
			ready = true;
		}
		
		public static int Size () {return cells.GetLength(0);}
		
		public static Cell Cell (int x, int y){
			if ((x > 0) && (x <= Size()) && (y > 0) && (y <= Size())) {
				return cells[x-1,y-1];
			}
			else {
				GameLog.Debug("Board: Cell # less than 1 or greater than size.");
				return default(Cell);
			}
		}
		
		public static bool HasCell (int x, int y, out Cell cell) {
			if (Cell(x,y) != default(Cell)) {
				cell = Cell(x,y);
				return true;
			}
			cell = default(Cell);
			return false;
		}
		
		public static List<Cell> Cells () {
			List<Cell> list = new List<Cell>();
			foreach (Cell c in cells) {list.Add(c);}
			return list;
		}
		
		public static Cell RandomCell () {
			int randX, randY;
			randX = RandomSync.Range(0, Size()-1);
			randY = RandomSync.Range(0, Size()-1);
			return cells[randX,randY];
		}
		
		public static bool RandomLegalCell (Token t, out Cell outCell) {
			List<PLANE> planes = t.Plane();
			List<Cell> remainingCells = new List<Cell>();
			foreach (Cell cell in cells) {remainingCells.Add(cell);}
			
			while (remainingCells.Count > 0){
				Cell cell = Board.RandomCell();
				if (cell.Occupied(planes)) {remainingCells.Remove(cell);}
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
	//		if (cells != default(Cell[,])) {foreach (Cell c in cells) {c.Clear();}}
		}
		
		public static void ClearLegal () {
			foreach (Cell cell in cells) {
				cell.Legalize(false);
			}
		}
	}
	
}