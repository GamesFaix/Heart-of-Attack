using System;
using UnityEngine; 

namespace HOA { 

	public class Board {
	
		BoardPhysical physical;

		public static Size2 MinZones {get {return new Size2(2,2);} }
		public static Size2 MaxZones {get {return new Size2(6,6);} }

		Size2 ZoneCount {get; set;}
		public Size2 CellCount {get; private set;}

		Matrix<Zone> zones;
		Matrix<Cell> cells;

		Matrix<Terrain> terrains;
		Matrix<EToken> landscape;

		public Cell TemplateCell {get; private set;}

		Board (Size2 size){
			CheckBoardSize(size);
			if (Game.Board != null) {Game.Board.Destroy();}

			ZoneCount = size;
			CellCount = ZoneCount * Zone.size;
			physical = new BoardPhysical(this);

			zones = new Matrix<Zone> (ZoneCount);
			terrains = new Matrix<Terrain> (ZoneCount);
			foreach (Index2 index in ZoneCount) {zones[index] = new Zone(index);}
			foreach (Index2 index in terrains.Size) {terrains[index] = Terrain.Blank;}

			cells = new Matrix<Cell>(CellCount);
			landscape = new Matrix<EToken>(CellCount);
			foreach (Index2 zIndex in ZoneCount) {
				foreach (Index2 cIndex in Zone.size) {
					Zone z = zones[zIndex];
					Cell c = z[cIndex];
					cells[zIndex.x*Zone.size.x+cIndex.x, zIndex.y*Zone.size.y+cIndex.y] = c;
				}
			}
			foreach (Index2 index in CellCount) {landscape[index] = EToken.NONE;}

			TemplateCell = new Cell (Size2.Max);
			physical.AttachCellPrefabs();
		}

		public static Board Random (Size2 size) {
			Board board = new Board(size);
			SpawnKings(SpawnZones(board.zones.Periphery));
			return board;
		}

		public static Board Map1 () {
			Size2 size = new Size2(3,3);
			Board board = new Board(size);
			board.terrains [0,0] = Terrain.RockCorner;//.FlipVer();
			board.terrains [0,2] = Terrain.RockCorner;
			board.terrains [2,0] = Terrain.RockCorner;//.FlipVer().FlipHor();
			board.terrains [2,2] = Terrain.RockCorner;//.FlipHor();
			board.terrains [1,1] = Terrain.Lake;

			CreateTerrain(board);
			SpawnKings(SpawnZones(board.zones.Periphery));
			return board;
		}

		public static Board Map2 () {
			Size2 size = new Size2(5,5);
			Board board = new Board(size);
			board.terrains [0,0] = Terrain.RockCorner;//.FlipVer();
			board.terrains [0,4] = Terrain.RockCorner;
			board.terrains [4,0] = Terrain.RockCorner;//.FlipVer().FlipHor();
			board.terrains [4,4] = Terrain.RockCorner;//.FlipHor();
			board.terrains [2,2] = Terrain.Volcano;
			
			CreateTerrain(board);
			SpawnKings(SpawnZones(board.zones.Periphery));
			return board;
		}



		static void CreateTerrain (Board board) {
			foreach (Index2 zIndex in board.ZoneCount) {
				foreach (Index2 cIndex in Zone.size) {
					Terrain z = board.terrains[zIndex];
					EToken t = z[cIndex];
					board.landscape[zIndex.x*Zone.size.x+cIndex.x, zIndex.y*Zone.size.y+cIndex.y] = t;
				}
			}

			EffectGroup effects = new EffectGroup();
			foreach (Index2 index in board.cells.Size) {
				if (board.landscape[index] != EToken.NONE) {
					effects.Add(new ECreate(new Source(Roster.Neutral), board.landscape[index], board.cells[index]));
				}
			}
			EffectQueue.Add(effects);
		}

		void CheckBoardSize (Size2 zoneCount) {
			if (!zoneCount.FitsInside(MaxZones)) {
				throw new Exception("Board: New board must be smaller than "+MaxZones+" zones.");
			}
			if (!zoneCount.FitsAround(MinZones)) {
				throw new Exception("Board: New board must be larger than "+MinZones+" zones.");
			}
		}

		public static int MaxPlayers (Size2 zoneCount) {
			int peripheralZones = 2* (zoneCount.x-1 + zoneCount.y-1);
			return Mathf.Min(8, peripheralZones/2);
		}

		static Group<Zone> SpawnZones (Group<Zone> periphery) {
			int playerCount = Roster.Players().Count;
			int playerSpacing = (int)Mathf.Floor(periphery.Count/playerCount);
			
			Group<Zone> spawnZones = new Group<Zone>();
			Zone firstZone = periphery.Random();
			spawnZones.Add(firstZone);
			int index = periphery.IndexOf(firstZone);
			
			for (int i=2; i<=playerCount; i++) {
				int nextIndex = (index+playerSpacing) % periphery.Count;
				spawnZones.Add(periphery[nextIndex]);
				index = nextIndex;
			}
			return spawnZones;
		}
		
		static void SpawnKings (Group<Zone> spawnZones) {
			spawnZones.Shuffle();
			int zoneIndex = 0;
			
			EffectGroup heroSpawn = new EffectGroup();
			
			foreach (Player p in Roster.Players()) {
				Cell cell;
				Token temp = TemplateFactory.Template(p.King);
				
				Zone zone = spawnZones[zoneIndex];
				
				if (zone.RandomLegalCell(temp, out cell)) {
					heroSpawn.Add(new ECreate (new Source(p), p.King, cell));
				}
				else {
					Debug.Log("Cannot spawn "+temp+". No legal cells.");
				}
				zoneIndex++;
			}
			EffectQueue.Add(heroSpawn);
		}
	

		public CellGroup Cells {
			get {
				CellGroup cellGroup = new CellGroup();
				foreach (Cell c in cells) {cellGroup.Add(c);}
				return cellGroup;
			}
		}
		
		public Cell Cell (Index2 index) {
			Cell cell = null;
			if (cells.TryIndex(index, out cell)) {return cell;}
			else {throw new Exception ("Board does not contain cell at "+index+".");}
		}
		public Cell Cell (byte x, byte y) {return Cell(new Index2(x,y));}

		public bool HasCell (Index2 index, out Cell cell) {
			cell = null;
			if (cells.TryIndex(index, out cell)) {return true;}
			return false;
		}
		public bool HasCell (byte x, byte y, out Cell cell) {return HasCell(new Index2(x,y), out cell);}

		
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
