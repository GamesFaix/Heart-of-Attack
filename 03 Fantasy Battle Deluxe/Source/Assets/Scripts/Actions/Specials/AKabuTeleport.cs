using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AKabuTeleport : Action {

		public AKabuTeleport (Price p, Unit u, Aim a) {
			actor = u;
			price = p;
			aim = a;

			name = "Warp Drive";
			desc = "Move teammate within range to another cell within range.";
		}

		public override void Perform () {
			/*
			if (Charge()) {
				Legalizer.Find(actor, aim);
				Legalizer.FilterTeammate();
				GUISelectors.DoWithInstance(new RMoveTo(new Source(actor), default(Token)));
			}
			*/
		}
	}
}
