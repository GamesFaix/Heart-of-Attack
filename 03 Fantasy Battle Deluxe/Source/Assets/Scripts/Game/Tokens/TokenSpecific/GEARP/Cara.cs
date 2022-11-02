using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class CarapaceInvader : Unit {
		public CarapaceInvader(Source s, bool template=false){
			NewLabel(EToken.CARA, s, false, template);
			sprite = new HOA.Sprite(this);
			body = new BodyCara(this);
			
			NewWallet();
			
			health = new HealthCara(this, 35, 2);
			NewWatch(4);
			
			NewArsenal();
			arsenal.Add(new AFocus(this)); 
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new AShock(Price.Cheap, this, Aim.Melee(), 10, 5));
			arsenal.Add(new ACaraDischarge(new Price(1,2), this, 10, 5));
			arsenal.Sort();
		}
		public override string Notes () {return "Defense +1 per Focus. Defense can never exceed 5.  \nAll non-Carapace neighboring teammates add Carapace's Defense.";}
		
		public override void Die (Source s, bool corpse=true, bool log=true) {
			BodyCara bc = (BodyCara)body;
			bc.DestroySensors();
			
			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}
			if (this == TurnQueue.Top) {TurnQueue.Advance();}
			TurnQueue.Remove((Unit)this);
			TokenFactory.Remove(this);
			Cell oldCell = Cell;
			Exit();
			if (corpse) {CreateRemains(oldCell);}
			if (log) {GameLog.Out(s.Token+" killed "+this+".");}
		}	
	}

	public class BodyCara : Body{
		List<Sensor> sensors;
		
		public BodyCara(Token t){
			parent = t;
			SetPlane(EPlane.GND);
			SetClass(EClass.UNIT);
			OnDeath = EToken.CORP;
			sensors = new List<Sensor>();
		}
		
		public override bool Enter (Cell newCell) {
			if (CanEnter(newCell)) {
				if (cell != default(Cell)) {Exit();}
				cell = newCell;
				if (CanTrample(newCell)) {Trample(newCell);}
				newCell.Enter(parent);
				
				foreach (Sensor s in sensors) {s.Delete();}
				
				CellGroup shieldCells = newCell.Neighbors(true);
				foreach (Cell c in shieldCells) {
					Sensor s = new SensorCaraShield((Unit)parent, c);
					sensors.Add(s);
					c.AddSensor(s);
				}
				
				return true;
			}	
			if (newCell == TemplateFactory.c) {
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

	public class HealthCara : Health{
		public HealthCara (Unit u, int n=0, int d=0){
			parent = u; max = n; Fill(); def = d;
		}
		public override int DEF {
			get {return Mathf.Min(def+parent.FP, 5);}
		}
	}

	public class ACaraDischarge : Action {
		int damage;
		int stun;
		
		public ACaraDischarge (Price p, Unit u, int d, int st) {
			weight = 4;
			
			price = p;
			AddAim(HOA.Aim.Self);
			actor = u;
			
			damage = d;
			stun = st;
			
			name = "Discharge";
			desc = "Do "+d+" damage to self, neighbors, and cellmates.  \nAll damaged units are stunned for "+st+" turns.";
			
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();

			TokenGroup cellMates = actor.Cell.Occupants;
			TokenGroup neighbors = actor.Cell.Neighbors().Occupants;
			foreach (Token t in neighbors) {cellMates.Add(t);}
			cellMates = cellMates.OnlyClass(EClass.UNIT);
			foreach (Token t in cellMates) {
				AEffects.Shock(new Source(actor), (Unit)t, damage, stun);
			}
			Targeter.Reset();
		}
	}
}
