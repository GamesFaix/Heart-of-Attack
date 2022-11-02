using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AOldtSecond : Action {
		
	public AOldtSecond (Price p, Unit u) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.FREE, TARGET.TOKEN, TTAR.UNIT);
			aim.IncludeSelf = false;

			name = "Second in Command";
			desc = "Target unit takes the next turn.\n(Cannot target self.)";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new ROldtSecond(new Source(actor), default(Token)));

			}
		}
	}
}
