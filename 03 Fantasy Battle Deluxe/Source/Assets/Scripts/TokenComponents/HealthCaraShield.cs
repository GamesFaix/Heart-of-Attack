using System;
using HOA.Players;
using UnityEngine;

namespace HOA.Tokens.Components {
	
	public class HealthCaraShield : Health {
		SensorCaraShield sensor;
		Health original;
		HealthCaraShield wrapper;
		
		public HealthCaraShield (SensorCaraShield s, Unit u){
			parent = u;
			sensor = s;
			Wrap(u.health);
			if (original is HealthCaraShield) {
				HealthCaraShield hcs = (HealthCaraShield)original;
				hcs.WrapWith(this);
			}
		}
		
		
		void Wrap (Health h) {original = h;}
		
		public void WrapWith (HealthCaraShield nextWrapper) {wrapper = nextWrapper;}
		bool Wrapped () {if (wrapper != default(HealthCaraShield)) {return true;}
			return false;
		}
		
		public Health Original () {return original;}
		
		
		public void Remove () {
			if (!Wrapped()) {
				parent.health = original;
			}
			
			else {
				wrapper.Wrap(original);	
			}
		}
		
		public override int DEF(){
			int d = original.DEF() + sensor.Parent().DEF();
			if (parent.Code() == TTYPE.CARA) {d = Mathf.Min(d, 5);}
			return d;
		}
		
		public override int HP(){return original.HP();}
		public override int MaxHP(){return original.MaxHP();}

		public override string HPString(){return original.HPString();}
		public override string DEFString(){return "("+DEF()+")";}

		public override void Fill(){original.Fill();}

		public override int SetHP (Source s, int n, bool log=true){
			return original.SetHP(s,n,log);
		}
		public override int SetMaxHP (Source s, int n, bool log=true){
			return original.SetMaxHP(s, n, log);
		}
		public override int AddHP (Source s, int n, bool log=true){
			return original.AddHP(s,n,log);
		}
		public override int AddMaxHP (Source s, int n, bool log=true){
			return original.AddMaxHP (s, n, log);
		}

		public override int MultHP (Source s, float f, bool log=true){
			return original.MultHP (s, f, log);
		}
		public override int MultMaxHP (Source s, float f, bool log=true){
			return original.MultMaxHP (s, f, log);
		}
		public override int SetDEF (Source s, int n, bool log=true){
			return original.SetDEF(s, n, log);
		}
		public override int AddDEF (Source s, int n, bool log=true){
			return original.AddDEF(s, n, log);
		}

		public override int Damage(Source s, int n, bool log=true){
			return original.Damage(s, n, log);
		}
	}
}
