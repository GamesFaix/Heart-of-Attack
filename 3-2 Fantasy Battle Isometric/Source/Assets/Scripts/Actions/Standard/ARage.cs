using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class ARage : Task {

		public override string Desc {get {return "Do "+damage+" damage to target unit. " +
				"\n"+Parent+" takes 50% damage (rounded down)."; } }
		
		int damage;
		
		public ARage (Unit parent, int d) {
			Parent = parent;
			Name = "Rage";
			Weight = 3;
			Price = Price.Cheap;
			AddAim(HOA.Aim.Melee());
			damage = d;
		}
		
		protected override void ExecuteMain (TargetGroup targets) {
			EffectQueue.Add(new ERage(new Source(Parent), (Unit)targets[0], damage));
		}
	}
}
