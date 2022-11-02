using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AGargTailWhip : Action {
		int damage;
		
		public AGargTailWhip (Price p, Unit u, int d) {
			weight = 4;
			
			price = p;
			actor = u;
			aim = new Aim(AIMTYPE.SELF, TARGET.SELF, TTAR.NA);
			damage = d;
			
			name = "Tail Whip";
			desc = "Do "+d+" damage to all neighboring units.";
		}
		
		public override void Perform () {
			if (Charge()) {
				TokenGroup targets = actor.Neighbors(false);
				targets = targets.FilterUnit;
				foreach (Token t in targets) {
					Unit u = (Unit)t;
					InputBuffer.Submit(new RDamage(new Source(actor), u, damage));
				}
			}
		}
	}
}