using UnityEngine;
using System.Collections.Generic;

namespace HOA {

	public class AMove : Action, IMove {

		Cell target;
		
		public AMove (Unit u, Aim a) {
			weight = 1;
			AddAim(a);
			actor = u;
			
			name = "Move";
			desc = "Move "+actor+" to target cell.";
			
		}
		
		public override void Execute (List<ITarget> targets) {
			Charge();
			EffectQueue.Add(new EMove(new Source(actor), actor, (Cell)targets[0]));
			Targeter.Reset();
		}
	}
}
