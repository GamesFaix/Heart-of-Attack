using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class ADonate : Action {

		int damage;
		
		public ADonate (Price p, Unit u, Aim a, int d) {
			weight = 4;
			price = p;
			aim = a;
			damage = d;
			actor = u;
			name = "Donate Life";
			desc = "Heal target unit "+d+" HP. \n"+actor+" takes damage equal to HP successfully gained.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDonate(new Source(actor), default(Token), damage));
			}
		}
	}
}
