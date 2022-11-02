
using System.Collections.Generic;

namespace HOA {
	
	public class Web : Obstacle {
		public Web(Source s, bool template=false){
			NewLabel(EToken.WEBB, s, false, template);
			sprite = new HOA.Sprite(this);
			body = new BodyWeb(this);	
			Neutralize();
		}
		public override string Notes () {return "Ground and Air units may not move through "+FullName+".\nUnits sharing "+FullName+"'s Cell have a Move Range of 1.";}
	
		public override void Die (Source s, bool corpse=true, bool log=true) {
			BodyWeb bw = (BodyWeb)body;
			bw.DestroySensors();
			
			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}
			TokenFactory.Remove(this);
			Exit();
			if (log) {GameLog.Out(s.Token+" destroyed "+this+".");}
		}
	
	}
	
	public class BodyWeb : Body{
		Sensor sensor;
		
		public BodyWeb(Token t){
			parent = t;
			SetPlane(EPlane.SUNK);
			SetClass(new List<EClass>{EClass.OB, EClass.DEST});
			OnDeath = EToken.NONE;
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

		Dictionary<Unit, Action> affected = new Dictionary<Unit, Action>();

		public SensorWeb (Token par, Cell c) {
			parent = par;	
			cell = c;
			Enter(c);
		}
		
		public override void Enter (Cell c) {
			c.SetStop(EPlane.GND, true);
			c.SetStop(EPlane.AIR, true);


			TokenGroup occupants = c.Occupants.OnlyClass(EClass.UNIT);

			foreach (Token t in occupants) {
				if (t is Unit) {
					Unit u = (Unit)t;
					for (int i=u.Arsenal().Count-1; i>=0; i--) { 
						Action a = u.Arsenal()[i];
						if (a is AMove) {
							affected.Add(u, a);

							Aim oldAim = a.Aim[0];
							Aim newAim = new Aim (oldAim.AimType, oldAim.TargetClass, oldAim.Purpose, 1);

							u.Arsenal().Add(new AMove(u, newAim));
							u.Arsenal().Remove(a);
							u.Arsenal().Sort();
							return;
						}
					}
				}
			}
		}
		public override void Exit () {
			cell.SetStop(EPlane.GND, false);
			cell.SetStop(EPlane.AIR, false);

			foreach (Unit u in affected.Keys) {
				foreach (Action a in u.Arsenal()) {
					if (a is AMove) {

						u.Arsenal().Remove(a);
						u.Arsenal().Add(affected[u]);
						u.Arsenal().Sort();
						affected.Remove(u);
					}
				}
			}
		}
		
		public override void OtherEnter (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				foreach (Action a in u.Arsenal()) {
					if (a is AMove) {
						affected.Add(u, a);
						
						Aim oldAim = a.Aim[0];
						Aim newAim = new Aim (oldAim.AimType, oldAim.TargetClass, oldAim.Purpose, 1);
						
						u.Arsenal().Add(new AMove(u, newAim));
						u.Arsenal().Remove(a);
						u.Arsenal().Sort();
						return;
					}
				}
			}
		}
		public override void OtherExit (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				foreach (Action a in u.Arsenal()) {
					if (a is AMove) {
						
						u.Arsenal().Remove(a);
						u.Arsenal().Add(affected[u]);
						u.Arsenal().Sort();
						affected.Remove(u);
					}
				}
			}
		}
		
		public override string ToString () {
			return "("+parent.ToString()+")";
		}
	}
}