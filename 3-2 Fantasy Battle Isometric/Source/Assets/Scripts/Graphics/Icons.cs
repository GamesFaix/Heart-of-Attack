using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public static class Icons {

		static Dictionary<EStat, Texture2D> stats;
		static Dictionary<EPlane, Texture2D> planes;
		static Dictionary<EType, Texture2D> types;
		static Dictionary<ETraj, Texture2D> aims;

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

			types = new Dictionary<EType, Texture2D>();
			AddType (EType.DEST, "DEST"); 
			AddType (EType.REM, "REM"); 
			AddType (EType.KING, "KING"); 
			AddType (EType.HEART, "HEART"); 
			AddType (EType.TRAM, "TRAM");
			AddType (EType.UNIT, "UNIT");
			AddType (EType.OB, "OBSTACLE");
			AddType (EType.CELL, "CELL");

			aims = new Dictionary<ETraj, Texture2D>();
			AddAim (ETraj.NEIGHBOR, "NEIGHBOR"); 
			AddAim (ETraj.PATH, "PATH"); 
			AddAim (ETraj.LINE, "LINE");
			AddAim (ETraj.ARC, "ARC"); 
			AddAim (ETraj.FREE, "FREE"); 
			AddAim (ETraj.GLOBAL, "GLB"); 
			AddAim (ETraj.SELF, "SELF");

			skip = Resources.Load("Icons/SKIP") as Texture2D;
			onDeath = Resources.Load("Icons/ONDEATH") as Texture2D;
			timer = Resources.Load("Icons/TIMER") as Texture2D;

		}

		static Texture2D LoadFile (string name) {return (Resources.Load("Icons/"+name) as Texture2D);}

		static void AddStat (EStat s, string fileName) {stats.Add(s, LoadFile(fileName));}
		static void AddPlane (EPlane p, string fileName) {planes.Add(p, LoadFile(fileName));}
		static void AddType (EType s, string fileName) {types.Add(s, LoadFile(fileName));}
		static void AddAim (ETraj a, string fileName) {aims.Add(a, LoadFile(fileName));}

		public static Texture2D Stat(EStat s) {return stats[s];}
		public static Texture2D Plane(EPlane p) {return planes[p];}
		public static Texture2D Type(EType s) {return types[s];}
		public static Texture2D Aim(ETraj a) {return aims[a];}

		public static Texture2D SKIP() {return skip;}
		public static Texture2D ONDEATH() {return onDeath;}
		public static Texture2D TIMER() {return timer;}
	}
}