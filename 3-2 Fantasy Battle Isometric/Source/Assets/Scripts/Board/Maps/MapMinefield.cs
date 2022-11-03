using UnityEngine; namespace HOA { 
	
	public class MapMinefield : Map {
		
		public MapMinefield () {
			Name = "Minefield";
			Size = new Int2(5,5);
			
			Density = 0.4f;
			Dist = new Distribution<EToken> ();
			Dist.Add(new Possibility<EToken>(EToken.TREE,4));
			Dist.Add(new Possibility<EToken>(EToken.HOUS,2));
			Dist.Add(new Possibility<EToken>(EToken.MNTN,1));
			Dist.Add(new Possibility<EToken>(EToken.HILL,1));
		}
		
		public override Board Build () {
			Board = new Board(Size, TileSet.NewRep);
			Landscape = new Landscape(Board);
			Landscape.Add(new Int2(4,2), MountainLine.FlipPos());
			Landscape.Add(new Int2(10,2), MountainLine.FlipPos());
			Landscape.Add(new Int2(4,12), MountainLine.FlipPos());
			Landscape.Add(new Int2(10,12), MountainLine.FlipPos());
			Landscape.Add(new Int2(2,4), MountainLine);
			Landscape.Add(new Int2(2,10), MountainLine);
			Landscape.Add(new Int2(12,4), MountainLine);
			Landscape.Add(new Int2(12,10), MountainLine);

			Landscape.Add(new Int2(7,2), MineWall.FlipPos());
			Landscape.Add(new Int2(2,7), MineWall);
			Landscape.Add(new Int2(7,12), MineWall.FlipPos());
			Landscape.Add(new Int2(12,7), MineWall);

			Landscape.Add(new Int2(4,4), MineCorner);
			Landscape.Add(new Int2(4,10), MineCorner.FlipVer());
			Landscape.Add(new Int2(10,4), MineCorner.FlipHor());
			Landscape.Add(new Int2(10,10), MineCorner.FlipHor().FlipVer());

			Landscape.Add(new Int2(4,7), MineHill.FlipPos());
			Landscape.Add(new Int2(10,7), MineHill.FlipPos().FlipHor());
			Landscape.Add(new Int2(7,4), MineHill);
			Landscape.Add(new Int2(7,10), MineHill.FlipVer());

			Landscape.Add(new Int2(7,7), Center);
			Populate();
			return Board;
		}

		Terrain MountainLine {
			get {
				return new Terrain ( new EToken[] {
					EToken.NONE, EToken.MNTN, EToken.NONE,
					EToken.NONE, EToken.MNTN, EToken.NONE,
					EToken.NONE, EToken.MNTN, EToken.NONE
				});
			}
		}
		
		Terrain MineWall {
			get {
				return new Terrain (new EToken[] {
					EToken.NONE, EToken.HOUS, EToken.NONE,
					EToken.NONE, EToken.MINE, EToken.NONE,
					EToken.NONE, EToken.HOUS, EToken.NONE
				});
			}
		}
		
		Terrain MineCorner {
			get {
				return new Terrain (new EToken[] {
					EToken.HOUS, EToken.HOUS, EToken.NONE,
					EToken.HOUS, EToken.MINE, EToken.NONE,
					EToken.NONE, EToken.NONE, EToken.TARG
				});
			}
		}
		
		Terrain MineHill {
			get {
				return new Terrain (new EToken[] {
					EToken.NONE, EToken.NONE, EToken.NONE,
					EToken.NONE, EToken.MINE, EToken.NONE,
					EToken.HILL, EToken.NONE, EToken.HILL
				});
			}
		}

		Terrain Center {
			get {
				return new Terrain (new EToken[] {
					EToken.HOUS, EToken.NONE, EToken.HOUS,
					EToken.NONE, EToken.MINE, EToken.NONE,
					EToken.HOUS, EToken.NONE, EToken.HOUS
				});
			}
		}
	}
}
