using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public static class Icons {

		static Dictionary<EStat, Texture2D> stats;
		static Dictionary<EPlane, Texture2D> planes;
		static Dictionary<EType, Texture2D> classes;
		static Dictionary<EAim, Texture2D> aims;

		static Texture2D skip, onDeath, timer;

		public static void Load() {
			stats = new Dictionary<EStat, Texture2D>();
			AddStat (EStat.HP, "HP");  
			AddStat (EStat.DEF, "DEF"); 
			AddStat (EStat.IN, "IN"); 
			AddStat (EStat.COR, "COR");
			AddStat (EStat.STUN, "STUN"); 
			AddStat (EStat.AP, "AP"); 
			AddStat (EStat.FP, "FP");

			planes = new Dictionary<EPlane, Texture2D>();
			AddPlane (EPlane.GND, "GND"); 
			AddPlane (EPlane.AIR, "AIR"); 
			AddPlane (EPlane.ETH, "ETH");
			AddPlane (EPlane.SUNK, "SUNK");

			classes = new Dictionary<EType, Texture2D>();
			AddClass (EType.DEST, "DEST"); 
			AddClass (EType.REM, "REM"); 
			AddClass (EType.KING, "KING"); 
			AddClass (EType.HEART, "HEART"); 
			AddClass (EType.TRAM, "TRAM");
			AddClass (EType.UNIT, "UNIT");
			AddClass (EType.CELL, "CELL");

			aims = new Dictionary<EAim, Texture2D>();
			AddAim (EAim.NEIGHBOR, "NEIGHBOR"); 
			AddAim (EAim.PATH, "PATH"); 
			AddAim (EAim.LINE, "LINE");
			AddAim (EAim.ARC, "ARC"); 
			AddAim (EAim.FREE, "FREE"); 
			AddAim (EAim.GLOBAL, "GLB"); 
			AddAim (EAim.SELF, "SELF");

			skip = Resources.Load("Icons/SKIP") as Texture2D;
			onDeath = Resources.Load("Icons/ONDEATH") as Texture2D;
			timer = Resources.Load("Icons/TIMER") as Texture2D;

		}

		static Texture2D LoadFile (string name) {return (Resources.Load("Icons/"+name) as Texture2D);}

		static void AddStat (EStat s, string fileName) {stats.Add(s, LoadFile(fileName));}
		static void AddPlane (EPlane p, string fileName) {planes.Add(p, LoadFile(fileName));}
		static void AddClass (EType s, string fileName) {classes.Add(s, LoadFile(fileName));}
		static void AddAim (EAim a, string fileName) {aims.Add(a, LoadFile(fileName));}

		public static Texture2D Stat(EStat s) {return stats[s];}
		public static Texture2D Plane(EPlane p) {return planes[p];}
		public static Texture2D Class(EType s) {return classes[s];}
		public static Texture2D Aim(EAim a) {return aims[a];}

		public static Texture2D SKIP() {return skip;}
		public static Texture2D ONDEATH() {return onDeath;}
		public static Texture2D TIMER() {return timer;}
	}
}