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
					newCell.Links.Add(otherCell);
					otherCell.Links.Add(newCell);
				}
			}
			if (sensor != default(Sensor)) {sensor.Delete();}
			sensor = Sensor.Aperture(parent, newCell);
			newCell.Sensors.Add(sensor);
		}

		public override void Exit () {
			foreach (Token t in TokenFactory.Tokens) {
				if (t is Tokens.Aperture && t != parent) {
					Cell otherCell = t.Body.Cell;
					parent.Body.Cell.Links.Remove(otherCell);
					otherCell.Links.Remove(parent.Body.Cell);
				}
			}
			if (sensor != null) {sensor.Exit();}
			Cell.Exit(parent);
		}

		public void DestroySensors () {sensor.Delete();}
	}
}
