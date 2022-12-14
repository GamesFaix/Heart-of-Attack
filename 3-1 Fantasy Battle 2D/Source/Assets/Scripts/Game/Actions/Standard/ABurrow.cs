using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class ABurrow : Action {
		
		Cell target;
		
		public ABurrow (Unit u, int r, int mr=0) {
			weight = 1;
			AddAim(new Aim(EAim.ARC, EClass.CELL, EPurpose.MOVE, r, mr));
			actor = u;
			
			name = "Burrow";
			desc = "Move "+actor+" to target cell.";
			
		}
		
		public override void Execute (List<ITargetable> targets) {
			Charge();
			EffectQueue.Add(new EBurrow(new Source(actor), actor, (Cell)targets[0]));
			//AEffects.Move(new Source(actor), actor, (Cell)targets[0]);
			Targeter.Reset();
		}
	}
}
