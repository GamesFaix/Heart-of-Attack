using UnityEngine; 

namespace HOA { 

	public class MapRandom : Map, IMapCustom {
		public MapRandom () {
			Name = "Random";
			
			Density = 0.3f;
			Dist = new Distribution<EToken>();
			Dist.Add(new Possibility<EToken>(EToken.TREE,4));
			Dist.Add(new Possibility<EToken>(EToken.ROCK,2));
			Dist.Add(new Possibility<EToken>(EToken.WATR,2));
			Dist.Add(new Possibility<EToken>(EToken.LAVA,1));
			Dist.Add(new Possibility<EToken>(EToken.MNTN,3));
			Dist.Add(new Possibility<EToken>(EToken.HILL,1));
		}
		
		public Board Build (size2 size) {
			Size = size;
			Board = new Board(Size);
			Landscape = new Landscape(Board);
			Populate();
			return Board;
		}
		public override Board Build () {return Build(new size2(4,4));}
	}
}
