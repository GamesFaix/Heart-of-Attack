using UnityEngine; namespace HOA { 

	public class MapGrove : Map {
		
		public MapGrove () {
			Name = "The Grove";
			Size = new Int2(3,3);
			
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
			Landscape.Add(new Int2(1,1), Terrain.RockCorner.FlipVer());
			Landscape.Add(new Int2(1,7), Terrain.RockCorner);
			Landscape.Add(new Int2(7,1), Terrain.RockCorner.FlipVer().FlipHor());
			Landscape.Add(new Int2(7,7), Terrain.RockCorner.FlipHor());
			Landscape.Add(new Int2(4,4), Terrain.FrozenLake);
			Populate();
			return Board;
		}
	}
}
