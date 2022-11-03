using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class TimeWell : Obstacle {
		List<Unit> affected;
		public List<Unit> Affected {get {return affected;} }
		
		public TimeWell(Source s, bool template=false){
			ID = new ID(this, EToken.TWEL, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodyTimeWell(this);	
			Neutralize();
			affected = new List<Unit>();
		}
		public override string Notes () {return 
			"Units sharing "+ID.Name+"'s Cell have +2 Initiative.";
		}
		
		public override void Die (Source s, bool corpse=true, bool log=true) {
			((BodyTimeWell)Body).DestroySensors();
			base.Die(s,corpse,log);
		}
		
	}
	
	public class BodyTimeWell : Body{
		Sensor sensor;
		
		public BodyTimeWell(Token t){
			parent = t;
		}
		
		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				newCell.Enter(parent);
				
				if (sensor != default(Sensor)) {sensor.Delete();}
				sensor = new SensorTimeWell(parent, newCell);
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
	
	public class SensorTimeWell : Sensor {
		
		TimeWell timeWell;
		
		public SensorTimeWell (Token par, Cell c) {
			parent = par;
			timeWell = (TimeWell)par;
			cell = c;
			Enter(c);
		}
		
		public override void Enter (Cell c) {
			
			foreach (Token t in c.Occupants) {
				if (t is Unit) {
					timeWell.Affected.Add((Unit)t);
					EffectQueue.Add(new EAddStat(new Source(parent), (Unit)t, EStat.IN, 2));
				}
			}
		}
		public override void Exit () {
			
			for (int i=timeWell.Affected.Count-1; i>=0; i--) {
				Unit u = timeWell.Affected[i];
				EffectQueue.Add(new EAddStat(new Source(parent), u, EStat.IN, -2));
				timeWell.Affected.Remove(u);
			}
		}
		
		public override void OtherEnter (Token t) {
			if (t is Unit) {
				timeWell.Affected.Add((Unit)t);
				EffectQueue.Add(new EAddStat(new Source(parent), (Unit)t, EStat.IN, 2));
			}
		}
		public override void OtherExit (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				EffectQueue.Add(new EAddStat(new Source(parent), u, EStat.IN, -2));
				timeWell.Affected.Remove(u);
			}
		}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}
}