
namespace HOA { 

	public class BodySensor1 : Body {
	
		public BodySensor1 (Token parent, SensorContructor sc) {
			this.parent = parent;
			this.sc = sc;
		}

		public delegate Sensor SensorContructor (Token parent, Cell cell);
		SensorContructor sc;
		Sensor sensor;

		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				Exit();
				cell = newCell;
				newCell.Enter(parent);
				
				if (sensor != default(Sensor)) {sensor.Delete();}
				sensor = sc(parent, newCell);
				newCell.AddSensor(sensor);
				return true;
			}
			if (newCell == Game.Board.TemplateCell) {
				cell = newCell;
				return true;	
			}
			return false;
		}

		public override void Exit () {
			if (sensor != null) {sensor.Exit();}
			if (cell != null) {cell.Exit(parent);}
		}
		
		public void DestroySensors () {sensor.Delete();}
	}
}
