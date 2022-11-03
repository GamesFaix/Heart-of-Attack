using UnityEngine;
using System;

namespace HOA { 

	public struct int2 {

		public int x {get; set;}
		public int y {get; set;}

		public int2 (int x, int y) {
			this.x = x;
			this.y = y;
		}

		public override string ToString () {
			return "("+x+","+y+")";
		}

		public static int2 operator * (int2 a, int2 b) {return new int2(a.x*b.x, a.y*b.y);}
		public static int2 operator * (int2 a, int b) {return new int2(a.y*b, a.y*b);}
		public static int2 operator * (int a, int2 b) {return new int2(a*b.x, a*b.y);}

		public static int2 operator + (int2 a, int2 b) {return new int2(a.x+b.x, a.y+b.y);}
		public static int2 operator + (int2 a, int b) {return new int2(a.x+b, a.y+b);}
		public static int2 operator + (int a, int2 b) {return b+a;}

		public static int2 operator - (int2 a, int2 b) {return new int2(a.x-b.x, a.y-b.y);}

		public static int2 operator / (int2 a, int2 b) {
			if (b.x==0 || b.y==0) {throw new Exception ("int2: Divide by zero.");}
			else {return new int2 (a.x/b.x, a.y/b.y);}
		}

		public int2 Abs {get {return new int2(Math.Abs(x), Math.Abs(y));} }

		public bool Covers (int2 other) {
			if (other.x < 0 || other.y < 0) {
				Debug.Log ("int2.Cover(int2) requires positive argument.");
				return false;
			}
			if (x > other.x && y > other.y) {return true;}
			return false;
		}

		public bool Fits (int2 other) {
			if (x >= other.x && y >= other.y) {return true;}
			return false;
		}

		public int Product {get {return x*y;} }
	}
}
