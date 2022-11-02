using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA.Tokens {
	
	public static class FactionRef {
	
	//red-green-blue
		static Color spaceBlue = new Color(0, 0, 1, 1);
		static Color armyGreen = new Color(0, 0.2f, 0, 1);
		static Color silver = new Color(0.8f, 0.8f, 0.8f, 1);
		static Color granite = new Color(0.5f, 0.5f, 0.5f ,1);
		static Color barkBrown = new Color(0.7f, 0.5f, 0, 1);
		static Color brass = new Color(1, 0.8f, 0, 1);
		static Color midnightPurple = new Color(0.3f, 0, 0.5f, 1);
	//		static Color venomGreen = new Color(0.1f, 0.9f, 0.3f, 1);
		static Color psiSky = new Color(0.4f, 0.8f, 1, 1);
		static Color blood = new Color(0.6f, 0.1f, 0.1f, 1);
		
		static Faction gearp = new Faction("G.E.A.R.P.", TTYPE.KABU, new List<TTYPE>{TTYPE.MAWT, TTYPE.CARA, TTYPE.KATA}, TTYPE.HSIL, spaceBlue, Color.white);
		static Faction newRep = new Faction("New Republic", TTYPE.DECI, new List<TTYPE> {TTYPE.PANO, TTYPE.MEIN, TTYPE.DEMO, TTYPE.MINE}, TTYPE.HSTE, armyGreen, silver);
		static Faction torr = new Faction("Torridale", TTYPE.GARG, new List<TTYPE> {TTYPE.BATT, TTYPE.CONF, TTYPE.ASHE, TTYPE.SMAS, TTYPE.ROOK}, TTYPE.HSTO, granite, blood);
		static Faction forGro = new Faction("Forgotten Grove", TTYPE.ULTR, new List<TTYPE> {TTYPE.META, TTYPE.TALO, TTYPE.GRIZ}, TTYPE.HFIR, barkBrown, armyGreen);
		static Faction chrono = new Faction("Chrononistas", TTYPE.OLDT, new List<TTYPE> {TTYPE.REPR, TTYPE.PIEC, TTYPE.APER, TTYPE.REVO}, TTYPE.HBRA, brass, Color.magenta);
		static Faction psyTro = new Faction("Psycho Tropics", TTYPE.BLAC, new List<TTYPE> {TTYPE.MART, TTYPE.MYCO, TTYPE.BEES, TTYPE.LICH, TTYPE.WEBB}, TTYPE.HSLK, midnightPurple, Color.green);
		static Faction psilent = new Faction("Psilent", TTYPE.DREA, new List<TTYPE> {TTYPE.PRIE, TTYPE.AREN, TTYPE.PRIS}, TTYPE.HGLA, psiSky, brass);
		static Faction voidoid = new Faction("Voidoids", TTYPE.MONO, new List<TTYPE> {TTYPE.MOUT, TTYPE.NECR, TTYPE.RECY}, TTYPE.HBLO, blood, Color.black);
		static Faction obstacles = new Faction("Obstacles", TTYPE.NONE, new List<TTYPE> {TTYPE.MNTN, TTYPE.HILL, TTYPE.ROCK, TTYPE.TREE, TTYPE.WATR, TTYPE.LAVA, TTYPE.CORP}, TTYPE.NONE, Color.white, Color.gray);
		
		static List<Faction> factions = new List<Faction>{gearp, newRep, torr, forGro, chrono, psyTro, psilent, voidoid, obstacles};
		
		public static Faction Faction (int n) {
			if (n>=0 && n<factions.Count) {return factions[n];}
			GameLog.Debug("TokenReference: Attempt to access invalid faction");
			return default(Faction);
		}
		
		public static int Count () {return factions.Count;}
		
		public static int LargestSize () {
			int largest = 0;
			foreach (Faction f in factions) {
				if (f.Size() > largest) {largest = f.Size();}
			}
			return largest;
		}
		
		public static List<Faction> Playable () {
			List<Faction> playable = new List<Faction>();
			foreach (Faction f in factions) {playable.Add(f);}
			playable.Remove(obstacles);
			return playable;
		}
		
		public static List<TTYPE> Kings () {
			List<TTYPE> kings = new List<TTYPE>();
			foreach (Faction f in factions) {
				if (f.King() != TTYPE.NONE){kings.Add(f.King());}
			}
			return kings;
		}
	}
}
