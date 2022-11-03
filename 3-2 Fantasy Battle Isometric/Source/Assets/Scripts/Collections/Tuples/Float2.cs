using UnityEngine;

namespace HOA { 
	
	public struct Float2{
		
		public float x {get; set;}
		public float y {get; set;}
		
		public Float2 (float x, float y) {
			this.x = x;
			this.y = y;
		}

		public Float2 (Vector2 v) : this(v.x, v.y) {}
		
		public override string ToString () {
			return "("+x+","+y+")";
		}
		
		public static Float2 operator * (Float2 a, Float2 b) {return new Float2(a.x*b.x, a.y*b.y);}
		public static Float2 operator * (Float2 a, float b) {return new Float2(a.y*b, a.y*b);}
		public static Float2 operator * (float a, Float2 b) {return new Float2(a*b.x, a*b.y);}
		
		public bool Covers (Float2 other) {
			if (x > other.x && y > other.y) {return true;}
			return false;
		}
		
		public bool Fits (Float2 other) {
			if (x >= other.x && y >= other.y) {return true;}
			return false;
		}

		public float Product {get {return x*y;} }

		public Int2 Round {
			get {
				int i = (int)Mathf.RoundToInt(x);
				int j = (int)Mathf.RoundToInt(y);
				return new Int2(i,j);
			}
		}

		public Int2 Floor {
			get {
				int i = (int)Mathf.Floor(x);
				int j = (int)Mathf.Floor(y);
				return new Int2(i,j);
			}
		}

		public Int2 Ceil {
			get {
				int i = (int)Mathf.Ceil(x);
				int j = (int)Mathf.Ceil(y);
				return new Int2(i,j);
			}
		}

	}
}
