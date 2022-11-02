using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class ACaraDischarge : Action {
		int damage;
		int stun;
		
		public ACaraDischarge (Price p, Unit u, int d, int st) {
			price = p;
			aim = new Aim (AIMTYPE.SELF, TARGET.SELF, TTAR.NA);
			actor = u;

			damage = d;
			stun = st;

			name = "Discharge";
			desc = "Do "+d+" damage to self, neighbors, and cellmates.  \nAll damaged units are stunned for "+st+" turns.";

		}
		
		public override void Perform () {
			if (Charge()) {
				TokenGroup cellMates = actor.Cell.Occupants;
				TokenGroup neighbors = actor.Cell.Neighbors().Occupants;
				foreach (Token t in neighbors) {cellMates.Add(t);}
				TokenGroup targets = cellMates.FilterUnit;
				foreach (Token t in targets) {
					InputBuffer.Submit(new RShock(new Source(actor), t, damage, stun));
				}
			}
		}
	}
}
