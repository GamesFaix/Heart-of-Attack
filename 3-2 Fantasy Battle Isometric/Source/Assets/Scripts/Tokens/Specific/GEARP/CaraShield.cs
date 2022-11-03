using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOA {
	
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
		
		public override int DEF {
			get {
				int d = original.DEF + ((Unit)sensor.Parent).DEF;
				if (parent.ID.Code == EToken.CARA) {d = Mathf.Min(d, 5);}
				return d;
			}
			set {original.DEF = value;}
		}
		
		public override int HP {
			get {return original.HP;} 
			set {original.HP = value;}
		}
		public override int MaxHP {
			get {return original.MaxHP;} 
			set {original.MaxHP = value;}
		}
		
		public override string HPString {get {return original.HPString;} }
		public override string DEFString {get {return "("+DEF+")";} }
		
		public override void Fill(){original.Fill();}
		
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
		
		public override int AddDEF (Source s, int n, bool log=true){
			return original.AddDEF(s, n, log);
		}
		
		public override int Damage(Source s, int n, bool log=true){
			return original.Damage(s, n, log);
		}
	}

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
					u.health = shield;
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
				u.health = shield;
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

		public override string ToString () {
			return "Shield ("+parent.ToString()+")";
		}
	}
}
