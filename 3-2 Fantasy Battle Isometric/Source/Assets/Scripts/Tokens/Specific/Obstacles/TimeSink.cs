using System.Collections.Generic;

namespace HOA.Tokens {
	
	public class TimeSink : Obstacle {
		List<Unit> affected;
		public List<Unit> Affected {get {return affected;} }

		public static Token Instantiate (Source source, bool template) {
			return new TimeSink (source, template);
		}

		TimeSink(Source s, bool template=false){
			ID = new ID(this, EToken.TSNK, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodySensor1(this, SensorTimeSink.Instantiate);		
			Neutralize();
			affected = new List<Unit>();
		}
		public override string Notes () {return 
			"Units sharing "+ID.Name+"'s Cell have -2 Initiative.";
		}
		
		public override void Die (Source s, bool corpse=true, bool log=true) {
			((BodySensor1)Body).DestroySensors();
			base.Die(s,corpse,log);
		}
		
	}
}