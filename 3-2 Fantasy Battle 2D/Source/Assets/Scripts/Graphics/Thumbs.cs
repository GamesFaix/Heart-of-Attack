using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public static class Thumbs {
		
		static Dictionary<EToken, Texture2D> thumbnails;
		
		public static void Load() {
			thumbnails = new Dictionary<EToken, Texture2D>();

			Add(EToken.KATA, "KATA"); Add(EToken.CARA, "CARA"); Add(EToken.MAWT, "MAWT"); Add(EToken.KABU, "KABU");
			Add(EToken.DEMO, "DEMO"); Add(EToken.MEIN, "MEIN"); Add(EToken.MINE, "MINE"); Add(EToken.PANO, "PANO"); Add(EToken.DECI, "DECI");
			Add(EToken.ROOK, "ROOK"); Add(EToken.SMAS, "SMAS"); Add(EToken.CONF, "CONF"); Add(EToken.ASHE, "ASHE"); Add(EToken.BATT, "BATT"); Add(EToken.GARG, "GARG");
			Add(EToken.GRIZ, "GRIZ"); Add(EToken.TALO, "TALO"); Add(EToken.META, "META"); Add(EToken.ULTR, "ULTR");
			Add(EToken.REVO, "REVO"); Add(EToken.PIEC, "PIEC"); Add(EToken.APER, "APER"); Add(EToken.REPR, "REPR"); Add(EToken.OLDT, "OLDT");
			Add(EToken.LICH, "LICH"); Add(EToken.BEES, "BEES"); Add(EToken.MYCO, "MYCO"); Add(EToken.MART, "MART"); Add(EToken.BLAC, "BLAC"); Add(EToken.WEBB, "WEBB");
			Add(EToken.PRIS, "PRIS"); Add(EToken.AREN, "AREN"); Add(EToken.PRIE, "PRIE"); Add(EToken.DREA, "DREA");
			Add(EToken.RECY, "RECY"); Add(EToken.NECR, "NECR"); Add(EToken.MOUT, "MOUT"); Add(EToken.MONO, "MONO");

			Add(EToken.HSIL, "HSIL"); Add(EToken.HSTE, "HSTE"); Add(EToken.HSTO, "HSTO"); Add(EToken.HFIR, "HFIR"); 
			Add(EToken.HBRA, "HBRA"); Add(EToken.HSLK, "HSLK"); Add(EToken.HGLA, "HGLA"); Add(EToken.HBLO, "HBLO");

			Add(EToken.MNTN, "MNTN"); Add(EToken.HILL, "HILL"); Add(EToken.WATR, "WATR"); Add(EToken.LAVA, "LAVA");
			Add(EToken.ROCK, "ROCK"); Add(EToken.TREE, "TREE"); Add(EToken.CORP, "CORP");

			thumbnails.Add(EToken.NONE, default(Texture2D));
		}
		static void Add (EToken code, string fileName) {thumbnails.Add(code, LoadFile(fileName));}

		static Texture2D LoadFile (string name) {return (Resources.Load("Thumbnails/"+name) as Texture2D);}

		public static Texture2D CodeToThumb (EToken code) {return thumbnails[code];}
	}
}