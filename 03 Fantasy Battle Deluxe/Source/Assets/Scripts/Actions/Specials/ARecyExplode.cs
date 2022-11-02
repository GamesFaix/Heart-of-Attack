using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class ARecyExplode : Action {
		int damage;
		
		public ARecyExplode (Price p, Unit u, int d) {
			weight = 4;
			price = p;
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, TTAR.NA);
			actor = u;

			damage = d;
			int cor = (int)Mathf.Floor(d*0.5f);
			name = "Burst";
			desc = "Destroy "+actor+".\nDo "+d+" damage to cellmates and neighbors. \nDamaged units take "+cor+" corrosion counters. \n(If a unit has corrosion counters, at the beginning of its turn it takes damage equal to the number of counters, then removes half the counters (rounded up).)";
		}
		
		public override void Perform () {
			if (Charge()) {
				TokenGroup victims = actor.Neighbors(true);
				foreach (Token t in victims) {
					InputBuffer.Submit(new RCorrode(new Source(actor), t, damage));	
				}
				actor.Die(new Source(actor));
			}
		}
	}
}
