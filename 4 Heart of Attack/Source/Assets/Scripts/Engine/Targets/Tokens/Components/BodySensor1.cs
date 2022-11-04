
namespace HOA { 

	public class BodySensor1 : Body, IDeepCopyToken<BodySensor1> {
	
		public BodySensor1 (Token parent, SensorContructor sc) : base(parent) {
			this.sc = sc;
		}

		public new BodySensor1 DeepCopy (Token parent) {return new BodySensor1(parent, sc);}

		public delegate Sensor SensorContructor (Token parent, Cell cell);
		SensorContructor sc;
		Sensor sensor;

		protected override void EnterSpecial (Cell newCell) {
			if (sensor != default(Sensor)) {sensor.Delete();}
			sensor = sc(Parent, newCell);
			newCell.Sensors.Add(sensor);
		}

		public override void Exit () {
			if (sensor != null) {sensor.Exit();}
			if (Cell != null) {Cell.Exit(Parent);}
		}
		
		public override void DestroySensors () {sensor.Delete();}
	}
}
