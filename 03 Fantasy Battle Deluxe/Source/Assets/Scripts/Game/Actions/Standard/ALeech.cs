using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class ALeech : Action {

		int damage;
		
		public ALeech (string n, Price p, Unit u, Aim a, int d) {
			weight = 3;
			price = p;
			AddAim(a);
			damage = d;
			actor = u;
			name = n;
			desc = "Do "+d+" damage to target unit. \nGain health equal to damage successfully dealt.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			AEffects.Leech(new Source(actor), (Unit)targets[0], damage);
			Targeter.Reset();
		}
	}
}
