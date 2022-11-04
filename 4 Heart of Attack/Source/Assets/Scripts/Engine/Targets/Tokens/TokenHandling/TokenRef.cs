using System.Collections.Generic;

namespace HOA {

	public static class TokenRef {
		
		public static Dictionary<EToken,string>.ValueCollection Names {get {return codeNames.Values;} }
		public static Dictionary<EToken,string>.KeyCollection Codes {get {return codeNames.Keys;} }
		public static string CodeToString (EToken code) {return codeNames[code];}
	
		static Dictionary<EToken,string> codeNames = new Dictionary<EToken,string> {
			{EToken.NONE,""},
			
			{EToken.KATA, "Katandroid"},
			{EToken.CARA, "Carapace Invader"},
			{EToken.MAWT, "MAWTH"},
			{EToken.KABU, "Kabutomachine"},
			{EToken.HSIL, "Silicon Heart of Attack"},
			
			{EToken.DEMO, "Demolitia"},
			{EToken.MEIN, "Mein Schutz"},
			{EToken.MINE, "Mine"},
			{EToken.PANO, "Panopticannon"},
			{EToken.DECI, "Decimatrix"},
			{EToken.HSTE, "Steel Heart of Attack"},
			
			{EToken.ROOK, "Rook"},
			{EToken.SMAS, "Smashbuckler"},
			{EToken.CONF, "Conflagragon"},
			{EToken.ASHE, "Ashes"},
			{EToken.BATT, "Battering Rambuchet"},
			{EToken.GARG, "Gargoliath"},
			{EToken.HSTO, "Stone Heart of Attack"},
	
			{EToken.GRIZ, "Grizzly Elder"},
			{EToken.TALO, "Taloned Scout"},
			{EToken.META, "Metaterrainean"},
			{EToken.ULTR, "Ultratherium"},
			{EToken.HFIR, "Fir Heart of Attack"},
			
			{EToken.REVO, "Revolving Tom"},
			{EToken.PIEC, "Piecemaker"},
			{EToken.APER, "Aperture"},
			{EToken.REPR, "Reprospector"},
			{EToken.OLDT, "Old Three Hands"},
			{EToken.HBRA, "Brass Heart of Attack"},
	
			{EToken.LICH, "Lichenthrope"},
			{EToken.BEES, "Beesassin"},
			{EToken.MYCO, "Mycolonist"},
			{EToken.MART, "Martian Man Trap"},
			{EToken.BLAC, "Black Winnow"},
			{EToken.WEBB, "Web"},
			{EToken.HSLK, "Silk Heart of Attack"},
	
			{EToken.PRIS, "Prism Guard"},
			{EToken.AREN, "Arena Non Sensus"},
			{EToken.PRIE, "Priest of Naja"},
			{EToken.DREA, "Dream Reaver"},
			{EToken.HGLA, "Glass Heart of Attack"},
	
			{EToken.RECY, "Recyclops"},	
			{EToken.NECR, "Necrochancellor"},
			{EToken.GATE, "Gatecreeper"},
			{EToken.MONO, "Monolith"},
			{EToken.HBLO, "Blood Heart of Attack"},
			
			{EToken.CORP, "Corpse"},
			{EToken.TREE, "Tree"},
			{EToken.MNTN, "Mountain"},
			{EToken.HILL, "Hill"},
			{EToken.LAVA, "Lava"},
			{EToken.WATR, "Water"},
			{EToken.ROCK, "Rock"},

			{EToken.HOLE, "Hole"},
			{EToken.EXHA, "Exhaust Vent"},
			{EToken.ANTE, "Antenna"},
			{EToken.PYLO, "Pylon"},

			{EToken.TARG, "Bombing Range"},
			{EToken.HOUS, "House"},

			{EToken.TREE2, "Tree"},
			{EToken.TREE3, "Tree"},
			{EToken.TREE4, "Tree"},
			{EToken.ICE, "Ice"},

			{EToken.COTT, "House"},
			{EToken.RAMP, "Rampart"},

			{EToken.TSNK, "Time Sink"},
			{EToken.TWEL, "Time Well"},

			{EToken.TEMP, "Temple"},
			{EToken.PYRA, "Pyramid"},

			{EToken.CURS, "Curse"}

			
		};
	}
}