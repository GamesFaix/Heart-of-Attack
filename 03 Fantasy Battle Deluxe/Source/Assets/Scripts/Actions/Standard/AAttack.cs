using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class AAttack : Action {

		int damage;
		
		public AAttack (Price p, Unit u, Aim a, int d) {
			weight = 3;
			actor = u;
			price = p;
			aim = a;
			damage = d;
			
			name = "Attack";
			desc = "Do "+d+" damage to a single unit.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDamage(new Source(actor), default(Token), damage));
			}
		}
	}
}
