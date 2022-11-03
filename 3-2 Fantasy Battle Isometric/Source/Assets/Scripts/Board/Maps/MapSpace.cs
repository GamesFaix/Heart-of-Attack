using UnityEngine; namespace HOA { 
	
	public class MapSpace : Map {
		
		public MapSpace () {
			Name = "Orbit";
			Size = new Int2(5,5);
			
			Density = 0.5f;
			Dist = new Distribution<EToken> ();
			Dist.Add(new Possibility<EToken>(EToken.ANTE,3));
			//Dist.Add(new Possibility<EToken>(EToken.HOLE,1));
			Dist.Add(new Possibility<EToken>(EToken.PYLO,1));
			//Dist.Add(new Possibility<EToken>(EToken.EXHA,1));
		}
		
		public override Board Build () {
			Board = new Board(Size, TileSet.Gearp);
			Landscape = new Landscape(Board);

			Landscape.Add(new Int2(4,1), Gap);
			Landscape.Add(new Int2(10,1), Gap.FlipHor());
			Landscape.Add(new Int2(4,13), Gap.FlipVer());
			Landscape.Add(new Int2(10,13), Gap.FlipHor().FlipVer());
			Landscape.Add(new Int2(1,4), Gap.FlipPos());
			Landscape.Add(new Int2(1,10), Gap.FlipHor().FlipPos());
			Landscape.Add(new Int2(13,4), Gap.FlipVer().FlipPos());
			Landscape.Add(new Int2(13,10), Gap.FlipHor().FlipVer().FlipPos());

			Landscape.Add(new Int2(4,4), CornerPass);
			Landscape.Add(new Int2(10,4), CornerPass.FlipHor());
			Landscape.Add(new Int2(4,10), CornerPass.FlipVer());
			Landscape.Add(new Int2(10,10), CornerPass.FlipHor().FlipVer());

			Landscape.Add(new Int2(4,8), EToken.EXHA);
			Landscape.Add(new Int2(8,4), EToken.EXHA);
			Landscape.Add(new Int2(8,12), EToken.EXHA);
			Landscape.Add(new Int2(12,8), EToken.EXHA);



			Populate();
			return Board;
		}

		Terrain Gap {
			get {
				return new Terrain (new EToken[] {
					EToken.HOLE, EToken.HOLE, EToken.HOLE,
					EToken.HOLE, EToken.HOLE, EToken.HOLE,
					EToken.NONE, EToken.HOLE, EToken.HOLE
				});
			}
		}

		Terrain CornerPass {
			get {
				return new Terrain (new EToken[] {
					EToken.EXHA, EToken.NONE, EToken.HOLE,
					EToken.NONE, EToken.NONE, EToken.NONE,
					EToken.HOLE, EToken.NONE, EToken.NONE
				});
			}
		}
	}
}
