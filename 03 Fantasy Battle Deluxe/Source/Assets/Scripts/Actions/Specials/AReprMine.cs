using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AReprMine : Action {
		
		public AReprMine (Price p, Unit u) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.DESTREM);
			
			name = "Time Mine";
			desc = "Destroy neighboring destructible.\nIf IN is less than 6, IN +1.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RReprMine(new Source(actor), default(Token)));
			}
		}
	}
}
