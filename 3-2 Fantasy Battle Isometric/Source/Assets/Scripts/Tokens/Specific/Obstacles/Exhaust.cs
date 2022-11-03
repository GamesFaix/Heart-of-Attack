
using System.Collections.Generic;

namespace HOA {
	
	public class Exhaust : Obstacle {
		public Exhaust(Source s, bool template=false){
			ID = new ID(this, EToken.EXHA, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodyExhaust(this);	
			Neutralize();
		}
		public override string Notes () {return 
			"Ground and Flying units must stop on "+ID.Name+"." +
			"\nGround and Flying Units take 5 damage upon entering "+ID.Name+"'s Cell." +
			"\nGround and Flying Units sharing "+ID.Name+"'s Cell take 5 damage at the end of their turn.";
		}
		
		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodyExhaust)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}
	
	public class BodyExhaust : Body{
		Sensor sensor;
		
		public BodyExhaust(Token t){
			parent = t;
		}
		
		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				newCell.Enter(parent);
				
				if (sensor != default(Sensor)) {sensor.Delete();}
				sensor = new SensorExhaust(parent, newCell);
				newCell.AddSensor(sensor);
				return true;
			}	
			if (newCell == Game.Board.TemplateCell) {
				cell = newCell;
				return true;	
			}
			return false;
		}
		
		public override void Exit () {cell.Exit(parent);}
		
		public void DestroySensors () {sensor.Delete();}
	}
	
	public class SensorExhaust : Sensor {
		
		public SensorExhaust (Token par, Cell c) {
			parent = par;	
			cell = c;
			Enter(c);
		}
		
		public override void Enter (Cell c) {
			c.SetStop(EPlane.GND, true);
			c.SetStop(EPlane.AIR, true);
			
			TokenGroup occupants = c.Occupants.OnlyType(EType.UNIT);
			occupants = occupants.RemovePlane(EPlane.ETH);
			occupants = occupants.RemovePlane(EPlane.SUNK);
			
			foreach (Token t in occupants) {
				if (t is Unit) {
					Unit u = (Unit)t;
					u.timers.Add(new TExhaust(u, parent));
				}
			}
		}
		public override void Exit () {
			cell.SetStop(EPlane.GND, false);
			cell.SetStop(EPlane.AIR, false);

			TokenGroup cellUnits = cell.Occupants.OnlyType(EType.UNIT);
			cellUnits = cellUnits.RemovePlane(EPlane.ETH);
			cellUnits = cellUnits.RemovePlane(EPlane.SUNK);

			foreach (Unit u in cellUnits) {
				for (int i=u.timers.Count-1; i>=0; i--) {
					Timer timer = u.timers[i];
					if (timer is TExhaust) {u.timers.Remove(timer);}
				}
			}
			
		}
		
		public override void OtherEnter (Token t) {
			if (t is Unit && (t.Plane.Is(EPlane.GND) || t.Plane.Is(EPlane.AIR))) {
				Unit u = (Unit)t;
				u.timers.Add(new TExhaust(u, parent));
				if (Game.Active) {EffectQueue.Interrupt(new EIncinerate(new Source(parent), u, 5));}
			}
		}
		public override void OtherExit (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				for (int i=u.timers.Count-1; i>=0; i--) {
					Timer timer = u.timers[i];
					if (timer is TExhaust) {u.timers.Remove(timer);}
				}
			}
		}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}
}