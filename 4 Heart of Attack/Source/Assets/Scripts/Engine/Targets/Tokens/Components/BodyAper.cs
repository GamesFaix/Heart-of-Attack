using UnityEngine; 

namespace HOA { 

	public class BodyAper : Body, IDeepCopyToken<BodyAper> {
	
		Sensor sensor;

		public BodyAper (Token parent) : base (parent) {}

		public new BodyAper DeepCopy (Token Parent) {return new BodyAper(Parent);}

		protected override void EnterSpecial (Cell newCell) {
			foreach (Token t in TokenRegistry.Tokens) {
				if ((t.ID.Species == Species.Aperture) && t != Parent) {
					Cell otherCell = t.Body.Cell;
					newCell.Links.Add(otherCell);
					otherCell.Links.Add(newCell);
				}
			}
			if (sensor != default(Sensor)) {sensor.Delete();}
			sensor = Sensor.Aperture(Parent, newCell);
			newCell.Sensors.Add(sensor);
		}

		public override void Exit () {
			foreach (Token t in TokenRegistry.Tokens) {
                if ((t.ID.Species == Species.Aperture) && t != Parent)
                {
					Cell otherCell = t.Body.Cell;
					Parent.Body.Cell.Links.Remove(otherCell);
					otherCell.Links.Remove(Parent.Body.Cell);
				}
			}
			if (sensor != null) {sensor.Exit();}
			Cell.Exit(Parent);
		}

		public override void DestroySensors () {sensor.Delete();}
	}
}
