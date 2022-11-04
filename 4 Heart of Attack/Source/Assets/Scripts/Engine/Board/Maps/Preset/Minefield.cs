namespace HOA.Maps { 
	
	public class Minefield : Map {
		
		public Minefield () {
			Name = "Minefield";
			Size = new size2(5,5);
			
			Density = 0.4f;
			Dist = new Distribution<Species> ();
			Dist.Add(new Possibility<Species>(Species.Tree,4));
			Dist.Add(new Possibility<Species>(Species.House,2));
			Dist.Add(new Possibility<Species>(Species.Mountain,1));
			Dist.Add(new Possibility<Species>(Species.Hill,1));
		}
		
		public override Board Build () {
			Board = new Board(Size, TileSet.NewRep);
			Landscape = new Landscape(Board);
			Landscape.Add(new index2(4,2), MountainLine.FlipPos());
			Landscape.Add(new index2(10,2), MountainLine.FlipPos());
			Landscape.Add(new index2(4,12), MountainLine.FlipPos());
			Landscape.Add(new index2(10,12), MountainLine.FlipPos());
			Landscape.Add(new index2(2,4), MountainLine);
			Landscape.Add(new index2(2,10), MountainLine);
			Landscape.Add(new index2(12,4), MountainLine);
			Landscape.Add(new index2(12,10), MountainLine);

			Landscape.Add(new index2(7,2), MineWall.FlipPos());
			Landscape.Add(new index2(2,7), MineWall);
			Landscape.Add(new index2(7,12), MineWall.FlipPos());
			Landscape.Add(new index2(12,7), MineWall);

			Landscape.Add(new index2(4,4), MineCorner);
			Landscape.Add(new index2(4,10), MineCorner.FlipVer());
			Landscape.Add(new index2(10,4), MineCorner.FlipHor());
			Landscape.Add(new index2(10,10), MineCorner.FlipHor().FlipVer());

			Landscape.Add(new index2(4,7), MineHill.FlipPos());
			Landscape.Add(new index2(10,7), MineHill.FlipPos().FlipHor());
			Landscape.Add(new index2(7,4), MineHill);
			Landscape.Add(new index2(7,10), MineHill.FlipVer());

			Landscape.Add(new index2(7,7), Center);
			Populate();
			return Board;
		}

		Terrain MountainLine {
			get {
				return new Terrain ( new Species[] {
					Species.None, Species.Mountain, Species.None,
					Species.None, Species.Mountain, Species.None,
					Species.None, Species.Mountain, Species.None
				});
			}
		}
		
		Terrain MineWall {
			get {
				return new Terrain (new Species[] {
					Species.None, Species.House, Species.None,
					Species.None, Species.Mine, Species.None,
					Species.None, Species.House, Species.None
				});
			}
		}
		
		Terrain MineCorner {
			get {
				return new Terrain (new Species[] {
					Species.House, Species.House, Species.None,
					Species.House, Species.Mine, Species.None,
					Species.None, Species.None, Species.BombingRange
				});
			}
		}
		
		Terrain MineHill {
			get {
				return new Terrain (new Species[] {
					Species.None, Species.None, Species.None,
					Species.None, Species.Mine, Species.None,
					Species.Hill, Species.None, Species.Hill
				});
			}
		}

		Terrain Center {
			get {
				return new Terrain (new Species[] {
					Species.House, Species.None, Species.House,
					Species.None, Species.Mine, Species.None,
					Species.House, Species.None, Species.House
				});
			}
		}
	}
}
