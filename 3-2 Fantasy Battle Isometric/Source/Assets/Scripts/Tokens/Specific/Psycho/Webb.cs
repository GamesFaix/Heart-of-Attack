using UnityEngine;
using System.Collections.Generic;

namespace HOA.Tokens {
	
	public class Web : Obstacle {
		public Dictionary<Unit, int> Affected {get; private set;}

		public static Token Instantiate (Source source, bool template) {
			return new Web (source, template);
		}

		Web(Source s, bool template=false){
			ID = new ID(this, EToken.WEBB, s, false, template);
			Plane = Plane.Sunk;
			Special.Add(ESpecial.DEST);
			Body = new BodySensor1(this, SensorWeb.Instantiate);	
			Neutralize();
			Affected = new Dictionary<Unit, int>();
		}
		public override string Notes () {return 
			"Ground and Air units may not move through "+ID.Name+"." +
			"\nUnits sharing "+ID.Name+"'s Cell have a Move Range of 1.";
		}
	
		public override void Die (Source s, bool corpse=true, bool log=true) {
			((BodySensor1)Body).DestroySensors();
			base.Die(s,corpse,log);
		}
	
	}
}