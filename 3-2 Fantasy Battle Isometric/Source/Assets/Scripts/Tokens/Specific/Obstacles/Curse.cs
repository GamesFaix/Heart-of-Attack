
using System.Collections.Generic;

namespace HOA {
	
	public class Curse : Obstacle {
		public Curse(Source s, bool template=false){
			ID = new ID(this, EToken.CURS, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodyCurse(this);	
			Neutralize();
		}
		public override string Notes () {return 
			"Units take 2 damage upon entering "+ID.Name+"'s cell or a neighboring cell." +
			"\nUnits sharing "+ID.Name+"'s cell or in a neighboring cell take 2 damage at the end of their turn.";
		}
		
		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodyCurse)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}
	
	public class BodyCurse : Body{
		List<Sensor> sensors;
		
		public BodyCurse(Token t){
			parent = t;
		}
		
		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				newCell.Enter(parent);
				
				sensors = new List<Sensor>();
				CellGroup cells = cell.Neighbors(true);
				foreach (Cell c in cells) {
					if (!(c is ExoCell)) {
						Sensor s = new SensorCurse(parent, c);
						sensors.Add(s);
						c.AddSensor(s);
					}
				}
				return true;
			}	
			if (newCell == Game.Board.TemplateCell) {
				cell = newCell;
				return true;	
			}
			return false;
		}
		
		public override void Exit () {cell.Exit(parent);}
		
		public void DestroySensors () {
			for (int i=sensors.Count-1; i>=0; i--) {
				sensors[i].Delete();
			}
		}
	}
	
	public class SensorCurse : Sensor {
		
		public SensorCurse (Token par, Cell c) {
			parent = par;	
			cell = c;
			Enter(c);
		}
		
		public override void Enter (Cell c) {
			TokenGroup occupants = c.Occupants.OnlyType(EType.UNIT);

			foreach (Token t in occupants) {
				if (t is Unit) {
					Unit u = (Unit)t;
					u.timers.Add(new TCurse(u, parent));
				}
			}
		}
		public override void Exit () {
			TokenGroup cellUnits = cell.Occupants.OnlyType(EType.UNIT);

			foreach (Unit u in cellUnits) {
				for (int i=u.timers.Count-1; i>=0; i--) {
					Timer timer = u.timers[i];
					if (timer is TCurse) {u.timers.Remove(timer);}
				}
			}
		}
		
		public override void OtherEnter (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				u.timers.Add(new TCurse(u, parent));
				if (Game.Active) {EffectQueue.Interrupt(new EDamage(new Source(parent), u, 2));}	
			}
		}
		public override void OtherExit (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				for (int i=u.timers.Count-1; i>=0; i--) {
					Timer timer = u.timers[i];
					if (timer is TCurse) {u.timers.Remove(timer);}
				}
			}
		}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}
}