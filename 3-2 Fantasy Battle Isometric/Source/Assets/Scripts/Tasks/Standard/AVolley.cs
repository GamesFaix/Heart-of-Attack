using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {
	
	public class AVolley : Task {
		public override string Desc {get {return "Do "+damage+" damage to target unit.";} }

		int damage;
		
		public AVolley (Unit parent, int range, int d) {
			Parent = parent;
			Name = "Volley";
			damage = d;
			Weight = 3;
			Price = Price.Cheap;
			NewAim(HOA.Aim.Arc(range));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EDamage (new Source(Parent), (Unit)targets[0], damage));
		}
	}
}
