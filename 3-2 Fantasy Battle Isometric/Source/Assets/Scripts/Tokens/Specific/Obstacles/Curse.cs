
using System.Collections.Generic;

namespace HOA {
	
	public class Curse : Obstacle {
		public Curse(Source s, bool template=false){
			ID = new ID(this, EToken.CURS, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodySensor9(this, SensorCurse.Instantiate);	
			Neutralize();
		}
		public override string Notes () {return 
			"Units take 2 damage upon entering "+ID.Name+"'s cell or a neighboring cell." +
			"\nUnits sharing "+ID.Name+"'s cell or in a neighboring cell take 2 damage at the end of their turn.";
		}
		
		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodySensor9)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}
	

}