using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public static class Icons {

		static Dictionary<Stats, Texture2D> stats;
		static Dictionary<Planes, Texture2D> planes;
		static Dictionary<TargetClasses, Texture2D> targetClasses;
		static Dictionary<ETraj, Texture2D> trajectories;

		static Texture2D skip, onDeath, timer, sensor, cor, fir, exp, dmg;

		public static void Load() {
			stats = new Dictionary<Stats, Texture2D>();
			AddStat (Stats.Health, "HP");  
			AddStat (Stats.Defense, "DEF"); 
			AddStat (Stats.Initiative, "IN"); 
			AddStat (Stats.Stun, "STUN"); 
			AddStat (Stats.Energy, "AP"); 
			AddStat (Stats.Focus, "FP");

			planes = new Dictionary<Planes, Texture2D>();
            AddPlane(Planes.Sunken, "Sunken");
            AddPlane(Planes.Ground, "Ground"); 
			AddPlane (Planes.Air, "Air"); 
			AddPlane (Planes.Ethereal, "Ethereal");
			
			targetClasses = new Dictionary<TargetClasses, Texture2D>();
            AddType(TargetClasses.Cell, "Cell");
            AddType(TargetClasses.Token, "Token");
            AddType(TargetClasses.Unit, "Unit");
            AddType(TargetClasses.Ob, "Ob");
            AddType(TargetClasses.King, "King");
            AddType(TargetClasses.Heart, "Heart");
            AddType(TargetClasses.Tram, "Tram");
            AddType(TargetClasses.Dest, "Dest"); 
			AddType (TargetClasses.Corpse, "Corpse"); 
			
			trajectories = new Dictionary<ETraj, Texture2D>();
			AddTraj (ETraj.NEIGHBOR, "NEIGHBOR"); 
			AddTraj (ETraj.PATH, "PATH"); 
			AddTraj (ETraj.LINE, "LINE");
			AddTraj (ETraj.ARC, "ARC"); 
			AddTraj (ETraj.FREE, "FREE"); 
			AddTraj (ETraj.GLOBAL, "GLB"); 
			AddTraj (ETraj.SELF, "SELF");
			AddTraj (ETraj.RADIAL, "RAD");

			skip = LoadFile("Stats/SKIP");
			onDeath = LoadFile("ONDEATH");
			timer = LoadFile("TIMER");
			sensor = LoadFile("SENSOR");
			cor = LoadFile("Effects/COR");
			fir = LoadFile("Effects/FIR");
			exp = LoadFile("Effects/EXP");
			dmg = LoadFile("Effects/DMG");
		}

		static Texture2D LoadFile (string name) {return (Resources.Load("Images/Icons/"+name) as Texture2D);}

		static void AddStat (Stats s, string fileName) {stats.Add(s, LoadFile("Stats/"+fileName));}
		static void AddPlane (Planes p, string fileName) {planes.Add(p, LoadFile("Planes/"+fileName));}
		static void AddType (TargetClasses tc, string fileName) {targetClasses.Add(tc, LoadFile("TargetClasses/"+fileName));}
		static void AddTraj (ETraj a, string fileName) {trajectories.Add(a, LoadFile("Trajectories/"+fileName));}

		public static Texture2D Stat(Stats s) {return stats[s];}
		public static Texture2D Plane(Planes p) {return planes[p];}
		public static Texture2D TargetClass(TargetClasses tc) {return targetClasses[tc];}
		public static Texture2D Traj(ETraj a) {return trajectories[a];}

		public static Texture2D SKIP() {return skip;}
		public static Texture2D ONDEATH() {return onDeath;}
		public static Texture2D TIMER() {return timer;}
		public static Texture2D SENSOR() {return sensor;}
		
		public static Texture2D COR() {return cor;}
		public static Texture2D FIR() {return fir;}
		public static Texture2D EXP() {return exp;}
		public static Texture2D DMG() {return dmg;}
	}
}