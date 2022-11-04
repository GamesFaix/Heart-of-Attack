using System.Collections.Generic;
using HOA.Maps;

namespace HOA { 

	public abstract class Map {
		public string Name {get; protected set;}
		public size2 Size {get; protected set;}
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
			Maps.Add(new Orbit());
			Maps.Add(new Minefield());
			Maps.Add(new Stronghold());
			Maps.Add(new Grove());
			Maps.Add(new Frontier());
			Maps.Add(new Void());

			MapsCustom = new List<Map>();
			MapsCustom.Add(new MapBlank());
			MapsCustom.Add(new MapRandom());
		}
	}

	public interface IMapCustom {
		Board Build (size2 size);
	}
}
