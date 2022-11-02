using System.Collections.Generic;

namespace HOA.Tokens {
	public enum TTYPE {
		TREE,
		KATA, CARA, MAWT, KABU, HSIL,
		DEMO, MINE, MEIN, PANO, DECI, HSTE,
		ROOK, SMAS, CONF, ASHE, BATT, GARG, HSTO,
		GRIZ, TALO, META, ULTR, HFIR,
		REVO, APER, PIEC, REPR, OLDT, HBRA,
		BEES, MYCO, MART, LICH, WEBB, BLAC, HSLK,
		PRIS, AREN, PRIE, DREA, HGLA,
		RECY, NECR, MOUT, MONO, HBLO,
		CORP, ROCK, HILL, MNTN, WATR, LAVA,
		NONE
	}
	
	public static class TokenRef {
		
		public static Dictionary<TTYPE,string>.ValueCollection Names {get {return codeNames.Values;} }
		public static Dictionary<TTYPE,string>.KeyCollection Codes {get {return codeNames.Keys;} }
		public static string CodeToString (TTYPE code) {return codeNames[code];}
	
		static Dictionary<TTYPE,string> codeNames = new Dictionary<TTYPE,string> {
			{TTYPE.NONE,""},
			
			{TTYPE.KATA, "Katandroid"},
			{TTYPE.CARA, "Carapace Invader"},
			{TTYPE.MAWT, "MAWTH"},
			{TTYPE.KABU, "Kabutomachine"},
			{TTYPE.HSIL, "Silicon Heart of Attack"},
			
			{TTYPE.DEMO, "Demolitia"},
			{TTYPE.MEIN, "Mein Schutz"},
			{TTYPE.MINE, "Mine"},
			{TTYPE.PANO, "Panopticannon"},
			{TTYPE.DECI, "Decimatrix"},
			{TTYPE.HSTE, "Steel Heart of Attack"},
			
			{TTYPE.ROOK, "Rook"},
			{TTYPE.SMAS, "Smashbuckler"},
			{TTYPE.CONF, "Conflagragon"},
			{TTYPE.ASHE, "Ashes"},
			{TTYPE.BATT, "Battering Rambuchet"},
			{TTYPE.GARG, "Gargoliath"},
			{TTYPE.HSTO, "Stone Heart of Attack"},
	
			{TTYPE.GRIZ, "Grizzly Elder"},
			{TTYPE.TALO, "Taloned Scout"},
			{TTYPE.META, "Metaterrainean"},
			{TTYPE.ULTR, "Ultratherium"},
			{TTYPE.HFIR, "Fir Heart of Attack"},
			
			{TTYPE.REVO, "Revolving Tom"},
			{TTYPE.PIEC, "Piecemaker"},
			{TTYPE.APER, "Aperture"},
			{TTYPE.REPR, "Reprospector"},
			{TTYPE.OLDT, "Old Three Hands"},
			{TTYPE.HBRA, "Brass Heart of Attack"},
	
			{TTYPE.LICH, "Lichenthrope"},
			{TTYPE.BEES, "Beesassin"},
			{TTYPE.MYCO, "Mycolonist"},
			{TTYPE.MART, "Martian Man Trap"},
			{TTYPE.BLAC, "Black Winnow"},
			{TTYPE.WEBB, "Web"},
			{TTYPE.HSLK, "Silk Heart of Attack"},
	
			{TTYPE.PRIS, "Prism Guard"},
			{TTYPE.AREN, "Arena Non Sensus"},
			{TTYPE.PRIE, "Priest of Naja"},
			{TTYPE.DREA, "Dream Reaver"},
			{TTYPE.HGLA, "Glass Heart of Attack"},
	
			{TTYPE.RECY, "Recyclops"},	
			{TTYPE.NECR, "Necrochancellor"},
			{TTYPE.MOUT, "Mouth of the Underworld"},
			{TTYPE.MONO, "Monolith"},
			{TTYPE.HBLO, "Blood Heart of Attack"},
			
			{TTYPE.CORP, "Corpse"},
			{TTYPE.TREE, "Tree"},
			{TTYPE.MNTN, "Mountain"},
			{TTYPE.HILL, "Hill"},
			{TTYPE.LAVA, "Lava"},
			{TTYPE.WATR, "Water"},
			{TTYPE.ROCK, "Rock"}
			
		};
	}
}