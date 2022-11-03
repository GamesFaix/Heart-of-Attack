using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class CarapaceInvader : Unit {
		public CarapaceInvader(Source s, bool template=false){
			id = new ID(this, EToken.CARA, s, false, template);
			//sprite = new HOA.Sprite(this);
			body = new BodyCara(this);
			plane = Plane.Gnd;

			ScaleMedium();
			NewWallet();
			
			health = new HealthCara(this, 35, 2);
			NewWatch(4);
			BuildArsenal();
		}

		protected override void BuildArsenal() {
			base.BuildArsenal();
			arsenal.Add(new Task[]{
				new AMovePath(this, 3),
				new ACaraShock(this),
				new ACaraDischarge(this)
			});
			arsenal.Sort();
		}

		public override string Notes () {return "Defense +1 per Focus. Defense can never exceed 5.  \nAll non-Carapace neighboring teammates add Carapace's Defense.";}
		
		public override void Die (Source s, bool corpse=true, bool log=true) {
			BodyCara bc = (BodyCara)body;
			bc.DestroySensors();
			GameObject.Destroy(Display.gameObject);
			if (this == GUIInspector.Inspected) {GUIInspector.Inspected = default(Token);}
			if (this == TurnQueue.Top) {TurnQueue.Advance();}
			TurnQueue.Remove((Unit)this);
			TokenFactory.Remove(this);
			Cell oldCell = Body.Cell;
			Body.Exit();
			if (corpse) {CreateRemains(oldCell);}
			if (log) {GameLog.Out(s.Token+" killed "+this+".");}
		}	
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

	public class ACaraShock : Task {

		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\nTarget is stunned for "+stun+" turns.";} }

		int damage = 10;
		int stun = 5;
		
		public ACaraShock (Unit parent) {
			Name = "Shock";
			Weight = 3;
			Price = Price.Cheap;
			AddAim(HOA.Aim.Melee());
			Parent = parent;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EShock(new Source(Parent), (Unit)targets[0], damage, stun));
		}
	}

	public class ACaraDischarge : Task {

		public override string Desc {get {return "Do "+damage+" damage to self, neighbors, and cellmates.  " +
				"\nAll damaged units are stunned for "+stun+" turns.";} }

		int damage = 10;
		int stun = 5;
		
		public ACaraDischarge (Unit parent) {
			Name = "Discharge";
			Weight = 4;
			Price = new Price(1,2);
			AddAim(HOA.Aim.Self());
			Parent = parent;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			TokenGroup cellMates = Parent.Body.Cell.Occupants;
			TokenGroup neighbors = Parent.Body.Cell.Neighbors().Occupants;
			foreach (Token t in neighbors) {cellMates.Add(t);}
			cellMates = cellMates.OnlyType(EType.UNIT);

			EffectGroup nextEffects = new EffectGroup();
			foreach (Token t in cellMates) {
				nextEffects.Add(new EShock(new Source(Parent), (Unit)t, damage, stun));
			}
			EffectQueue.Add(nextEffects);
		}
	}
}
