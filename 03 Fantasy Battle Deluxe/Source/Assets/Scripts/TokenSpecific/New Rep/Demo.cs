using UnityEngine;

namespace HOA{
	public class Demolitia : Unit {
		public Demolitia(Source s, bool template=false){
			NewLabel(TTYPE.DEMO, s, false, template);
			BuildGround();
			
			health = new HealthDemo(this, 30);
			NewWatch(3);
			
			arsenal.Add(new AMove(this, Aim.MovePath(3)));
			arsenal.Add(new AGrenade("Grenade", Price.Cheap, this, 3, 10));
			arsenal.Sort();
		}
		public override string Notes () {return "Defense +1 per Focus (up to 4).";}
	}

	public class HealthDemo : Health{
		public HealthDemo (Unit u, int n=0, int d=0){
			parent = u; max = n; Fill(); def = d;
		}
		public override int DEF {
			get {return def + Mathf.Min(4, parent.FP);}
		}
	}
}