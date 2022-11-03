using System;
using UnityEngine;

namespace HOA { 

	public struct index2 {
		public ushort x, y;
		public index2 (ushort x, ushort y) {
			this.x = x;
			this.y = y;
		}
		public index2 (int x, int y) {
			if (Safe(x, out this.x) && Safe(y, out this.y)) {}
			else {Debug.Log("index2 constuctor argument out of range. ("+x+","+y+")");}
		}

		public static index2 MaxValue {get {return new index2(ushort.MaxValue, ushort.MaxValue);} }

		static ushort min = 0;
		static ushort max = ushort.MaxValue;
		static bool Safe (int i, out ushort u) {
			u = max;
			if (i>=min && i<=max) {u = (ushort)i; return true;}
			return false;
		}

		public static bool Safe (int2 i, out index2 index) {
			ushort ux=max, uy=max;
			if (Safe(i.x, out ux) && Safe(i.y, out uy)) {
				index = new index2(ux,uy);
				return true;
			}
			else {
				index = new index2(ux,uy);
				return false;
			}
		}

		public static explicit operator int2(index2 a) {return new int2(a.x, a.y);}
		public static explicit operator index2(int2 a) {
			ushort ux=max, uy=max;
			if (Safe(a.x, out ux) && Safe(a.y, out uy)) {}
			else {Debug.Log("Casting overflow, int2 to index2. ("+a.x+","+a.y+")");}
			return new index2(ux, uy);
		}

		public static index2 operator + (index2 a, int2 b) {return (index2) ((int2)a + b);}
		public static index2 operator + (int2 a, index2 b) {return b+a;}
		public static index2 operator - (index2 a, int2 b) {return a + (-b);}
	}
}
