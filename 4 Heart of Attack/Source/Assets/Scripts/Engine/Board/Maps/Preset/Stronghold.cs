namespace HOA.Maps { 
	
	public class Stronghold : Map {
		
		public Stronghold () {
			Name = "Stronghold";
			Size = new size2(4,4);
			
			Density = 0.4f;
			Dist = new Distribution<Species> ();
			//Dist.Add(new Possibility<Species>(Species.Rampart,1));
			Dist.Add(new Possibility<Species>(Species.Cottage,1));
			Dist.Add(new Possibility<Species>(Species.Rock,2));
			Dist.Add(new Possibility<Species>(Species.Lava,1));
			Dist.Add(new Possibility<Species>(Species.Mountain,1));

		}
		
		public override Board Build () {
			Board = new Board(Size, TileSet.Torridale);
			Landscape = new Landscape(Board);

			Landscape.Add(new index2(1,6), Side);
			Landscape.Add(new index2(10,6), Side.FlipHor());
			Landscape.Add(new index2(4,6), Inside);
			Landscape.Add(new index2(7,6), Inside.FlipHor());

			Landscape.Add(new index2(3,3), Corner);
			Landscape.Add(new index2(3,8), Corner.FlipVer());
			Landscape.Add(new index2(8,3), Corner.FlipHor());
			Landscape.Add(new index2(8,8), Corner.FlipVer().FlipHor());

			Landscape.Add(new index2(6,1), Species.Lava);
			Landscape.Add(new index2(7,1), Species.Lava);
			Landscape.Add(new index2(6,12), Species.Lava);
			Landscape.Add(new index2(7,12), Species.Lava);


			Populate();
			return Board;
		}

		Terrain Corner {
			get {
				return new Terrain (new Species[] {
					Species.Mountain, Species.Water, Species.Water,
					Species.Water, Species.Mountain, Species.Rampart,
					Species.Water, Species.Rampart, Species.None
				});
			}
		}

		Terrain Side {
			get {
				return new Terrain (new Species[] {
					Species.Lava, Species.None, Species.Water,
					Species.Lava, Species.None, Species.Water,
					Species.None, Species.None, Species.None
				});
			}
		}

		Terrain Inside {
			get {
				return new Terrain (new Species[] {
					Species.Rampart, Species.None, Species.Lava,
					Species.Rampart, Species.None, Species.Lava,
					Species.None, Species.None, Species.None
				});
			}
		}

	}
}
