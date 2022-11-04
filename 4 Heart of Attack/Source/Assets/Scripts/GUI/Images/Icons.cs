using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace HOA {

	public static class Icons {

        public static Dictionary<HOA.Stats, Texture2D> Stats { get; private set; }
        public static Dictionary<Plane, Texture2D> Planes { get; private set; }
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

            Cell = LoadFile("TargetClasses/Cell");
            Token = LoadFile("TargetClasses/Token");
            Unit = LoadFile("TargetClasses/Unit");
            Ob = LoadFile("TargetClasses/Obstacle");
            King = LoadFile("TargetClasses/King");
            Heart = LoadFile("TargetClasses/Heart");
            Trample = LoadFile("TargetClasses/Trample");
            Destructible = LoadFile("TargetClasses/Destructible");
            Corpse = LoadFile("TargetClasses/Corpse");

            Cell = LoadFile("TargetClasses/Cell");
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
		static void AddTraj (ETraj a, string fileName) {Trajectories.Add(a, LoadFile("Trajectories/"+fileName));}

		public static Texture2D SKIP() {return skip;}
		public static Texture2D ONDEATH() {return onDeath;}
		public static Texture2D TIMER() {return timer;}
		public static Texture2D SENSOR() {return sensor;}
		
		public static Texture2D COR() {return cor;}
		public static Texture2D FIR() {return fir;}
		public static Texture2D EXP() {return exp;}
		public static Texture2D DMG() {return dmg;}

        public static Texture2D Cell { get; private set; }
        public static Texture2D Token { get; private set; }
        public static Texture2D Unit { get; private set; }
        public static Texture2D Ob { get; private set; }
        public static Texture2D King { get; private set; }
        public static Texture2D Heart { get; private set; }
        public static Texture2D Trample { get; private set; }
        public static Texture2D Destructible { get; private set; }
        public static Texture2D Corpse { get; private set; }
    }
}