using HOA.Map;
using System.Collections.Generic;

namespace HOA.Tokens.Components {

	public class SensorCaraShield : Sensor {
		Dictionary<Unit, HealthCaraShield> shields;

		public SensorCaraShield (Unit par, Cell c) {
			parent = par;	
			cell = c;
			shields = new Dictionary<Unit, HealthCaraShield>();
			Enter(c);
		
		}

		public override void Enter (Cell c) {
			cell = c;
			TokenGroup cellUnits = cell.Occupants.FilterUnit;
			foreach (Unit u in cellUnits) {
				if (u.Code != TTYPE.CARA 
					&& u.Owner == parent.Owner) {
					HealthCaraShield shield = new HealthCaraShield(this, u);
					u.health = shield;
					shields.Add(u, shield);
				}
			}
		}
		public override void Exit () {
			foreach (HealthCaraShield hcs in shields.Values) {
				hcs.Remove();
			}
			shields = new Dictionary<Unit, HealthCaraShield>();
		}
		
		public override void OtherEnter (Token t) {
			if (t is Unit 
				&& t.Code != TTYPE.CARA
				&& t.Owner == parent.Owner) {
				Unit u = (Unit)t;
				HealthCaraShield shield = new HealthCaraShield(this, u);
				u.health = shield;
				shields.Add(u, shield);
			}
		}
		public override void OtherExit (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				if (shields.ContainsKey(u)) {
					shields[u].Remove();
					shields.Remove(u);
				}
			}
		}
	}
}