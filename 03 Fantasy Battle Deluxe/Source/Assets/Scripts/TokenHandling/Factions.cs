using UnityEngine;
using System.Collections.Generic;
using HOA.Tokens;

public class FGearp : Faction {
	public FGearp () {
		name = "G.E.A.R.P.";
		tokens = new List<TTYPE> {TTYPE.KABU, TTYPE.MAWT, TTYPE.CARA, TTYPE.KATA, TTYPE.HSIL};
		king = TTYPE.KABU;
		heart = TTYPE.HSIL;
		color1 = new Color(0, 0, 1, 1);
		color2 = Color.white;
		playable = true;
	}
}

public class FNewRepublic : Faction {
	public FNewRepublic () {
		name = "New Republic";
		tokens =  new List<TTYPE> {TTYPE.DECI, TTYPE.PANO, TTYPE.MEIN, TTYPE.DEMO, TTYPE.MINE, TTYPE.HSTE};
		king = TTYPE.DECI;
		heart = TTYPE.HSTE;
		color1 = new Color(0, 0.2f, 0, 1);
		color2 = new Color(0.8f, 0.8f, 0.8f, 1);
		playable = true;
	}
}

public class FTorridale : Faction {
	public FTorridale () {
		name = "Torridale";
		tokens = new List<TTYPE> {TTYPE.GARG, TTYPE.BATT, TTYPE.CONF, TTYPE.ASHE, TTYPE.SMAS, TTYPE.ROOK, TTYPE.HSTO};
		king = TTYPE.GARG;
		heart = TTYPE.HSTO;
		color1 = new Color(0.5f, 0.5f, 0.5f ,1);
		color2 = new Color(0.6f, 0.1f, 0.1f, 1);
		playable = true;
	}
}

public class FGrove : Faction {
	public FGrove () {
		name = "Forgotten Grove";
		tokens = new List<TTYPE> {TTYPE.ULTR, TTYPE.META, TTYPE.TALO, TTYPE.GRIZ, TTYPE.HFIR};
		king = TTYPE.ULTR;
		heart = TTYPE.HFIR;
		color1 = new Color(0.7f, 0.5f, 0, 1);
		color2 = new Color(0, 0.2f, 0, 1);
		playable = true;
	}
}
public class FChrono : Faction {
	public FChrono () {
		name = "Chrononistas";
		tokens = new List<TTYPE> {TTYPE.OLDT, TTYPE.REPR, TTYPE.PIEC, TTYPE.REVO, TTYPE.APER, TTYPE.HBRA};
		king = TTYPE.OLDT;
		heart = TTYPE.HBRA;
		color1 = new Color(1, 0.8f, 0, 1);
		color2 = Color.magenta;
		playable = true;
	}
}

public class FPsycho : Faction {
	public FPsycho () {
		name = "Psycho Tropics";
		tokens = new List<TTYPE> {TTYPE.BLAC, TTYPE.MART, TTYPE.MYCO, TTYPE.BEES, TTYPE.LICH, TTYPE.WEBB, TTYPE.HSLK};
		king = TTYPE.BLAC;
		heart = TTYPE.HSLK;
		color1 = new Color(0.3f, 0, 0.5f, 1);
		color2 = Color.green;
		playable = true;
	}
}

public class FPsilent : Faction {
	public FPsilent () {
		name = "Psilent";
		tokens = new List<TTYPE> {TTYPE.DREA, TTYPE.PRIE, TTYPE.AREN, TTYPE.PRIS, TTYPE.HGLA};
		king = TTYPE.DREA;
		heart = TTYPE.HGLA;
		color1 = new Color(0.4f, 0.8f, 1, 1);
		color2 = new Color(1, 0.8f, 0, 1);
		playable = true;
	}
}

public class FVoidoid : Faction {
	public FVoidoid () {
		name = "Voidoids";
		tokens = new List<TTYPE> {TTYPE.MONO, TTYPE.MOUT, TTYPE.NECR, TTYPE.RECY, TTYPE.HBLO};
		king = TTYPE.MONO;
		heart = TTYPE.HBLO;
		color1 = new Color(0.6f, 0.1f, 0.1f, 1);
		color2 = Color.black;
		playable = true;
	}
}

public class FObstacle : Faction {
	public FObstacle () {
		name = "(Obstacles)";
		tokens = new List<TTYPE> {TTYPE.MNTN, TTYPE.HILL, TTYPE.ROCK, TTYPE.TREE, TTYPE.WATR, TTYPE.LAVA, TTYPE.CORP};
		king = TTYPE.NONE; 
		heart = TTYPE.NONE;
		color1 = Color.white;
		color2 = Color.grey;
		playable = false;
	}
}