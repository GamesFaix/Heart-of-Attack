using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public class AAttackFir : Action {

		int damage;
		
		public AAttackFir (string n, Price p, Unit u, Aim a, int d) {
			weight = 3;
			price = p;
			AddAim(a);
			damage = d;
			actor = u;
			name = n;
			desc = "Do "+d+" damage to target unit. \nTarget's neighbors and cellmates take 50% damage (rounded down).  \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			InputBuffer.Submit(new RDamageFir(new Source(actor), (Token)targets[0], damage));
		}
	}
}
