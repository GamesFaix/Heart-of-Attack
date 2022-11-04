using System.Collections.Generic;

namespace HOA.Tokens {
	
	public class TimeSink : Obstacle {
		
		public static Token Instantiate (Source source, bool template) {
			return new TimeSink (source, template);
		}

		TimeSink(Source s, bool template=false){
			ID = new TokenID(this, EToken.TSNK, s, false, template);
			Plane = Plane.Sunken;
			Body = new BodySensor1(this, Sensor.TimeSink);		
			Neutralize();
            WatchList = new WatchList();
            Notes = () => {return "Units sharing "+ID.Name+"'s Cell have -2 Initiative."; };
		
		}
		public override void Die (Source s, bool corpse=true, bool log=true) {
			((BodySensor1)Body).DestroySensors();
			base.Die(s,corpse,log);
		}
		
	}
}