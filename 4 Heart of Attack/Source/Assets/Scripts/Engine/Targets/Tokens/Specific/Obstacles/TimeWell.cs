using System.Collections.Generic;

namespace HOA.Tokens {
	
	public class TimeWell : Obstacle {
		
		public static Token Instantiate (Source source, bool template) {
			return new TimeWell (source, template);
		}

		TimeWell(Source s, bool template=false){
			ID = new TokenID(this, EToken.TWEL, s, false, template);
			Plane = Plane.Sunken;
			Body = new BodySensor1(this, Sensor.TimeWell);	
			Neutralize();
			WatchList = new WatchList();
            Notes = () => {return "Units sharing "+ID.Name+"'s Cell have +2 Initiative.";};
		}
		
		public override void Die (Source s, bool corpse=true, bool log=true) {
			((BodySensor1)Body).DestroySensors();
			base.Die(s,corpse,log);
		}
		
	}
}