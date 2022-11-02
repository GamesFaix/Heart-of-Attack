using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class ARage : Action {
		int damage;
		
		public ARage (Price p, Unit u, Aim a, int d) {
			price = p;
			aim = a;
			damage = d;
			actor = u;
			
			name = "Rage";
			desc = "Do "+d+" damage to target unit. \n"+actor+" takes 50% damage (rounded down).";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RRage(new Source(actor), default(Token), damage));
			}
		}
	}
}
