using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class SensorCaraShield : Sensor {
		Dictionary<Unit, HealthCaraShield> shields;
		
		public SensorCaraShield (Unit par, Cell c) {
			parent = par;	
			cell = c;
			shields = new Dictionary<Unit, HealthCaraShield>();
			Enter(c);
			
		}
		
		public override void Enter (Cell c) {
			cell = c;
			TokenGroup cellUnits = cell.Occupants.OnlyType(EType.UNIT);
			foreach (Unit u in cellUnits) {
				if (u.ID.Code != EToken.CARA 
				    && u.Owner == parent.Owner) {
					HealthCaraShield shield = new HealthCaraShield(this, u);
					u.SetHealth(shield);
					shields.Add(u, shield);
				}
			}
		}
		public override void Exit () {
			foreach (HealthCaraShield hcs in shields.Values) {
				hcs.Remove();
			}
			shields = new Dictionary<Unit, HealthCaraShield>();
		}
		
		public override void OtherEnter (Token t) {
			if (t is Unit 
			    && t.ID.Code != EToken.CARA
			    && t.Owner == parent.Owner) {
				Unit u = (Unit)t;
				HealthCaraShield shield = new HealthCaraShield(this, u);
				u.SetHealth(shield);
				shields.Add(u, shield);
			}
		}
		public override void OtherExit (Token t) {
			if (t is Unit) {
				Unit u = (Unit)t;
				if (shields.ContainsKey(u)) {
					shields[u].Remove();
					shields.Remove(u);
				}
			}
		}
		
		public override string ToString () {return "Shield ("+parent.ToString()+")";}
	}

	public class HealthCaraShield : Health {
		Unit carapace;
		SensorCaraShield sensor;
		public Health Original {get; protected set;}
		HealthCaraShield OverShield {get; set;}
		
		public HealthCaraShield (SensorCaraShield s, Unit u){
			parent = u;
			sensor = s;
			carapace = (Unit)sensor.Parent;
			Original = u.Health;
			if (Original is HealthCaraShield) {
				((HealthCaraShield)Original).OverShield = this;
			}
		}

		public void Remove () {
			if (OverShield == null) {parent.SetHealth(Original);}
			else {OverShield.Original = Original;}
		}
		
		public override Stat DEF {
			get {
				checked {
					byte d = (byte)(Original.DEF);
					if (parent.ID.Code != EToken.CARA) {d += (byte)(carapace.DEF);}
					return Stat.DEFBonus(parent, d);
				}
			}
//			protected set {Original.DEF = value;}
		}
		
		public override Stat HP {
			get {return Original.HP;} 
			//protected set {Original.HP = value;}
		}

		public override int AddHP (Source s, int n, bool log=true){
			return Original.AddHP(s,n,log);
		}
		public override int AddMaxHP (Source s, byte n, bool log=true){
			return Original.AddMaxHP (s, n, log);
		}

		public override bool Damage(Source s, int n, bool log=true) {return Original.Damage(s, n, log);}
	}
}
