using System;
using HOA.Players;
using UnityEngine;

namespace HOA.Tokens.Components {
	
	public class HealthCara : Health{
		public HealthCara (Unit u, int n=0, int d=0){
			parent = u; max = n; Fill(); def = d;
		}
		public override int DEF {
			get {return Mathf.Min(def+parent.FP, 5);}
		}
	}
	
	public class HealthDemo : Health{
		public HealthDemo (Unit u, int n=0, int d=0){
			parent = u; max = n; Fill(); def = d;
		}
		public override int DEF {
			get {return def + Mathf.Min(4, parent.FP);}
		}
	}
	
	public class HealthPano : Health{
		public HealthPano (Unit u, int n=0, int d=0){
			parent = u; max = n; Fill(); def = d;
		}
		public override int DEF {
			get {return def + Mathf.Min(2, parent.FP);}
		}
	}
}
