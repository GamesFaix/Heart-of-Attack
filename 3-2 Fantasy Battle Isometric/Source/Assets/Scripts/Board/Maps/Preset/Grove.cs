namespace HOA.Maps { 

	public class Grove : Map {
		
		public Grove () {
			Name = "The Grove";
			Size = new size2(3,3);
			
			Density = 0.0f;
			Dist = new Distribution<EToken> ();
			Dist.Add(new Possibility<EToken>(EToken.TREE3,4));
			Dist.Add(new Possibility<EToken>(EToken.ICE,2));
			Dist.Add(new Possibility<EToken>(EToken.MNTN,1));
			Dist.Add(new Possibility<EToken>(EToken.HILL,1));
		}
		
		public override Board Build () {
			Board = new Board(Size, TileSet.Grove);
			Landscape = new Landscape(Board);
			Landscape.Add(new index2(1,1), Terrain.RockCorner.FlipVer());
			Landscape.Add(new index2(1,7), Terrain.RockCorner);
			Landscape.Add(new index2(7,1), Terrain.RockCorner.FlipVer().FlipHor());
			Landscape.Add(new index2(7,7), Terrain.RockCorner.FlipHor());
			Landscape.Add(new index2(4,4), Terrain.FrozenLake);
			Populate();
			return Board;
		}
	}
}
