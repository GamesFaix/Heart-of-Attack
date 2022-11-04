using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {

	public class HealthCaraShield : Health {
		Unit carapace;
		Sensor sensor;
		public Health Original {get; protected set;}
		HealthCaraShield OverShield {get; set;}
		
		public HealthCaraShield (Sensor s, Unit parent) : base (parent) {
			sensor = s;
			carapace = (Unit)sensor.Parent;
			Original = parent.Health;
			if (Original is HealthCaraShield) {
				((HealthCaraShield)Original).OverShield = this;
			}
		}

		public void Remove () {
			if (OverShield == null) {((Unit)Parent).Health = Original;}
			else {OverShield.Original = Original;}
		}
		
		public override Stat DEF {
			get {
				checked {
					byte d = (byte)(Original.DEF);
					if (Parent.ID.Species != Species.Carapace) {d += (byte)(carapace.DEF);}
					return Stat.DefenseBonus((Unit)Parent, d);
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
