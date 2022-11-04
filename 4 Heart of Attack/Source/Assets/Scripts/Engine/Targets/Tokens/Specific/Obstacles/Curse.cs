namespace HOA.Tokens {
	
	public class Curse : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Curse (source, template);
		}

		Curse(Source s, bool template=false){
			ID = new TokenID(this, EToken.CURS, s, false, template);
			Plane = Plane.Sunken;
			Body = new BodySensor9(this, Sensor.Curse);	
			Neutralize();
            Notes = () =>
            {
                return "Units take 2 damage upon entering " + ID.Name + "'s cell "+
                "or a neighboring cell." +
                "\nUnits sharing " + ID.Name + "'s cell or in a neighboring cell "+
                "take 2 damage at the end of their turn.";
             };
		}
		
		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodySensor9)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}
	

}