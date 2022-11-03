using System.Collections.Generic;

namespace HOA { 
	
	public class BodySensor9 : Body {
		
		public BodySensor9 (Token parent, SensorContructor sc) {
			this.parent = parent;
			this.sc = sc;
		}
		
		public delegate Sensor SensorContructor (Token parent, Cell cell);
		SensorContructor sc;
		List<Sensor> sensors;
		
		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				Exit();
				cell = newCell;
				newCell.Enter(parent);
				
				sensors = new List<Sensor>();
				CellGroup cells = cell.Neighbors(true);
				foreach (Cell c in cells) {
					if (!(c is ExoCell)) {
						Sensor s = sc(parent, c);
						sensors.Add(s);
						c.AddSensor(s);
					}
				}
				return true;
			}
			if (newCell == Game.Board.TemplateCell) {
				cell = newCell;
				return true;	
			}
			return false;
		}
		
		public override void Exit () {
			if (sensors != null) {
				for (int i=sensors.Count-1; i>=0; i--) {
					sensors[i].Delete();
				}
			}
			if (cell != null) {cell.Exit(parent);}
		}
		
		public void DestroySensors () {foreach (Sensor s in sensors) {s.Delete();} }
	}
}
