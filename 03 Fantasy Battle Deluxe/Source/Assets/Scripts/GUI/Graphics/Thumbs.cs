using UnityEngine;
using System.Collections.Generic;
using HOA.Tokens;

public static class Thumbs {
	
	static Dictionary<TTYPE, Texture2D> thumbnails;
	
	public static Texture2D CodeToThumb (TTYPE code) {
		return thumbnails[code];
	}
	
	public static void Load() {
		thumbnails = new Dictionary<TTYPE, Texture2D>();
		
		thumbnails.Add(TTYPE.KATA, UnityEngine.Resources.Load("Thumbnails/KATA") as Texture2D);
		thumbnails.Add(TTYPE.CARA, UnityEngine.Resources.Load("Thumbnails/CARA") as Texture2D);
		thumbnails.Add(TTYPE.MAWT, UnityEngine.Resources.Load("Thumbnails/MAWT") as Texture2D);
		thumbnails.Add(TTYPE.KABU, UnityEngine.Resources.Load("Thumbnails/KABU") as Texture2D);
		thumbnails.Add(TTYPE.HSIL, UnityEngine.Resources.Load("Thumbnails/HSIL") as Texture2D);
		
		thumbnails.Add(TTYPE.DEMO, UnityEngine.Resources.Load("Thumbnails/DEMO") as Texture2D);
		thumbnails.Add(TTYPE.MEIN, UnityEngine.Resources.Load("Thumbnails/MEIN") as Texture2D);
		thumbnails.Add(TTYPE.MINE, UnityEngine.Resources.Load("Thumbnails/MINE") as Texture2D);
		thumbnails.Add(TTYPE.PANO, UnityEngine.Resources.Load("Thumbnails/PANO") as Texture2D);
		thumbnails.Add(TTYPE.DECI, UnityEngine.Resources.Load("Thumbnails/DECI") as Texture2D);
		thumbnails.Add(TTYPE.HSTE, UnityEngine.Resources.Load("Thumbnails/HSTE") as Texture2D);
		
		thumbnails.Add(TTYPE.ROOK, UnityEngine.Resources.Load("Thumbnails/ROOK") as Texture2D);
		thumbnails.Add(TTYPE.SMAS, UnityEngine.Resources.Load("Thumbnails/SMAS") as Texture2D);
		thumbnails.Add(TTYPE.CONF, UnityEngine.Resources.Load("Thumbnails/CONF") as Texture2D);
		thumbnails.Add(TTYPE.ASHE, UnityEngine.Resources.Load("Thumbnails/ASHE") as Texture2D);
		thumbnails.Add(TTYPE.BATT, UnityEngine.Resources.Load("Thumbnails/BATT") as Texture2D);
		thumbnails.Add(TTYPE.GARG, UnityEngine.Resources.Load("Thumbnails/GARG") as Texture2D);
		thumbnails.Add(TTYPE.HSTO, UnityEngine.Resources.Load("Thumbnails/HSTO") as Texture2D);
		
		thumbnails.Add(TTYPE.GRIZ, UnityEngine.Resources.Load("Thumbnails/GRIZ") as Texture2D);
		thumbnails.Add(TTYPE.TALO, UnityEngine.Resources.Load("Thumbnails/TALO") as Texture2D);
		thumbnails.Add(TTYPE.META, UnityEngine.Resources.Load("Thumbnails/META") as Texture2D);
		thumbnails.Add(TTYPE.ULTR, UnityEngine.Resources.Load("Thumbnails/ULTR") as Texture2D);
		thumbnails.Add(TTYPE.HFIR, UnityEngine.Resources.Load("Thumbnails/HFIR") as Texture2D);
		
		thumbnails.Add(TTYPE.REVO, UnityEngine.Resources.Load("Thumbnails/REVO") as Texture2D);
		thumbnails.Add(TTYPE.PIEC, UnityEngine.Resources.Load("Thumbnails/PIEC") as Texture2D);
		thumbnails.Add(TTYPE.APER, UnityEngine.Resources.Load("Thumbnails/APER") as Texture2D);
		thumbnails.Add(TTYPE.REPR, UnityEngine.Resources.Load("Thumbnails/REPR") as Texture2D);
		thumbnails.Add(TTYPE.OLDT, UnityEngine.Resources.Load("Thumbnails/OLDT") as Texture2D);
		thumbnails.Add(TTYPE.HBRA, UnityEngine.Resources.Load("Thumbnails/HBRA") as Texture2D);
		
		thumbnails.Add(TTYPE.LICH, UnityEngine.Resources.Load("Thumbnails/LICH") as Texture2D);
		thumbnails.Add(TTYPE.BEES, UnityEngine.Resources.Load("Thumbnails/BEES") as Texture2D);
		thumbnails.Add(TTYPE.MYCO, UnityEngine.Resources.Load("Thumbnails/MYCO") as Texture2D);
		thumbnails.Add(TTYPE.MART, UnityEngine.Resources.Load("Thumbnails/MART") as Texture2D);
		thumbnails.Add(TTYPE.BLAC, UnityEngine.Resources.Load("Thumbnails/BLAC") as Texture2D);
		thumbnails.Add(TTYPE.WEBB, UnityEngine.Resources.Load("Thumbnails/WEBB") as Texture2D);
		thumbnails.Add(TTYPE.HSLK, UnityEngine.Resources.Load("Thumbnails/HSLK") as Texture2D);
		
		thumbnails.Add(TTYPE.PRIS, UnityEngine.Resources.Load("Thumbnails/PRIS") as Texture2D);
		thumbnails.Add(TTYPE.AREN, UnityEngine.Resources.Load("Thumbnails/AREN") as Texture2D);
		thumbnails.Add(TTYPE.PRIE, UnityEngine.Resources.Load("Thumbnails/PRIE") as Texture2D);
		thumbnails.Add(TTYPE.DREA, UnityEngine.Resources.Load("Thumbnails/DREA") as Texture2D);
		thumbnails.Add(TTYPE.HGLA, UnityEngine.Resources.Load("Thumbnails/HGLA") as Texture2D);
		
		thumbnails.Add(TTYPE.RECY, UnityEngine.Resources.Load("Thumbnails/RECY") as Texture2D);
		thumbnails.Add(TTYPE.NECR, UnityEngine.Resources.Load("Thumbnails/NECR") as Texture2D);
		thumbnails.Add(TTYPE.MOUT, UnityEngine.Resources.Load("Thumbnails/MOUT") as Texture2D);
		thumbnails.Add(TTYPE.MONO, UnityEngine.Resources.Load("Thumbnails/MONO") as Texture2D);
		thumbnails.Add(TTYPE.HBLO, UnityEngine.Resources.Load("Thumbnails/HBLO") as Texture2D);
		
		thumbnails.Add(TTYPE.MNTN, UnityEngine.Resources.Load("Thumbnails/MNTN") as Texture2D);
		thumbnails.Add(TTYPE.HILL, UnityEngine.Resources.Load("Thumbnails/HILL") as Texture2D);
		thumbnails.Add(TTYPE.ROCK, UnityEngine.Resources.Load("Thumbnails/ROCK") as Texture2D);
		thumbnails.Add(TTYPE.TREE, UnityEngine.Resources.Load("Thumbnails/TREE") as Texture2D);
		thumbnails.Add(TTYPE.CORP, UnityEngine.Resources.Load("Thumbnails/CORP") as Texture2D);
		thumbnails.Add(TTYPE.WATR, UnityEngine.Resources.Load("Thumbnails/WATR") as Texture2D);
		thumbnails.Add(TTYPE.LAVA, UnityEngine.Resources.Load("Thumbnails/LAVA") as Texture2D);
		
		thumbnails.Add(TTYPE.NONE, default(Texture2D));
	}
}
