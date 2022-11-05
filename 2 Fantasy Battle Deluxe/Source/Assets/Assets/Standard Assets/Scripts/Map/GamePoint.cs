using UnityEngine;
using System.Collections;

namespace FBI.Map {
	
	public enum CellZ {FLAT, ITEM, GND, TRM, FLY, IMPASS, GAS}

	public struct GamePoint {
		Cell cell;
		public int x;
		public int y;
		public CellZ z;
		
		public GamePoint(Cell parentCell, CellZ c){
			cell = parentCell;
			x = parentCell.x;
			y = parentCell.y;
			z = c;
		}
		
		public Cell GetCell() {return cell;}
		
		public Vector3 ToWorldPoint(){
			Vector3 worldPoint = new Vector3();
			
			worldPoint.x = x * MapScale.scale + (MapScale.scale/2);
			worldPoint.y = MapScale.scale/2;
			worldPoint.z = y * MapScale.scale + (MapScale.scale/2);
			
			return worldPoint;	
		}
		
		public override string ToString(){
			return "("+x+", "+y+", "+z+")";	
		}
	}
}