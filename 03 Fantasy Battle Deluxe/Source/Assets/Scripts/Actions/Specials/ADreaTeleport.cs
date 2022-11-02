using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class ADreaTeleport : Action {
		Aim aim2;
		
		public ADreaTeleport (Price p, Unit u, int range) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.ARC, TARGET.TOKEN, TTAR.UNIT, range);
			//aim.EnemyOnly = true;
			aim.NoKings = true;
			aim2 = new Aim(AIMTYPE.ARC, TARGET.CELL, CTAR.MOVE, range);
			
			name = "Teleport Enemy";
			desc = "Move target enemy (exluding Attack Kings) to target cell.\n"+aim2.ToString();
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithInstance(new RTeleport(new Source(actor), default(Token), aim2));
			}
		}
	}
}
