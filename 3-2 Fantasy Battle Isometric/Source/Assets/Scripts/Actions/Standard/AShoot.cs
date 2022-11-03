using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {
	
	public class AShoot : Task {

		public override string Desc {get {return "Do "+damage+" damage to target unit.";} }
		
		int damage;
		
		public AShoot (Unit parent, int range, int d) {
			Parent = parent;
			Name = "Shoot";
			damage = d;
			Weight = 3;
			Price = Price.Cheap;
			AddAim(HOA.Aim.Shoot(range));
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new EDamage (new Source(Parent), (Unit)targets[0], damage));
		}
	}
}
