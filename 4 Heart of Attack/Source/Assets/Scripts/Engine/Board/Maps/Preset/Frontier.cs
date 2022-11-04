namespace HOA.Maps { 
	
	public class Frontier : Map {
		public Frontier () {
			Name = "Frontier";
			Size = new size2(4,4);
			
			Density = 0.5f;
			Dist = new Distribution<Species> ();
			Dist.Add(new Possibility<Species>(Species.Mountain,2));
			Dist.Add(new Possibility<Species>(Species.Cottage,1));
			Dist.Add(new Possibility<Species>(Species.Tree,2));
			Dist.Add(new Possibility<Species>(Species.Rock,2));
		}
		
		public override Board Build () {
			Board = new Board(Size, TileSet.Chrono);
			Landscape = new Landscape(Board);

			Landscape.Add(new index2(1,1), Sink);
			Landscape.Add(new index2(6,1), Sink);
			Landscape.Add(new index2(11,1), Sink);
			Landscape.Add(new index2(1,6), Sink);
			Landscape.Add(new index2(11,6), Sink);
			Landscape.Add(new index2(1,11), Sink);
			Landscape.Add(new index2(6,11), Sink);
			Landscape.Add(new index2(11,11), Sink);

			Landscape.Add(new index2(5,5), Well);
			Landscape.Add(new index2(6,6), Well.FlipVer().FlipHor());

			Landscape.Add(new index2(3,3), Corner);
			Landscape.Add(new index2(3,8), Corner.FlipVer());
			Landscape.Add(new index2(8,3), Corner.FlipHor());
			Landscape.Add(new index2(8,8), Corner.FlipVer().FlipHor());

			Populate();
			return Board;
		}

		Terrain Sink {
			get {
				return new Terrain (new Species[] {
					Species.TimeSink, Species.TimeSink, Species.None,
					Species.TimeSink, Species.TimeSink, Species.None,
					Species.None, Species.None, Species.None
				});
			}
		}

		Terrain Well {
			get {
				return new Terrain (new Species[] {
					Species.None, Species.TimeWell, Species.TimeWell,
					Species.TimeWell, Species.TimeWell, Species.TimeWell,
					Species.TimeWell, Species.TimeWell, Species.TimeWell
				});
			}
		}

		Terrain Corner {
			get {
				return new Terrain (new Species[] {
					Species.None, Species.Mountain, Species.None,
					Species.Mountain, Species.None, Species.Rock,
					Species.None, Species.Rock, Species.Water
				});
			}
		}

	}
}
