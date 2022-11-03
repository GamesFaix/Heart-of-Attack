namespace HOA {
	
	public class Exhaust : Obstacle {
		delegate Sensor SensorConstructor ();

		public static Token Instantiate (Source source, bool template) {
			return new Exhaust (source, template);
		}

		Exhaust(Source s, bool template=false){
			ID = new ID(this, EToken.EXHA, s, false, template);
			Plane = Plane.Sunk;

			Body = new BodySensor1(this, SensorLava.Instantiate);	

			Neutralize();
		}
		public override string Notes () {return 
			"Ground and Flying units must stop on "+ID.Name+"." +
			"\nGround and Flying Units take 5 damage upon entering "+ID.Name+"'s Cell." +
			"\nGround and Flying Units sharing "+ID.Name+"'s Cell take 5 damage at the end of their turn.";
		}
		
		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodySensor1)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}
}