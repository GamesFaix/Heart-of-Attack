using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class AMove : Action {

		Cell target;
		
		public AMove (Unit u, Aim a) {
			weight = 1;
			AddAim(a);
			actor = u;
			
			name = "Move";
			desc = "Move "+actor+" to target cell.";
			
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			InputBuffer.Submit(new RMove(new Source(actor), actor, (Cell)targets[0]));
		}
	}
}
