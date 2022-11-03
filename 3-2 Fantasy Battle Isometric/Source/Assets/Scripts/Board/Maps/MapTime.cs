using UnityEngine; 

namespace HOA { 
	
	public class MapTime : Map {
		public MapTime () {
			Name = "Frontier";
			Size = new Int2(4,4);
			
			Density = 0.5f;
			Dist = new Distribution<EToken> ();
			Dist.Add(new Possibility<EToken>(EToken.MNTN,2));
			Dist.Add(new Possibility<EToken>(EToken.COTT,1));
			Dist.Add(new Possibility<EToken>(EToken.TREE,2));
			Dist.Add(new Possibility<EToken>(EToken.ROCK,2));
		}
		
		public override Board Build () {
			Board = new Board(Size, TileSet.Chrono);
			Landscape = new Landscape(Board);

			Landscape.Add(new Int2(1,1), Sink);
			Landscape.Add(new Int2(6,1), Sink);
			Landscape.Add(new Int2(11,1), Sink);
			Landscape.Add(new Int2(1,6), Sink);
			Landscape.Add(new Int2(11,6), Sink);
			Landscape.Add(new Int2(1,11), Sink);
			Landscape.Add(new Int2(6,11), Sink);
			Landscape.Add(new Int2(11,11), Sink);

			Landscape.Add(new Int2(5,5), Well);
			Landscape.Add(new Int2(6,6), Well.FlipVer().FlipHor());

			Landscape.Add(new Int2(3,3), Corner);
			Landscape.Add(new Int2(3,8), Corner.FlipVer());
			Landscape.Add(new Int2(8,3), Corner.FlipHor());
			Landscape.Add(new Int2(8,8), Corner.FlipVer().FlipHor());

			Populate();
			return Board;
		}

		Terrain Sink {
			get {
				return new Terrain (new EToken[] {
					EToken.TSNK, EToken.TSNK, EToken.NONE,
					EToken.TSNK, EToken.TSNK, EToken.NONE,
					EToken.NONE, EToken.NONE, EToken.NONE
				});
			}
		}

		Terrain Well {
			get {
				return new Terrain (new EToken[] {
					EToken.NONE, EToken.TWEL, EToken.TWEL,
					EToken.TWEL, EToken.TWEL, EToken.TWEL,
					EToken.TWEL, EToken.TWEL, EToken.TWEL
				});
			}
		}

		Terrain Corner {
			get {
				return new Terrain (new EToken[] {
					EToken.NONE, EToken.MNTN, EToken.NONE,
					EToken.MNTN, EToken.NONE, EToken.ROCK,
					EToken.NONE, EToken.ROCK, EToken.WATR
				});
			}
		}

	}
}
