using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class ABeesDeathSting : Action {
		int range;
		int damage;
		
		public ABeesDeathSting (Price p, Unit u, Aim a, int d) {
			weight = 4;
			price = p;
			actor = u;
			aim = a;
			damage = d;
			int cor = (int)Mathf.Floor(d*0.5f);
			name = "Fatal Blow";
			desc = "Destroy "+actor+".\nDo "+d+" damage to target unit. \nTarget takes "+cor+" corrosion counters. \n(If a unit has corrosion counters, at the beginning of its turn it takes damage equal to the number of counters, then removes half the counters (rounded up).)";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RDeathSting(new Source(actor), default(Token), damage));
			}
		}
	}
}
