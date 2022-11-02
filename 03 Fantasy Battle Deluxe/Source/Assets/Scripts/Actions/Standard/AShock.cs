using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class AShock : Action {

		int damage;
		int stun;
		
		public AShock (Price p, Unit u, Aim a, int d, int st) {
			weight = 3;
			price = p;
			aim = a;
			damage = d;
			stun = st;
			actor = u;
			name = "Shock";
			desc = "Do "+d+" damage to target unit. \nTarget is stunned for "+st+" turns.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RShock(new Source(actor), default(Token), damage, stun));
			}
		}
	}
}
