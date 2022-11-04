using UnityEngine;
using System;

namespace HOA{

	public struct Price : IComparable<Price>, IInspectable{
		public byte Energy {get; private set;}
		public byte Focus {get; private set;}
        public void Draw(Panel panel) { InspectorInfo.Price(this, panel); }


		public Price (byte energy, byte focus) {
			Energy = energy;
			Focus = focus;
		}

		public static Price Free {get {return new Price(0,0);} }
		public static Price Cheap {get {return new Price(1,0);} }

		public byte Total {get {return (byte)(Energy + Focus);} }

		public override string ToString () {return "("+Energy+"E / "+Focus+"F)";}
		
		public int CompareTo (Price other) {
			if (Total < other.Total) {return -1;}
			else if (Total > other.Total) {return 1;}
			else {
                if (Energy > other.Energy) { return -1; }
                else if (Energy < other.Energy) { return 1; }
				else {return 0;}
			}
		}

        public bool Equals(Price other) { return ((Energy == other.Energy && Focus == other.Focus) ? true : false); }	
		public override bool Equals (System.Object obj) {
			if (obj == null) {return false;}
			if (!(obj is Price)) {return false;}
			Price other = (Price)obj;
			return Equals(other);
		}

        public override int GetHashCode() { return (Energy << 16) & Focus; }

		public static bool operator == (Price a, Price b) {return a.Equals(b);}
		public static bool operator != (Price a, Price b) {return !(a.Equals(b));}

	}
}
