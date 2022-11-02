using System.Collections.Generic;

namespace HOA {
	public class AEnd : Action {
		
		public AEnd (Unit u) {
			weight = 0;
			actor = u;
			price = Price.Free;
			AddAim(HOA.Aim.Self);
			
			name = "End turn";
			desc = "";
		}

		public override void Execute (List<ITargetable> targets) {
			TurnQueue.Advance();
		}
	}
}
