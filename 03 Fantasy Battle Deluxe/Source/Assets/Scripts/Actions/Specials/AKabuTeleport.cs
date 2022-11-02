using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AKabuTeleport : Action {
		Aim aim2;
		
		public AKabuTeleport (Price p, Unit u, int range) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, range);
			aim.TeamOnly = true;
			aim2 = new Aim(AIMTYPE.ARC, TARGET.CELL, CTAR.MOVE, range);
			
			name = "Warp Drive";
			desc = "Move target teammate (including self) to target cell.\n"+aim2.ToString();
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RTeleport(new Source(actor), default(Token), aim2));
			}
		}
	}
}
