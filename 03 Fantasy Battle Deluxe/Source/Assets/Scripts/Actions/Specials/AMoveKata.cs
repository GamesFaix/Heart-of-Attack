using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HOA.Tokens;
using HOA.Map;

namespace HOA.Actions {

	public class AMoveKata : Action {
		
		public AMoveKata (Price p, Unit u, Aim a) {
			price = p;
			actor = u;
			aim = a;
			
			name = "Move";
			desc = "Move "+actor+" to target cell.  \nRange +2 per FP, upto 6 cells.  \nRemove all FP.";
			
		}
		
		public override void Perform () {
			if (Charge()) {
				int bonus = Mathf.Min(actor.FP()*2, 6);
				actor.SetStat(new Source(actor), STAT.FP, 0, false);
				
				aim = new Aim (aim.AimType(), aim.Target(), aim.CTar(), aim.Range()+bonus);
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithCell(new RMove(new Source(actor), actor, default(Cell)));
			}
		}
	}
}