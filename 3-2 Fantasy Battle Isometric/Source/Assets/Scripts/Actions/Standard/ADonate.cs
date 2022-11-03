using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public class ADonate : Action {

		int damage;
		
		public ADonate (Price p, Unit u, Aim a, int d) {
			weight = 4;
			price = p;
			AddAim(a);
			damage = d;
			actor = u;
			name = "Donate Life";
			desc = "Target unit gains "+d+" health. \n"+actor+" takes damage equal to health successfully gained.";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
		
			EffectQueue.Add(new EDonate(new Source(actor), (Unit)targets[0], damage));
			Targeter.Reset();
		}
	}
}
