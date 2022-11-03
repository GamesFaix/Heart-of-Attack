using UnityEngine; 

namespace HOA { 

	public class MapBlank : Map, IMapCustom {
		public MapBlank () {
			Name = "Blank";
			Density = 0f;
			Dist = new Distribution<EToken>();
		}
		
		public Board Build (Int2 size) {
			Size = size;
			Board = new Board(Size);
			Landscape = new Landscape(Board);
			Populate();
			return Board;
		}
		public override Board Build () {return Build(new Int2(4,4));}
	}
}
