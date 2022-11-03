using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class TimeWell : Obstacle {
		List<Unit> affected;
		public List<Unit> Affected {get {return affected;} }
		
		public TimeWell(Source s, bool template=false){
			ID = new ID(this, EToken.TWEL, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodySensor1(this, SensorTimeWell.Instantiate);	
			Neutralize();
			affected = new List<Unit>();
		}
		public override string Notes () {return 
			"Units sharing "+ID.Name+"'s Cell have +2 Initiative.";
		}
		
		public override void Die (Source s, bool corpse=true, bool log=true) {
			((BodySensor1)Body).DestroySensors();
			base.Die(s,corpse,log);
		}
		
	}
}