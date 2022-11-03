using UnityEngine;
using System.Collections;

namespace HOA {

	public static class Direction {
		
		public static Int2 FromInt (int i) {
			switch (i) {
				case 0: return new Int2(1,0);
				case 1: return new Int2(1,1);
				case 2: return new Int2(0,1);
				case 3: return new Int2(-1,1);
				case 4: return new Int2(-1,0);
				case 5: return new Int2(-1,-1);
				case 6: return new Int2(0,-1);
				case 7: return new Int2(1,-1);
				default:
					GameLog.Debug("Direction: Invalid direction request.");
					return new Int2(0,0);
			
			}
		}
		
		public static Int2 FromCells (Cell c1, Cell c2) {
			Int2 diff = c2.Index - c1.Index;
			Int2 dir = new Int2(0,0);

			if (diff.x!=0) {dir.x = diff.x/(Mathf.Abs(diff.x));}
			if (diff.y!=0) {dir.y = diff.y/(Mathf.Abs(diff.y));}

			return dir;
		}
	}
}