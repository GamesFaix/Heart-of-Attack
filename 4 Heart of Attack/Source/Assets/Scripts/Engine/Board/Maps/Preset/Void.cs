namespace HOA.Maps { 

	public class Void : Map {
		public Void () {
			Name = "The Void";
			Size = new size2(5,5);
			
			Density = 0.4f;
			Dist = new Distribution<Species> ();
			Dist.Add(new Possibility<Species>(Species.Curse,2));
			Dist.Add(new Possibility<Species>(Species.Corpse,2));
			Dist.Add(new Possibility<Species>(Species.Mountain,1));
			Dist.Add(new Possibility<Species>(Species.Rock,2));
		}
		
		public override Board Build () {
			Board = new Board(Size, TileSet.Voidoid);
			Landscape = new Landscape(Board);
			Landscape.Add(new index2(1,1), Terrain.RockCorner.FlipVer());
			Landscape.Add(new index2(1,13), Terrain.RockCorner);
			Landscape.Add(new index2(13,1), Terrain.RockCorner.FlipVer().FlipHor());
			Landscape.Add(new index2(13,13), Terrain.RockCorner.FlipHor());
			Landscape.Add(new index2(7,7), Terrain.Volcano);
			Populate();
			return Board;
		}
	}
}
