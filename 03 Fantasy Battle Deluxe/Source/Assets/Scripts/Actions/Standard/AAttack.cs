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
			aim = a;
			damage = d;
			
			name = n;
			desc = "Do "+d+" damage to target unit.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDamage(new Source(actor), default(Token), damage));
			}
		}
	}
}
