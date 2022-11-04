namespace HOA.Tokens {
	
	public class Ice : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Ice (source, template);
		}

		Ice(Source s, bool template=false){
			ID = new TokenID(this, EToken.ICE, s, false, template);
			Plane = Plane.Sunken;
			Body = new BodySensor1(this, Sensor.Ice);	
			Neutralize();
            Notes = () =>
            {
                return "Ground Units moving into " + ID.Name + "'s Cell "+
                    "have a 25% of turning " + ID.Name + " into Water.";
            };
		}
		
		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodySensor1)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}
}