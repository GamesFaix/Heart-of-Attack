namespace HOA.Maps { 
	
	public class Stronghold : Map {
		
		public Stronghold () {
			Name = "Stronghold";
			Size = new size2(4,4);
			
			Density = 0.4f;
			Dist = new Distribution<EToken> ();
			//Dist.Add(new Possibility<EToken>(EToken.RAMP,1));
			Dist.Add(new Possibility<EToken>(EToken.COTT,1));
			Dist.Add(new Possibility<EToken>(EToken.ROCK,2));
			Dist.Add(new Possibility<EToken>(EToken.LAVA,1));
			Dist.Add(new Possibility<EToken>(EToken.MNTN,1));

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

			Landscape.Add(new index2(6,1), EToken.LAVA);
			Landscape.Add(new index2(7,1), EToken.LAVA);
			Landscape.Add(new index2(6,12), EToken.LAVA);
			Landscape.Add(new index2(7,12), EToken.LAVA);


			Populate();
			return Board;
		}

		Terrain Corner {
			get {
				return new Terrain (new EToken[] {
					EToken.MNTN, EToken.WATR, EToken.WATR,
					EToken.WATR, EToken.MNTN, EToken.RAMP,
					EToken.WATR, EToken.RAMP, EToken.NONE
				});
			}
		}

		Terrain Side {
			get {
				return new Terrain (new EToken[] {
					EToken.LAVA, EToken.NONE, EToken.WATR,
					EToken.LAVA, EToken.NONE, EToken.WATR,
					EToken.NONE, EToken.NONE, EToken.NONE
				});
			}
		}

		Terrain Inside {
			get {
				return new Terrain (new EToken[] {
					EToken.RAMP, EToken.NONE, EToken.LAVA,
					EToken.RAMP, EToken.NONE, EToken.LAVA,
					EToken.NONE, EToken.NONE, EToken.NONE
				});
			}
		}

	}
}
