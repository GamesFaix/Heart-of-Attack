using UnityEngine;
using System.Collections;
using System;

namespace HOA { 

	public struct Int2 : IEnumerable{

		public int x {get; set;}
		public int y {get; set;}

		public Int2 (int x, int y) {
			this.x = x;
			this.y = y;
		}

		public override string ToString () {
			return "("+x+","+y+")";
		}

		public static Int2 operator * (Int2 a, Int2 b) {return new Int2(a.x*b.x, a.y*b.y);}
		public static Int2 operator * (Int2 a, int b) {return new Int2(a.y*b, a.y*b);}
		public static Int2 operator * (int a, Int2 b) {return new Int2(a*b.x, a*b.y);}

		public static Int2 operator / (Int2 a, Int2 b) {
			if (a.x%b.x != 0 || a.y%b.y != 0) {
				Debug.Log("Int2 cannot be divided unevenly.");
				return new Int2(0,0);
			}
			if (b.x==0 || b.y==0) {
				Debug.Log("Int2 divide by zero.");
				return new Int2(0,0);
			}
			return new Int2 ((int)(a.x/b.x), (int)(a.y/b.y));
		}

		public static Int2 operator + (Int2 a, Int2 b) {return new Int2(a.x+b.x, a.y+b.y);}
		public static Int2 operator + (Int2 a, int b) {return new Int2(a.x+b, a.y+b);}
		public static Int2 operator + (int a, Int2 b) {return b+a;}

		public static Int2 operator - (Int2 a, Int2 b) {return new Int2(a.x-b.x, a.y-b.y);}
		public static Int2 operator - (Int2 a, int b) {return new Int2(a.x-b, a.y-b);}

		public Int2 Abs {get {return new Int2(Math.Abs(x), Math.Abs(y));} }

		public bool Covers (Int2 other) {
			if (other.x < 0 || other.y < 0) {
				Debug.Log ("Int2.Cover(Int2) requires positive argument.");
				return false;
			}
			if (x > other.x && y > other.y) {return true;}
			return false;
		}

		public bool Fits (Int2 other) {
			if (x >= other.x && y >= other.y) {return true;}
			return false;
		}

		public int Product {get {return x*y;} }


		public IEnumerator GetEnumerator() {
			for (int i=0; i<x; i++) {
				for (int j=0; j<y; j++) {
					yield return new Int2(i,j);
				}
			}
		}
		IEnumerator IEnumerable.GetEnumerator() {return GetEnumerator();}

	}
}
