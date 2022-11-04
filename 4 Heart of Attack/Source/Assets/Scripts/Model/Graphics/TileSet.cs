using UnityEngine; 

namespace HOA { 

	public class TileSet {
	
		public Texture2D Even {get; private set;}
		public Texture2D Odd {get; private set;}
	

		public TileSet	(string name1, string name2) {
			Even = Resources.Load("Images/Textures/Cell/"+name1) as Texture2D;
			Odd = Resources.Load("Images/Textures/Cell/"+name2) as Texture2D;
		}
	

		public static TileSet Chess {get {return new TileSet("blackCell", "whiteCell");} }
		public static TileSet Minecraft {get {return new TileSet("mc grass", "mc dirt");} }
		public static TileSet Gearp {get {return new TileSet("plate", "solar panel");} }
		public static TileSet NewRep {get {return new TileSet("dry grass", "sand");} }
		public static TileSet Grove {get {return new TileSet("rough wood", "snow");} }
		public static TileSet Torridale {get {return new TileSet("mc cobble", "stone");} }
		public static TileSet Chrono {get {return new TileSet("brass", "old board");} }
		public static TileSet Psycho {get {return new TileSet("moss", "mc dirt");} }
		public static TileSet Psilent {get {return new TileSet("sand", "sand");} }
		public static TileSet Voidoid {get {return new TileSet("basalt", "obsidian");} }

		public static TileSet Random {
			get {
				int random = UnityEngine.Random.Range(1,10);
				switch (random) {
					case 1: return Gearp;
					case 2: return NewRep;
					case 3: return Grove;
					case 4: return Torridale;
					case 5: return Chrono;
					case 6: return Psycho;
					case 7: return Psilent;
					case 8: return Voidoid;
					case 9: return Chess;
					default: return Minecraft;
				}
			}
		}
	}
}
