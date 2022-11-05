using UnityEngine;
using System.Collections;
using FBI.Collections;

namespace FBI.Map {
	public class CellMatrix {
	
		ReDictionary<Vector2, Cell> cells;
		ReDictionary<Vector3, GamePoint> points;
		
		public CellMatrix(){
			cells = new ReDictionary<Vector2, Cell>();
			points = new ReDictionary<Vector3, GamePoint>();	
			
		}
		
		public void AddCell(Vector2 key, Cell newCell) {
			cells.Add(key, newCell);
		}
		public Cell GetCell(Vector2 key) {
			return cells.GetValue(key);
		}
		public Vector2 GetCellV2(Cell cell) {
			return cells.GetKey(cell);
		}
		
		public void AddPoint(Vector3 key, GamePoint point) {
			points.Add(key, point);
		}
		public GamePoint GetPoint(Vector3 key) {
			return points.GetValue(key);
		}
		public Vector3 GetPointV3(GamePoint point) {
			return points.GetKey(point);
		}
		
		
	}
}