namespace HOA.Tokens {
	
	public class Targ : Obstacle {
		public static Token Instantiate (Source source, bool template) {
			return new Targ (source, template);
		}

		Targ(Source s, bool template=false){
			ID = new TokenID(this, EToken.TARG, s, false, template);
			Plane = Plane.Sunken;
			Body = new BodySensor1(this, Sensor.BombingRange);	
			Neutralize();
            Notes = () =>
            {
                return "\nIf any unit shares " + ID.Name + "'s Cell, " +
                "\n10 explosive damage is dealt in " + ID.Name + "'s cell at the end of that unit's turn.";
            };
    	}
		
		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodySensor1)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}
}