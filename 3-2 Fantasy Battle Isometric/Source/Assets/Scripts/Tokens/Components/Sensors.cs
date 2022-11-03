using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class SensorLava : Sensor {

		protected override string Desc {get {return 
				"Stops Ground Tokens." +
				"\nGround Units entering Cell take 7 damage." +
				"\nGround Units take 7 damage if in Cell at the end of their turn."
				;} }

		SensorLava (Token par, Cell c) {
			parent = par;	
			planesToStop = Plane.Gnd;
			Enter(c);
		}

		public static Sensor Instantiate (Token par, Cell c) {return new SensorLava(par, c);}

		protected override bool IsTrigger (Token trigger) {
			if (trigger.Plane.Is(EPlane.GND)) {return true;}
			return false;
		}

		protected override void EnterEffects (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				u.timers.Add(new TLava(u, parent));
			}
		}

		protected override void ExitEffects (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				for (int i=u.timers.Count-1; i>=0; i--) {
					Timer timer = u.timers[i];
					if (timer is TLava) {u.timers.Remove(timer);}
				}
			}
		}
		
		protected override void OtherEnterEffects (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				u.timers.Add(new TLava(u, parent));
			}
			if (Game.Active && (t is Unit || t.Special.Is(ESpecial.DEST))) {
				EffectQueue.Interrupt(new Effects.Fire(new Source(parent), t, 7));
			}
		}
		
		protected override void OtherExitEffects (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				for (int i=u.timers.Count-1; i>=0; i--) {
					Timer timer = u.timers[i];
					if (timer is TLava) {u.timers.Remove(timer);}
				}
			}
		}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}

	public class SensorWater : Sensor {
		protected override string Desc {get {return 
				"Stops Ground Tokens." +
				"\nGround Units take 5 damage if in Cell at the end of their turn."
				;} }

		SensorWater (Token par, Cell c) {
			parent = par;	
			planesToStop = Plane.Gnd;
			Enter(c);
		}

		public static Sensor Instantiate (Token par, Cell c) {return new SensorWater(par, c);}

		protected override bool IsTrigger (Token trigger) {
			if (trigger is Unit && trigger.Plane.Is(EPlane.GND)) {return true;}
			return false;
		}
		
		protected override void EnterEffects (Token t) {
			Unit u = (Unit)t;
			u.timers.Add(new TWater(u, parent));
		}
		
		protected override void ExitEffects (Token t) {
			Unit u = (Unit)t;
			for (int i=u.timers.Count-1; i>=0; i--) {
				Timer timer = u.timers[i];
				if (timer is TWater) {u.timers.Remove(timer);}
			}
		}
		
		protected override void OtherEnterEffects (Token t) {
			Unit u = (Unit)t;
			u.timers.Add(new TWater(u, parent));
		}

		protected override void OtherExitEffects (Token t) {
			Unit u = (Unit)t;
			for (int i=u.timers.Count-1; i>=0; i--) {
				Timer timer = u.timers[i];
				if (timer is TWater) {u.timers.Remove(timer);}
			}
		}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}

	public class SensorTimeWell : Sensor {
		protected override string Desc {get {return "" +
			"Units in Cell have +2 Initiative";} }

		Tokens.TimeWell timeWell;
		
		SensorTimeWell (Token par, Cell c) {
			parent = par;
			timeWell = (Tokens.TimeWell)par;
			Enter(c);
		}

		public static Sensor Instantiate (Token par, Cell c) {return new SensorTimeWell(par, c);}

		protected override bool IsTrigger (Token trigger) {
			if (trigger is Unit) {return true;}
			return false;
		}

		protected override void EnterEffects (Token t) {
			Unit u = (Unit)t;
			timeWell.Affected.Add(u);
			EffectQueue.Add(new Effects.AddStat(new Source(parent), u, EStat.IN, 2));
		}

		protected override void ExitEffects (Token t) {
 			Unit u = (Unit)t;
			timeWell.Affected.Remove(u);
			EffectQueue.Add(new Effects.AddStat(new Source(parent), u, EStat.IN, -2));
		}
		
		protected override void OtherEnterEffects (Token t) {
			Unit u = (Unit)t;
			timeWell.Affected.Add(u);
			EffectQueue.Add(new Effects.AddStat(new Source(parent), u, EStat.IN, 2));
		}

		protected override void OtherExitEffects (Token t) {
			Unit u = (Unit)t;
			timeWell.Affected.Remove(u);
			EffectQueue.Add(new Effects.AddStat(new Source(parent), u, EStat.IN, -2));
		}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}

	public class SensorTimeSink : Sensor {
		protected override string Desc {get {return 
				"Units in Cell have -2 Initiative.";} }

		Tokens.TimeSink timeSink;
		
		public SensorTimeSink (Token par, Cell c) {
			parent = par;
			timeSink = (Tokens.TimeSink)par;
			Enter(c);
		}

		public static Sensor Instantiate (Token par, Cell c) {return new SensorTimeSink(par, c);}

		protected override bool IsTrigger (Token trigger) {
			if (trigger is Unit) {return true;}
			return false;
		}
		
		protected override void EnterEffects (Token t) {
			Unit u = (Unit)t;
			timeSink.Affected.Add(u);
			EffectQueue.Add(new Effects.AddStat(new Source(parent), u, EStat.IN, -2));
		}
		
		protected override void ExitEffects (Token t) {
			Unit u = (Unit)t;
			timeSink.Affected.Remove(u);
			EffectQueue.Add(new Effects.AddStat(new Source(parent), u, EStat.IN, 2));
		}
		
		protected override void OtherEnterEffects (Token t) {
			Unit u = (Unit)t;
			timeSink.Affected.Add(u);
			EffectQueue.Add(new Effects.AddStat(new Source(parent), u, EStat.IN, -2));
		}
		
		protected override void OtherExitEffects (Token t) {
			Unit u = (Unit)t;
			timeSink.Affected.Remove(u);
			EffectQueue.Add(new Effects.AddStat(new Source(parent), u, EStat.IN, 2));
		}
		
		public override string ToString () {return "("+parent.ToString()+")";}
	}

	public class SensorTarg : Sensor {
		protected override string Desc {get {return 
				"If a Unit is in Cell at the end of its turn," +
				"\n10 Explosive damage is dealth at Cell."
				;} }

		SensorTarg (Token par, Cell c) {
			parent = par;	
			Enter(c);
		}

		public static Sensor Instantiate (Token par, Cell c) {return new SensorTarg(par, c);}

		protected override bool IsTrigger (Token trigger) {
			if (trigger is Unit) {return true;}
			return false;
		}

		protected override void EnterEffects (Token t) {
			Unit u = (Unit)t;
			u.timers.Add(new TTarg(u, parent));
		}

		protected override void ExitEffects (Token t) {
			Unit u = (Unit)t;
			for (int i=u.timers.Count-1; i>=0; i--) {
				Timer timer = u.timers[i];
				if (timer is TTarg) {u.timers.Remove(timer);}
			}
		}
		
		protected override void OtherEnterEffects (Token t) {
			Unit u = (Unit)t;
			u.timers.Add(new TTarg(u, parent));
		}
		protected override void OtherExitEffects (Token t) {
			Unit u = (Unit)t;
			for (int i=u.timers.Count-1; i>=0; i--) {
				Timer timer = u.timers[i];
				if (timer is TTarg) {u.timers.Remove(timer);}
			}
		}
		
		public override string ToString () {return "("+parent.ToString()+")";}
	}

	public class SensorIce : Sensor {
		protected override string Desc {get {return "" +
				"When any Ground Token enters Cell," +
				"\nthere is a 25% chance Ice breaks, turning into Water." +
				"\nTokens moving through Cell when Ice breaks do not stop," +
				"\nonly Tokens stopping on Ice that breaks are affected by Water."
				;} }

		SensorIce (Token par, Cell c) {
			parent = par;	
			Enter(c);
		}

		public static Sensor Instantiate (Token par, Cell c) {return new SensorIce(par, c);}

		protected override bool IsTrigger (Token trigger) {
			if (trigger.Plane.Is(EPlane.GND)) {return true;}
			return false;
		}

		protected override void OtherEnterEffects (Token t) {
			if (Game.Active) {
				int random = DiceCoin.Throw(new Source(parent), EDice.D4);
				if (random == 1) {
					EffectQueue.Add(new Effects.Replace(new Source(parent), parent, EToken.WATR));
				}
			}
		}

		public override string ToString () {return "("+parent.ToString()+")";}
	}

	public class SensorExhaust : Sensor {
		protected override string Desc {get {return 
				"Stops Ground and Air Tokens." +
				"\nGround and Air Units entering Cell take 5 damage." +
				"\nGround and Air Units take 5 damage if in Cell at the end of their turn."
				;} }

		public SensorExhaust (Token par, Cell c) {
			parent = par;	
			planesToStop = Plane.Tall;
			Enter(c);
		}

		public static Sensor Instantiate (Token par, Cell c) {return new SensorExhaust(par, c);}

		protected override bool IsTrigger (Token trigger) {
			if (trigger is Unit 
			    && (trigger.Plane.Is(EPlane.GND) || trigger.Plane.Is(EPlane.AIR))) {
				return true;
			}
			return false;
		}

		protected override void EnterEffects (Token t) {
			Unit u = (Unit)t;
			u.timers.Add(new TExhaust(u, parent));
		}

		protected override void ExitEffects (Token t) {
			Unit u = (Unit)t;
			for (int i=u.timers.Count-1; i>=0; i--) {
				Timer timer = u.timers[i];
				if (timer is TExhaust) {u.timers.Remove(timer);}
			}
		}
		
		protected override void OtherEnterEffects (Token t) {
			Unit u = (Unit)t;
			u.timers.Add(new TExhaust(u, parent));
			if (Game.Active) {EffectQueue.Interrupt(new Effects.Damage(new Source(parent), u, 5));}
		}

		protected override void OtherExitEffects (Token t) {
			Unit u = (Unit)t;
			for (int i=u.timers.Count-1; i>=0; i--) {
				Timer timer = u.timers[i];
				if (timer is TExhaust) {u.timers.Remove(timer);}
			}
		}
		
		public override string ToString () {return "("+parent.ToString()+")";}
	}

	public class SensorCurse : Sensor {
		protected override string Desc {get {return 
				"\nUnits entering Cell take 2 damage." +
				"\nUnits take 2 damage if in Cell at the end of their turn."
				;} }

		public SensorCurse (Token par, Cell c) {
			parent = par;	
			Enter(c);
		}

		public static Sensor Instantiate (Token par, Cell c) {return new SensorCurse(par, c);}

		protected override bool IsTrigger (Token trigger) {
			if (trigger is Unit) {return true;}
			return false;
		}		
		protected override void EnterEffects (Token t) {
			Unit u = (Unit)t;
			u.timers.Add(new TCurse(u, parent));
		}
		protected override void ExitEffects (Token t) {
			Unit u = (Unit)t;
			for (int i=u.timers.Count-1; i>=0; i--) {
				Timer timer = u.timers[i];
				if (timer is TCurse) {u.timers.Remove(timer);}
			}
		}
		
		protected override void OtherEnterEffects (Token t) {
			Unit u = (Unit)t;
			u.timers.Add(new TCurse(u, parent));
			if (Game.Active) {EffectQueue.Interrupt(new Effects.Damage(new Source(parent), u, 2));}	
		}

		protected override void OtherExitEffects (Token t) {
			Unit u = (Unit)t;
			for (int i=u.timers.Count-1; i>=0; i--) {
				Timer timer = u.timers[i];
				if (timer is TCurse) {u.timers.Remove(timer);}
			}
		}
		
		public override string ToString () {return "("+parent.ToString()+")";}
	}

	public class SensorMine : Sensor {
		protected override string Desc {get {return 
				"If a Token enters Cell, " +
				"\n10 Explosive damage is dealt at "+parent+"'s Cell."
				;} }

		public SensorMine (Token par, Cell c) {
			parent = par;	
			Enter(c);
		}

		public static Sensor Instantiate (Token par, Cell c) {return new SensorMine(par, c);}

		protected override bool IsTrigger (Token trigger) {return true;}		

		protected override void OtherEnterEffects (Token t) {
			if (Game.Active) {EffectQueue.Interrupt(new Effects.Detonate(new Source(t), parent));}
		}

		public override string ToString () {return "Trigger ("+parent.ToString()+")";}
	}

	public class SensorWeb : Sensor {
		protected override string Desc {get {return 
				"Stops Ground and Air Tokens." +
				"\nGround and Air Units in Cell have a Move Range of 1."
				;} }

		Tokens.Web web;
		
		public SensorWeb (Token par, Cell c) {
			parent = par;
			web = (Tokens.Web)par;
			planesToStop = Plane.Tall;
			Enter(c);
		}

		public static Sensor Instantiate (Token par, Cell c) {return new SensorWeb(par, c);}

		protected override bool IsTrigger (Token trigger) {
			if (trigger is Unit
			    && (trigger.Plane.Is(EPlane.GND) || trigger.Plane.Is(EPlane.AIR))) {
				return true;
			}
			return false;
		}	

		protected override void EnterEffects (Token t) {
			EffectQueue.Add(new Effects.Stick(new Source(parent), (Unit)t));
		}

		protected override void ExitEffects (Token t) {
			foreach (Unit u in web.Affected.Keys) {
				Task move = u.Arsenal.Move;
				if (move != default(Task)) {
					move.Aim[0].Range = web.Affected[u];
					web.Affected.Remove(u);
				}
			}
		}
		
		protected override void OtherEnterEffects (Token t) {
			EffectQueue.Add(new Effects.Stick(new Source(parent), (Unit)t));
		}

		protected override void OtherExitEffects (Token t) {
			Unit u = (Unit)t;
			Task move = u.Arsenal.Move;
			if (move != default(Task)) {
				move.Aim[0].Range = web.Affected[u];
				web.Affected.Remove(u);
			}
		}

		public override string ToString () {return "("+parent.ToString()+")";}
	}

	public class SensorCaraShield : Sensor {
		protected override string Desc {
			get { 
				string s = "Units in Cell on "+parent.ToString()+"'s team ";
				s += "\n(except Carapace Invaders) add";
				s += "\n"+parent.ToString()+"'s Defense to their own.";
				return s;				
			} 
		}
		Dictionary<Unit, HealthCaraShield> shields;
		
		public SensorCaraShield (Token par, Cell c) {
			parent = par;	
			shields = new Dictionary<Unit, HealthCaraShield>();
			Enter(c);
		}

		public static Sensor Instantiate (Token par, Cell c) {return new SensorCaraShield(par, c);}

		protected override bool IsTrigger (Token t) {
			if (t is Unit
			    && t.ID.Code != EToken.CARA
			    && t.Owner == parent.Owner) {
				return true;
			}
			return false;
		}

		protected override void EnterEffects (Token t) {
			Unit u = (Unit)t;
			HealthCaraShield shield = new HealthCaraShield(this, u);
			u.SetHealth(shield);
			shields.Add(u, shield);
		}

		protected override void ExitEffects (Token t) {
			foreach (HealthCaraShield hcs in shields.Values) {
				hcs.Remove();
			}
			shields = new Dictionary<Unit, HealthCaraShield>();
		}
		
		protected override void OtherEnterEffects (Token t) {
			Unit u = (Unit)t;
			HealthCaraShield shield = new HealthCaraShield(this, u);
			u.SetHealth(shield);
			shields.Add(u, shield);
		}
		protected override void OtherExitEffects (Token t) {
			Unit u = (Unit)t;
			if (shields.ContainsKey(u)) {
				shields[u].Remove();
				shields.Remove(u);
			}
		}
		
		public override string ToString () {return "Shield ("+parent.ToString()+")";}
	}

	public class SensorAper : Sensor {
		static SensorAper sender = null;

		protected override string Desc {get {return 
				"Stops all Tokens."
				;} }
		
		SensorAper (Token par, Cell c) {
			parent = par;	
			planesToStop = new Plane (new List<EPlane>() {EPlane.GND, EPlane.AIR, EPlane.ETH});
			Enter(c);
		}
		
		public static Sensor Instantiate (Token par, Cell c) {return new SensorAper(par, c);}
		
		protected override bool IsTrigger (Token trigger) {return true;}

		protected override void EnterEffects (Token t) {
			if (sender == null) {
				Token otherAper = null;
				foreach (Token token in TokenFactory.Tokens) {
					if (token is Tokens.Aperture && token != parent) {otherAper = token;}
				}
				if (otherAper != null) {
					Cell otherCell = otherAper.Body.Cell;
					if (t.Body.CanEnter(otherCell)) {
						sender = this;
						EffectQueue.Add(new Effects.Teleport(new Source(parent), t, otherCell));
					}
				}
			}
			else {sender = null;}
		}

		protected override void OtherEnterEffects (Token t) {
			if (sender == null) {
				Token otherAper = null;
				foreach (Token token in TokenFactory.Tokens) {
					if (token is Tokens.Aperture && token != parent) {otherAper = token;}
				}
				if (otherAper != null) {
					Cell otherCell = otherAper.Body.Cell;
					if (t.Body.CanEnter(otherCell)) {
						sender = this;
						EffectQueue.Add(new Effects.Teleport(new Source(parent), t, otherCell));
					}
				}
			}
			else {sender = null;}
		}

		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}
}