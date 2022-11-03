using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class Web : Obstacle {
		Dictionary<Unit, int> affected;
		public Dictionary<Unit, int> Affected {get {return affected;} }

		public Web(Source s, bool template=false){
			ID = new ID(this, EToken.WEBB, s, false, template);
			Plane = Plane.Sunk;
			Special.Add(EType.DEST);
			Body = new BodyWeb(this);	
			Neutralize();
			affected = new Dictionary<Unit, int>();
		}
		public override string Notes () {return 
			"Ground and Air units may not move through "+ID.Name+"." +
			"\nUnits sharing "+ID.Name+"'s Cell have a Move Range of 1.";
		}
	
		public override void Die (Source s, bool corpse=true, bool log=true) {
			((BodyWeb)Body).DestroySensors();
			base.Die(s,corpse,log);
		}
	
	}
	
	public class BodyWeb : Body{
		Sensor sensor;
		
		public BodyWeb(Token t){
			parent = t;
		}
		
		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				newCell.Enter(parent);
				
				if (sensor != default(Sensor)) {sensor.Delete();}
				sensor = new SensorWeb(parent, newCell);
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
	
	public class SensorWeb : Sensor {

		Web web;

		public SensorWeb (Token par, Cell c) {
			parent = par;
			web = (Web)par;
			cell = c;
			Enter(c);
		}
		
		public override void Enter (Cell c) {
			c.SetStop(EPlane.GND, true);
			c.SetStop(EPlane.AIR, true);

			foreach (Token t in c.Occupants) {
				if (t is Unit) {
					EffectQueue.Add(new EStick(new Source(parent), (Unit)t));
				}
			}
		}
		public override void Exit () {
			cell.SetStop(EPlane.GND, false);
			cell.SetStop(EPlane.AIR, false);

			foreach (Unit u in web.Affected.Keys) {
				Task move = u.Arsenal.Move;
				if (move != default(Task)) {
					move.Aim[0].Range = web.Affected[u];
					web.Affected.Remove(u);
				}
			}
		}
		
		public override void OtherEnter (Token t) {
			if (t is Unit) {
				EffectQueue.Add(new EStick(new Source(parent), (Unit)t));
			}
		}
		public override void OtherExit (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				Task move = u.Arsenal.Move;
				if (move != default(Task)) {
					move.Aim[0].Range = web.Affected[u];
					web.Affected.Remove(u);
				}
			}
		}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}

	public class EStick : Effect {
		public override string ToString () {return "Effect - Stick";}
		Web parent;
		Unit target;
		
		public EStick (Source s, Unit u) {
			source = s; target = u;
			parent = (Web)source.Token; 
		}
		public override void Process() {
			Task move = target.Arsenal.Move;
			if (move != default(Task)) {
				parent.Affected.Add(target, move.Aim[0].Range);
				move.Aim[0].Range = 1;
				Mixer.Play(SoundLoader.Effect(EEffect.STICK));
				target.Display.Effect(EEffect.STICK);
			}
		}
	}
}