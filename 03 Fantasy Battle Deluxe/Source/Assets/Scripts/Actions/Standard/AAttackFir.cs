using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class AAttackFir : Action {

		int damage;
		
		public AAttackFir (string n, Price p, Unit u, Aim a, int d) {
			weight = 3;
			price = p;
			aim = a;
			damage = d;
			actor = u;
			name = n;
			desc = "Do "+d+" damage to target unit. \nTarget's neighbors and cellmates take 50% damage (rounded down).  \nDestroy all destructible tokens that would take damage.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDamageFir(new Source(actor), default(Token), damage));
			}
		}
	}
}
