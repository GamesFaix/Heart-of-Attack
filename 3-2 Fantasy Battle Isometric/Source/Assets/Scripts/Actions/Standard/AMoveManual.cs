using UnityEngine;
using System.Collections.Generic;

namespace HOA {
	
	public class AMoveManual : Action {

		public AMoveManual (Token t) {
			weight = 1;
			AddAim( new Aim (ETraj.FREE, EType.CELL, EPurp.MOVE));
			actor = TurnQueue.Top;
			childTemplate = t;
			
			name = "Manual Move";
			desc = "Move "+actor+" to target cell.";
			
		}

		public override bool Legal() {return true;}
		
		public override void Execute (List<ITarget> targets) {
			EffectQueue.Add(new EMove(new Source(actor), childTemplate, (Cell)targets[0]));
			Targeter.Reset();
		}
	}
}
