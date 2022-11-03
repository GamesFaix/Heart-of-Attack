using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class ARage : Action {
		int damage;
		
		public ARage (Price p, Unit u, Aim a, int d) {
			weight = 3;
			
			price = p;
			AddAim(a);
			damage = d;
			actor = u;
			
			name = "Rage";
			desc = "Do "+d+" damage to target unit. \n"+actor+" takes 50% damage (rounded down).";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			EffectQueue.Add(new ERage(new Source(actor), (Unit)targets[0], damage));
			Targeter.Reset();
		}
	}
}
