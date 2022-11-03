using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class Web : Obstacle {
		Dictionary<Unit, Action> affected;
		public Dictionary<Unit, Action> Affected {get {return affected;} }

		public Web(Source s, bool template=false){
			id = new ID(this, EToken.WEBB, s, false, template);
			plane = Plane.Sunk;
			type.Add(EType.DEST);
			body = new BodyWeb(this);	
			Neutralize();
			affected = new Dictionary<Unit, Action>();
		}
		public override string Notes () {return "Ground and Air units may not move through "+id.Name+".\nUnits sharing "+id.Name+"'s Cell have a Move Range of 1.";}
	
		public override void Die (Source s, bool corpse=true, bool log=true) {
			BodyWeb bw = (BodyWeb)body;
			bw.DestroySensors();
			
			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}
			TokenFactory.Remove(this);
			Body.Exit();
			if (log) {GameLog.Out(s.Token+" destroyed "+this+".");}
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


		public SensorWeb (Token par, Cell c) {
			parent = par;	
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

			foreach (Unit u in ((Web)parent).Affected.Keys) {
				foreach (Action a in u.Arsenal()) {
					if (a is AMove) {

						u.Arsenal().Remove(a);
						u.Arsenal().Add(((Web)parent).Affected[u]);
						u.Arsenal().Sort();
						((Web)parent).Affected.Remove(u);
					}
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
				foreach (Action a in u.Arsenal()) {
					if (a is AMove) {
						
						u.Arsenal().Remove(a);
						u.Arsenal().Add(((Web)parent).Affected[u]);
						u.Arsenal().Sort();
						((Web)parent).Affected.Remove(u);
					}
				}
			}
		}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}

	public class EStick : Effect {
		public override string ToString () {return "Effect - Stick";}
		Unit target;
		
		public EStick (Source s, Unit u) {
			source = s; target = u;
		}
		public override void Process() {
			Debug.Log("sticking "+target);
			for(int i=target.Arsenal().Count-1; i>=0; i--) {
				Action a = target.Arsenal()[i];
				if (a is AMove) {
					//Debug.Log(target+" has move");
					((Web)source.Token).Affected.Add(target, a);
					
					Aim oldAim = a.Aim[0];
					Aim newAim = new Aim (oldAim.Trajectory, oldAim.Type, oldAim.Purpose, 1);
					
					target.Arsenal().Add(new AMove(target, newAim));
					target.Arsenal().Remove(a);
					target.Arsenal().Sort();

					Mixer.Play(SoundLoader.Effect(EEffect.STICK));
					target.Display.Effect(EEffect.STICK);
					return;
				}
			}
		}
	}

}