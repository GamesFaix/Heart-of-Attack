using UnityEngine;
using System.Collections;

namespace HOA {

	public static class Direction {
		
		public static int2 FromInt (int i) {
			switch (i) {
				case 0: return new int2(1,0);
				case 1: return new int2(1,1);
				case 2: return new int2(0,1);
				case 3: return new int2(-1,1);
				case 4: return new int2(-1,0);
				case 5: return new int2(-1,-1);
				case 6: return new int2(0,-1);
				case 7: return new int2(1,-1);
				default:
					GameLog.Debug("Direction: Invalid direction request.");
					return new int2(0,0);
			
			}
		}
		
		public static int2 FromCells (Cell c1, Cell c2) {
			int2 diff = c2.Index - c1.Index;
			int2 dir = new int2(0,0);

			if (diff.x!=0) {dir.x = diff.x/(Mathf.Abs(diff.x));}
			if (diff.y!=0) {dir.y = diff.y/(Mathf.Abs(diff.y));}

			return dir;
		}
	}
}