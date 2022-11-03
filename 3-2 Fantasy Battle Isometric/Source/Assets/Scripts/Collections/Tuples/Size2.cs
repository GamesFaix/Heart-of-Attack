using System;
using System.Collections;

namespace HOA { 

	public class Size2 : Index2, IEnumerable {
	
		public Size2 (byte x, byte y) : base(x,y) {
			if (x==0 || y==0) {
				throw new Exception("Size2 cannot have dimension equal to 0.");
			}
		}
	
		public static Size2 operator * (Size2 a, Size2 b) {checked {return new Size2 ((byte)(a.x*b.x), (byte)(a.y*b.y));} } 



		public static Index2 Max {get {return new Index2 (254,254);} }
		public Index2 Last {get {return new Index2((byte)(x-1), (byte)(y-1));} }

		public bool Contains (Index2 index) {
			if (x > index.x && y > index.y) {return true;}
			return false;
		}
		
		public bool FitsAround (Size2 other) {
			if (x >= other.x && y >= other.y) {return true;}
			return false;
		}

		public bool FitsInside (Size2 other) {
			if (x <= other.x && y <= other.y) {return true;}
			return false;
		}

		public int Count {get {return x*y;} }
		
		public IEnumerator GetEnumerator() {
			for (byte i=0; i<x; i++) {
				for (byte j=0; j<y; j++) {
					yield return new Index2(i,j);
				}
			}
		}
		IEnumerator IEnumerable.GetEnumerator() {return GetEnumerator();}

	}
}
