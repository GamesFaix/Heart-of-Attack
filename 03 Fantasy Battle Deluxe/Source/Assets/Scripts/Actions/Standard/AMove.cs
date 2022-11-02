using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HOA {

	public class AMove : Action {

		Cell target;
		
		public AMove (Unit u, Aim a) {
			weight = 1;
			aim = a;
			actor = u;
			
			name = "Move";
			desc = "Move "+actor+" to target cell.";
			
		}
		
		public override void Perform () {
			if (Charge()) {
				Legalizer.Find(actor, aim);
				GUISelectors.DoWithCell(new RMove(new Source(actor), actor, default(Cell)));
			}
		}
	}
}
