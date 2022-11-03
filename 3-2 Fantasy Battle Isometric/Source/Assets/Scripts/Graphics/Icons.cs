using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public static class Icons {

		static Dictionary<EStat, Texture2D> stats;
		static Dictionary<EPlane, Texture2D> planes;
		static Dictionary<EType, Texture2D> types;
		static Dictionary<ETraj, Texture2D> trajectories;

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

			trajectories = new Dictionary<ETraj, Texture2D>();
			AddTraj (ETraj.NEIGHBOR, "NEIGHBOR"); 
			AddTraj (ETraj.PATH, "PATH"); 
			AddTraj (ETraj.LINE, "LINE");
			AddTraj (ETraj.ARC, "ARC"); 
			AddTraj (ETraj.FREE, "FREE"); 
			AddTraj (ETraj.GLOBAL, "GLB"); 
			AddTraj (ETraj.SELF, "SELF");

			skip = LoadFile("Stats/SKIP");
			onDeath = LoadFile("ONDEATH");
			timer = LoadFile("TIMER");

		}

		static Texture2D LoadFile (string name) {return (Resources.Load("Images/Icons/"+name) as Texture2D);}

		static void AddStat (EStat s, string fileName) {stats.Add(s, LoadFile("Stats/"+fileName));}
		static void AddPlane (EPlane p, string fileName) {planes.Add(p, LoadFile("Planes/"+fileName));}
		static void AddType (EType s, string fileName) {types.Add(s, LoadFile("Types/"+fileName));}
		static void AddTraj (ETraj a, string fileName) {trajectories.Add(a, LoadFile("Trajectories/"+fileName));}

		public static Texture2D Stat(EStat s) {return stats[s];}
		public static Texture2D Plane(EPlane p) {return planes[p];}
		public static Texture2D Special(EType s) {return types[s];}
		public static Texture2D Traj(ETraj a) {return trajectories[a];}

		public static Texture2D SKIP() {return skip;}
		public static Texture2D ONDEATH() {return onDeath;}
		public static Texture2D TIMER() {return timer;}
	}
}