
/*
	public class SensorCaraShield : Sensor {
		Dictionary<Unit, HealthCaraShield> shields;
		
		public SensorCaraShield (Token par, Cell c) {
			shields = new Dictionary<Unit, HealthCaraShield>();
			Enter(c);
		}

        SNCE {
			Unit u = (Unit)t;
			HealthCaraShield shield = new HealthCaraShield(this, u);
			u.SetHealth(shield);
			shields.Add(u, shield);
		}
		SXCE {
			foreach (HealthCaraShield hcs in shields.Values) {
				hcs.Remove();
			}
			shields = new Dictionary<Unit, HealthCaraShield>();
		}	
		ONE {
			Unit u = (Unit)t;
			HealthCaraShield shield = new HealthCaraShield(this, u);
			u.SetHealth(shield);
			shields.Add(u, shield);
		}
		OXE {
			Unit u = (Unit)t;
			if (shields.ContainsKey(u)) {
				shields[u].Remove();
				shields.Remove(u);
			}
		}
		
	}

	public class SensorAper : Sensor {
		static SensorAper sender = null;

		SNCE {
			if (sender == null) {
				Token otherAper = null;
				foreach (Token token in TokenRegistry.Tokens) {
					if (token is Tokens.Aperture && token != parent) {otherAper = token;}
				}
				if (otherAper != null) {
					Cell otherCell = otherAper.Body.Cell;
					if (t.Body.CanEnter(otherCell)) {
						sender = this;
						EffectQueue.Add(Effect.Teleport(new Source(parent), t, otherCell));
					}
				}
			}
			else {sender = null;}
		}
		ONE {
			if (sender == null) {
				Token otherAper = null;
				foreach (Token token in TokenRegistry.Tokens) {
					if (token is Tokens.Aperture && token != parent) {otherAper = token;}
				}
				if (otherAper != null) {
					Cell otherCell = otherAper.Body.Cell;
					if (t.Body.CanEnter(otherCell)) {
						sender = this;
						EffectQueue.Add(Effect.Teleport(new Source(parent), t, otherCell));
					}
				}
			}
			else {sender = null;}
		}
	}

 * 
 * */
