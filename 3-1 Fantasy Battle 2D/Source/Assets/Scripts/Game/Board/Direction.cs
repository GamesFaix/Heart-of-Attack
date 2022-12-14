using UnityEngine;
using System.Collections;

namespace HOA {

	public static class Direction {
		
		public static int[] FromInt (int i) {
			switch (i) {
				case 0: return new int[2] {1,0};
				case 1: return new int[2] {1,1};
				case 2: return new int[2] {0,1};
				case 3: return new int[2] {-1,1};
				case 4: return new int[2] {-1,0};
				case 5: return new int[2] {-1,-1};
				case 6: return new int[2] {0,-1};
				case 7: return new int[2] {1,-1};
				default:
					GameLog.Debug("Direction: Invalid direction request.");
					return new int[2] {0,0};
			
			}
		}
		
		public static int[] FromCells (Cell c1, Cell c2) {
			int x = c2.X - c1.X;
			if (x!=0) {x = x/(Mathf.Abs(x));}

			int y = c2.Y - c1.Y;
			if (y!=0) {y = y/(Mathf.Abs(y));}
			return new int[2] {x,y};
		}
		
		
		
	}
}