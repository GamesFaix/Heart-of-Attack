using UnityEngine; 
using System;
using System.Collections;
namespace HOA { 

	public class Index2 {

		public byte x {get; set;}
		public byte y {get; set;}

		protected Index2 () {}

		public Index2 (byte x, byte y) {
			this.x = x;
			this.y = y;
		}

		public override string ToString () {
			return "("+x+","+y+")";
		}

		public static Index2 operator + (Index2 a, Index2 b) {checked {return new Index2((byte)(a.x+b.x), (byte)(a.y+b.y));} }
		public static Index2 operator + (Index2 a, byte b) {checked {return new Index2((byte)(a.x+b), (byte)(a.y+b));} }
		public static Index2 operator + (byte a, Index2 b) {return b+a;}
		
		public static Index2 operator - (Index2 a, Index2 b) {checked {return new Index2((byte)(a.x-b.x), (byte)(a.y-b.y));} }

		public static Index2 operator * (Index2 a, Index2 b) {checked {return new Index2((byte)(a.x*b.x), (byte)(a.y*b.y));} }
		public static Index2 operator * (Index2 a, byte b) {checked {return new Index2((byte)(a.x*b), (byte)(a.y*b));} }
		public static Index2 operator * (byte a, Index2 b) {return b*a;}



	}
}
