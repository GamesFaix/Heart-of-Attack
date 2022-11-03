using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class CarapaceInvader : Unit {
		public CarapaceInvader(Source s, bool template=false){
			ID = new ID(this, EToken.CARA, s, false, template);
			Plane = Plane.Gnd;
			Body = new BodyCara(this);
			ScaleMedium();
			Health = new HealthDEFCap(this, 35, 2, 5);
			NewWatch(4);
			Wallet = new DEFWallet (this, 2, 3);
			BuildArsenal();
		}

		protected override void BuildArsenal() {
			base.BuildArsenal();
			Arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new ACaraShock(this),
				new ACaraDischarge(this)
			});
			Arsenal.Sort();
		}

		public override string Notes () {return "All non-Carapace neighboring teammates add Carapace's Defense.";}
		
		public override void Die (Source s, bool corpse=true, bool log=true) {
			//BodyCara bc = (BodyCara)Body;
			((BodyCara)Body).DestroySensors();
			base.Die(s,corpse,log);
/*
			GameObject.Destroy(Display.gameObject);
			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}
			if (this == TurnQueue.Top) {TurnQueue.Advance();}
			TurnQueue.Remove((Unit)this);
			TokenFactory.Remove(this);
			Cell oldCell = Body.Cell;
			Body.Exit();
			if (corpse) {CreateRemains(oldCell);}
			if (log) {GameLog.Out(s.Token+" killed "+this+".");}
*/		}	
	}

	public class BodyCara : Body{
		List<Sensor> sensors;
		
		public BodyCara(Token t){
			parent = t;
			sensors = new List<Sensor>();
		}
		
		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				if (CanTrample(newCell)) {Trample(newCell);}
				newCell.Enter(parent);
				if (parent.Display != null) {((TokenDisplay)parent.Display).MoveTo(cell);}
				foreach (Sensor s in sensors) {s.Delete();}
				
				CellGroup shieldCells = newCell.Neighbors(true);
				foreach (Cell c in shieldCells) {
					Sensor s = new SensorCaraShield((Unit)parent, c);
					sensors.Add(s);
					c.AddSensor(s);
				}
				
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
		
		public void DestroySensors () {
			foreach (Sensor s in sensors) {s.Delete();}
		}
	}
}
