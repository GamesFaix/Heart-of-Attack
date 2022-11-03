using System;
using System.Collections;
using UnityEngine;

namespace HOA { 
	
	public struct size2 : IEnumerable {
		public ushort x, y;
		public size2 (ushort x, ushort y) {
			this.x = x;
			this.y = y;
		}
		public size2 (int x, int y) {
			if (Safe(x, out this.x) && Safe(y, out this.y)) {}
			else {Debug.Log("size2 constuctor argument out of range.");}
		}
		
		static ushort min = 1;
		static ushort max = ushort.MaxValue;
		static bool Safe (int i, out ushort u) {
			u = max;
			if (i>=min && i<=max) {u = (ushort)i; return true;}
			return false;
		}
		
		public static explicit operator int2(size2 a) {return new int2(a.x, a.y);}
		public static explicit operator size2(int2 a) {
			ushort ux=max, uy=max;
			if (Safe(a.x, out ux) && Safe(a.y, out uy)) {}
			else {Debug.Log("Casting overflow, int2 to size2");}
			return new size2(ux, uy);
		}
		
		public static size2 operator + (size2 a, size2 b) {return new size2(a.x+b.x, a.y+b.y);}
		public static size2 operator + (size2 a, int2 b) {return (size2) ((int2)a + b);}
		public static size2 operator + (int2 a, size2 b) {return b+a;}
		public static size2 operator + (size2 a, int b) {return new size2(a.x+b, a.y+b);}
		public static size2 operator + (int a, size2 b) {return b+a;}

		public static size2 operator - (size2 a, size2 b) {return a - (int2)b;}
		public static size2 operator - (size2 a, int2 b) {return a + (-b);}
		public static size2 operator - (size2 a, int b) {return a + (-b);}

		public static size2 operator * (size2 a, size2 b) {return new size2(a.x*b.x, a.y*b.y);}
		public static size2 operator * (size2 a, int2 b) {return (size2) ((int2)a * b);}
		public static size2 operator * (int2 b, size2 a) {return b*a;}
		public static size2 operator * (size2 a, int b) {return new size2(a.x*b, a.y*b);}
		public static size2 operator * (int a, size2 b) {return b*a;}

		public int Count {get {return x*y;} }

		public bool Contains (index2 i) {
			if (i.x < x && i.y < y) {return true;}
			return false;
		}

		public bool FitsIn (size2 s) {
			if (x <= s.x && y <= s.y) {return true;}
			return false;
		}

		public bool FitsAround (size2 s) {
			if (s.x <= x && s.y <= y) {return true;}
			return false;
		}

		public IEnumerator GetEnumerator() {
			for (ushort i=0; i<x; i++) {
				for (ushort j=0; j<y; j++) {
					yield return new index2(i,j);
				}
			}
		}
		IEnumerator IEnumerable.GetEnumerator() {return GetEnumerator();}

	}
}
