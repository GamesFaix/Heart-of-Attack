using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public class AAttack : Action {

		int damage;
		
		public AAttack (string n, Price p, Unit u, Aim a, int d) {
			weight = 3;
			actor = u;
			price = p;
			AddAim(a);
			damage = d;
			
			name = n;
			desc = "Do "+d+" damage to target unit.";
		}

		public override void Execute (List<ITargetable> targets) {
			Charge();
			InputBuffer.Submit(new RDamage (new Source(actor), (Unit)targets[0], damage));
		}
	}
}
