using UnityEngine; 

namespace HOA { 

	public class BodyAper : Body, IDeepCopyToken<BodyAper> {
	
		Sensor sensor;

		public BodyAper (Token parent) : base (parent) {}

		public new BodyAper DeepCopy (Token parent) {return new BodyAper(parent);}

		protected override void EnterSpecial (Cell newCell) {
			foreach (Token t in TokenFactory.Tokens) {
				if (t is Tokens.Aperture && t != parent) {
					Cell otherCell = t.Body.Cell;
					newCell.AddLink(otherCell);
					otherCell.AddLink(newCell);
				}
			}
			if (sensor != default(Sensor)) {sensor.Delete();}
			sensor = SensorAper.Instantiate(parent, newCell);
			newCell.AddSensor(sensor);
		}

		public override void Exit () {
			foreach (Token t in TokenFactory.Tokens) {
				if (t is Tokens.Aperture && t != parent) {
					Cell otherCell = t.Body.Cell;
					parent.Body.Cell.RemoveLink(otherCell);
					otherCell.RemoveLink(parent.Body.Cell);
				}
			}
			if (sensor != null) {sensor.Exit();}
			Cell.Exit(parent);
		}

		public void DestroySensors () {sensor.Delete();}
	}
}
