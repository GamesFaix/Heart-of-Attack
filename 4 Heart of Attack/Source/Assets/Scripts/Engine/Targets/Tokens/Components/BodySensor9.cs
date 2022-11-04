using System.Collections.Generic;

namespace HOA { 
	
	public class BodySensor9 : Body, IDeepCopyToken<BodySensor9> {
		
		public BodySensor9 (Token parent, SensorContructor sc) : base(parent) {
			this.sc = sc;
		}

		public new BodySensor9 DeepCopy (Token parent) {return new BodySensor9(parent, sc);}
		
		public delegate Sensor SensorContructor (Token parent, Cell cell);
		SensorContructor sc;
		List<Sensor> sensors;

		protected override void EnterSpecial (Cell newCell) {
			sensors = new List<Sensor>();
			TargetGroup cells = Cell.Neighbors(true);
			foreach (Cell c in cells) {
				if (!(c is ExoCell)) {
					Sensor s = sc(parent, c);
					sensors.Add(s);
					c.Sensors.Add(s);
				}
			}
		}
		
		public override void Exit () {
			if (sensors != null) {
				for (int i=sensors.Count-1; i>=0; i--) {
					sensors[i].Delete();
				}
			}
			if (Cell != null) {Cell.Exit(parent);}
		}
		
		public void DestroySensors () {foreach (Sensor s in sensors) {s.Delete();} }
	}
}
