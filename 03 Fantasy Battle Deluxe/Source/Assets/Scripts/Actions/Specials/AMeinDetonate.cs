using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {
	
	public class AMeinDetonate : Action {
		
		public AMeinDetonate (Price p, Unit u) {
			weight = 4;
			actor = u;
			price = p;
			aim = new Aim(AIMTYPE.GLOBAL, TARGET.TOKEN, TTAR.NA);

			name = "Detonate";
			desc = "Destroy all mines on team.";
		}
		
		public override void Perform () {
			if (Charge()) {
				TokenGroup mines = actor.Owner.OwnedUnits;
				for (int i=mines.Count-1; i>=0; i--) {
					Token t = mines[i];
					if (t.Code != TTYPE.MINE) {mines.Remove(t);}
				}

				foreach (Token t in mines) {t.Die(new Source(actor));}
			}
		}
	}
}
