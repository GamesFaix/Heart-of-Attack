using System;
using UnityEngine;

namespace HOA {

	public enum Planes {Sunken, Ground, Air, Ethereal}
	
	public struct Plane {

		public const byte count = 4;
		public bool sunken, ground, air, ethereal;
		
		public Plane (bool sunken, bool ground, bool air, bool ethereal) {
			this.sunken = sunken;
			this.ground = ground;
			this.air = air;
			this.ethereal = ethereal;
		}
		
		public bool[] planes {get {return new bool[4] {sunken, ground, air, ethereal};} }
		
		public bool Equals (Plane other) {
			if (sunken == other.sunken 
			    && ground == other.ground 
			    && air == other.air 
			    && ethereal == other.ethereal) {
				return true;
			}
			return false;
		}
		public override bool Equals (System.Object obj) {return (obj is Plane ? Equals((Plane)obj) : false);}

		public override int GetHashCode () {
			int hash = 0;
			for (byte i=0; i<count; i++) {
				if (planes[i]) {hash += (1 << 2);}
			}
			return hash;
		}

		public static bool operator == (Plane a, Plane b) {return a.Equals(b);}
		public static bool operator != (Plane a, Plane b) {return !(a.Equals(b));}
		
		public static Plane Sunken {get {return new Plane(true,false,false,false);} }
		public static Plane Ground {get {return new Plane(false,true,false,false);} }
		public static Plane Air {get {return new Plane(false,false,true,false);} }
		public static Plane Ethereal {get {return new Plane(false,false,false,true);} }
		public static Plane Tall {get {return new Plane(false,true,true,false);} }



		public void Display (Panel p) {
			Rect box = p.IconBox;
			for (byte i=0; i<planes.Length; i++) {
				if (planes[i]) {
					if (GUI.Button(box, "")) {TipInspector.Inspect(ETip.Plane);}
					GUI.Box (box, Icons.Planes.planes[i], p.s);
					p.NudgeX();
					box = p.IconBox;
				}
			}
		}


	}	
}