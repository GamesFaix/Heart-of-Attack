using UnityEngine; 

namespace HOA { 

	public class BodyAper : Body {
	
		Sensor sensor;


		public BodyAper (Token t) {parent = t;}

		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				foreach (Token t in TokenFactory.Tokens) {
					if (t is Aperture && t != parent) {
						Cell otherCell = t.Body.Cell;
						newCell.AddLink(otherCell);
						otherCell.AddLink(newCell);
					}
				}
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				newCell.Enter(parent);

				if (sensor != default(Sensor)) {sensor.Delete();}
				sensor = SensorAper.Instantiate(parent, newCell);
				newCell.AddSensor(sensor);

				if (parent.Display != null) {((TokenDisplay)parent.Display).MoveTo(cell);}
				return true;
			}	
			if (newCell == Game.Board.TemplateCell) {
				cell = newCell;
				return true;	
			}
			return false;
		}

		public override void Exit () {
			foreach (Token t in TokenFactory.Tokens) {
				if (t is Aperture && t != parent) {
					Cell otherCell = t.Body.Cell;
					parent.Body.Cell.RemoveLink(otherCell);
					otherCell.RemoveLink(parent.Body.Cell);
				}
			}
			if (sensor != null) {sensor.Exit();}
			cell.Exit(parent);
		}

		public void DestroySensors () {sensor.Delete();}
	}
}
