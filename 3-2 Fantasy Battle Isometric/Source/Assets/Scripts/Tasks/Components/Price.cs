using UnityEngine;
using System;

namespace HOA{

	public struct Price : IComparable<Price> {
		public byte E {get; private set;}
		public byte F {get; private set;}

		public Price (byte energy, byte focus) {
			E = energy;
			F = focus;
		}

		public static Price Free {get {return new Price(0,0);} }
		public static Price Cheap {get {return new Price(1,0);} }

		public byte Total {get {return (byte)(E+F);} }

		public override string ToString () {return "("+E+"E / "+F+"F)";}
		
		public void Draw (Panel p) {
			Rect box = p.IconBox;
			if (GUI.Button(box,"")) {
				if (GUIInspector.RightClick) {TipInspector.Inspect(ETip.AP);}
			}
			GUI.Box(box, Icons.Stats.energy, p.s);
			box = p.IconBox;
			GUI.Label(box, E+"", p.s);
			p.NudgeX();

			box = p.IconBox;
			if (GUI.Button(box,"")) {
				if (GUIInspector.RightClick) {TipInspector.Inspect(ETip.FP);}
			}
			GUI.Box(box, Icons.Stats.focus, p.s);
			box = p.IconBox;
			GUI.Label(box, F+"", p.s);
		}

		public int CompareTo (Price other) {
			if (Total < other.Total) {return -1;}
			else if (Total > other.Total) {return 1;}
			else {
				if (E > other.E) {return -1;}
				else if (E < other.E) {return 1;}
				else {return 0;}
			}
		}

		public bool Equals (Price other) {return ((E==other.E && F==other.F) ? true : false);}	
		public override bool Equals (System.Object obj) {
			if (obj == null) {return false;}
			if (!(obj is Price)) {return false;}
			Price other = (Price)obj;
			return Equals(other);
		}

		public override int GetHashCode () {return (E << 16) & F;}

		public static bool operator == (Price a, Price b) {return a.Equals(b);}
		public static bool operator != (Price a, Price b) {return !(a.Equals(b));}

	}
}
