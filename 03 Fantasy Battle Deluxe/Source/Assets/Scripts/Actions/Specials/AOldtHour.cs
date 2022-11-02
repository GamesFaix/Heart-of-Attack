using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AOldtHour : Action {
		
		public AOldtHour (Price p, Unit u) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.GLOBAL, TARGET.TOKEN, TTAR.NA);

			name = "Hour Saviour";
			desc = "All teammates shift up one slot in the Queue.";
		}
		
		public override void Perform () {
			if (Charge()) {
				TokenGroup team = actor.Owner.OwnedUnits;
				team.Remove(actor);
				foreach (Token t in team) {
					InputBuffer.Submit(new RQueueShift(new Source(actor), t, 1));
				}
			}
		}
	}
}
