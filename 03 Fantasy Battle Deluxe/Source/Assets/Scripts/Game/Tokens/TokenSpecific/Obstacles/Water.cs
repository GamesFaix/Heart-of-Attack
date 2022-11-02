
using System.Collections.Generic;

namespace HOA {

	public class Water : Obstacle {
		public Water(Source s, bool template=false){
			NewLabel(EToken.WATR, s, false, template);
			sprite = new HOA.Sprite(this);
			body = new BodyWater(this);	
			Neutralize();
		}
		public override string Notes () {return "Ground units may not move through "+FullName+".\nGround Units sharing "+FullName+"'s Cell take 5 damage at the end of their turn.";}
	}

	public class BodyWater : Body{
		Sensor sensor;
		
		public BodyWater(Token t){
			parent = t;
			SetPlane(EPlane.SUNK);
			SetClass(EClass.OB);
			OnDeath = EToken.NONE;
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
			if (newCell == TemplateFactory.c) {
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
			TokenGroup occupants = c.Occupants.OnlyClass(EClass.UNIT);
			occupants = occupants.OnlyPlane(EPlane.GND);

			foreach (Token t in occupants) {
				if (t is Unit) {
					Unit u = (Unit)t;
					u.timers.Add(new TWater(u, parent));
				}
			}
		}
		public override void Exit () {
			TokenGroup cellUnits = cell.Occupants.OnlyClass(EClass.UNIT);
			cellUnits = cellUnits.OnlyPlane(EPlane.GND);
			
			foreach (Unit u in cellUnits) {
				for (int i=u.timers.Count-1; i>=0; i--) {
					Timer timer = u.timers[i];
					if (timer is TWater) {u.timers.Remove(timer);}
				}
			}
		
		}
		
		public override void OtherEnter (Token t) {
			if (t is Unit && t.IsPlane(EPlane.GND)) {
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

	public class TWater : Timer {
		
		Token source;
		
		public TWater (Unit par, Token s) {
			parent = par;
			source = s;
			turns = 1;
			
			name = "Waterlogged";
			desc = "Do 5 damage to "+parent.ToString()+" at the end of its turn if sharing cell with "+source.ToString()+".";		
		}
		
		public override void Activate () {
			InputBuffer.Submit(new RDamage(new Source(source), parent, 5));
			turns++;
		}
	}




}