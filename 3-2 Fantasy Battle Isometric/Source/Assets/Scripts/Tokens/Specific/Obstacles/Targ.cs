
using System.Collections.Generic;

namespace HOA {
	
	public class Targ : Obstacle {
		public Targ(Source s, bool template=false){
			ID = new ID(this, EToken.TARG, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodyTarg(this);	
			Neutralize();
		}
		public override string Notes () {return 
			"\nIf any unit shares "+ID.Name+"'s Cell, " +
			"\n10 explosive damage is dealt in "+ID.Name+"'s cell at the end of that unit's turn.";
		}
		
		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodyTarg)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}
	
	public class BodyTarg : Body{
		Sensor sensor;
		
		public BodyTarg(Token t){
			parent = t;
		}
		
		public override bool Enter (Cell newCell) {

			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				newCell.Enter(parent);
				
				if (sensor != default(Sensor)) {sensor.Delete();}
				sensor = new SensorTarg(parent, newCell);
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
			cell.Exit(parent);
		}
		
		public void DestroySensors () {sensor.Delete();}
	}
	
	public class SensorTarg : Sensor {
		
		public SensorTarg (Token par, Cell c) {
			parent = par;	
			cell = c;
			Enter(c);
		}
		
		public override void Enter (Cell c) {
			TokenGroup occupants = c.Occupants.OnlyType(EType.UNIT);

			foreach (Token t in occupants) {
				if (t is Unit) {
					Unit u = (Unit)t;
					u.timers.Add(new TTarg(u, parent));
				}
			}
		}
		public override void Exit () {
			TokenGroup cellUnits = cell.Occupants.OnlyType(EType.UNIT);

			foreach (Unit u in cellUnits) {
				for (int i=u.timers.Count-1; i>=0; i--) {
					Timer timer = u.timers[i];
					if (timer is TTarg) {u.timers.Remove(timer);}
				}
			}
			
		}
		
		public override void OtherEnter (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				u.timers.Add(new TTarg(u, parent));
			}
		}
		public override void OtherExit (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				for (int i=u.timers.Count-1; i>=0; i--) {
					Timer timer = u.timers[i];
					if (timer is TTarg) {u.timers.Remove(timer);}
				}
			}
		}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}
}