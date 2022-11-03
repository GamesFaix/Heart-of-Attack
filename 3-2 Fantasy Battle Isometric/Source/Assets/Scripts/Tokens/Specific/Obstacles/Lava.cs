namespace HOA.Tokens {
	
	public class Lava : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Lava (source, template);
		}

		Lava(Source s, bool template=false){
			ID = new ID(this, EToken.LAVA, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodySensor1(this, SensorLava.Instantiate);	
			Neutralize();
		}
		public override string Notes () {return 
			"Ground units must stop on "+ID.Name+"." +
			"\nGround Units take 7 damage upon entering "+ID.Name+"'s Cell." +
			"\nGround Units sharing "+ID.Name+"'s Cell take 7 damage at the end of their turn.";
		}

		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodySensor1)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}
}