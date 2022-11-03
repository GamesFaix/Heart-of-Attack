using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class TimeSink : Obstacle {
		List<Unit> affected;
		public List<Unit> Affected {get {return affected;} }
		
		public TimeSink(Source s, bool template=false){
			ID = new ID(this, EToken.TSNK, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodyTimeSink(this);	
			Neutralize();
			affected = new List<Unit>();
		}
		public override string Notes () {return 
			"Units sharing "+ID.Name+"'s Cell have -2 Initiative.";
		}
		
		public override void Die (Source s, bool corpse=true, bool log=true) {
			((BodyTimeSink)Body).DestroySensors();
			base.Die(s,corpse,log);
		}
		
	}
	
	public class BodyTimeSink : Body{
		Sensor sensor;
		
		public BodyTimeSink(Token t){
			parent = t;
		}
		
		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				newCell.Enter(parent);
				
				if (sensor != default(Sensor)) {sensor.Delete();}
				sensor = new SensorTimeSink(parent, newCell);
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
	
	public class SensorTimeSink : Sensor {
		
		TimeSink timeSink;
		
		public SensorTimeSink (Token par, Cell c) {
			parent = par;
			timeSink = (TimeSink)par;
			cell = c;
			Enter(c);
		}
		
		public override void Enter (Cell c) {

			foreach (Token t in c.Occupants) {
				if (t is Unit) {
					timeSink.Affected.Add((Unit)t);
					EffectQueue.Add(new EAddStat(new Source(parent), (Unit)t, EStat.IN, -2));
				}
			}
		}
		public override void Exit () {

			for (int i=timeSink.Affected.Count-1; i>=0; i--) {
				Unit u = timeSink.Affected[i];
				EffectQueue.Add(new EAddStat(new Source(parent), u, EStat.IN, 2));
				timeSink.Affected.Remove(u);
			}
		}
		
		public override void OtherEnter (Token t) {
			if (t is Unit) {
				timeSink.Affected.Add((Unit)t);
				EffectQueue.Add(new EAddStat(new Source(parent), (Unit)t, EStat.IN, -2));
			}
		}
		public override void OtherExit (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				EffectQueue.Add(new EAddStat(new Source(parent), u, EStat.IN, 2));
				timeSink.Affected.Remove(u);
			}
		}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}
}