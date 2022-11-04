using System;
using UnityEngine;

namespace HOA { 

	public struct float2 {
		public float x, y;
		public float2 (float x, float y) {
			this.x = x;
			this.y = y;
		}

		public float2 (Vector2 v) : this(v.x, v.y) {}

		public static float2 operator * (float2 a, float2 b) {return new float2(a.x*b.x, a.y*b.y);}
		public static float2 operator * (float2 a, float b) {return new float2(a.y*b, a.y*b);}
		public static float2 operator * (float a, float2 b) {return new float2(a*b.x, a*b.y);}

		public int2 Round {
			get {
				int i = (int)Mathf.RoundToInt(x);
				int j = (int)Mathf.RoundToInt(y);
				return new int2(i,j);
			}
		}
		
		public int2 Floor {
			get {
				int i = (int)Mathf.Floor(x);
				int j = (int)Mathf.Floor(y);
				return new int2(i,j);
			}
		}
		
		public int2 Ceil {
			get {
				int i = (int)Mathf.Ceil(x);
				int j = (int)Mathf.Ceil(y);
				return new int2(i,j);
			}
		}
	}
}
