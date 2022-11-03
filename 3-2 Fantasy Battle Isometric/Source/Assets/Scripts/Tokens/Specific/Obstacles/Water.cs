
using System.Collections.Generic;

namespace HOA {

	public class Water : Obstacle {
		public Water(Source s, bool template=false){
			ID = new ID(this, EToken.WATR, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodyWater(this);	
			Neutralize();
		}
		public override string Notes () {return 
			"Ground units must stop on "+ID.Name+"." +
			"\nGround Units sharing "+ID.Name+"'s Cell take 5 damage at the end of their turn.";
		}

		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodyWater)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}

	public class BodyWater : Body{
		Sensor sensor;
		
		public BodyWater(Token t){
			parent = t;
		}
		
		public override bool Enter (Cell newCell) {
			newCell.SetStop(EPlane.GND, true);

			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				newCell.Enter(parent);

				if (sensor != default(Sensor)) {sensor.Delete();}
				sensor = new SensorWater(parent, newCell);
				newCell.AddSensor(sensor);
				return true;
			}	
			if (newCell == Game.Board.TemplateCell) {
				cell = newCell;
				return true;	
			}
			return false;
		}
		
		public override void Exit () {
			cell.SetStop(EPlane.GND, false);
			cell.Exit(parent);
		}
		
		public void DestroySensors () {sensor.Delete();}
	}

	public class SensorWater : Sensor {

		public SensorWater (Token par, Cell c) {
			parent = par;	
			cell = c;
			Enter(c);
		}
		
		public override void Enter (Cell c) {
			TokenGroup occupants = c.Occupants.OnlyType(EType.UNIT);
			occupants = occupants.OnlyPlane(EPlane.GND);

			foreach (Token t in occupants) {
				if (t is Unit) {
					Unit u = (Unit)t;
					u.timers.Add(new TWater(u, parent));
				}
			}
		}
		public override void Exit () {
			TokenGroup cellUnits = cell.Occupants.OnlyType(EType.UNIT);
			cellUnits = cellUnits.OnlyPlane(EPlane.GND);
			
			foreach (Unit u in cellUnits) {
				for (int i=u.timers.Count-1; i>=0; i--) {
					Timer timer = u.timers[i];
					if (timer is TWater) {u.timers.Remove(timer);}
				}
			}
		
		}
		
		public override void OtherEnter (Token t) {
			if (t is Unit && t.Plane.Is(EPlane.GND)) {
				Unit u = (Unit)t;
				u.timers.Add(new TWater(u, parent));
			}
		}
		public override void OtherExit (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				for (int i=u.timers.Count-1; i>=0; i--) {
					Timer timer = u.timers[i];
					if (timer is TWater) {u.timers.Remove(timer);}
				}
			}
		}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}

	public class EWaterlog : Effect {
		public override string ToString () {return "Effect - Waterlog";}
		Unit target; int dmg;
		
		public EWaterlog (Source s, Unit u, int n) {
			source = s; target = u; dmg = n;
		}
		public override void Process() {
			target.Damage(source, dmg);
			Mixer.Play(SoundLoader.Effect(EEffect.WATERLOG));
			target.Display.Effect(EEffect.WATERLOG);
		}
	}



}