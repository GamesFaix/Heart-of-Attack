using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AGrizHeal : Action {
		
		int magnitude;
		
		public AGrizHeal (Price p, Unit u, int n) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.UNIT);
			aim.TeamOnly = true;
			aim.IncludeSelf = false;
			magnitude = n;

			name = "Heal";
			desc = "Target teammate: +"+magnitude+" HP.\n(Cannot target self.)";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RAddStat(new Source(actor), default(Token), STAT.HP, magnitude));
			}
		}
	}
}
