using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {
	
	public class AStrike : Task {

		public override string Desc {get {return "Do "+damage+" damage to target unit.";} }
		
		int damage;
		
		public AStrike (Unit parent, int d) {
			Parent = parent;
			Name = "Strike";

			damage = d;
			Weight = 3;
			Price = Price.Cheap;
			NewAim(HOA.Aim.Melee());
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EDamage (new Source(Parent), (Unit)targets[0], damage));
		}
	}
}
