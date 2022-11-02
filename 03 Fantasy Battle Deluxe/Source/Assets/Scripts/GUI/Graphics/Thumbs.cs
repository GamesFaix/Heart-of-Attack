using UnityEngine;
using System.Collections.Generic;
using HOA.Tokens;

public static class Thumbs {
	
	static Dictionary<TTYPE, Texture2D> thumbnails;
	
	public static void Load() {
		thumbnails = new Dictionary<TTYPE, Texture2D>();

		Add(TTYPE.KATA, "KATA"); Add(TTYPE.CARA, "CARA"); Add(TTYPE.MAWT, "MAWT"); Add(TTYPE.KABU, "KABU");
		Add(TTYPE.DEMO, "DEMO"); Add(TTYPE.MEIN, "MEIN"); Add(TTYPE.MINE, "MINE"); Add(TTYPE.PANO, "PANO"); Add(TTYPE.DECI, "DECI");
		Add(TTYPE.ROOK, "ROOK"); Add(TTYPE.SMAS, "SMAS"); Add(TTYPE.CONF, "CONF"); Add(TTYPE.ASHE, "ASHE"); Add(TTYPE.BATT, "BATT"); Add(TTYPE.GARG, "GARG");
		Add(TTYPE.GRIZ, "GRIZ"); Add(TTYPE.TALO, "TALO"); Add(TTYPE.META, "META"); Add(TTYPE.ULTR, "ULTR");
		Add(TTYPE.REVO, "REVO"); Add(TTYPE.PIEC, "PIEC"); Add(TTYPE.APER, "APER"); Add(TTYPE.REPR, "REPR"); Add(TTYPE.OLDT, "OLDT");
		Add(TTYPE.LICH, "LICH"); Add(TTYPE.BEES, "BEES"); Add(TTYPE.MYCO, "MYCO"); Add(TTYPE.MART, "MART"); Add(TTYPE.BLAC, "BLAC"); Add(TTYPE.WEBB, "WEBB");
		Add(TTYPE.PRIS, "PRIS"); Add(TTYPE.AREN, "AREN"); Add(TTYPE.PRIE, "PRIE"); Add(TTYPE.DREA, "DREA");
		Add(TTYPE.RECY, "RECY"); Add(TTYPE.NECR, "NECR"); Add(TTYPE.MOUT, "MOUT"); Add(TTYPE.MONO, "MONO");

		Add(TTYPE.HSIL, "HSIL"); Add(TTYPE.HSTE, "HSTE"); Add(TTYPE.HSTO, "HSTO"); Add(TTYPE.HFIR, "HFIR"); 
		Add(TTYPE.HBRA, "HBRA"); Add(TTYPE.HSLK, "HSLK"); Add(TTYPE.HGLA, "HGLA"); Add(TTYPE.HBLO, "HBLO");

		Add(TTYPE.MNTN, "MNTN"); Add(TTYPE.HILL, "HILL"); Add(TTYPE.WATR, "WATR"); Add(TTYPE.LAVA, "LAVA");
		Add(TTYPE.ROCK, "ROCK"); Add(TTYPE.TREE, "TREE"); Add(TTYPE.CORP, "CORP");

		thumbnails.Add(TTYPE.NONE, default(Texture2D));
	}
	static void Add (TTYPE code, string fileName) {thumbnails.Add(code, LoadFile(fileName));}

	static Texture2D LoadFile (string name) {return (Resources.Load("Thumbnails/"+name) as Texture2D);}

	public static Texture2D CodeToThumb (TTYPE code) {return thumbnails[code];}
}
