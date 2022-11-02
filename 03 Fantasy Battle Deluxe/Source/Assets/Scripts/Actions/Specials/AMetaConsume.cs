using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AMetaConsume : Action {
		
		public AMetaConsume (Price p, Unit u) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.NEIGHBOR, TARGET.TOKEN, TTAR.DEST);

			name = "Consume Terrain";
			desc = "Destroy neighboring non-Remains destructible.\n"+actor+" gains 12 health.";
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RMetaConsume(new Source(actor), default(Token)));
			}
		}
	}
}
