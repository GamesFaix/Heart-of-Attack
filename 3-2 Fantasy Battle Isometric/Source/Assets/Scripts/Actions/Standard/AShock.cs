using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class AShock : Action {

		int damage;
		int stun;
		
		public AShock (Price p, Unit u, Aim a, int d, int st) {
			weight = 3;
			price = p;
			AddAim(a);
			damage = d;
			stun = st;
			actor = u;
			name = "Shock";
			desc = "Do "+d+" damage to target unit. \nTarget is stunned for "+st+" turns.";
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			EffectQueue.Add(new EShock(new Source(actor), (Unit)targets[0], damage, stun));
			Targeter.Reset();
		}
	}
}
