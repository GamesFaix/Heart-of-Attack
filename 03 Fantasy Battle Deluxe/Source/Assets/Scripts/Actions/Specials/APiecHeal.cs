using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class APiecHeal : Action {
		
		int magnitude;
		
		public APiecHeal (Price p, Unit u, int n) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, 2);
			magnitude = n;
			
			name = "Heal";
			desc = "Target unit gains "+magnitude+" health.\n(Can target self.)";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RAddStat(new Source(actor), default(Token), STAT.HP, magnitude));
			}
		}
	}
}
