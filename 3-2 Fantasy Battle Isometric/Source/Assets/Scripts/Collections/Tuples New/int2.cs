using System;
using UnityEngine;

namespace HOA { 

	public struct int2 {
		public int x, y;

		public int2 (int x, int y) {
			this.x = x;
			this.y = y;
		}

		public static int2 MaxValue {get {return new int2(int.MaxValue, int.MaxValue);} }

		public static int2 operator + (int2 a, int2 b) {return new int2 (a.x+b.x, a.y+b.y);}
		public static int2 operator + (int2 a, int b) {return new int2 (a.x+b, a.y+b);}
		public static int2 operator + (int a, int2 b) {return b+a;}

		public static int2 operator - (int2 a) {return new int2(-a.x, -a.y);}
		public static int2 operator - (int2 a, int2 b) {return a + (-b);}
		public static int2 operator - (int2 a, int b) {return a + (-b);}

		public static int2 operator * (int2 a, int2 b) {return new int2 (a.x*b.x, a.y*b.y);}
		public static int2 operator * (int2 a, int b) {return new int2 (a.x*b, a.y*b);}
		public static int2 operator * (int a, int2 b) {return b*a;}

		public static int2 operator / (int2 a, int b) {
			if (a.x%b != 0  || a.y%b != 0) {
				Debug.Log("int2 cannot be divided unevenly");
				return MaxValue;
			}
			else if (b == 0) {
				Debug.Log("int2 divide by zero.");
				return MaxValue;
			}
			return new int2 ((int)(a.x/b), (int)(a.y/b));
		}

		public static int2 operator / (int2 a, int2 b) {
			if (a.x%b.x != 0 || a.y%b.y != 0) {
				Debug.Log("int2 cannot be divided unevenly.");
				return MaxValue;
			}
			else if (b.x==0 || b.y==0) {
				Debug.Log("int2 divide by zero.");
				return MaxValue;
			}
			return new int2 ((int)(a.x/b.x), (int)(a.y/b.y));
		}

		public int2 Abs {get {return new int2 (Math.Abs(x), Math.Abs(y));} }
		public int Product {get {return x*y;} }
	}
}
