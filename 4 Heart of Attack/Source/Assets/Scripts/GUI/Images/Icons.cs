using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace HOA {

	public static class Icons {

        public static Dictionary<HOA.Stats, Texture2D> Stats { get; private set; }
        public static Dictionary<Plane, Texture2D> Planes { get; private set; }
        public static Dictionary<TargetClasses, Texture2D> TargetClasses { get; private set; }
        public static Dictionary<ETraj, Texture2D> Trajectories { get; private set; }

		static Texture2D skip, onDeath, timer, sensor, cor, fir, exp, dmg;

		public static void Load() {
			Stats = new Dictionary<Stats, Texture2D>();
			AddStat (HOA.Stats.Health, "HP");  
			AddStat (HOA.Stats.Defense, "DEF");
            AddStat(HOA.Stats.Initiative, "IN"); 
			AddStat (HOA.Stats.Stun, "STUN"); 
			AddStat (HOA.Stats.Energy, "AP"); 
			AddStat (HOA.Stats.Focus, "FP");

			Planes = new Dictionary<Plane, Texture2D>();
            AddPlane(Plane.Sunken, "Sunken");
            AddPlane(Plane.Ground, "Ground");
            AddPlane(Plane.Air, "Air");
            AddPlane(Plane.Ethereal, "Ethereal");
			
			TargetClasses = new Dictionary<TargetClasses, Texture2D>();
            AddType(HOA.TargetClasses.Cell, "Cell");
            AddType(HOA.TargetClasses.Token, "Token");
            AddType(HOA.TargetClasses.Unit, "Unit");
            AddType(HOA.TargetClasses.Ob, "Ob");
            AddType(HOA.TargetClasses.King, "King");
            AddType(HOA.TargetClasses.Heart, "Heart");
            AddType(HOA.TargetClasses.Tram, "Tram");
            AddType(HOA.TargetClasses.Dest, "Dest");
            AddType(HOA.TargetClasses.Corpse, "Corpse"); 
			
			Trajectories = new Dictionary<ETraj, Texture2D>();
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

		static void AddStat (Stats s, string fileName) {Stats.Add(s, LoadFile("Stats/"+fileName));}
		static void AddPlane (Plane p, string fileName) {Planes.Add(p, LoadFile("Planes/"+fileName));}
		static void AddType (TargetClasses tc, string fileName) {TargetClasses.Add(tc, LoadFile("TargetClasses/"+fileName));}
		static void AddTraj (ETraj a, string fileName) {Trajectories.Add(a, LoadFile("Trajectories/"+fileName));}

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