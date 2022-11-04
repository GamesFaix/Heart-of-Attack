using UnityEngine; 

namespace HOA { 

	public class MapRandom : Map, IMapCustom {
		public MapRandom () {
			Name = "Random";
			
			Density = 0.3f;
			Dist = new Distribution<Species>();
			Dist.Add(new Possibility<Species>(Species.Tree,4));
			Dist.Add(new Possibility<Species>(Species.Rock,2));
			Dist.Add(new Possibility<Species>(Species.Water,2));
			Dist.Add(new Possibility<Species>(Species.Lava,1));
			Dist.Add(new Possibility<Species>(Species.Mountain,3));
			Dist.Add(new Possibility<Species>(Species.Hill,1));
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
