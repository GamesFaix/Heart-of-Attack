using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public static class Icons {

		static Dictionary<STAT, Texture2D> stats;
		static Dictionary<PLANE, Texture2D> planes;
		static Dictionary<SPECIAL, Texture2D> specials;
		static Dictionary<AIMTYPE, Texture2D> aimTypes;
		static Dictionary<TTAR, Texture2D[]> tTars;

		static Texture2D skip, cell, unit, onDeath;

		public static void Load() {
			stats = new Dictionary<STAT, Texture2D>();
			AddStat (STAT.HP, "HP");  AddStat (STAT.DEF, "DEF"); AddStat (STAT.IN, "IN"); AddStat (STAT.COR, "COR");
			AddStat (STAT.STUN, "STUN"); AddStat (STAT.AP, "AP"); AddStat (STAT.FP, "FP");

			planes = new Dictionary<PLANE, Texture2D>();
			AddPlane (PLANE.GND, "GND"); AddPlane (PLANE.AIR, "AIR"); AddPlane (PLANE.ETH, "ETH"); AddPlane (PLANE.SUNK, "SUNK");

			specials = new Dictionary<SPECIAL, Texture2D>();
			AddSpecial (SPECIAL.DEST, "DEST"); AddSpecial (SPECIAL.REM, "REM"); AddSpecial (SPECIAL.KING, "KING"); 
			AddSpecial (SPECIAL.HOA, "HEART"); AddSpecial (SPECIAL.TRAM, "TRAM");

			aimTypes = new Dictionary<AIMTYPE, Texture2D>();
			AddAimType (AIMTYPE.NEIGHBOR, "NEIGHBOR"); AddAimType (AIMTYPE.PATH, "PATH"); AddAimType (AIMTYPE.LINE, "LINE");
			AddAimType (AIMTYPE.ARC, "ARC"); AddAimType (AIMTYPE.FREE, "FREE"); AddAimType (AIMTYPE.GLOBAL, "GLB"); AddAimType (AIMTYPE.SELF, "SELF");

			skip = Resources.Load("Icons/SKIP") as Texture2D;
			cell = Resources.Load("Icons/CELL") as Texture2D;
			onDeath = Resources.Load("Icons/ONDEATH") as Texture2D;

			tTars = new Dictionary<TTAR, Texture2D[]>();
			AddTTar (TTAR.UNIT, new string[]{"UNIT"});
			AddTTar (TTAR.DEST, new string[]{"DEST", "NOREM"});
			AddTTar (TTAR.REM, new string[]{"REM"});
			AddTTar (TTAR.UNITDEST, new string[]{"UNIT", "DEST"});
			AddTTar (TTAR.DESTREM, new string[]{"UNIT"});
		}

		static Texture2D LoadFile (string name) {return (Resources.Load("Icons/"+name) as Texture2D);}

		static void AddStat (STAT s, string fileName) {stats.Add(s, LoadFile(fileName));}
		static void AddPlane (PLANE p, string fileName) {planes.Add(p, LoadFile(fileName));}
		static void AddSpecial (SPECIAL s, string fileName) {specials.Add(s, LoadFile(fileName));}
		static void AddAimType (AIMTYPE a, string fileName) {aimTypes.Add(a, LoadFile(fileName));}

		static void AddTTar (TTAR t, string[] fileNames) {
			int count = fileNames.Length;
			Texture2D[] texs = new Texture2D[count];

			for (int i=0; i<count; i++) {texs[i] = LoadFile(fileNames[i]);}
			tTars.Add(t, texs);
		}

		public static Texture2D Stat(STAT s) {return stats[s];}
		public static Texture2D Plane(PLANE p) {return planes[p];}
		public static Texture2D Special(SPECIAL s) {return specials[s];}
		public static Texture2D AimType(AIMTYPE a) {return aimTypes[a];}
		public static Texture2D[] TTar(TTAR t) {return tTars[t];}

		public static Texture2D SKIP() {return skip;}
		public static Texture2D CELL() {return cell;}
		public static Texture2D ONDEATH() {return onDeath;}
	}
}