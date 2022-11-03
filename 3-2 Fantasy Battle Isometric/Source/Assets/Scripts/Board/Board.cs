using System;
using UnityEngine; 

namespace HOA { 

	public class Board {
		public static Int2 MinZones {get {return new Int2(2,2);} }
		public static Int2 MaxZones {get {return new Int2(6,6);} }

		BoardPhysical physical;

		public Int2 CellCount {get; private set;}
		Matrix<Cell> cells;

		public Cell TemplateCell {get; private set;}

		public TileSet TileSet {get; private set;}

		public Board (Int2 zoneCount, TileSet tileSet=null){
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

				TemplateCell = new Cell (this, new Int2(-1,-1));
				TemplateCell.Display.HideTerrain();
			}
		}



		static bool LegalBoardSize (Int2 zoneCount) {
			if (!MaxZones.Fits(zoneCount)) {
				Debug.Log("Board: New board must be smaller than "+MaxZones+" zones.");
				return false;
			}
			if (!zoneCount.Fits(MinZones)) {
				Debug.Log("Board: New board must be larger than "+MinZones+" zones.");
				return false;
			}
			return true;
		}

		void CreateBorder () {
			for (int i=0; i<CellCount.x; i++) {
				Int2 index = new Int2(i,0);
				cells[index] = new ExoCell(this, index);
			}
			for (int i=0; i<CellCount.x; i++) {
				Int2 index = new Int2(i,CellCount.x-1);
				cells[index] = new ExoCell(this, index);
			}
			for (int i=0; i<CellCount.y; i++) {
				Int2 index = new Int2(0,i);
				cells[index] = new ExoCell(this, index);
			}
			for (int i=0; i<CellCount.y; i++) {
				Int2 index = new Int2(CellCount.y-1,i);
				cells[index] = new ExoCell(this, index);
			}
		}

		void CreateCells () {
			for (int i=1; i<CellCount.x-1; i++) {
				for (int j=1; j<CellCount.y-1; j++) {
					Int2 index = new Int2(i,j);
					cells[index] = new Cell(this, index);
				}
			}
		}

		public Matrix<Zone> Zones () {
			Int2 zoneCount = (CellCount-2)/Zone.size;
			Matrix<Zone> zones = new Matrix<Zone>(zoneCount);
			for (int i=0; i<zoneCount.x; i++) {
				for (int j=0; j<zoneCount.y; j++) {
					Int2 zoneIndex = new Int2(i,j);
					zones[zoneIndex] = new Zone();
					for (int k=0; k<Zone.size.x; k++) {
						for (int l=0; l<Zone.size.y; l++) {
							Int2 localIndex = new Int2(k,l);
							Int2 globalIndex = zoneIndex*Zone.size + localIndex + 1;
							zones[zoneIndex][localIndex] = Cell(globalIndex);
						}
					}
				}
			}

			return zones;
		}




		public static int MaxPlayers (Int2 zoneCount) {
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
		
		public Cell Cell (Int2 index) {
			Cell cell = null;
			if (cells.TryIndex(index, out cell)) {return cell;}
			else {throw new Exception ("Board does not contain cell at "+index+".");}
		}
		public Cell Cell (int x, int y) {return Cell(new Int2(x,y));}

		public bool HasCell (Int2 index, out Cell cell) {
			cell = null;
			if (cells.TryIndex(index, out cell)) {return true;}
			return false;
		}
		public bool HasCell (int x, int y, out Cell cell) {return HasCell(new Int2(x,y), out cell);}

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
