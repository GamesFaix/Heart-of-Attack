using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
	public class Ice : Obstacle {
		public Ice(Source s, bool template=false){
			ID = new ID(this, EToken.ICE, s, false, template);
			Plane = Plane.Sunk;
			Body = new BodyIce(this);	
			Neutralize();
		}
		public override string Notes () {return 
			"Ground Units moving into "+ID.Name+"'s Cell have a 25% of turning "+ID.Name+" into Water.";
		}
		
		public override void Die (Source source, bool corpse=true, bool log=true) {
			((BodyIce)Body).DestroySensors();
			base.Die(source, corpse, log);
		}
	}
	
	public class BodyIce : Body{
		Sensor sensor;
		
		public BodyIce(Token t){
			parent = t;
		}
		
		public override bool Enter (Cell newCell) {

			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				newCell.Enter(parent);
				
				if (sensor != default(Sensor)) {sensor.Delete();}
				sensor = new SensorIce(parent, newCell);
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
	
	public class SensorIce : Sensor {
		
		public SensorIce (Token par, Cell c) {
			parent = par;	
			cell = c;
			Enter(c);
		}
		
		public override void Enter (Cell c) {}
		public override void Exit () {}
		
		public override void OtherEnter (Token t) {
			if (Game.Active) {
				if (t is Unit && t.Plane.Is(EPlane.GND)) {
					int random = DiceCoin.Throw(new Source(parent), EDice.D4);
					if (random == 1) {
						EffectQueue.Add(new EReplace(new Source(parent), parent, EToken.WATR));
					}
				}
			}
		}
		public override void OtherExit (Token t) {}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}
}