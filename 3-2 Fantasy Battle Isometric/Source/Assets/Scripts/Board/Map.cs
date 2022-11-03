using System.Collections.Generic;

namespace HOA { 

	public abstract class Map {
		public string Name {get; protected set;}
		public Int2 Size {get; protected set;}
		public Board Board {get; protected set;}
		public Landscape Landscape {get; protected set;}
		public float Density {get; protected set;}
		public Distribution<EToken> Dist {get; protected set;}

		public abstract Board Build ();

		public void Populate () {
			Landscape.AddRandom(Density, Dist);
			Landscape.Build();
			BoardFactory.SpawnKings(BoardFactory.SpawnZones(Board.Zones().Periphery));
		}

		public static List<Map> Maps {get; private set;}
		public static List<Map> MapsCustom {get; private set;}

		public static void LoadMaps() {
			Maps = new List<Map>();
			Maps.Add(new MapSpace());
			Maps.Add(new MapMinefield());
			Maps.Add(new MapCastle());
			Maps.Add(new MapGrove());
			Maps.Add(new MapTime());
			Maps.Add(new MapVoid());

			MapsCustom = new List<Map>();
			MapsCustom.Add(new MapBlank());
			MapsCustom.Add(new MapRandom());
		}
	}

	public interface IMapCustom {
		Board Build (Int2 size);
	}
}
