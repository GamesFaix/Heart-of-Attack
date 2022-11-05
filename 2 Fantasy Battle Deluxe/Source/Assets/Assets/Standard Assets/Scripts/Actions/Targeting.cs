using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FBI.Map;

namespace FBI.Actions {
	public static class Targeting {
		
		public static List<Cell> AdjacentCells (Cell startCell){
			Vector2 startXY = startCell.XY();
			
			List<Cell> cells = new List<Cell>();
			
			Vector2[] directions = Directions();
			foreach (Vector2 dir in directions){
				Vector2 checkXY = startXY + dir;
				Cell checkCell = Board.cellMatrix.GetCell(checkXY);		
				if (checkCell.GetStatus() != CellStatus.BORDER){
					cells.Add(checkCell);
				}
			}
			
			return cells;	
		}
		
		static Vector2[] Directions(){
			Vector2[] directions = new Vector2[8];
			
			directions[0] = new Vector2(0,1);
			directions[1] = new Vector2(1,1);	
			directions[2] = new Vector2(1,0);
			directions[3] = new Vector2(1,-1);
			directions[4] = new Vector2(0,-1);
			directions[5] = new Vector2(-1,-1);
			directions[6] = new Vector2(-1,0);
			directions[7] = new Vector2(-1,1);
			
			return directions;
		}
		

		
	
	}
}