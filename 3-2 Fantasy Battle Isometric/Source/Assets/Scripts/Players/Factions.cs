using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class FGearp : Faction {
		public FGearp () {
			name = "G.E.A.R.P.";
			tokens = new List<EToken> {EToken.KABU, EToken.MAWT, EToken.CARA, EToken.KATA, EToken.HSIL};
			king = EToken.KABU;
			heart = EToken.HSIL;
			color1 = new Color(0, 0, 1, 1);
			color2 = Color.white;
			playable = true;
			theme = SoundLoader.Theme(0);
		}
	}

	public class FNewRepublic : Faction {
		public FNewRepublic () {
			name = "New Republic";
			tokens =  new List<EToken> {EToken.DECI, EToken.PANO, EToken.MEIN, EToken.DEMO, EToken.MINE, EToken.HSTE};
			king = EToken.DECI;
			heart = EToken.HSTE;
			color1 = new Color(0, 0.2f, 0, 1);
			color2 = new Color(0.8f, 0.8f, 0.8f, 1);
			playable = true;
			theme = SoundLoader.Theme(1);
		}
	}

	public class FTorridale : Faction {
		public FTorridale () {
			name = "Torridale";
			tokens = new List<EToken> {EToken.GARG, EToken.BATT, EToken.CONF, EToken.ASHE, EToken.SMAS, EToken.ROOK, EToken.HSTO};
			king = EToken.GARG;
			heart = EToken.HSTO;
			color1 = new Color(0.5f, 0.5f, 0.5f ,1);
			color2 = new Color(0.6f, 0.1f, 0.1f, 1);
			playable = true;
			theme = SoundLoader.Theme(2);
		}
	}

	public class FGrove : Faction {
		public FGrove () {
			name = "Forgotten Grove";
			tokens = new List<EToken> {EToken.ULTR, EToken.META, EToken.TALO, EToken.GRIZ, EToken.HFIR};
			king = EToken.ULTR;
			heart = EToken.HFIR;
			color1 = new Color(0.7f, 0.5f, 0, 1);
			color2 = new Color(0, 0.2f, 0, 1);
			playable = true;
			theme = SoundLoader.Theme(3);
		}
	}
	public class FChrono : Faction {
		public FChrono () {
			name = "Chrononistas";
			tokens = new List<EToken> {EToken.OLDT, EToken.REPR, EToken.PIEC, EToken.REVO, EToken.APER, EToken.HBRA};
			king = EToken.OLDT;
			heart = EToken.HBRA;
			color1 = new Color(1, 0.8f, 0, 1);
			color2 = Color.magenta;
			playable = true;
			theme = SoundLoader.Theme(4);
		}
	}

	public class FPsycho : Faction {
		public FPsycho () {
			name = "Psycho Tropics";
			tokens = new List<EToken> {EToken.BLAC, EToken.MART, EToken.MYCO, EToken.BEES, EToken.LICH, EToken.WEBB, EToken.HSLK};
			king = EToken.BLAC;
			heart = EToken.HSLK;
			color1 = new Color(0.3f, 0, 0.5f, 1);
			color2 = Color.green;
			playable = true;
			theme = SoundLoader.Theme(5);
		}
	}

	public class FPsilent : Faction {
		public FPsilent () {
			name = "Psilent";
			tokens = new List<EToken> {EToken.DREA, EToken.PRIE, EToken.AREN, EToken.PRIS, EToken.HGLA};
			king = EToken.DREA;
			heart = EToken.HGLA;
			color1 = new Color(0.4f, 0.8f, 1, 1);
			color2 = new Color(1, 0.8f, 0, 1);
			playable = true;
			theme = SoundLoader.Theme(6);
		}
	}

	public class FVoidoid : Faction {
		public FVoidoid () {
			name = "Voidoids";
			tokens = new List<EToken> {EToken.MONO, EToken.GATE, EToken.NECR, EToken.RECY, EToken.HBLO};
			king = EToken.MONO;
			heart = EToken.HBLO;
			color1 = new Color(0.6f, 0.1f, 0.1f, 1);
			color2 = Color.black;
			playable = true;
			theme = SoundLoader.Theme(7);
		}
	}

	public class FObstacle : Faction {
		public FObstacle () {
			name = "(Obstacles)";
			tokens = new List<EToken> {
				EToken.MNTN, EToken.HILL, EToken.ROCK, 
				EToken.TREE, EToken.TREE2, EToken.TREE3, EToken.TREE4, 
				EToken.WATR, EToken.ICE, EToken.LAVA, 
				EToken.CORP, EToken.CURS,
				EToken.PYRA, EToken.TEMP, EToken.HOUS, EToken.COTT, EToken.RAMP,
				EToken.TARG, EToken.TSNK, EToken.TWEL,
				EToken.PYLO, EToken.HOLE, EToken.ANTE, EToken.EXHA
			};
			king = EToken.NONE; 
			heart = EToken.NONE;
			color1 = Color.white;
			color2 = Color.grey;
			playable = false;
			theme = default(AudioClip);
		}
	}
}