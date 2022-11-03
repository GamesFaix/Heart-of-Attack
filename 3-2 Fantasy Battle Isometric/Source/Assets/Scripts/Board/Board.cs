using System;
using UnityEngine; 

namespace HOA { 

	public class Board {
		public static size2 MinZones {get {return new size2(2,2);} }
		public static size2 MaxZones {get {return new size2(6,6);} }

		BoardPhysical physical;

		public size2 CellCount {get; private set;}
		Matrix<Cell> cells;

		public TileSet TileSet {get; private set;}

		public Board (size2 zoneCount, TileSet tileSet=null){
			if (LegalBoardSize(zoneCount)) {
				if (Game.Board != null) {Game.Board.Destroy();}
			
				if (tileSet==null) {TileSet = TileSet.Random;}
				else {TileSet = tileSet;}

				CellCount = (zoneCount * Zone.size) + 2;
				cells = new Matrix<Cell>(CellCount);
				CreateBorder();
				CreateCells();

				physical = new BoardPhysical(this);
				physical.AttachCellPrefabs();
			}
		}

		static bool LegalBoardSize (size2 zoneCount) {
			if (!zoneCount.FitsIn(MaxZones)) {
				Debug.Log("Board: New board must be smaller than "+MaxZones+" zones.");
				return false;
			}
			if (!zoneCount.FitsAround(MinZones)) {
				Debug.Log("Board: New board must be larger than "+MinZones+" zones.");
				return false;
			}
			return true;
		}

		void CreateBorder () {
			for (int i=0; i<CellCount.x; i++) {
				index2 index = new index2(i,0);
				cells[index] = new ExoCell(this, index);
			}
			for (int i=0; i<CellCount.x; i++) {
				index2 index = new index2(i,CellCount.x-1);
				cells[index] = new ExoCell(this, index);
			}
			for (int i=0; i<CellCount.y; i++) {
				index2 index = new index2(0,i);
				cells[index] = new ExoCell(this, index);
			}
			for (int i=0; i<CellCount.y; i++) {
				index2 index = new index2(CellCount.y-1,i);
				cells[index] = new ExoCell(this, index);
			}
		}

		void CreateCells () {
			for (int i=1; i<CellCount.x-1; i++) {
				for (int j=1; j<CellCount.y-1; j++) {
					index2 index = new index2(i,j);
					cells[index] = new Cell(this, index);
				}
			}
		}

		public Matrix<Zone> Zones () {
			size2 zoneCount = (CellCount-2)/Zone.size;
			Matrix<Zone> zones = new Matrix<Zone>(zoneCount);
			for (int i=0; i<zoneCount.x; i++) {
				for (int j=0; j<zoneCount.y; j++) {
					index2 zoneIndex = new index2(i,j);
					zones[zoneIndex] = new Zone();
					for (int k=0; k<Zone.size.x; k++) {
						for (int l=0; l<Zone.size.y; l++) {
							index2 localIndex = new index2(k,l);
							int2 zoneStart = (int2)zoneIndex*(int2)(Zone.size);
							index2 globalIndex = new int2(1,1) + localIndex + zoneStart;
							zones[zoneIndex][localIndex] = Cell(globalIndex);
						}
					}
				}
			}

			return zones;
		}

		public static int MaxPlayers (size2 zoneCount) {
			int peripheralZones = 2* (zoneCount.x-1 + zoneCount.y-1);
			return Mathf.Min(8, peripheralZones/2);
		}
	
		public CellGroup Cells {
			get {
				CellGroup cellGroup = new CellGroup();
				foreach (Cell c in cells) {cellGroup.Add(c);}
				return cellGroup;
			}
		}
		
		public Cell Cell (index2 index) {return cells[index];}
		public Cell Cell (int x, int y) {return Cell(new index2(x,y));}

		public bool HasCell (index2 index, out Cell cell) {
			cell = null;
			if (cells.Contains(index, out cell)) {
				if (cell is ExoCell) {return false;}
				return true;
			}
			return false;
		}
		public bool HasCell (int x, int y, out Cell cell) {
			index2 index;
			cell = null;
			if (index2.Safe(new int2(x,y), out index)) {return HasCell(index, out cell);}
			return false;
		}

		public Cell RandomCell {get {return cells.Random;} }

		public bool RandomLegalCell (Token t, out Cell outCell) {
			CellGroup remainingCells = Cells;
			while (remainingCells.Count > 0){
				Cell cell = remainingCells.Random();
				if (!t.Body.CanEnter(cell)) {remainingCells.Remove(cell);}
				else {outCell = cell; return true;}
			}
			outCell = null;
			return false;		
		}

		public void ClearLegal () {
			foreach (Cell cell in cells) {cell.Legal = false;}
		}

		public void Destroy () {physical.Destroy();}
	}
}
