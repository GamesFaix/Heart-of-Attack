using UnityEngine; 
using System;

namespace HOA { 

	public class NeighborMatrix : Matrix<Cell> {

		public NeighborMatrix (Cell center) {
			Size = new Int2 (3,3);
			array = new Cell[Size.x, Size.y];
			foreach (Int2 index in Size) {
				this[index] = null;
			}

			Int2 startIndex = center.Index - 1;
			foreach (Int2 index in Size) {
				Cell cell;
				if (Game.Board.HasCell(startIndex+index, out cell)) {
					this[index] = cell;
				}
			}
		}

		bool IndexClockwise (Int2 start, out Int2 next) {
			int x = start.x;
			int y = start.y;
			next = new Int2 (0,0);

			if (!Size.Covers(start)) {
				Debug.Log("NeighborMatrix.IndexClockwise: Start cell not in matrix.");
				return false;
			}
			else if (x==1 && y==1) {
				Debug.Log("NeighborMatrix.IndexClockwise: Cannot start at center.");
				return false;
			}
			else {
				if (x==0 && y==0) {next = new Int2(1,0);}
				else if (x==1 && y==0) {next = new Int2(2,0);}
				else if (x==2 && y==0) {next = new Int2(2,1);}
				else if (x==2 && y==1) {next = new Int2(2,2);}
				else if (x==2 && y==2) {next = new Int2(1,2);}
				else if (x==1 && y==2) {next = new Int2(0,2);}
				else if (x==0 && y==2) {next = new Int2(0,1);}
				else if (x==0 && y==1) {next = new Int2(0,0);}
				return true;	
			}
		}

		bool IndexCounter (Int2 start, out Int2 next) {
			int x = start.x;
			int y = start.y;
			next = new Int2 (0,0);
			
			if (!Size.Covers(start)) {
				Debug.Log("NeighborMatrix.IndexCounter: Start cell not in matrix.");
				return false;
			}
			else if (x==1 && y==1) {
				Debug.Log("NeighborMatrix.IndexCounter: Cannot start at center.");
				return false;
			}
			else {
				if (x==0 && y==0) {next = new Int2(0,1);}
				else if (x==0 && y==1) {next = new Int2(0,2);}
				else if (x==0 && y==2) {next = new Int2(1,2);}
				else if (x==1 && y==2) {next = new Int2(2,2);}
				else if (x==2 && y==2) {next = new Int2(2,1);}
				else if (x==2 && y==1) {next = new Int2(2,0);}
				else if (x==2 && y==0) {next = new Int2(1,0);}
				else if (x==1 && y==0) {next = new Int2(0,0);}
				return true;	
			}
		}

		public bool CellClockwise (Cell start, out Cell next) {
			next = null;

			if (!Contains(start)) {
				Debug.Log("NeighborMatrix.CellClockwise: Start cell not in Matrix.");
				return false;
			}
			else if (start == this[new Int2(1,1)]) {
				Debug.Log("NeighborMatrix.CellClockwise: Cannot start at center.");
				return false;
			}
			else {
				Int2 startIndex = IndexOf(start);
				Int2 nextIndex;
				if (IndexClockwise(startIndex, out nextIndex)) {
					next = this[nextIndex];
					return true;
				}
				else {return false;}
			}
		}

		public bool CellCounter (Cell start, out Cell next) {
			next = null;
			
			if (!Contains(start)) {
				Debug.Log("NeighborMatrix.CellCounter: Start cell not in Matrix.");
				return false;
			}
			else if (start == this[new Int2(1,1)]) {
				Debug.Log("NeighborMatrix.CellCounter: Cannot start at center.");
				return false;
			}
			else {
				Int2 startIndex = IndexOf(start);
				Int2 nextIndex;
				if (IndexCounter(startIndex, out nextIndex)) {
					next = this[nextIndex];
					return true;
				}
				else {return false;}
			}
		}

		public CellGroup Ring (Cell first, Cell second) {
			CellGroup ring = new CellGroup(first);
			bool clockwise;

			Cell nextClockwise;
			Cell nextCounter;

			if (CellClockwise(first, out nextClockwise) 
			&& second == nextClockwise) {
				clockwise = true;
			}
			else if (CellCounter(first, out nextCounter)
			&& second == nextCounter) {
				clockwise = false;
			}
			else {
				Debug.Log("NeighborMatrix.Ring: Second cell invalid.");
				return ring;
			}

			Cell last = first;
			Cell next;

			for (int i=0; i<8; i++) {
				if (clockwise) {
					if (CellClockwise(last, out next)) {
						ring.Add(next);
						last = next;
					}
					else {return ring;}
				}
				else {
					if (CellCounter(last, out next)) {
						ring.Add(next);
						last = next;
					}
					else {return ring;}
				}
			}
			return ring;
		}
	}
}
