
using System.Collections.Generic;

namespace HOA {
	
	public class Lava : Obstacle {
		public Lava(Source s, bool template=false){
			ID = new ID(this, EToken.LAVA, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodyLava(this);	
			Neutralize();
		}
		public override string Notes () {return 
			"Ground units may not move through "+ID.Name+"." +
			"\nGround Units take 7 damage upon entering "+ID.Name+"'s Cell." +
			"\nGround Units sharing "+ID.Name+"'s Cell take 7 damage at the end of their turn.";
		}

		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodyLava)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}
	
	public class BodyLava : Body{
		Sensor sensor;
		
		public BodyLava(Token t){
			parent = t;
		}
		
		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				newCell.Enter(parent);
				
				if (sensor != default(Sensor)) {sensor.Delete();}
				sensor = new SensorLava(parent, newCell);
				newCell.AddSensor(sensor);
				return true;
			}	
			if (newCell == TemplateFactory.c) {
				cell = newCell;
				return true;	
			}
			return false;
		}
		
		public override void Exit () {cell.Exit(parent);}
		
		public void DestroySensors () {sensor.Delete();}
	}
	
	public class SensorLava : Sensor {
		
		public SensorLava (Token par, Cell c) {
			parent = par;	
			cell = c;
			Enter(c);
		}
		
		public override void Enter (Cell c) {
			c.SetStop(EPlane.GND, true);

			TokenGroup occupants = c.Occupants.OnlyType(EType.UNIT);
			occupants = occupants.OnlyPlane(EPlane.GND);
			
			foreach (Token t in occupants) {
				if (t is Unit) {
					Unit u = (Unit)t;
					u.timers.Add(new TLava(u, parent));
				}
			}
		}
		public override void Exit () {
			cell.SetStop(EPlane.GND, false);

			TokenGroup cellUnits = cell.Occupants.OnlyType(EType.UNIT);
			cellUnits = cellUnits.OnlyPlane(EPlane.GND);
			
			foreach (Unit u in cellUnits) {
				for (int i=u.timers.Count-1; i>=0; i--) {
					Timer timer = u.timers[i];
					if (timer is TLava) {u.timers.Remove(timer);}
				}
			}
			
		}
		
		public override void OtherEnter (Token t) {
			if (t is Unit && t.Plane.Is(EPlane.GND)) {
				Unit u = (Unit)t;
				u.timers.Add(new TLava(u, parent));
				EffectQueue.Interrupt(new EIncinerate(new Source(parent), u, 7));
			}
		}
		public override void OtherExit (Token t) {
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

	public class EIncinerate : Effect {
		public override string ToString () {return "Effect - Incinerate";}
		Unit target; int dmg;
		
		public EIncinerate (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			target.Damage(source, dmg);
			Mixer.Play(SoundLoader.Effect(EEffect.INCINERATE));
			target.Display.Effect(EEffect.INCINERATE);
		}
	}
	
	
	
}