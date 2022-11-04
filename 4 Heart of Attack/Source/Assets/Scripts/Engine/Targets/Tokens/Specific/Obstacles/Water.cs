namespace HOA.Tokens {

	public class Water : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Water (source, template);
		}

		Water(Source s, bool template=false){
			ID = new TokenID(this, EToken.WATR, s, false, template);
			Plane = Plane.Sunken;
			Body = new BodySensor1(this, Sensor.Water);	
			Neutralize();
            Notes = () =>
            {
                return "Ground units must stop on " + ID.Name + "." +
                "\nGround Units sharing " + ID.Name + "'s Cell take 5 damage at the end of their turn.";
            };
		}

		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodySensor1)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}
}